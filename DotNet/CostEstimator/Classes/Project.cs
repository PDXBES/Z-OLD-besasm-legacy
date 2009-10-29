// Project: Classes, File: Project.cs
// Namespace: CostEstimator.Classes, Class: Project
// Path: C:\Development\CostEstimatorV2\Classes, Author: Arnel
// Code lines: 14, Size of file: 185 Bytes
// Creation date: 3/1/2008 3:02 PM
// Last modified: 5/11/2009 1:44 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.ComponentModel;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Modeling.Alternatives;
#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
	/// <summary>
	/// Project
	/// </summary>
	public class Project
	{
		#region Constants
		private const string DESC_PIPE_DIRECT_CONSTRUCTION = "Pipe direct construction";
		private const string DESC_INFLOW_CONTROL_DIRECT_CONSTRUCTION = "Inflow control direct construction";
		private const string DESC_OTHER_DIRECT_CONSTRUCTION = "Other direct construction";
		private const string DESC_CONTINGENCY = "Contingency";
		private const double PROJECT_FILE_FORMAT_VERSION = 1.1;
		#endregion

		#region Variables
		private string _FileName= "";
		private CostItemFactor _ProjectEstimate;
		internal SortedList<int, CostItemFactor> _CostItemFactors = new SortedList<int, CostItemFactor>();
		private SortedList<int, CostItemFactor> _Estimates = new SortedList<int, CostItemFactor>();
		private SortedList<int, CostFactor> _StandardCostFactorPool = new SortedList<int, CostFactor>();
		private SortedList<int, CostFactor> _CostFactorPool = new SortedList<int, CostFactor>();
		private SortedList<int, CostItem> _CostItemPool = new SortedList<int, CostItem>();

		private PipeCoster _PipeCoster = new PipeCoster();
		private InflowControlCoster _ICCoster = new InflowControlCoster();

		private ProjectInfo _ProjectInfo = new ProjectInfo();

		private bool _IsDirty = false;
		#endregion

		#region Constructors
		/// <summary>
		/// Create project
		/// </summary>
		public Project()
		{
			ResetProject();
			CreateStandardFactors();
			CreateFactors();

			_ProjectInfo.CostEstimator = Environment.UserName;
		} // Project()
		#endregion

		#region Properties
		/// <summary>
		/// File name
		/// </summary>
		/// <returns>String</returns>
		public string FileName
		{
			get
			{
				return _FileName;
			} // get

			set
      {
      	_FileName = value;
      } // set
		} // FileName

		/// <summary>
		/// Root
		/// </summary>
		/// <returns>Cost item factor</returns>
		public CostItemFactor Root
		{
			get
			{
				return _ProjectEstimate;
			} // get
		} // Root

		/// <summary>
		/// Estimate count
		/// </summary>
		/// <returns>Int</returns>
		public int EstimateCount
		{
			get
			{
				return _Estimates.Count;
			} // get
		} // EstimateCount

		/// <summary>
		/// Cost factor pool count
		/// </summary>
		/// <returns>Int</returns>
		public int CostFactorPoolCount
		{
			get
			{
				return _CostFactorPool.Count;
			} // get
		} // CostFactorPoolCount

		/// <summary>
		/// Cost item pool count
		/// </summary>
		/// <returns>Int</returns>
		public int CostItemPoolCount
		{
			get
			{
				return _CostItemPool.Count;
			} // get
		} // CostItemPoolCount

		/// <summary>
		/// Cost item factor pool count
		/// </summary>
		/// <returns>Int</returns>
		public int CostItemFactorPoolCount
		{
			get
			{
				return _CostItemFactors.Count;
			} // get
		} // CostItemFactorPoolCount

		/// <summary>
		/// Pipe coster
		/// </summary>
		/// <returns>Pipe coster</returns>
		public PipeCoster PipeCoster
		{
			get
			{
				return _PipeCoster;
			} // get
		} // PipeCoster

		/// <summary>
		/// Inflow control coster
		/// </summary>
		/// <returns>Inflow control coster</returns>
		public InflowControlCoster InflowControlCoster
		{
			get
			{
				return _ICCoster;
			} // get
		} // InflowControlCoster

		/// <summary>
		/// ENR
		/// </summary>
		/// <returns>Int</returns>
		public int ENR
		{
			get
			{
				CostFactor enrFactor = FactorFromPool("ENR");
				if (enrFactor != null)
					return Convert.ToInt32(enrFactor.FactorValue * (double)_PipeCoster.BaseENR.Value);
				else
					return 0;
			} // get
		} // ENR

		/// <summary>
		/// Project info
		/// </summary>
		/// <returns>Project info</returns>
		public ProjectInfo ProjectInfo
		{
			get
			{
				return _ProjectInfo;
			} // get
		} // ProjectInfo

		/// <summary>
		/// Estimates
		/// </summary>
		/// <returns>List</returns>
		public List<CostItemFactor> Estimates
		{
			get
			{
				List<CostItemFactor> estimates = new List<CostItemFactor>();
				foreach (KeyValuePair<int, CostItemFactor> kvpair in _Estimates)
					estimates.Add(kvpair.Value);

				return estimates;
			} // get
		} // Estimates

		/// <summary>
		/// Is dirty
		/// </summary>
		/// <returns>Bool</returns>
		public bool IsDirty
		{
			get
			{
				return _IsDirty || _ProjectInfo.IsDirty;
			} // get
		} // IsDirty
		#endregion

		#region Methods
		/// <summary>
		/// Reset project
		/// </summary>
		private void ResetProject()
		{
			ResetIDGenerators();
			_ProjectEstimate = new CostItemFactor("Project");
			_StandardCostFactorPool.Clear();
			_CostFactorPool.Clear();
			_CostItemPool.Clear();
			_CostItemFactors.Clear();
		} // ResetProject()

/// <summary>
		/// Reset ID generators
		/// </summary>
		private void ResetIDGenerators()
		{
			CostItemFactor.ResetIDGenerator();
			CostItem.ResetIDGenerator();
			CostFactor.ResetIDGenerator();
		} // ResetIDGenerators()

		/// <summary>
		/// Save to file
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <returns>Bool</returns>
		public bool SaveToFile(string fileName)
		{
			XmlWriterSettings saveFileSettings = new XmlWriterSettings();
			saveFileSettings.Indent = true;
			saveFileSettings.IndentChars = "  ";
			using (XmlWriter saveFile = XmlWriter.Create(fileName, saveFileSettings))
      {
				try
				{
					saveFile.WriteStartDocument();
					saveFile.WriteStartElement("Project");

					saveFile.WriteStartAttribute("version");
					saveFile.WriteValue(PROJECT_FILE_FORMAT_VERSION);
					saveFile.WriteEndAttribute();

					saveFile.WriteStartElement("Info");
					_ProjectInfo.WriteXML(saveFile);
					saveFile.WriteEndElement();

					saveFile.WriteStartElement("CostItemFactorPool");
					foreach (KeyValuePair<int, CostItemFactor> kvpair in _CostItemFactors)
					{
						kvpair.Value.WriteXML(saveFile);
					} // foreach  (kvpair)
					saveFile.WriteEndElement();

					saveFile.WriteStartElement("StandardCostFactorPool");
					foreach (KeyValuePair<int, CostFactor> kvpair in _StandardCostFactorPool)
					{
						kvpair.Value.WriteXML(saveFile);
					} // foreach  (kvpair)
					saveFile.WriteEndElement();

					saveFile.WriteStartElement("CostFactorPool");
					foreach (KeyValuePair<int, CostFactor> kvpair in _CostFactorPool)
					{
						kvpair.Value.WriteXML(saveFile);
					} // foreach  (kvpair)
					saveFile.WriteEndElement();

					saveFile.WriteStartElement("CostItemPool");
					foreach (KeyValuePair<int, CostItem> kvpair in _CostItemPool)
					{
						kvpair.Value.WriteXML(saveFile);
					} // foreach  (kvpair)
					saveFile.WriteEndElement();

					saveFile.WriteStartElement("Estimates");
					foreach (KeyValuePair<int, CostItemFactor> kvpair in _Estimates)
					{
						saveFile.WriteStartElement("id");
						saveFile.WriteValue(kvpair.Key);
						saveFile.WriteEndElement();
					} // foreach  (kvpair)
					saveFile.WriteEndElement();

					saveFile.WriteEndElement();
					saveFile.WriteEndDocument();
				} // try
				catch (Exception e)
				{
					return false;
				} // catch
      } // using (saveFile)

			_IsDirty = false;
			_ProjectInfo.Clean();

			return true;
		} // SaveToFile(fileName)

		/// <summary>
		/// Load from file
		/// </summary>
		/// <param name="fileName">File name</param>
		/// <returns>Bool</returns>
		public bool LoadFromFile(string fileName)
		{
			ResetProject();

			XmlReaderSettings readerFileSettings = new XmlReaderSettings();
			readerFileSettings.IgnoreWhitespace = true;
			readerFileSettings.ValidationType = ValidationType.Schema;
			readerFileSettings.ConformanceLevel = ConformanceLevel.Document;

			using (XmlReader loadFile = XmlReader.Create(fileName, readerFileSettings))
      {
				XPathDocument projectDoc = new XPathDocument(loadFile, XmlSpace.None);
				XPathNavigator projectNav = projectDoc.CreateNavigator();

				// Read in Project Info
				string xpathQuery = "/Project/Info/*";
				XPathNodeIterator xpathIter = projectNav.Select(xpathQuery);
				while (xpathIter.MoveNext())
				{
					switch (xpathIter.Current.Name)
					{
						case "ProjectTitle":
							_ProjectInfo.ProjectTitle = xpathIter.Current.Value;
							break;
						case "ProjectNumber":
							_ProjectInfo.ProjectNumber = xpathIter.Current.Value;
							break;
						case "ProjectDescription":
							_ProjectInfo.ProjectDescription = xpathIter.Current.Value;
							break;
						case "ProjectManager":
							_ProjectInfo.ProjectManager = xpathIter.Current.Value;
							break;
						case "CostEstimator":
							_ProjectInfo.CostEstimator = xpathIter.Current.Value;
							break;
						case "PreparedDate":
							_ProjectInfo.PreparedDate = xpathIter.Current.ValueAsDateTime;
							break;
						case "Source":
							_ProjectInfo.Source = xpathIter.Current.Value;
							break;
						case "ENR":
							{
								int month = Convert.ToInt32(xpathIter.Current.GetAttribute("month", string.Empty));
								int year = Convert.ToInt32(xpathIter.Current.GetAttribute("year", string.Empty));
								_ProjectInfo.ENR = new ENR(xpathIter.Current.ValueAsInt, month, year);
							} // block
							break;
						case "BaseENR":
							{
								int month = Convert.ToInt32(xpathIter.Current.GetAttribute("month", string.Empty));
								int year = Convert.ToInt32(xpathIter.Current.GetAttribute("year", string.Empty));
								_ProjectInfo.BaseENR = new ENR(xpathIter.Current.ValueAsInt, month, year);
							} // block
							break;
					} // switch
				} // while

				// Read in CostFactors
				xpathQuery = "/Project/CostFactorPool/*";
				xpathIter = projectNav.Select(xpathQuery);
				int maxCostFactorID = -1;
				while (xpathIter.MoveNext())
				{
					int id = 0;
					string name = null;
					double factorvalue = 1.0;
					CostFactorType factortype = CostFactorType.Simple;
					string comment = null;

					XPathNodeIterator childIter = xpathIter.Current.SelectChildren(XPathNodeType.Element);
					while (childIter.MoveNext())
					{
						switch (childIter.Current.Name)
						{
							case "id":
								id = childIter.Current.ValueAsInt;
								break;
							case "name":
								name = childIter.Current.Value;
								break;
							case "factorvalue":
								factorvalue = childIter.Current.ValueAsDouble;
								break;
							case "factortype":
								factortype = (CostFactorType)Enum.Parse(typeof(CostFactorType),
									childIter.Current.Value, true);
								break;
							case "comment":
								comment = childIter.Current.Value;
								break;
						} // switch
					} // while

					maxCostFactorID = Math.Max(maxCostFactorID, id);
					CostFactor newCostFactor = new CostFactor(id, name, factorvalue, factortype, comment);
					AddFactorToPool(newCostFactor);
				};
				CostFactor.SetCurrentID(maxCostFactorID + 1);

				// Read in Standard CostFactors
				xpathQuery = "/Project/StandardCostFactorPool/*";
				xpathIter = projectNav.Select(xpathQuery);
				while (xpathIter.MoveNext())
				{
					int id = 0;

					XPathNodeIterator childIter = xpathIter.Current.SelectChildren(XPathNodeType.Element);
					while (childIter.MoveNext())
					{
						switch (childIter.Current.Name)
						{
							case "id":
								id = childIter.Current.ValueAsInt;
								break;
						} // switch
					} // while

					CostFactor stdCostFactor = FactorFromPool(id);
					_StandardCostFactorPool.Add(stdCostFactor.ID, stdCostFactor);
				} // while
			
				// Read in CostItems
				xpathQuery = "/Project/CostItemPool/*";
				xpathIter = projectNav.Select(xpathQuery);
				int maxCostItemID = -1;
				while (xpathIter.MoveNext())
				{
					int id = 0;
					string name = null;
					double quantity = 0;
					decimal unitcost = 0m;
					string unitname = "ea";
					string comment = null;

					XPathNodeIterator childIter = xpathIter.Current.SelectChildren(XPathNodeType.Element);
					while (childIter.MoveNext())
					{
						switch (childIter.Current.Name)
						{
							case "id":
								id = childIter.Current.ValueAsInt;
								break;
							case "name":
								name = childIter.Current.Value;
								break;
							case "quantity":
								quantity = childIter.Current.ValueAsDouble;
								break;
							case "unitcost":
								unitcost = (decimal)childIter.Current.ValueAsDouble;
								break;
							case "unitname":
								unitname = childIter.Current.Value;
								break;
							case "comment":
								comment = childIter.Current.Value;
								break;
						} // switch
					} // while

					maxCostItemID = Math.Max(maxCostItemID, id);
					CostItem newCostItem = new CostItem(id, name, quantity, unitcost, unitname, comment);
					AddCostItemToPool(newCostItem);
				} // while
				CostItem.SetCurrentID(maxCostItemID + 1);

				// We need to iterate over the CostItemFactorPool because CostItemFactors
				// can be parents of other CostItemFactors.  Therefore, the pool needs
				// to be generated, and then the child CostItemFactors can be assigned.
				xpathQuery = "/Project/CostItemFactorPool/*";
				xpathIter = projectNav.Select(xpathQuery);
				while (xpathIter.MoveNext())
				{
					string reportItemTypeString = xpathIter.Current.GetAttribute("reportItemType", string.Empty);
					ReportItemType reportItemType = (ReportItemType)Enum.Parse(typeof(ReportItemType), reportItemTypeString, true);
					int	id = Convert.ToInt32(xpathIter.Current.GetAttribute("id", string.Empty));
					string name = null;
					string comment = null;
					double quantity = 0;
					decimal mincost = 0;
					int costitemid = -1;
					List<int> factorIDs = new List<int>();

					XPathNodeIterator childIter = xpathIter.Current.SelectChildren(XPathNodeType.Element);
					ReportItem genericReportItem = null;
					while (childIter.MoveNext())
					{

						switch (childIter.Current.Name)
						{
							//case "id": // deprecated
							//  id = childIter.Current.ValueAsInt;
							//  break;
							case "name":
								name = childIter.Current.Value;
								break;
							case "comment":
								comment = childIter.Current.Value;
								break;
							case "quantity":
								quantity = childIter.Current.ValueAsDouble;
								break;
							case "mincost":
								mincost = (decimal)childIter.Current.ValueAsDouble;
								break;
							case "CostItemId":
								if (childIter.Current.Value != "")
									costitemid = childIter.Current.ValueAsInt;
								break;
							case "CostFactors":
								{
									XPathNodeIterator factorIter = childIter.Current.SelectChildren(XPathNodeType.Element);
									while (factorIter.MoveNext())
									{
										factorIDs.Add(factorIter.Current.ValueAsInt);
									} // while
								} // block
								break;
							case "data":
								{
									switch (reportItemType)
									{
										case ReportItemType.Pipe:
											{
												XPathNodeIterator dataIter = childIter.Current.SelectChildren(XPathNodeType.Element);
												ReportPipeItem reportItem = new ReportPipeItem();
												while (dataIter.MoveNext())
                        {
                        	switch (dataIter.Current.Name)
                          {
                          	case "id":
                          		reportItem.ID = dataIter.Current.ValueAsInt;
                          		break;
														case "name":
															reportItem.Name = dataIter.Current.Value;
															break;
														case "materialType":
															reportItem.MaterialType = dataIter.Current.Value;
															break;
														case "diameterIn":
															reportItem.DiameterIn = dataIter.Current.ValueAsDouble;
															break;
														case "depthFt":
															reportItem.DepthFt = dataIter.Current.ValueAsDouble;
															break;
														case "length":
															reportItem.Length = dataIter.Current.ValueAsDouble;
															break;
														case "excavationVolCuYd":
															reportItem.ExcavationVolCuYd = dataIter.Current.ValueAsDouble;
															break;
														case "comment":
															reportItem.Comment = dataIter.Current.Value;
															break;
														//case "manholeID":
														//  reportItem.Manhole = dataIter.Current.ValueAsInt;
														//  break;
														//case "pipeID":
														//  reportItem.Pipe = dataIter.Current.ValueAsInt;
														//  break;
														//case "manholeID":
														//  reportItem.PipeAndManhole = dataIter.Current.ValueAsInt;
														//  break;
                          } // switch
                        } // while
												genericReportItem = reportItem;
											} // block
											break;
										case ReportItemType.InflowControl:
											{
												XPathNodeIterator dataIter = childIter.Current.SelectChildren(XPathNodeType.Element);
												ReportInflowControlItem reportItem = new ReportInflowControlItem();
												while (dataIter.MoveNext())
                        {
                        	switch (dataIter.Current.Name)
                          {
                          	case "id":
                          		reportItem.ID = dataIter.Current.ValueAsInt;
                          		break;
														case "name":
															reportItem.Name = dataIter.Current.Value;
															break;
														case "controlType":
															reportItem.ControlType = dataIter.Current.Value;
															break;
														case "controlSubtype":
															reportItem.ControlSubtype = dataIter.Current.Value;
															break;
														case "cost":
															reportItem.Cost = (decimal)dataIter.Current.ValueAsDouble;
															break;
														case "comment":
															reportItem.Comment = dataIter.Current.Value;
															break;
                          } // switch
                        } // while
												genericReportItem = reportItem;
											} // block
											break;
										case ReportItemType.Generic:
											{
												XPathNodeIterator dataIter = childIter.Current.SelectChildren(XPathNodeType.Element);
												ReportGenericItem reportItem = new ReportGenericItem();
												while (dataIter.MoveNext())
                        {
                        	switch (dataIter.Current.Name)
                          {
														case "name":
															reportItem.Name = dataIter.Current.Value;
															break;
														case "quantity":
															reportItem.Quantity = dataIter.Current.ValueAsDouble;
															break;
														case "unitName":
															reportItem.UnitName = dataIter.Current.Value;
															break;
														case "unitPrice":
															reportItem.UnitPrice = (decimal)dataIter.Current.ValueAsDouble;
															break;
														case "group":
															reportItem.Group = dataIter.Current.Value;
															break;
														case "comment":
															reportItem.Comment = dataIter.Current.Value;
															break;
														//case "item":
														//  reportItem.Item = dataIter.Current.ValueAsInt;
														//  break;
                          } // switch
                        } // while
												genericReportItem = reportItem;
											} // block
											break;
										case ReportItemType.Summary:
											{
												XPathNodeIterator dataIter = childIter.Current.SelectChildren(XPathNodeType.Element);
												ReportSummaryItem reportItem = new ReportSummaryItem();
												while (dataIter.MoveNext())
                        {
                        	switch (dataIter.Current.Name)
                          {
														case "name":
															reportItem.Name = dataIter.Current.Value;
															break;
														case "cost":
															reportItem.Cost = (decimal)dataIter.Current.ValueAsDouble;
															break;
														case "group":
															reportItem.Group = dataIter.Current.Value;
															break;
														case "summaryGroup":
															reportItem.SummaryGroup = dataIter.Current.Value;
															break;
                          } // switch
                        } // while
												genericReportItem = reportItem;
											} // block
											break;
									} // switch
								} // block
								break;
						} // switch
					} // while

					maxCostItemID = Math.Max(maxCostItemID, id);
					CostItemFactor costItemFactor = new CostItemFactor(id, name, comment, quantity, mincost);

					switch (reportItemType)
					{
						case ReportItemType.Generic:
							costItemFactor.Data = genericReportItem as ReportGenericItem;
							break;
						case ReportItemType.InflowControl:
							costItemFactor.Data = genericReportItem as ReportInflowControlItem;
							break;
						case ReportItemType.Pipe:
							costItemFactor.Data = genericReportItem as ReportPipeItem;
							break;
						case ReportItemType.Summary:
							costItemFactor.Data = genericReportItem as ReportSummaryItem;
							break;
					} // switch
					costItemFactor.ReportItemType = reportItemType;
					if (costitemid > -1)
						costItemFactor.CostItem = CostItemFromPool(costitemid);
					foreach (int factorID in factorIDs)
						costItemFactor.AddFactor(FactorFromPool(factorID));
					AddCostItemFactorToPool(costItemFactor);
				};
				CostItemFactor.SetCurrentID(maxCostItemID + 1);

				// Pass over the CostItemFactors again to recalculate costs and costitemfactors

				xpathQuery = "/Project/CostItemFactorPool/*";
				xpathIter = projectNav.Select(xpathQuery);
				while (xpathIter.MoveNext())
				{
					string reportItemTypeString = xpathIter.Current.GetAttribute("reportItemType", string.Empty);
					ReportItemType reportItemType = (ReportItemType)Enum.Parse(typeof(ReportItemType), reportItemTypeString, true);

					int id = Convert.ToInt32(xpathIter.Current.GetAttribute("id", string.Empty));
					CostItemFactor costItemFactor = CostItemFactorFromPool(id);

					List<int> costItemFactorIDs = new List<int>();
					XPathNodeIterator childIter = xpathIter.Current.SelectChildren(XPathNodeType.Element);
					while (childIter.MoveNext())
					{
						switch (childIter.Current.Name)
						{
							case "ChildCostItemFactors":
								{
									XPathNodeIterator costItemFactorIter = childIter.Current.SelectChildren(XPathNodeType.Element);
									while (costItemFactorIter.MoveNext())
									{
										costItemFactorIDs.Add(costItemFactorIter.Current.ValueAsInt);
									} // while
								} // block
								break;
							case "data":
								{
									switch (reportItemType)
									{
										case ReportItemType.Pipe:
											{
												XPathNodeIterator dataIter = childIter.Current.SelectChildren(XPathNodeType.Element);
												ReportPipeItem reportItem = costItemFactor.Data as ReportPipeItem;
												while (dataIter.MoveNext())
												{
													switch (dataIter.Current.Name)
													{
														case "manholeID":
															reportItem.Manhole = CostItemFactorFromPool(dataIter.Current.ValueAsInt);
															break;
														case "pipeID":
															reportItem.Pipe = CostItemFactorFromPool(dataIter.Current.ValueAsInt);
															break;
														case "pipeAndManholeID":
															reportItem.PipeAndManhole = CostItemFactorFromPool(dataIter.Current.ValueAsInt);
															break;
													} // switch
												} // while
											} // block
											break;
										case ReportItemType.Generic:
											{
												XPathNodeIterator dataIter = childIter.Current.SelectChildren(XPathNodeType.Element);
												ReportGenericItem reportItem = costItemFactor.Data as ReportGenericItem;
												while (dataIter.MoveNext())
												{
													switch (dataIter.Current.Name)
													{
														case "item":
															reportItem.Item = CostItemFactorFromPool(dataIter.Current.ValueAsInt);
															break;
													} // switch
												} // while
											} // block
											break;
									} // switch
								} // block
								break;
						} // switch

					} // while

					foreach (int costItemFactorID in costItemFactorIDs)
					{
						costItemFactor.AddCostItemFactor(CostItemFactorFromPool(costItemFactorID));
						CostItemFactorFromPool(costItemFactorID).AddAsParent(costItemFactor);
					} // foreach  (costItemFactorID)
				};

				foreach (KeyValuePair<int, CostItemFactor> kvPair in _CostItemFactors)
				{
					CostItemFactor item = kvPair.Value;
					if (item.Data is ReportSummaryItem)
						(item.Data as ReportSummaryItem).Cost = item.Cost;
					else if (item.Data is ReportInflowControlItem)
						(item.Data as ReportInflowControlItem).Cost = item.Cost;
				} // foreach  (kvPair)

				xpathQuery = "/Project/Estimates/*";
				xpathIter = projectNav.Select(xpathQuery);
				while (xpathIter.MoveNext())
				{
					CostItemFactor estimate = CostItemFactorFromPool(xpathIter.Current.ValueAsInt);
					_Estimates.Add(estimate.ID, estimate);
					//Root.AddCostItemFactor(estimate, true);
					Root.AddCostItemFactor(estimate);
				} // while
			} // using (loadFile)

			_IsDirty = false;

			return true;
		} // LoadFromFile(fileName)

		/// <summary>
		/// Create standard factors
		/// </summary>
		private void CreateStandardFactors()
		{
			CostFactor newCostFactor = new CostFactor("General conditions", 0.10, CostFactorType.Additive);
			_StandardCostFactorPool.Add(newCostFactor.ID, newCostFactor);
			newCostFactor = new CostFactor("Waste allowance", 0.05, CostFactorType.Additive);
			_StandardCostFactorPool.Add(newCostFactor.ID, newCostFactor);
			newCostFactor = new CostFactor("ENR", (double)_PipeCoster.CurrentENR.Value / (double)_PipeCoster.BaseENR.Value, CostFactorType.Simple);
			_StandardCostFactorPool.Add(newCostFactor.ID, newCostFactor);
		} // CreateStandardFactors()

		/// <summary>
		/// Create factors
		/// </summary>
		private void CreateFactors()
		{
			foreach (KeyValuePair<int, CostFactor> kvpair in _StandardCostFactorPool)
				_CostFactorPool.Add(kvpair.Key, kvpair.Value);

			CostFactor newCostFactor = new CostFactor(DESC_CONTINGENCY, 0.25, CostFactorType.Additive);
			_CostFactorPool.Add(newCostFactor.ID, newCostFactor);
			newCostFactor = new CostFactor("Const mgt, Insp, Test", 0.15, CostFactorType.IndirectAdditive);
			_CostFactorPool.Add(newCostFactor.ID, newCostFactor);
			newCostFactor = new CostFactor("Design", 0.20, CostFactorType.IndirectAdditive);
			_CostFactorPool.Add(newCostFactor.ID, newCostFactor);
			newCostFactor = new CostFactor("PI, I&C, Easements, Environmental", 0.03, CostFactorType.IndirectAdditive);
			_CostFactorPool.Add(newCostFactor.ID, newCostFactor);
			newCostFactor = new CostFactor("Startup/closeout", 0.01, CostFactorType.IndirectAdditive);
			_CostFactorPool.Add(newCostFactor.ID, newCostFactor);
			newCostFactor = new CostFactor("2008 Escalation", 0.25, CostFactorType.Additive);
			_CostFactorPool.Add(newCostFactor.ID, newCostFactor);
		} // CreateFactors()

		/// <summary>
		/// Create estimate from alternative
		/// </summary>
		/// <param name="modelPath">Model path</param>
		/// <param name="alternativeID">Alternative ID</param>
		/// <returns>Bool</returns>
		public bool CreateEstimateFromAlternative(BackgroundWorker bw, string modelPath, string alternativeID,
			out string errorMessage)
		{
			string alternativePath = modelPath + Path.DirectorySeparatorChar + "alternatives" +
				Path.DirectorySeparatorChar + alternativeID;
			try
			{
				if (Directory.Exists(alternativePath))
				{
					AlternativePackage altPackage = new AlternativePackage(alternativePath);
					AltLinks altLinks = altPackage.ModelAltLinks;
					Dictionary<int, RoofTarget> altRoofTargets = altPackage.ModelAltRoofTargets;
					Dictionary<int, ParkingTarget> altParkingTargets = altPackage.ModelAltParkingTargets;
					Dictionary<int, StreetTarget> altStreetTargets = altPackage.ModelAltStreetTargets;

					List<string> focusAreas = new List<string>();

					focusAreas.Clear();
					string error = string.Empty;
					if (!CreatePipeEstimatesFromAlternative(bw, altPackage, focusAreas, out error))
					{
						errorMessage = error;
						return false;
					} // if

					if (!CreateInflowControlEstimatesFromAlternative(bw, altPackage, focusAreas, out error))
					{
						errorMessage = error;
						return false;
					} // if

					foreach (KeyValuePair<int, CostItemFactor> kvpair in _Estimates)
					{
						int index = _ProjectEstimate.AddCostItemFactor(kvpair.Value);
						if (index == -1)
							throw new Exception("Cannot add CostItemFactor");
						_ProjectEstimate.ChildCostItemFactor(index).AddFactor(FactorFromPool(DESC_CONTINGENCY));
						_ProjectEstimate.ChildCostItemFactor(index).AddFactor(FactorFromPool("Const mgt, Insp, Test"));
						_ProjectEstimate.ChildCostItemFactor(index).AddFactor(FactorFromPool("Design"));
						_ProjectEstimate.ChildCostItemFactor(index).AddFactor(FactorFromPool("PI, I&C, Easements, Environmental"));
						_ProjectEstimate.ChildCostItemFactor(index).AddFactor(FactorFromPool("Startup/closeout"));
					} // foreach  (kvpair)

					_ProjectInfo.Source = alternativePath;
					_ProjectInfo.BaseENR = _PipeCoster.BaseENR;
					_ProjectInfo.ENR = _PipeCoster.CurrentENR;
				} // if
			} // try
			catch (Exception e)
			{
				_IsDirty = true;
				errorMessage = e.Message;
				return false;
			} // catch (Exception)

			_IsDirty = true;
			errorMessage = string.Empty;
			return true;
		} // CreateEstimateFromAlternative(bw, modelPath, alternativeID)

		private void CheckFocusArea(List<string> focusAreas, string focusArea)
		{
			// Create focus area if not already present
			if (!focusAreas.Contains(focusArea))
			{
				focusAreas.Add(focusArea);
				CostItemFactor focusAreaCIF = new CostItemFactor(focusArea);
				_CostItemFactors.Add(focusAreaCIF.ID, focusAreaCIF);
				_Estimates.Add(focusAreaCIF.ID, focusAreaCIF);

				CostItemFactor directConstructionCIF = new CostItemFactor(DESC_PIPE_DIRECT_CONSTRUCTION);
				_CostItemFactors.Add(directConstructionCIF.ID, directConstructionCIF);
				directConstructionCIF.AddFactor(FactorFromPool("2008 Escalation"));
				focusAreaCIF.AddCostItemFactor(directConstructionCIF);

				CostItemFactor inflowControlCIF = new CostItemFactor(DESC_INFLOW_CONTROL_DIRECT_CONSTRUCTION);
				_CostItemFactors.Add(inflowControlCIF.ID, inflowControlCIF);
				focusAreaCIF.AddCostItemFactor(inflowControlCIF);

				CostItemFactor roofControlCIF = new CostItemFactor("Roof");
				_CostItemFactors.Add(roofControlCIF.ID, roofControlCIF);
				inflowControlCIF.AddCostItemFactor(roofControlCIF);

				CostItemFactor parkingControlCIF = new CostItemFactor("Parking");
				_CostItemFactors.Add(parkingControlCIF.ID, parkingControlCIF);
				inflowControlCIF.AddCostItemFactor(parkingControlCIF);

				CostItemFactor streetControlCIF = new CostItemFactor("Street");
				_CostItemFactors.Add(streetControlCIF.ID, streetControlCIF);
				inflowControlCIF.AddCostItemFactor(streetControlCIF);

				CostItemFactor otherDirectConstructionCIF = new CostItemFactor(DESC_OTHER_DIRECT_CONSTRUCTION);
				_CostItemFactors.Add(otherDirectConstructionCIF.ID, otherDirectConstructionCIF);
				focusAreaCIF.AddCostItemFactor(otherDirectConstructionCIF);

				_IsDirty = true;
			} // if
		} // CheckFocusArea(focusAreas, focusArea)

		/// <summary>
		/// Create pipe estimates from alternative
		/// </summary>
		/// <param name="altLinks">Alt links</param>
		private bool CreatePipeEstimatesFromAlternative(BackgroundWorker bw, AlternativePackage altPackage, List<string> focusAreas,
			out string errorMessage)
		{
			try
			{
				int totalLinks = altPackage.ModelAltLinks.Count;
				int linkCounter = 0;
				foreach (AltLink altLink in altPackage.ModelAltLinks.Values)
				{
					linkCounter++;
					bw.ReportProgress((int)((double)linkCounter / (double)totalLinks * 100), "Reading links: " + linkCounter);
					if (altLink.Operation == SystemsAnalysis.Types.Enumerators.AlternativeOperation.DEL ||
						altLink.Operation == SystemsAnalysis.Types.Enumerators.AlternativeOperation.SPL ||
						(altLink.Operation == SystemsAnalysis.Types.Enumerators.AlternativeOperation.RIK && altLink.FocusArea == ""))
						continue;

					CheckFocusArea(focusAreas, altLink.FocusArea);
					CostItemFactor currentCostItem = Estimate(altLink.FocusArea).ChildCostItemFactor(DESC_PIPE_DIRECT_CONSTRUCTION);

					_PipeCoster.Depth = altPackage.PipeDepth(altLink);
					_PipeCoster.InsideDiameter = altLink.Diameter;
					switch (altLink.Material.ToUpper())
					{
						case "PVC":
						case "HDPE":
							_PipeCoster.Material = PipeMaterial.PVCHDPE;
							break;
						case "PIPEBURST":
							_PipeCoster.Material = PipeMaterial.Pipeburst;
							break;
						case "CIPP":
							_PipeCoster.Material = PipeMaterial.CIPP;
							break;
						default:
							_PipeCoster.Material = PipeMaterial.Concrete;
							break;
					} // switch

					// Pipe and Manhole CostItemFactor
					CostItemFactor pipeAndManholeCostItemFactor = new CostItemFactor(string.Format("{0} {1}-{2}",
						altLink.AltLinkID, altLink.USNodeName, altLink.DSNodeName));
					_CostItemFactors.Add(pipeAndManholeCostItemFactor.ID, pipeAndManholeCostItemFactor);
					int pipeAndManholeIndex = currentCostItem.AddCostItemFactor(pipeAndManholeCostItemFactor);
					if (pipeAndManholeIndex == -1)
						throw new Exception("Cannot add CostItemFactor");
					CostItemFactor pipeAndManholeItem = currentCostItem.ChildCostItemFactor(pipeAndManholeIndex);

					// Manhole CostItem
					string manholeCostItemName = string.Format("Manhole {0:F0} in diam {1:F0} ft deep",
						_PipeCoster.ManholeDiameter, _PipeCoster.Depth);
					bool outsideManholeTable;
					CostItem manholeCostItem = CostItemFromPool(manholeCostItemName);
					if (manholeCostItem == null)
					{
						manholeCostItem = new CostItem(manholeCostItemName,
							1, _PipeCoster.ManholeCost(_PipeCoster.InsideDiameter,
								_PipeCoster.Depth, out outsideManholeTable), "ea");
						AddCostItemToPool(manholeCostItem);
					} // if

					// Pipe CostItemFactor
					string pipeItemName = string.Format("Pipe {0:F1} in diam, {1:F0} ft deep, {2}",
						altLink.Diameter,
						altPackage.PipeDepth(altLink),
						altLink.Material);
					CostItemFactor pipeCostItemFactor = new CostItemFactor(pipeItemName, null, null, altLink.Length);
					_CostItemFactors.Add(pipeCostItemFactor.ID, pipeCostItemFactor);
					_PipeCoster.CreateDirectConstructionCostItems(this, pipeCostItemFactor);
					int pipeIndex = pipeAndManholeItem.AddCostItemFactor(pipeCostItemFactor);
					if (pipeIndex == -1)
						throw new Exception("Cannot add CostItemFactor");

					// Manhole CostItemFactor
					string manholeItemName = string.Format("Manhole {0:F0} in diam, {1:F0} ft deep",
						_PipeCoster.ManholeDiameter, _PipeCoster.Depth);
					CostItemFactor manholeCostItemFactor = new CostItemFactor(manholeItemName,
							manholeCostItem, null, 1);
					_CostItemFactors.Add(manholeCostItemFactor.ID, manholeCostItemFactor);
					int manholeIndex = pipeAndManholeItem.AddCostItemFactor(manholeCostItemFactor);
					if (manholeIndex == -1)
						throw new Exception("Cannot add CostItemFactor");

					// Ancillary CostItemFactors
					AncillaryCoster ancillaryCoster = new AncillaryCoster(altPackage);
					ancillaryCoster.AltLink = altLink;
					ancillaryCoster.AltPipXP = altPackage.AltConflictFromAltLink(altLink);

					List<AncillaryCost> ancillaryCosts = ancillaryCoster.AlternativeAncillaryCosts;
					foreach (AncillaryCost ancillaryCost in ancillaryCosts)
					{
						CostItem ancillaryItem = new CostItem(ancillaryCost.Name, 1, ancillaryCost.UnitCost, ancillaryCost.Unit);
						CostItem poolItem = AddCostItemToPool(ancillaryItem);
						CostItemFactor ancillaryCIF = new CostItemFactor(ancillaryCost.Name, poolItem, null, ancillaryCost.Units);
						AddCostItemFactorToPool(ancillaryCIF);
						pipeAndManholeCostItemFactor.AddCostItemFactor(ancillaryCIF);
					} // foreach  (ancillaryCost)

					// Ancillary CostFactors
					List<AncillaryFactor> ancillaryFactors = ancillaryCoster.AlternativeAncillaryFactors;
					foreach (AncillaryFactor ancillaryFactor in ancillaryFactors)
					{
						CostFactor factor = new CostFactor(ancillaryFactor.Name, ancillaryFactor.Factor, ancillaryFactor.FactorType);
						CostFactor factorItem = AddFactorToPool(factor);
						pipeAndManholeCostItemFactor.AddFactor(factorItem);
					} // foreach  (ancillaryFactor)

					foreach (KeyValuePair<int, CostFactor> kvpair in _StandardCostFactorPool)
						pipeAndManholeItem.AddFactor(kvpair.Value);

					// Prepare report item
					pipeAndManholeCostItemFactor.Data = new ReportPipeItem();
					pipeAndManholeCostItemFactor.ReportItemType = ReportItemType.Pipe;
					ReportPipeItem reportPipeItem = pipeAndManholeCostItemFactor.Data as ReportPipeItem;
					reportPipeItem.Name = string.Format("{0}", pipeAndManholeCostItemFactor.Name);
					reportPipeItem.ID = altLink.AltLinkID;
					reportPipeItem.MaterialType = _PipeCoster.Material.ToString();
					reportPipeItem.DiameterIn = _PipeCoster.InsideDiameter;
					reportPipeItem.DepthFt = _PipeCoster.Depth;
					reportPipeItem.Length = altLink.Length;
					reportPipeItem.ExcavationVolCuYd = _PipeCoster.ExcavationVolume * altLink.Length;
					reportPipeItem.Manhole = manholeCostItemFactor;
					reportPipeItem.Pipe = pipeCostItemFactor;
					reportPipeItem.PipeAndManhole = pipeAndManholeCostItemFactor;

				} // foreach  (altLink)

				_IsDirty = true;
				errorMessage = string.Empty;
				return true;
			} // try
			catch (Exception e)
			{
				errorMessage = e.Message;
				return false;
			} // catch (Exception)
		} // CreatePipeEstimatesFromAlternative(bw, altPackage, focusAreas)

		/// <summary>
		/// Prepare parking report item
		/// </summary>
		/// <param name="parkingTarget">Parking target</param>
		/// <param name="parkingTargetCostItemFactor">Parking target cost item factor</param>
		private void PrepareParkingReportItem(ParkingTarget parkingTarget, CostItemFactor parkingTargetCostItemFactor)
		{
			// Prepare report item
			parkingTargetCostItemFactor.Data = new ReportInflowControlItem();
			parkingTargetCostItemFactor.ReportItemType = ReportItemType.InflowControl;
			ReportInflowControlItem reportInflowControlItem = parkingTargetCostItemFactor.Data as ReportInflowControlItem;
			reportInflowControlItem.Name = string.Format("{0} {1}", parkingTarget.ID, CasedStringProcessor.SplitCasedString(_ICCoster.InflowControl.ToString()));
			reportInflowControlItem.ID = parkingTarget.ID;
			reportInflowControlItem.ControlType = "Parking";
			reportInflowControlItem.ControlSubtype = CasedStringProcessor.SplitCasedString(_ICCoster.InflowControl.ToString());
			reportInflowControlItem.Cost = parkingTargetCostItemFactor.Cost;
			reportInflowControlItem.Comment = parkingTargetCostItemFactor.Comment;
		
			_IsDirty = true;
		} // PrepareParkingReportItem(parkingTarget, parkingTargetCostItemFactor)

		/// <summary>
		/// Create parking target
		/// </summary>
		/// <param name="bw">Bw</param>
		/// <param name="altPackage">Alt package</param>
		/// <param name="focusAreas">Focus areas</param>
		/// <param name="totalTargets">Total targets</param>
		/// <param name="targetCounter">Target counter</param>
		private void CreateParkingTargets(BackgroundWorker bw, AlternativePackage altPackage,
			List<string> focusAreas, int totalTargets, ref int targetCounter)
		{
			foreach (ParkingTarget parkingTarget in altPackage.ModelAltParkingTargets.Values)
			{
				targetCounter++;

				if (!parkingTarget.ToBeBuilt)
					continue;

				bw.ReportProgress((int)((double)targetCounter / (double)totalTargets * 100),
					"Reading parking targets");

				CheckFocusArea(focusAreas, parkingTarget.FocusArea);
				CostItemFactor currentCostItem = Estimate(parkingTarget.FocusArea).ChildCostItemFactor(DESC_INFLOW_CONTROL_DIRECT_CONSTRUCTION).ChildCostItemFactor("Parking");

				_ICCoster.DrainageAreaSqFt = 43560;
				_ICCoster.InflowControl = InflowControl.VegetatedInfiltrationBasin;
				decimal icCost = _ICCoster.Cost;

				string itemName = string.Format("{0} {1}",
					_ICCoster.InflowControl.ToString(),
					parkingTarget.ID);
				CostItem newCostItem = new CostItem(_ICCoster.InflowControl.ToString(),
					1, icCost, "ac");
				CostItem poolCostItem = AddCostItemToPool(newCostItem);
				CostItemFactor parkingTargetCostItemFactor = new CostItemFactor(itemName,
					null, null, 1);
				CostItemFactor baseParkingTargetCostItemFactor = new CostItemFactor(newCostItem.Name,
					poolCostItem, null, parkingTarget.ControlledArea / 43560);
				parkingTargetCostItemFactor.MinCost = _ICCoster.VegetatedInfiltrationBasinMinCost;
				AddCostItemFactorToPool(parkingTargetCostItemFactor);
				AddCostItemFactorToPool(baseParkingTargetCostItemFactor);
				int icIndex = currentCostItem.AddCostItemFactor(parkingTargetCostItemFactor);
				if (icIndex == -1)
					throw new Exception("Cannot add CostItemFactor");
				icIndex = parkingTargetCostItemFactor.AddCostItemFactor(baseParkingTargetCostItemFactor);
				if (icIndex == -1)
					throw new Exception("Cannot add CostItemFactor");
				if (parkingTargetCostItemFactor.PrefactoredCost < parkingTargetCostItemFactor.Cost)
					parkingTargetCostItemFactor.Comment = "Raised to minimum cost";

				PrepareParkingReportItem(parkingTarget, parkingTargetCostItemFactor);
			} // foreach  (parkingTarget)
		
			_IsDirty = true;
		} // CreateParkingTargets(bw, altPackage, focusAreas)

		/// <summary>
		/// Prepare roof report item
		/// </summary>
		/// <param name="roofTarget">Roof target</param>
		/// <param name="roofTargetCostItemFactor">Roof target cost item factor</param>
		private void PrepareRoofReportItem(RoofTarget roofTarget, CostItemFactor roofTargetCostItemFactor)
		{
			// Prepare report item
			if (roofTargetCostItemFactor != null)
			{
				roofTargetCostItemFactor.Data = new ReportInflowControlItem();
				roofTargetCostItemFactor.ReportItemType = ReportItemType.InflowControl;
				ReportInflowControlItem reportInflowControlItem = roofTargetCostItemFactor.Data as ReportInflowControlItem;
				reportInflowControlItem.Name = string.Format("{0} {1}", roofTarget.ID, CasedStringProcessor.SplitCasedString(_ICCoster.InflowControl.ToString()));
				reportInflowControlItem.ID = roofTarget.ID;
				reportInflowControlItem.ControlType = "Roof";
				reportInflowControlItem.ControlSubtype = CasedStringProcessor.SplitCasedString(_ICCoster.InflowControl.ToString());
				reportInflowControlItem.Cost = roofTargetCostItemFactor.Cost;
				reportInflowControlItem.Comment = roofTargetCostItemFactor.Comment;
			} // if
		
			_IsDirty = true;
		} // PrepareRoofReportItem(roofTarget, roofTargetCostItemFactor)

		/// <summary>
		/// Create roof targets
		/// </summary>
		/// <param name="bw">Bw</param>
		/// <param name="altPackage">Alt package</param>
		/// <param name="focusAreas">Focus areas</param>
		/// <param name="totalTargets">Total targets</param>
		/// <param name="targetCounter">Target counter</param>
		private void CreateRoofTargets(BackgroundWorker bw, AlternativePackage altPackage,
			List<string> focusAreas, int totalTargets, ref int targetCounter)
		{
			foreach (RoofTarget roofTarget in altPackage.ModelAltRoofTargets.Values)
			{
				targetCounter++;

				if (!roofTarget.ToBeBuilt)
					continue;

				bw.ReportProgress((int)((double)targetCounter / (double)totalTargets * 100), "Reading roof targets");

				CheckFocusArea(focusAreas, roofTarget.FocusArea);
				CostItemFactor currentCostItem = Estimate(roofTarget.FocusArea).ChildCostItemFactor(DESC_INFLOW_CONTROL_DIRECT_CONSTRUCTION).ChildCostItemFactor("Roof"); ;

				_ICCoster.DrainageAreaSqFt = 43560;
				CostItemFactor roofTargetCostItemFactor = null;
				if (roofTarget.DSToBioretention > 0)
				{
					_ICCoster.InflowControl = InflowControl.VegetatedInfiltrationBasin;
					decimal icCost = _ICCoster.Cost;

					string itemName = string.Format("{0} {1}",
						_ICCoster.InflowControl.ToString(),
						roofTarget.ID);
					CostItem newCostItem = new CostItem(_ICCoster.InflowControl.ToString(), 1, icCost, "ac");
					CostItem poolCostItem = AddCostItemToPool(newCostItem);
					roofTargetCostItemFactor = new CostItemFactor(itemName,
						null, null, 1);
					CostItemFactor baseRoofTargetCostItemFactor = new CostItemFactor(newCostItem.Name,
						poolCostItem, null, roofTarget.AreaToBioretention / 43560);
					roofTargetCostItemFactor.MinCost = _ICCoster.VegetatedInfiltrationBasinMinCost;
					AddCostItemFactorToPool(roofTargetCostItemFactor);
					AddCostItemFactorToPool(baseRoofTargetCostItemFactor);
					int icIndex = currentCostItem.AddCostItemFactor(roofTargetCostItemFactor);
					if (icIndex == -1)
						throw new Exception("Cannot add CostItemFactor");
					icIndex = roofTargetCostItemFactor.AddCostItemFactor(baseRoofTargetCostItemFactor);
					if (icIndex == -1)
						throw new Exception("Cannot add CostItemFactor");
					if (roofTargetCostItemFactor.PrefactoredCost < roofTargetCostItemFactor.Cost)
						roofTargetCostItemFactor.Comment = "Raised to minimum cost";

					PrepareRoofReportItem(roofTarget, roofTargetCostItemFactor);
				} // if
				if (roofTarget.DSToEcoroof > 0)
				{
					_ICCoster.InflowControl = InflowControl.Ecoroof;
					decimal icCost = _ICCoster.Cost;

					string itemName = string.Format("{0} {1}",
						_ICCoster.InflowControl.ToString(),
						roofTarget.ID);
					CostItem newCostItem = new CostItem(_ICCoster.InflowControl.ToString(), 1, icCost, "ac");
					CostItem poolCostItem = AddCostItemToPool(newCostItem);
					roofTargetCostItemFactor = new CostItemFactor(itemName,
						null, null, 1);
					CostItemFactor baseRoofTargetCostItemFactor = new CostItemFactor(newCostItem.Name,
						poolCostItem, null, roofTarget.AreaToEcoroof / 43560);
					roofTargetCostItemFactor.MinCost = _ICCoster.EcoroofMinCost;
					AddCostItemFactorToPool(roofTargetCostItemFactor);
					AddCostItemFactorToPool(baseRoofTargetCostItemFactor);
					int icIndex = currentCostItem.AddCostItemFactor(roofTargetCostItemFactor);
					if (icIndex == -1)
						throw new Exception("Cannot add CostItemFactor");
					icIndex = roofTargetCostItemFactor.AddCostItemFactor(baseRoofTargetCostItemFactor);
					if (icIndex == -1)
						throw new Exception("Cannot add CostItemFactor");
					if (roofTargetCostItemFactor.PrefactoredCost < roofTargetCostItemFactor.Cost)
						roofTargetCostItemFactor.Comment = "Raised to minimum cost";

					PrepareRoofReportItem(roofTarget, roofTargetCostItemFactor);
				} // if
				if (roofTarget.DSToPlanter > 0)
				{
					_ICCoster.InflowControl = InflowControl.InfiltrationPlanter;
					decimal icCost = _ICCoster.Cost;

					string itemName = string.Format("{0} {1}",
						_ICCoster.InflowControl.ToString(),
						roofTarget.ID);
					CostItem newCostItem = new CostItem(_ICCoster.InflowControl.ToString(), 1, icCost, "ac");
					CostItem poolCostItem = AddCostItemToPool(newCostItem);
					roofTargetCostItemFactor = new CostItemFactor(itemName,
						null, null, 1);
					CostItemFactor baseRoofTargetCostItemFactor = new CostItemFactor(newCostItem.Name,
						poolCostItem, null, roofTarget.AreaToPlanter / 43560);
					roofTargetCostItemFactor.MinCost = _ICCoster.InfiltrationPlanterMinCost;
					AddCostItemFactorToPool(roofTargetCostItemFactor);
					AddCostItemFactorToPool(baseRoofTargetCostItemFactor);
					int icIndex = currentCostItem.AddCostItemFactor(roofTargetCostItemFactor);
					if (icIndex == -1)
						throw new Exception("Cannot add CostItemFactor");
					icIndex = roofTargetCostItemFactor.AddCostItemFactor(baseRoofTargetCostItemFactor);
					if (icIndex == -1)
						throw new Exception("Cannot add CostItemFactor");
					if (roofTargetCostItemFactor.PrefactoredCost < roofTargetCostItemFactor.Cost)
						roofTargetCostItemFactor.Comment = "Raised to minimum cost";

					PrepareRoofReportItem(roofTarget, roofTargetCostItemFactor);
				} // if
				if (roofTarget.DSToVeg > 0)
				{
					_ICCoster.InflowControl = InflowControl.DownspoutDisconnection;
					decimal icCost = _ICCoster.Cost;

					string itemName = string.Format("{0} {1}",
						_ICCoster.InflowControl.ToString(),
						roofTarget.ID);
					CostItem newCostItem = new CostItem(_ICCoster.InflowControl.ToString(), 1, icCost, "ac");
					CostItem poolCostItem = AddCostItemToPool(newCostItem);
					roofTargetCostItemFactor = new CostItemFactor(itemName,
						null, null, 1);
					CostItemFactor baseRoofTargetCostItemFactor = new CostItemFactor(newCostItem.Name,
						poolCostItem, null, roofTarget.AreaToVeg / 43560);
					roofTargetCostItemFactor.MinCost = _ICCoster.DownspoutDisconnectionMinCost;
					AddCostItemFactorToPool(roofTargetCostItemFactor);
					AddCostItemFactorToPool(baseRoofTargetCostItemFactor);
					int icIndex = currentCostItem.AddCostItemFactor(roofTargetCostItemFactor);
					if (icIndex == -1)
						throw new Exception("Cannot add CostItemFactor");
					icIndex = roofTargetCostItemFactor.AddCostItemFactor(baseRoofTargetCostItemFactor);
					if (icIndex == -1)
						throw new Exception("Cannot add CostItemFactor");
					if (roofTargetCostItemFactor.PrefactoredCost < roofTargetCostItemFactor.Cost)
						roofTargetCostItemFactor.Comment = "Raised to minimum cost";

					PrepareRoofReportItem(roofTarget, roofTargetCostItemFactor);
				} // if

			} // foreach  (roofTarget)
		
			_IsDirty = true;
		} // CreateRoofTargets(bw, altPackage, focusAreas)

		/// <summary>
		/// Prepare street report item
		/// </summary>
		/// <param name="streetTarget">Street target</param>
		/// <param name="streetTargetCostItemFactor">Street target cost item factor</param>
		private void PrepareStreetReportItem(StreetTarget streetTarget, CostItemFactor streetTargetCostItemFactor)
		{
			// Prepare report item
			streetTargetCostItemFactor.Data = new ReportInflowControlItem();
			streetTargetCostItemFactor.ReportItemType = ReportItemType.InflowControl;
			ReportInflowControlItem reportInflowControlItem = streetTargetCostItemFactor.Data as ReportInflowControlItem;
			reportInflowControlItem.Name = string.Format("{0} {1}", streetTarget.ID, CasedStringProcessor.SplitCasedString(_ICCoster.InflowControl.ToString()));
			reportInflowControlItem.ID = streetTarget.ID;
			reportInflowControlItem.ControlType = "Street";
			reportInflowControlItem.ControlSubtype = CasedStringProcessor.SplitCasedString(_ICCoster.InflowControl.ToString());
			reportInflowControlItem.Cost = streetTargetCostItemFactor.Cost;
			reportInflowControlItem.Comment = streetTargetCostItemFactor.Comment;
		
			_IsDirty = true;
		} // PrepareStreetReportItem(streetTarget, streetTargetCostItemFactor)

		private void CreateStreetTargets(BackgroundWorker bw, AlternativePackage altPackage, 
			List<string> focusAreas, int totalTargets, int targetCounter)
		{
			foreach (StreetTarget streetTarget in altPackage.ModelAltStreetTargets.Values)
			{
				targetCounter++;

				if (!streetTarget.ToBeBuilt)
					continue;

				bw.ReportProgress((int)((double)targetCounter / (double)totalTargets * 100), 
					"Reading street targets");

				CheckFocusArea(focusAreas, streetTarget.FocusArea);
				CostItemFactor currentCostItem = Estimate(streetTarget.FocusArea).ChildCostItemFactor(DESC_INFLOW_CONTROL_DIRECT_CONSTRUCTION).ChildCostItemFactor("Street");

				CostItem newCostItem = null;
				decimal icCost;
				CostItemFactor streetTargetCostItemFactor = null;
				string itemName = "";
				CostItemFactor baseStreetTargetCostItemFactor = null;
				CostItem poolCostItem = null;
				switch (streetTarget.TypeCode)
				{
					case "v":
						_ICCoster.InflowControl = InflowControl.FlowRestrictor;
						_ICCoster.FacilitySizeCuFt = 1;
						icCost = _ICCoster.Cost;
						itemName = string.Format("{0} {1}",
							_ICCoster.InflowControl.ToString(),
							streetTarget.ID);
						newCostItem = new CostItem(_ICCoster.InflowControl.ToString(), 1, icCost, "ea");
						poolCostItem = AddCostItemToPool(newCostItem);
						streetTargetCostItemFactor = new CostItemFactor(itemName,
							null, null, 1);
						baseStreetTargetCostItemFactor = new CostItemFactor(newCostItem.Name,
							poolCostItem, null, 1);
						break;
					case "p":
						_ICCoster.InflowControl = InflowControl.StormwaterPlanter;
						_ICCoster.FacilitySizeCuFt = _ICCoster.StormwaterPlanterMinCuFt + 10;
						icCost = _ICCoster.Cost;
						itemName = string.Format("{0} {1}",
							_ICCoster.InflowControl.ToString(),
							streetTarget.ID);
						newCostItem = new CostItem(_ICCoster.InflowControl.ToString(), 1,
							(decimal)((double)icCost / _ICCoster.FacilitySizeCuFt), "cuft");
						poolCostItem = AddCostItemToPool(newCostItem);
						streetTargetCostItemFactor = new CostItemFactor(itemName,
							null, null, 1);
						baseStreetTargetCostItemFactor = new CostItemFactor(newCostItem.Name,
							poolCostItem, null, streetTarget.CurbExtensionVol);
						streetTargetCostItemFactor.MinCost = _ICCoster.StormwaterPlanterMinCost;
						break;
					case "c":
						_ICCoster.InflowControl = InflowControl.CurbExtension;
						_ICCoster.FacilitySizeCuFt = _ICCoster.CurbExtensionMinCuFt + 10;
						icCost = _ICCoster.Cost;
						itemName = string.Format("{0} {1}",
							_ICCoster.InflowControl.ToString(),
							streetTarget.ID);
						newCostItem = new CostItem(_ICCoster.InflowControl.ToString(), 1,
							(decimal)((double)icCost / _ICCoster.FacilitySizeCuFt), "cuft");
						poolCostItem = AddCostItemToPool(newCostItem);
						streetTargetCostItemFactor = new CostItemFactor(itemName,
							null, null, 1);
						baseStreetTargetCostItemFactor = new CostItemFactor(newCostItem.Name,
							poolCostItem, null, streetTarget.CurbExtensionVol);
						streetTargetCostItemFactor.MinCost = _ICCoster.CurbExtensionMinCost;
						break;
				} // switch

				AddCostItemFactorToPool(streetTargetCostItemFactor);
				AddCostItemFactorToPool(baseStreetTargetCostItemFactor);
				int icIndex = currentCostItem.AddCostItemFactor(streetTargetCostItemFactor);
				if (icIndex == -1)
					throw new Exception("Cannot add CostItemFactor");
				icIndex = streetTargetCostItemFactor.AddCostItemFactor(baseStreetTargetCostItemFactor);
				if (icIndex == -1)
					throw new Exception("Cannot add CostItemFactor");
				if (streetTargetCostItemFactor.PrefactoredCost < streetTargetCostItemFactor.Cost)
					streetTargetCostItemFactor.Comment = "Raised to minimum cost";

				PrepareStreetReportItem(streetTarget, streetTargetCostItemFactor);
			} // foreach  (streetTarget)

			_IsDirty = true;
		} // CreateStreetTargets(bw, altPackage, focusAreas)

		/// <summary>
		/// Create inflow control estimates from alternative
		/// </summary>
		/// <param name="altPackage">Alt package</param>
		/// <param name="focusAreas">Focus areas</param>
		private bool CreateInflowControlEstimatesFromAlternative(BackgroundWorker bw, 
			AlternativePackage altPackage, List<string> focusAreas, out string errorMessage)
		{
			try
			{
				int totalTargets = altPackage.ModelAltParkingTargets.Count +
					altPackage.ModelAltRoofTargets.Count + altPackage.ModelAltStreetTargets.Count;
				int targetCounter = 0;

				CreateParkingTargets(bw, altPackage, focusAreas, totalTargets, ref targetCounter);
				CreateRoofTargets(bw, altPackage, focusAreas, totalTargets, ref targetCounter);
				CreateStreetTargets(bw, altPackage, focusAreas, totalTargets, targetCounter);

				_IsDirty = true;
				errorMessage = string.Empty;
				return true;
			} // try
			catch (Exception e)
			{
				errorMessage = e.Message;
				return false;
			} // catch (Exception)
		} // CreateInflowControlEstimatesFromAlternative(bw, altPackage, focusAreas)

		/// <summary>
		/// Create estimate from model
		/// </summary>
		/// <param name="bw">Bw</param>
		/// <param name="modelPath">Model path</param>
		/// <param name="selectedMLinkIds">Selected m link ids</param>
		/// <returns>Bool</returns>
		public bool CreateEstimateFromModel(BackgroundWorker bw, string modelPath, out string errorMessage)
		{
			return CreateEstimateFromModel(bw, modelPath, null, out errorMessage);
		} // CreateEstimateFromModel(bw, modelPath, errorMessage)

		/// <summary>
		/// Create estimate from model
		/// </summary>
		/// <param name="bw">Bw</param>
		/// <param name="modelPath">Model path</param>
		/// <param name="selectedMLinkIds">Selected m link ids</param>
		/// <param name="errorMessage">Error message</param>
		/// <returns>Bool</returns>
		public bool CreateEstimateFromModel(BackgroundWorker bw, string modelPath, List<int> selectedMLinkIds,
			out string errorMessage)
		{
			return CreateEstimateFromModel(bw, modelPath, selectedMLinkIds, true, out errorMessage);
		} // CreateEstimateFromModel(bw, modelPath, selectedMLinkIds)

		/// <summary>
		/// Create estimate from model
		/// </summary>
		/// <param name="bw">Bw</param>
		/// <param name="modelPath">Model path</param>
		public bool CreateEstimateFromModel(BackgroundWorker bw, string modelPath, List<int> selectedMLinkIds, 
			bool generateManholeCosts, out string errorMessage)
		{
			int linkCounter = 0;
			try
			{
				if (Directory.Exists(modelPath))
				{
					_ProjectInfo.Source = modelPath;
					_ProjectInfo.BaseENR = _PipeCoster.BaseENR;
					_ProjectInfo.ENR = _PipeCoster.CurrentENR;

					Model model = new Model(modelPath);
					CostItemFactor newEstimate = new CostItemFactor(Path.GetFileName(modelPath));
					_CostItemFactors.Add(newEstimate.ID, newEstimate);
					_Estimates.Add(newEstimate.ID, newEstimate);

					CostItemFactor directConstructionCIF = new CostItemFactor(DESC_PIPE_DIRECT_CONSTRUCTION);
					_CostItemFactors.Add(directConstructionCIF.ID, directConstructionCIF);
					newEstimate.AddCostItemFactor(directConstructionCIF);

					CostItemFactor otherDirectConstructionCIF = new CostItemFactor(DESC_OTHER_DIRECT_CONSTRUCTION);
					_CostItemFactors.Add(otherDirectConstructionCIF.ID, otherDirectConstructionCIF);
					newEstimate.AddCostItemFactor(otherDirectConstructionCIF);

					int totalLinks = model.ModelLinks.Count;

					if (selectedMLinkIds != null)
						selectedMLinkIds.Sort();
					DateTime startTime = DateTime.Now;

					foreach (Link link in model.ModelLinks.Values)
					{
						if (selectedMLinkIds != null && !selectedMLinkIds.Contains(link.MLinkID))
							continue;

						linkCounter++;
						double fractionDone = (double)linkCounter / (double)totalLinks;
						int elapsedDuration = Convert.ToInt32((DateTime.Now - startTime).Duration().TotalMinutes);
						int expectedDuration = Convert.ToInt32((DateTime.Now - startTime).Duration().TotalMinutes / fractionDone);
						int durationLeft = expectedDuration - elapsedDuration;
						bw.ReportProgress((int)(fractionDone * 100),
							string.Format("Reading links: {0} out of {1}, {2} minutes left",
							linkCounter, totalLinks, durationLeft));

						_PipeCoster.Depth = model.PipeDepth(link);
						_PipeCoster.InsideDiameter = link.Diameter;
						switch (link.Material.ToUpper())
						{
							case "PVC":
							case "HDPE":
								_PipeCoster.Material = PipeMaterial.PVCHDPE;
								break;
							case "PIPEBURST":
								_PipeCoster.Material = PipeMaterial.Pipeburst;
								break;
							case "CIPP":
								_PipeCoster.Material = PipeMaterial.CIPP;
								break;
							default:
								_PipeCoster.Material = PipeMaterial.Concrete;
								break;
						} // switch

						// Pipe and Manhole CostItemFactor
						CostItemFactor pipeAndManholeCostItemFactor = new CostItemFactor(string.Format("{0} {1}-{2}",
							link.MLinkID, link.USNodeName, link.DSNodeName));
						_CostItemFactors.Add(pipeAndManholeCostItemFactor.ID, pipeAndManholeCostItemFactor);
						int pipeAndManholeIndex = directConstructionCIF.AddCostItemFactor(pipeAndManholeCostItemFactor);
						if (pipeAndManholeIndex == -1)
							throw new Exception("Cannot add CostItemFactor");
						CostItemFactor pipeAndManholeItem = directConstructionCIF.ChildCostItemFactor(pipeAndManholeIndex);

						CostItem manholeCostItem = null;
						// Manhole CostItem
						if (generateManholeCosts)
						{
							string manholeCostItemName = string.Format("Manhole {0:F0} in diam {1:F0} ft deep",
								_PipeCoster.InsideDiameter, _PipeCoster.Depth);
							bool outsideManholeTable;
							manholeCostItem = CostItemFromPool(manholeCostItemName);
							if (manholeCostItem == null)
							{
								manholeCostItem = new CostItem(manholeCostItemName,
									1, _PipeCoster.ManholeCost(_PipeCoster.ManholeDiameter,
										_PipeCoster.Depth, out outsideManholeTable), "ea");
								AddCostItemToPool(manholeCostItem);
							} // if
						}

						// Pipe CostItemFactor
						string pipeItemName = string.Format("Pipe {0:F1} in diam, {1:F0} ft deep, {2}",
							link.Diameter,
							model.PipeDepth(link),
							link.Material);
						CostItemFactor pipeCostItemFactor = new CostItemFactor(pipeItemName, null, null, link.Length);
						_CostItemFactors.Add(pipeCostItemFactor.ID, pipeCostItemFactor);
						_PipeCoster.CreateDirectConstructionCostItems(this, pipeCostItemFactor);
						int pipeIndex = pipeAndManholeItem.AddCostItemFactor(pipeCostItemFactor);
						if (pipeIndex == -1)
							throw new Exception("Cannot add CostItemFactor");

						CostItemFactor manholeCostItemFactor = null;
						// Manhole CostItemFactor
						if (generateManholeCosts)
						{
							string manholeItemName = string.Format("Manhole {0:F0} in diam, {1:F0} ft deep",
								_PipeCoster.ManholeDiameter, _PipeCoster.Depth);
							manholeCostItemFactor = new CostItemFactor(manholeItemName,
									manholeCostItem, null, 1);
							_CostItemFactors.Add(manholeCostItemFactor.ID, manholeCostItemFactor);
							int manholeIndex = pipeAndManholeItem.AddCostItemFactor(manholeCostItemFactor);
							if (manholeIndex == -1)
								throw new Exception("Cannot add CostItemFactor");
						}

						// Ancillary CostItemFactors
						AncillaryCoster ancillaryCoster = new AncillaryCoster(model);
						ancillaryCoster.Link = link;
						ancillaryCoster.PipXP = model.ConflictFromLink(link);

						List<AncillaryCost> ancillaryCosts = ancillaryCoster.ModelAncillaryCosts;
						if (ancillaryCosts.Count == 0)
						{
							if (pipeAndManholeCostItemFactor.Comment != null && pipeAndManholeCostItemFactor.Comment.Length > 0)
								pipeAndManholeCostItemFactor.Comment = pipeAndManholeCostItemFactor + ";" + "No ancillary costs";
							else
								pipeAndManholeCostItemFactor.Comment = "No ancillaryCosts";
						} // if
						else
						{
							foreach (AncillaryCost ancillaryCost in ancillaryCosts)
							{
								CostItem ancillaryItem = new CostItem(ancillaryCost.Name, 1, ancillaryCost.UnitCost, ancillaryCost.Unit);
								CostItem poolItem = AddCostItemToPool(ancillaryItem);
								CostItemFactor ancillaryCIF = new CostItemFactor(ancillaryCost.Name, poolItem, null, ancillaryCost.Units);
								AddCostItemFactorToPool(ancillaryCIF);
								pipeAndManholeCostItemFactor.AddCostItemFactor(ancillaryCIF);
							} // foreach  (ancillaryCost)
						} // else

						// Ancillary CostFactors
						List<AncillaryFactor> ancillaryFactors = ancillaryCoster.ModelAncillaryFactors;
						foreach (AncillaryFactor ancillaryFactor in ancillaryFactors)
						{
							CostFactor factor = new CostFactor(ancillaryFactor.Name, ancillaryFactor.Factor, ancillaryFactor.FactorType);
							CostFactor factorItem = AddFactorToPool(factor);
							pipeAndManholeCostItemFactor.AddFactor(factorItem);
						} // foreach  (ancillaryFactor)

						// Escalation

						foreach (KeyValuePair<int, CostFactor> kvpair in _StandardCostFactorPool)
							pipeAndManholeItem.AddFactor(kvpair.Value);
						pipeAndManholeCostItemFactor.AddFactor(FactorFromPool("2008 Escalation"));

						// Prepare report item
						pipeAndManholeCostItemFactor.Data = new ReportPipeItem();
						pipeAndManholeCostItemFactor.ReportItemType = ReportItemType.Pipe;
						ReportPipeItem reportPipeItem = pipeAndManholeCostItemFactor.Data as ReportPipeItem;
						reportPipeItem.Name = string.Format("{0}", pipeAndManholeCostItemFactor.Name);
						reportPipeItem.ID = link.MLinkID;
						reportPipeItem.MaterialType = _PipeCoster.Material.ToString();
						reportPipeItem.DiameterIn = _PipeCoster.InsideDiameter;
						reportPipeItem.DepthFt = _PipeCoster.Depth;
						reportPipeItem.Length = link.Length;
						reportPipeItem.ExcavationVolCuYd = _PipeCoster.ExcavationVolume * link.Length;
						reportPipeItem.Manhole = generateManholeCosts ? manholeCostItemFactor : null;
						reportPipeItem.Pipe = pipeCostItemFactor;
						reportPipeItem.PipeAndManhole = pipeAndManholeCostItemFactor;

					} // foreach  (link)

					foreach (KeyValuePair<int, CostItemFactor> kvpair in _Estimates)
					{
						int index = _ProjectEstimate.AddCostItemFactor(kvpair.Value);
						if (index == -1)
							throw new Exception("Cannot add CostItemFactor");
						_ProjectEstimate.ChildCostItemFactor(index).AddFactor(FactorFromPool(DESC_CONTINGENCY));
						_ProjectEstimate.ChildCostItemFactor(index).AddFactor(FactorFromPool("Const mgt, Insp, Test"));
						_ProjectEstimate.ChildCostItemFactor(index).AddFactor(FactorFromPool("Design"));
						_ProjectEstimate.ChildCostItemFactor(index).AddFactor(FactorFromPool("PI, I&C, Easements, Environmental"));
						_ProjectEstimate.ChildCostItemFactor(index).AddFactor(FactorFromPool("Startup/closeout"));
					} // foreach  (kvpair)
				} // if
			} // try
			catch (OutOfMemoryException e)
			{
				_IsDirty = true;
				errorMessage = string.Format("Ran out of memory while processing model. Loaded {1} links. (System message:{0})",
					e.Message, linkCounter);
				return false;
			} // catch
			catch (Exception e)
			{
				_IsDirty = true;
				errorMessage = e.Message;
				return false;
			} // catch (Exception)

			errorMessage = string.Empty;
			_IsDirty = true;

			return true;
		} // CreateEstimateFromModel(bw, modelPath, selectedMLinkIds)

		/// <summary>
		/// Write estimates as strings
		/// </summary>
		/// <returns>List</returns>
		public List<string> WriteEstimatesAsStrings()
		{
			return _ProjectEstimate.CostItemFactorsAsStrings();
		} // WriteEstimatesAsStrings()

		/// <summary>
		/// Estimate
		/// </summary>
		/// <param name="index">Index</param>
		/// <returns>Cost item factor</returns>
		public CostItemFactor Estimate(int index)
		{
			return _Estimates.Values[index];
		} // Estimate(index)

		/// <summary>
		/// Estimate
		/// </summary>
		/// <param name="index">Estimate name</param>
		/// <returns>Cost item factor</returns>
		public CostItemFactor Estimate(string index)
		{
			for (int i = 0; i < _Estimates.Count; i++)
			{
				if (_Estimates.Values[i].Name == index)
					return _Estimates.Values[i];
			} // for
			return null;
		} // Estimate(index)

		/// <summary>
		/// Factor from pool
		/// </summary>
		/// <param name="index">Index</param>
		/// <returns>Cost factor</returns>
		public CostFactor FactorFromPool(string index)
		{
			for (int i = 0; i < _CostFactorPool.Count; i++)
			{
				if (_CostFactorPool.Values[i].Name == index)
					return _CostFactorPool.Values[i];
			} // for
			return null;
		} // FactorFromPool(index)

		/// <summary>
		/// Factor from pool
		/// </summary>
		/// <param name="index">Index</param>
		/// <returns>Cost factor</returns>
		public CostFactor FactorFromPool(int index)
		{
			return _CostFactorPool[index];
		} // FactorFromPool(index)

		/// <summary>
		/// Factor from pooly by index
		/// </summary>
		/// <param name="index">Index</param>
		/// <returns>Cost factor</returns>
		public CostFactor FactorFromPoolByIndex(int index)
		{
			return _CostFactorPool.Values[index];
		} // FactorFromPoolByIndex(index)

		/// <summary>
		/// Suggested next factor name for pool
		/// </summary>
		/// <param name="name">Name</param>
		/// <returns>String</returns>
		public string SuggestedNextFactorNameForPool(string factorName)
		{
			string suggestedName = factorName;
			int copyCounter = 1;
			while (PoolHasFactor(suggestedName))
			{
				suggestedName = string.Format("{0} ({1})", factorName, copyCounter);
			} // while
			return suggestedName;
		} // SuggestedNextFactorNameForPool(factorName)

		/// <summary>
		/// Pool has factor
		/// </summary>
		/// <param name="factorName">Factor name</param>
		/// <returns>Bool</returns>
		public bool PoolHasFactor(string factorName)
		{
			return FactorFromPool(factorName) != null;
		} // PoolHasFactor(factorName)

		/// <summary>
		/// Cost item from pool
		/// </summary>
		/// <param name="index">Index</param>
		/// <returns>Cost item</returns>
		public CostItem CostItemFromPool(string index)
		{
			for (int i = 0; i < _CostItemPool.Count; i++)
			{
				if (_CostItemPool.Values[i].Name == index)
					return _CostItemPool.Values[i];
			} // for
			return null;
		} // CostItemFromPool(index)

		/// <summary>
		/// Cost item from pool
		/// </summary>
		/// <param name="index">Index</param>
		/// <returns>Cost item</returns>
		public CostItem CostItemFromPool(int index)
		{
			return _CostItemPool[index];
		} // CostItemFromPool(index)

		/// <summary>
		/// Cost item from pool by index
		/// </summary>
		/// <param name="index">Index</param>
		/// <returns>Cost item</returns>
		public CostItem CostItemFromPoolByIndex(int index)
		{
			return _CostItemPool.Values[index];
		} // CostItemFromPoolByIndex(index)

		/// <summary>
		/// Suggested next cost item name for pool
		/// </summary>
		/// <param name="costItemName">Cost item name</param>
		/// <returns>String</returns>
		public string SuggestedNextCostItemNameForPool(string costItemName)
		{
			string suggestedName = costItemName;
			int copyCounter = 1;
			while (PoolHasCostItem(suggestedName))
			{
				suggestedName = string.Format("{0} ({1})", costItemName, copyCounter);
			} // while
			return suggestedName;
		} // SuggestedNextCostItemNameForPool(costItemName)

		/// <summary>
		/// Pool has cost item
		/// </summary>
		/// <param name="costItemName">Cost item name</param>
		/// <returns>Bool</returns>
		public bool PoolHasCostItem(string costItemName)
		{
			return CostItemFromPool(costItemName) != null;
		} // PoolHasCostItem(costItemName)

		/// <summary>
		/// Cost item factor
		/// </summary>
		/// <param name="index">Index</param>
		public CostItemFactor CostItemFactorFromPool(int index)
		{
			if (index == 0)
				return _ProjectEstimate;
			else
				return _CostItemFactors[index];
		} // CostItemFactorFromPool(index)

		/// <summary>
		/// Cost item factor from pool by index
		/// </summary>
		/// <param name="index">Index</param>
		/// <returns>Cost item factor</returns>
		public CostItemFactor CostItemFactorFromPoolByIndex(int index)
		{
			return _CostItemFactors.Values[index];
		} // CostItemFactorFromPoolByIndex(index)

		/// <summary>
		/// Copy factor
		/// </summary>
		/// <param name="costFactor">Cost factor</param>
		public CostFactor CopyFactor(CostFactor costFactor)
		{
			string newName = SuggestedNextFactorNameForPool(costFactor.Name);
			CostFactor newFactor = new CostFactor(costFactor, newName);
			_CostFactorPool.Add(newFactor.ID, newFactor);

			_IsDirty = true;

			return newFactor;
		} // CopyFactor(costFactor)

		/// <summary>
		/// Copy cost item
		/// </summary>
		/// <param name="costItem">Cost item</param>
		public CostItem CopyCostItem(CostItem costItem)
		{
			string newName = SuggestedNextCostItemNameForPool(costItem.Name);
			CostItem newCostItem = new CostItem(costItem, newName);
			AddCostItemToPool(newCostItem);

			_IsDirty = true;

			return newCostItem;
		} // CopyCostItem(costItem)

		/// <summary>
		/// Add cost item to pool
		/// </summary>
		/// <param name="newCostItem">New cost item</param>
		public CostItem AddCostItemToPool(CostItem newCostItem)
		{
			if (!PoolHasCostItem(newCostItem.Name))
			{
				_CostItemPool.Add(newCostItem.ID, newCostItem);
				_IsDirty = true;
				return newCostItem;
			} // if
			else
			{
				CostItem.RevertID();
				return CostItemFromPool(newCostItem.Name);
			} // else
		} // AddCostItemToPool(newCostItem)

		/// <summary>
		/// Add factor to pool
		/// </summary>
		/// <param name="newCostFactor">New cost factor</param>
		/// <returns>Bool</returns>
		public CostFactor AddFactorToPool(CostFactor newCostFactor)
		{
			if (!PoolHasFactor(newCostFactor.Name))
			{
				_CostFactorPool.Add(newCostFactor.ID, newCostFactor);
				_IsDirty = true;
				return newCostFactor;
			} // if
			else
			{
				CostFactor.RevertID();
				return FactorFromPool(newCostFactor.Name);
			} // else
		} // AddFactorToPool(newCostFactor)

		/// <summary>
		/// Add cost item factor to pool
		/// </summary>
		/// <param name="newCostItemFactor">New cost item factor</param>
		/// <returns>Bool</returns>
		public bool AddCostItemFactorToPool(CostItemFactor newCostItemFactor)
		{
			_CostItemFactors.Add(newCostItemFactor.ID, newCostItemFactor);
			_IsDirty = true;
			return true;
		} // AddCostItemFactorToPool(newCostItemFactor)

		/// <summary>
		/// Add estimate
		/// </summary>
		/// <param name="newCostItemFactor">New cost item factor</param>
		public void AddEstimate(CostItemFactor newCostItemFactor)
		{
			_Estimates.Add(newCostItemFactor.ID, newCostItemFactor);
			_ProjectEstimate.AddCostItemFactor(newCostItemFactor);
			AddCostItemFactorToPool(newCostItemFactor);
			newCostItemFactor.AddAsParent(Root);
			_IsDirty = true;
		} // AddEstimate(newCostItemFactor)

		/// <summary>
		/// Revert cost item factor pool
		/// </summary>
		public void RevertCostItemFactorPool()
		{
			CostItemFactor.RevertID();
			_CostItemFactors.RemoveAt(_CostItemFactors.Count - 1);
		} // RevertCostItemFactorPool()

		/// <summary>
		/// Revert cost item pool
		/// </summary>
		public void RevertCostItemPool()
		{
			CostItem.RevertID();
			_CostItemPool.RemoveAt(_CostItemPool.Count - 1);
		} // RevertCostItemPool()

		/// <summary>
		/// Revert factor pool
		/// </summary>
		public void RevertFactorPool()
		{
			CostFactor.RevertID();
			_CostFactorPool.RemoveAt(_CostFactorPool.Count - 1);
		} // RevertFactorPool()

		/// <summary>
		/// Revert estimate
		/// </summary>
		public void RevertEstimate()
		{
			RevertCostItemFactorPool();
			_Estimates.RemoveAt(_Estimates.Count - 1);
		} // RevertEstimate()

		/// <summary>
		/// Delete estimate
		/// </summary>
		/// <param name="index">Index</param>
		public void DeleteEstimate(int index)
		{
			_Estimates.Remove(index);
			_ProjectEstimate.DeleteCostItemFactor(index);
			_IsDirty = true;
		} // DeleteEstimate(index)

		/// <summary>
		/// Report pipe items
		/// </summary>
		/// <returns>List</returns>
		public List<ReportPipeItem> ReportPipeItems()
		{
			List<ReportPipeItem> pipeReportList = new List<ReportPipeItem>();

			for (int i = 0; i < _Estimates.Count; i++)
			{
				List<ReportPipeItem> estimatePipeReportList = ReportPipeItems(Estimate(i));
				pipeReportList.AddRange(estimatePipeReportList);
			} // foreach  (item)

			return pipeReportList;
		} // ReportPipeItems()

		/// <summary>
		/// Report pipe items
		/// </summary>
		/// <returns>List</returns>
		public List<ReportPipeItem> ReportPipeItems(int estimateIndex)
		{
			List<ReportPipeItem> pipeReportList = new List<ReportPipeItem>();
			CostItemFactor estimate = Estimate(estimateIndex);
			CostItemFactor pipeDirectConstructionCIF = estimate.ChildCostItemFactor(DESC_PIPE_DIRECT_CONSTRUCTION);
			foreach (CostItemFactor pipeCIF in pipeDirectConstructionCIF.ChildCostItemFactors)
			{
				if (pipeCIF.Data != null)
				{
					if (pipeCIF.Data is ReportPipeItem)
					{
						ReportPipeItem pipeData = pipeCIF.Data as ReportPipeItem;
						pipeReportList.Add(pipeData);
					} // if
				} // if
			} // foreach  (pipeCIF)

			return pipeReportList;
		} // ReportPipeItems(estimateIndex)

		/// <summary>
		/// Report inflow control items
		/// </summary>
		/// <param name="estimateIndex">Estimate index</param>
		/// <returns>List</returns>
		public List<ReportInflowControlItem> ReportInflowControlItems(string kind, int estimateIndex)
		{
			SortedList<string, ReportInflowControlItem> sortedInflowControlList =
				new SortedList<string, ReportInflowControlItem>();
			CostItemFactor estimate = Estimate(estimateIndex);
			CostItemFactor icDirectConstructionCIF = estimate.ChildCostItemFactor(DESC_INFLOW_CONTROL_DIRECT_CONSTRUCTION);
			if (icDirectConstructionCIF == null)
				return new List<ReportInflowControlItem>();
			CostItemFactor icChildDirectConstructionCIF = icDirectConstructionCIF.ChildCostItemFactor(kind);
			foreach (CostItemFactor icCIF in icChildDirectConstructionCIF.ChildCostItemFactors)
			{
				if (icCIF.Data != null)
				{
					ReportInflowControlItem icData = icCIF.Data as ReportInflowControlItem;
					if (icData != null)
						sortedInflowControlList.Add(icData.Name, icData);
				} // if
			} // foreach  (icCIF)

			List<ReportInflowControlItem> inflowControlReportList = new List<ReportInflowControlItem>();
			foreach (KeyValuePair<string, ReportInflowControlItem> kvPair in sortedInflowControlList)
				inflowControlReportList.Add(kvPair.Value);

			return inflowControlReportList;
		} // ReportInflowControlItems(kind, estimateIndex)

		/// <summary>
		/// Report inflow control items
		/// </summary>
		/// <param name="estimateIndex">Estimate index</param>
		/// <returns>List</returns>
		public List<ReportInflowControlItem> ReportInflowControlItems(int estimateIndex)
		{
			List<ReportInflowControlItem> inflowControlList = new List<ReportInflowControlItem>();

			inflowControlList.AddRange(ReportInflowControlItems("Roof", estimateIndex));
			inflowControlList.AddRange(ReportInflowControlItems("Parking", estimateIndex));
			inflowControlList.AddRange(ReportInflowControlItems("Street", estimateIndex));

			return inflowControlList;
		} // ReportInflowControlItems(estimateIndex)

		/// <summary>
		/// Report other direct construction items
		/// </summary>
		/// <param name="estimateIndex">Estimate index</param>
		/// <returns>List</returns>
		public List<ReportGenericItem> ReportOtherDirectConstructionItems(int estimateIndex)
		{
			List<ReportGenericItem> otherDirectConstructionItems = new List<ReportGenericItem>();
			CostItemFactor estimate = Estimate(estimateIndex);
			CostItemFactor otherDirectConstructionCIF = estimate.ChildCostItemFactor(DESC_OTHER_DIRECT_CONSTRUCTION);

			// Account for other direct construction node's factors
			for (int i = 0; i < otherDirectConstructionCIF.CostFactorsCount; i++)
			{
				CostFactor currentFactor = otherDirectConstructionCIF.CostFactor(i);
				ReportGenericItem reportItem = new ReportGenericItem();
				reportItem.Name = string.Format("{0} ({1:P0})", currentFactor.Name, currentFactor.FactorValue);
				reportItem.Quantity = 1;
				reportItem.UnitName = "ls";
				reportItem.UnitPrice = otherDirectConstructionCIF.CostFactorValue(i);
				otherDirectConstructionItems.Add(reportItem);
			}

			// Account for other direct construction node's children's factors
			for (int j = 0; j < otherDirectConstructionCIF.ChildCostItemFactorCount; j++)
			{
				CostItemFactor childCostItemFactor = otherDirectConstructionCIF.ChildCostItemFactor(j);
				for (int i = 0; i < childCostItemFactor.CostFactorsCount; i++)
				{
					CostFactor currentFactor = childCostItemFactor.CostFactor(i);
					ReportGenericItem reportItem = new ReportGenericItem();
					reportItem.Name = string.Format("{2} {0} ({1:P0})", currentFactor.Name, currentFactor.FactorValue,
						childCostItemFactor.Name);
					reportItem.Quantity = 1;
					reportItem.UnitName = "ls";
					reportItem.UnitPrice = childCostItemFactor.CostFactorValue(i);
					otherDirectConstructionItems.Add(reportItem);
				}
			}

			foreach (KeyValuePair<int, CostItemFactor> kvPair in _CostItemFactors)
			{
				CostItemFactor item = kvPair.Value;
				if (estimate.IsAParentOf(item))
				{
					if (item.Data is ReportGenericItem)
					{
						ReportGenericItem reportItem = item.Data as ReportGenericItem;
						reportItem.Name = item.Name;
						reportItem.Quantity = item.Quantity;
						reportItem.UnitName = item.CostItem != null ?
							item.CostItem.UnitName : "ea";
						reportItem.UnitPrice = item.CostItem != null ?
							(decimal)((double)item.CostItem.UnitCost *
							item.Factor) : item.Cost;
						reportItem.Group = "General";
						reportItem.Item = item;
						reportItem.Comment = item.Comment;
						otherDirectConstructionItems.Add(reportItem);
					} // if
				}
			} // foreach  (kvPair)
			return otherDirectConstructionItems;
		} // ReportOtherDirectConstructionItems(estimateIndex)

		/// <summary>
		/// Report generic items
		/// </summary>
		/// <param name="estimateIndex">Estimate index</param>
		/// <returns>List</returns>
		public List<ReportGenericItem> ReportGenericItems(int estimateIndex)
		{
			List<ReportGenericItem> genericList = new List<ReportGenericItem>();
			CostItemFactor estimate = Estimate(estimateIndex);
			CostItemFactor pipeDirectConstructionCIF = estimate.ChildCostItemFactor(DESC_PIPE_DIRECT_CONSTRUCTION);
			CostItemFactor icDirectConstructionCIF = estimate.ChildCostItemFactor(DESC_INFLOW_CONTROL_DIRECT_CONSTRUCTION);
			foreach (CostItemFactor genericCIF in estimate.ChildCostItemFactors)
			{
				if ((genericCIF != pipeDirectConstructionCIF) &&
					(genericCIF != icDirectConstructionCIF)) 
				{
					ReportGenericItem genericItem = new ReportGenericItem();
					genericItem.Name = genericCIF.Name;
					genericItem.Quantity = 1;
					genericItem.UnitName = "ea";
					genericItem.UnitPrice = genericCIF.Cost;
					genericList.Add(genericItem);
				} // if
			} // foreach  (genericCIF)

			return genericList;
		} // ReportGenericItems(estimateIndex)

		/// <summary>
		/// Report summary items
		/// </summary>
		/// <param name="estimateIndex">Estimate index</param>
		/// <returns>List</returns>
		public List<ReportSummaryItem> ReportSummaryItems(int estimateIndex)
		{
			List<ReportSummaryItem> summaryList = new List<ReportSummaryItem>();
			CostItemFactor estimate = Estimate(estimateIndex);
			if (estimate == null)
				return null;

			// Direct costs
			if (estimate.ChildCostItemFactor(DESC_PIPE_DIRECT_CONSTRUCTION) != null)
			{
				ReportSummaryItem directCostItem = new ReportSummaryItem();
				directCostItem.Name = "Direct construction pipe cost";
				directCostItem.Cost = estimate.ChildCostItemFactor(DESC_PIPE_DIRECT_CONSTRUCTION).PrefactoredCost;
				directCostItem.Group = "Total direct construction cost";
				directCostItem.SummaryGroup = "Total direct construction & contingency cost";
				summaryList.Add(directCostItem);
			}
			// Top-level Pipe Factors
			CostItemFactor directPipeCIF = estimate.ChildCostItemFactor(DESC_PIPE_DIRECT_CONSTRUCTION);
			if (directPipeCIF != null)
				foreach (CostFactor factor in directPipeCIF.CostFactors)
				{
					ReportSummaryItem pipeTopLevelFactorItem = new ReportSummaryItem();
					pipeTopLevelFactorItem.Name = string.Format("(Pipes) {0} ({1:P0})", factor.Name, factor.FactorValue);
					pipeTopLevelFactorItem.Cost = directPipeCIF.CostFactorValue(directPipeCIF.CostFactorIndex(factor));
					pipeTopLevelFactorItem.Group = "Total direct construction cost";
					pipeTopLevelFactorItem.SummaryGroup = "Total direct construction & contingency cost";
					summaryList.Add(pipeTopLevelFactorItem);
				} // foreach  (factor)

			if (estimate.ChildCostItemFactor(DESC_INFLOW_CONTROL_DIRECT_CONSTRUCTION) != null)
			{
				ReportSummaryItem icDirectCostItem = new ReportSummaryItem();
				icDirectCostItem.Name = "Direct construction inflow control cost";
				icDirectCostItem.Cost = estimate.ChildCostItemFactor(DESC_INFLOW_CONTROL_DIRECT_CONSTRUCTION).PrefactoredCost;
				icDirectCostItem.Group = "Total direct construction cost";
				icDirectCostItem.SummaryGroup = "Total direct construction & contingency cost";
				summaryList.Add(icDirectCostItem);
			}
			// Top-level IC Factors
			CostItemFactor directICCIF = estimate.ChildCostItemFactor(DESC_INFLOW_CONTROL_DIRECT_CONSTRUCTION);
			if (directICCIF != null)
				foreach (CostFactor factor in directICCIF.CostFactors)
				{
					ReportSummaryItem icTopLevelFactorItem = new ReportSummaryItem();
					icTopLevelFactorItem.Name = string.Format("(Inflow controls) {0} ({1:P0})", factor.Name, factor.FactorValue);
					icTopLevelFactorItem.Cost = directICCIF.CostFactorValue(directICCIF.CostFactorIndex(factor));
					icTopLevelFactorItem.Group = "Total direct construction cost";
					icTopLevelFactorItem.SummaryGroup = "Total direct construction & contingency cost";
					summaryList.Add(icTopLevelFactorItem);
				} // foreach  (factor)

			if (estimate.ChildCostItemFactor(DESC_OTHER_DIRECT_CONSTRUCTION) != null)
			{
				ReportSummaryItem otherDirectCostItem = new ReportSummaryItem();
				otherDirectCostItem.Name = "Other direct construction related cost";
				otherDirectCostItem.Cost = estimate.ChildCostItemFactor(DESC_OTHER_DIRECT_CONSTRUCTION).PrefactoredCost;
				otherDirectCostItem.Group = "Total direct construction cost";
				otherDirectCostItem.SummaryGroup = "Total direct construction & contingency cost";
				summaryList.Add(otherDirectCostItem);
			}
			// Top-level Other DC Factors
			CostItemFactor directOtherCIF = estimate.ChildCostItemFactor(DESC_OTHER_DIRECT_CONSTRUCTION);
			if (directOtherCIF != null)
				foreach (CostFactor factor in directOtherCIF.CostFactors)
				{
					ReportSummaryItem otherTopLevelFactorItem = new ReportSummaryItem();
					otherTopLevelFactorItem.Name = string.Format("(Other) {0} ({1:P0})", factor.Name, factor.FactorValue);
					otherTopLevelFactorItem.Cost = directOtherCIF.CostFactorValue(directOtherCIF.CostFactorIndex(factor));
					otherTopLevelFactorItem.Group = "Total direct construction cost";
					otherTopLevelFactorItem.SummaryGroup = "Total direct construction & contingency cost";
					summaryList.Add(otherTopLevelFactorItem);
				} // foreach  (factor)

			// Contingency
			CostFactor contingencyFactor = null;
			if (estimate.CostFactor(DESC_CONTINGENCY) != null)
			{
				ReportSummaryItem contingencyItem = new ReportSummaryItem();
				contingencyFactor = estimate.CostFactor(DESC_CONTINGENCY);
				contingencyItem.Name = string.Format("{0} ({1:P0})", contingencyFactor.Name, contingencyFactor.FactorValue);
				contingencyItem.Cost = estimate.CostFactorValue(estimate.CostFactorIndex(contingencyFactor));
				contingencyItem.Group = "Contingency";
				contingencyItem.SummaryGroup = "Total direct construction & contingency cost";
				summaryList.Add(contingencyItem);
			}

			// Indirect items
			foreach (CostFactor indirectFactor in estimate.CostFactors)
			{
				if (contingencyFactor == null || indirectFactor != contingencyFactor)
				{
					ReportSummaryItem summaryItem = new ReportSummaryItem();
					summaryItem.Name = string.Format("{0} ({1:P0})", indirectFactor.Name, indirectFactor.FactorValue);
					summaryItem.Cost = estimate.CostFactorValue(estimate.CostFactorIndex(indirectFactor));
					summaryItem.Group = "Total indirect project cost";
					summaryItem.SummaryGroup = "Total indirect project cost";
					summaryList.Add(summaryItem);
				}
			} // foreach  (indirectFactor)

			return summaryList;
		} // ReportSummaryItems(estimateIndex)

		/// <summary>
		/// Estimate index
		/// </summary>
		/// <param name="estimate">Estimate</param>
		/// <returns>Int</returns>
		public int EstimateIndex(CostItemFactor estimate)
		{
			return _Estimates.IndexOfValue(estimate);
		} // EstimateIndex(estimate)

		/// <summary>
		/// Report pipe items
		/// </summary>
		/// <param name="Estimate">Estimate</param>
		/// <returns>List</returns>
		public List<ReportPipeItem> ReportPipeItems(CostItemFactor estimate)
		{
			return ReportPipeItems(EstimateIndex(estimate));
		} // ReportPipeItems(Estimate)

		/// <summary>
		/// Report other direct construction items
		/// </summary>
		/// <param name="estimate">Estimate</param>
		/// <returns>List</returns>
		public List<ReportGenericItem> ReportOtherDirectConstructionItems(CostItemFactor estimate)
		{
			return ReportOtherDirectConstructionItems(EstimateIndex(estimate));
		} // ReportOtherDirectConstructionItems(estimate)

		/// <summary>
		/// Report generic items
		/// </summary>
		/// <param name="estimate">Estimate</param>
		/// <returns>List</returns>
		public List<ReportGenericItem> ReportGenericItems(CostItemFactor estimate)
		{
			return ReportGenericItems(EstimateIndex(estimate));
		} // ReportGenericItems(estimate)

		/// <summary>
		/// Report summary items
		/// </summary>
		/// <param name="estimate">Estimate</param>
		/// <returns>List</returns>
		public List<ReportSummaryItem> ReportSummaryItems(CostItemFactor estimate)
		{
			return ReportSummaryItems(EstimateIndex(estimate));
		} // ReportSummaryItems(estimate)

		/// <summary>
		/// Report inflow control items
		/// </summary>
		/// <returns>List</returns>
		public List<ReportInflowControlItem> ReportInflowControlItems(string kind, CostItemFactor estimate)
		{
			return ReportInflowControlItems(kind, EstimateIndex(estimate));
		} // ReportInflowControlItems()

		/// <summary>
		/// Report inflow control items
		/// </summary>
		/// <param name="Estimate">Estimate</param>
		/// <returns>List</returns>
		public List<ReportInflowControlItem> ReportInflowControlItems(CostItemFactor estimate)
		{
			return ReportInflowControlItems(EstimateIndex(estimate));
		} // ReportInflowControlItems(Estimate)

		/// <summary>
		/// Report generic inflow control items
		/// </summary>
		/// <param name="estimateIndex">Estimate index</param>
		/// <returns>List</returns>
		#endregion
	}
}
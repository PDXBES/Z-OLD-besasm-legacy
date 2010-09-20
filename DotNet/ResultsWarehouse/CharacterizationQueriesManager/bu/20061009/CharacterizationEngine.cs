using System;
using System.Xml;
using System.Collections;
using System.Collections.Specialized;
using SystemsAnalysis;
using SystemsAnalysis.Types;
using SystemsAnalysis.Tracer;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Utils.Events;

namespace SystemsAnalysis.Characterization
{
	/// <summary>
	/// A class to perform characterization queries for collections of Model
	/// components. Characterization operations are assumed to take place against
	/// component collections, not a Model or StartLinks/StopLinks collections.
	/// </summary>
	public class CharacterizationEngine
	{
		private Links charLinks;
		private Nodes charNodes;
		private DSCs charDSCs;

		private ListDictionary linkResults;		
		private ListDictionary nodeResults;		
		private ListDictionary dscResults;
		private string studyArea;

		private string template;
		private string outputFile;

		private string agMasterDB;
		private string resultsWarehouseDB;
		private string psDB;
		private string uicDB;
		private string mstSurfSC_FC;

		/// <summary>
		/// Constructor for the CharacterizationEngine.
		/// </summary>
		/// <param name="charLinks">A Links collection to be characterized.</param>		
		/// <param name="settings">A Settings DataSet that contains information regarding
		/// the source files for the characterization engine.</param>
		/// <param name="studyArea">A title for the study area used when creating table headers.</param>
		public CharacterizationEngine(Links charLinks, string studyArea, Settings settings)
		{			
			this.OnStatusChanged(new StatusChangedArgs("Creating the characterization engine."));
			this.agMasterDB = settings.Files[0].AGMasterDB;
			this.resultsWarehouseDB = settings.Files[0].ResultsWarehouseDB;
			this.psDB = settings.Files[0].PumpStationDB;
			this.template = settings.Files[0].CharTemplate;
			this.outputFile = settings.Files[0].OutputDir + "Char_" + studyArea + ".xml";
			this.uicDB = settings.FeatureClasses[0].UICDB;
			this.mstSurfSC_FC = settings.FeatureClasses[0].MstSSC;

			this.charLinks = charLinks;
			this.studyArea = studyArea;
			this.template = template;
			this.outputFile = outputFile;
		}

		/// <summary>
		/// Loads the AGMaster and Results Warehouse DataSets.
		/// </summary>
		public void LoadCharacterizationTables()
		{
				
			this.OnStatusChanged(new StatusChangedArgs("Loading the AGMaster DataSet from '"
				+ this.agMasterDB + "'."));
			DataAccess.AGMasterDataAccess agMasterDataAccess;							
			agMasterDataAccess = new DataAccess.AGMasterDataAccess(this.agMasterDB);			
				
			this.OnStatusChanged(new StatusChangedArgs("Creating Node objects."));
			this.charNodes = new Nodes();
			this.charNodes.StatusChanged += new OnStatusChangedEventHandler(this.OnStatusChanged);
			this.charNodes.Load(charLinks, this.agMasterDB);				

			this.OnStatusChanged(new StatusChangedArgs("Creating DSC objects."));			
			this.charDSCs = new DSCs();
			this.charDSCs.StatusChanged += new OnStatusChangedEventHandler(this.OnStatusChanged);
			this.charDSCs.Load(charLinks, this.agMasterDB);
						
			this.OnStatusChanged(new StatusChangedArgs("Loading the Results " +
				"Catalog DataSet from '" + this.resultsWarehouseDB + "'."));
			DataAccess.ModelCatalogDataAccess modelCatalogDataAccess;				
			modelCatalogDataAccess = new DataAccess.ModelCatalogDataAccess(this.resultsWarehouseDB);
			DataAccess.ModelCatalogDataSet modelCatalogDataSet;
			modelCatalogDataSet = new DataAccess.ModelCatalogDataSet();
			modelCatalogDataAccess.StormCatalogDataAdapter.Fill(modelCatalogDataSet);
			modelCatalogDataAccess.ModelScenarioDataAdapter.Fill(modelCatalogDataSet);
	
			this.OnStatusChanged(new StatusChangedArgs("Creating hydraulics results objects for all model scenarios."));
			//For convenience unique results objects are created for each scenario 
			//and stored in a list dictionary accessible via the scenarioID. This 
			//allows loading/accessing all model scenarios by simply iterating
			//through the dictionary
			linkResults = new ListDictionary();
			nodeResults = new ListDictionary();
			dscResults = new ListDictionary();
			foreach (DataAccess.ModelCatalogDataSet.ModelScenarioRow scenario
				in modelCatalogDataSet.ModelScenario)
			{
				this.OnStatusChanged(new StatusChangedArgs("Scenario " + Convert.ToString(scenario.scenarioID) + ": '" + scenario.description + "'."));
				linkResults.Add(
					scenario.scenarioID, new LinkHydraulics());
				nodeResults.Add(
					scenario.scenarioID, new NodeHydraulics());
				dscResults.Add(
					scenario.scenarioID, new DSCHydraulics());

			}
			this.OnStatusChanged(new StatusChangedArgs("Loading link hydraulics results for all model scenarios."));
			modelCatalogDataAccess.LinkHydraulicsDataAdapter.SelectCommand.Connection.Open();
			System.Data.OleDb.OleDbDataReader linkHydraulicsReader;
			linkHydraulicsReader = modelCatalogDataAccess.LinkHydraulicsDataAdapter.SelectCommand.ExecuteReader();
			while (linkHydraulicsReader.Read())
			{
				int scenarioID = (int)linkHydraulicsReader["scenarioID"];
				LinkHydraulics linkHydraulics = (LinkHydraulics)linkResults[scenarioID];
				int mLinkID = (int)linkHydraulicsReader["MLinkID"];
				Link link = charLinks.FindByMLinkID(mLinkID);
				if (link != null && !linkHydraulics.Contains(mLinkID))
				{
					LinkHydraulic lh = new LinkHydraulic(linkHydraulicsReader);
					lh.Link = link;
					linkHydraulics.Add(lh);

				}
			}			
			linkHydraulicsReader.Close();
			modelCatalogDataAccess.LinkHydraulicsDataAdapter.SelectCommand.Connection.Close();

			this.OnStatusChanged(new StatusChangedArgs("Loading node hydraulics results for all model scenarios."));
			modelCatalogDataAccess.NodeHydraulicsDataAdapter.SelectCommand.Connection.Open();
			System.Data.OleDb.OleDbDataReader nodeHydraulicsReader;
			nodeHydraulicsReader = modelCatalogDataAccess.NodeHydraulicsDataAdapter.SelectCommand.ExecuteReader();
			while (nodeHydraulicsReader.Read())
			{
				string nodeName = (string)nodeHydraulicsReader["nodeName"];
				int scenarioID = (int)nodeHydraulicsReader["scenarioID"];
				NodeHydraulics nodeHydraulics = (NodeHydraulics)nodeResults[scenarioID];
				if (!nodeHydraulics.Contains(nodeName) && charNodes.Contains(nodeName))
				{										
					nodeHydraulics.Add(new NodeHydraulic(nodeHydraulicsReader));
				}
			}				
			nodeHydraulicsReader.Close();
			modelCatalogDataAccess.NodeHydraulicsDataAdapter.SelectCommand.Connection.Close();

			this.OnStatusChanged(new StatusChangedArgs("Loading DSC hydraulics results for all model scenarios."));
			modelCatalogDataAccess.DSCHydraulicsDataAdapter.SelectCommand.Connection.Open();
			System.Data.OleDb.OleDbDataReader dscHydraulicsReader;
			dscHydraulicsReader = modelCatalogDataAccess.DSCHydraulicsDataAdapter.SelectCommand.ExecuteReader();
			while (dscHydraulicsReader.Read())
			{
				int dscID = (int)dscHydraulicsReader["DSCID"];
				int scenarioID = (int)dscHydraulicsReader["scenarioID"];					
				DSCHydraulics dscHydraulics = (DSCHydraulics)dscResults[scenarioID];
				if (!dscHydraulics.Contains(dscID) && charDSCs.Contains(dscID))
				{
					DSCHydraulic dh = new DSCHydraulic(dscHydraulicsReader);
					dh.DSC = charDSCs[dscID];
					dscHydraulics.Add(dh);					
				}
			}				
			dscHydraulicsReader.Close();
			modelCatalogDataAccess.DSCHydraulicsDataAdapter.SelectCommand.Connection.Close();		
		
		}

		/// <summary>
		/// Generates an XML characterization report using the parameters specified in the constructor.
		/// </summary>
		public void Characterize()
		{				
			this.OnStatusChanged(new StatusChangedArgs("Beginning characterization process."));
			string xmlTemplate= this.template;
						
			//xmlTable
			XmlTextReader xmlReader = new XmlTextReader(this.template);
			XmlTextWriter xmlWriter = new XmlTextWriter(this.outputFile, System.Text.Encoding.UTF8);
			xmlWriter.Formatting = Formatting.Indented;
			

			// Parse the file and display each of the nodes.
			while (xmlReader.Read()) 
			{				
				if (xmlReader.NodeType == System.Xml.XmlNodeType.EndElement)
				{
					xmlWriter.WriteEndElement();
					continue;
				}
				else if (xmlReader.NodeType == System.Xml.XmlNodeType.Whitespace)
				{ continue; }
				else if (xmlReader.NodeType == System.Xml.XmlNodeType.XmlDeclaration ||
					xmlReader.NodeType == System.Xml.XmlNodeType.ProcessingInstruction)
				{
					xmlWriter.WriteProcessingInstruction(xmlReader.Name, xmlReader.Value);
					continue;
				}

				switch (xmlReader.Name)
				{
					case "StudyArea":
						xmlWriter.WriteStartElement(xmlReader.Name);
						xmlWriter.WriteAttributeString("studyArea", this.studyArea);
						break;								
					case "Cell":					
						if (xmlReader.GetAttribute("cellType") == "FUNCTION")
						{
							xmlWriter.WriteStartElement(xmlReader.Name);							
							while (xmlReader.MoveToNextAttribute())
							{	
								if (xmlReader.Name != "data")
								{
									xmlWriter.WriteAttributeString(xmlReader.Name, xmlReader.Value);									
								}								
							}
							xmlReader.MoveToElement();
							string functionValue;
							try
							{
								functionValue = this.parseEmbeddedFunction(
									xmlReader.ReadInnerXml(), xmlReader);
							}
							catch
							{
								functionValue = "FUNCTION ERROR";
							}

							xmlWriter.WriteAttributeString("data", functionValue);
							xmlWriter.WriteEndElement();
							break;
						}
						else { goto default; }						
					case "Function":
						//Should not get here. <Function> should be handled 
						//internal to <Cell> by parseEmbeddedFunction
						break;  
					case "Parameter":
						//Should not get here. <Parameter> should be handled 
						//internal to <Cell> by parseEmbeddedFunction						
						break;
					case "CharacterizationTable":
						if (xmlReader.GetAttribute("isMultiTable") == "true")
						{
							xmlReader.MoveToElement();
							parseEmbeddedMultiTable(
								xmlReader.ReadOuterXml(), xmlReader, xmlWriter);							
							break;
						}
						else
						{ goto default; }						
					case "Row":
						if (xmlReader.GetAttribute("isMultiRow") == "true")
						{
							xmlReader.MoveToElement();							
							parseEmbeddedMultiRow(
								xmlReader.ReadOuterXml(), xmlReader, xmlWriter);							
							break;
						}							
						else
						{ goto default;	}
					case "Footer":
						goto default;
					default:
						xmlWriter.WriteStartElement(xmlReader.Name);
						while (xmlReader.MoveToNextAttribute())
						{	
							xmlWriter.WriteAttributeString(xmlReader.Name, xmlReader.Value);
						}						
						break;							
				}								
			}
			xmlWriter.Flush();
			xmlWriter.Close();
		}

		private string parseEmbeddedFunction(string xmlFragment, XmlReader xmlReader)
		{
			string functionName = "";
				
			ListDictionary rawParams = new ListDictionary();
			ListDictionary typedParams = new ListDictionary();
			XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlReader.NameTable);
									
			//Create the XmlParserContext.
			XmlParserContext context = new XmlParserContext(xmlReader.NameTable, nsmgr, null, XmlSpace.None);
			XmlTextReader functionReader = new XmlTextReader(xmlFragment, XmlNodeType.Element, context);
			while (functionReader.Read())
			{
				if (functionReader.NodeType==System.Xml.XmlNodeType.EndElement)
				{
					continue;
				}
				switch (functionReader.Name)
				{
					case "Function":
						functionName = functionReader.GetAttribute("functionName");							
						break;
					case "Parameter":
						rawParams.Add(functionReader.GetAttribute("parameterName"), 
							functionReader.GetAttribute("parameterValue"));
						break;
				}
			}			
			foreach (string paramName in rawParams.Keys)
			{
				string paramValue = (string)rawParams[paramName];
				#region Create typed parameters from raw xml data in <Parameter> elements
				switch (paramName)
				{
					case "MaxValue":
						double maxValue;
						maxValue = paramValue == "MAX" 
							? System.Double.MaxValue 
							: System.Convert.ToDouble(paramValue);
						typedParams.Add(paramName, maxValue);
						break;
					case "MinValue":
						double minValue;
						minValue = paramValue == "MIN" 
							? System.Double.MinValue 
							: System.Convert.ToDouble(paramValue);
						typedParams.Add(paramName, minValue);
						break;
					case "ScenarioID":
						int scenarioID;
						scenarioID = System.Convert.ToInt32(paramValue);
						typedParams.Add(paramName, scenarioID);
						break;
					case "TimeFrame":
						Enumerators.TimeFrames timeFrame;
						timeFrame = Enumerators.GetTimeFrameEnum(paramValue);
						typedParams.Add(paramName, timeFrame);						
						break;
					case "LandUse":
						Enumerators.LandUseTypes landUse;
						landUse = Enumerators.GetLandUseEnum(paramValue);
						typedParams.Add(paramName, landUse);
						break;
					case "LinkType":
						Enumerators.LinkTypes linkType;
						linkType = Enumerators.GetLinkTypeEnum(paramValue);
						typedParams.Add(paramName, linkType);
						break;
					case "IsFraction":
						bool isFraction;
						isFraction = Convert.ToBoolean(paramValue);
						typedParams.Add(paramName, isFraction);
						break;
					case "DiscoClass":
						string discoClass;
						discoClass = Convert.ToString(paramValue);
						typedParams.Add(paramName, discoClass);
						break;
					case "MinPipeSize":
						double minPipeSize;						
						minPipeSize = paramValue == "MIN" 
							? System.Double.MinValue 
							: System.Convert.ToDouble(paramValue);
						typedParams.Add(paramName, minPipeSize);
						break;
					case "MaxPipeSize":
						double maxPipeSize;
						maxPipeSize = paramValue == "MAX" 
							? System.Double.MaxValue 
							: System.Convert.ToDouble(paramValue);
						typedParams.Add(paramName, maxPipeSize);
						break;
					default:
						break;
				}
				#endregion
			}
			try
			{					
				#region Call function specifed by xml element <Function>	
				switch (functionName)
				{
					case "PipeSurchargeCount":							
						return this.handlePipeSurchargeCount(typedParams);																														
					case "QQRatio":
						return this.handleQQRatio(typedParams);
					case "QQRatioSummary":
						return this.handleQQRatioSummary(typedParams);
					case "AreaByLanduse":						
						return this.handleAreaByLanduse(typedParams);
					case "SewerBackupRisk":
						return this.handleSewerBackupRisk(typedParams);
					case "FalseBFRiskCount":
						return this.handleFalseBFRiskCount(typedParams);
					case "PipeSegmentCount":
						return this.handlePipeSegmentCount(typedParams);
					case "PipeSegmentLength":
						return this.handlePipeSegmentLength(typedParams);
					case "MinPipeDiameter":
						return this.handleMinPipeDiameter(typedParams);
					case "MaxPipeDiameter":
						return this.handleMaxPipeDiameter(typedParams);
					case "PipeDiameterRange":
						return this.handlePipeDiameterRange(typedParams);
					case "DSCCount":
						return this.handleDSCCount(typedParams);
					case "RoofDiscoCount":
						return this.handleRoofDiscoCount(typedParams);
					case "RoofDiscoCountByLanduse":
						return this.handleRoofDiscoCountByLanduse(typedParams);
					case "PipeSurchargeSummary":	
						return this.handlePipeSurchargeSummary(typedParams);
					case "PipeSurchargeLength":
						return this.handlePipeSurchargeLength(typedParams);
					case "PipeSurchargeTimeSummary":
						return this.handlePipeSurchargeTimeSummary(typedParams);
					case "PipeSurchargeTimeLength":
						return this.handlePipeSurchargeTimeLength(typedParams);
					case "CountByDiscoClass":
						return this.handleCountByDiscoClass(typedParams);
					case "UICCount":
						return this.handleUICCount(typedParams);
					case "UICForReviewCount":
						return this.handleUICForReviewCount(typedParams);
					default:
						return "UNKNOWN FUNCTION";
				}
				#endregion
			}
			catch (Exception e)
			{
				//If any of the embedded functions was invalid, had improper parameters,
				//or simply failed, write "INVALID" and keep trying.								
				this.OnStatusChanged(new StatusChangedArgs("Parse embedded XML function error! " + e.ToString()));				
				this.OnStatusChanged(new StatusChangedArgs("Function name: '" + functionName + "'. Parameters:"));				
				IDictionaryEnumerator paramEnum = typedParams.GetEnumerator();
				while (paramEnum.MoveNext())
				{
					this.OnStatusChanged(new StatusChangedArgs("	" + (string)paramEnum.Key + 
						"=" + Convert.ToString(paramEnum.Value) + 
						" (" + paramEnum.Value.GetType().ToString() + ")"));
				}				
				return "PARSE ERROR";
			}			
		}
		#region Handlers for embedded xml functions - All return string
		private string handlePipeSurchargeCount(ListDictionary typedParams)
		{
			LinkHydraulics linkHyd;
			int count;
			bool isFraction = (bool)typedParams["IsFraction"];
			linkHyd = (LinkHydraulics)linkResults[(int)typedParams["ScenarioID"]];
			count = linkHyd.PipeSurchargeCount(
				(double)typedParams["MinValue"],
				(double)typedParams["MaxValue"]);
			return isFraction
				? Convert.ToString(
				Math.Round((double)count / this.charLinks.Count, 2) * 100 + "%") 
				: Convert.ToString(count);
		}
		private string handleQQRatio(ListDictionary typedParams)
		{
			LinkHydraulics linkHyd;
			int count;
			bool isFraction = (bool)typedParams["IsFraction"];
			linkHyd = (LinkHydraulics)linkResults[(int)typedParams["ScenarioID"]];												
			count = linkHyd.CountByQQRatio(
				(double)typedParams["MinValue"],
				(double)typedParams["MaxValue"]);
			return isFraction
				? Convert.ToString(
				Math.Round((double)count / this.charLinks.Count, 2) * 100 + "%")
				: Convert.ToString(count);	
		}
		private string handleQQRatioSummary(ListDictionary typedParams)
		{
			LinkHydraulics linkHyd;
			double length;			
			int count;			
			linkHyd = (LinkHydraulics)linkResults[(int)typedParams["ScenarioID"]];				
			length = Math.Round(linkHyd.CountByQQRatioLength(
				(double)typedParams["MinValue"],
				(double)typedParams["MaxValue"],
				(double)typedParams["MinPipeSize"],
				(double)typedParams["MaxPipeSize"]) / 5280.0, 1);
			count = linkHyd.CountByQQRatio(
				(double)typedParams["MinValue"],
				(double)typedParams["MaxValue"],
				(double)typedParams["MinPipeSize"],
				(double)typedParams["MaxPipeSize"]);			
			return Convert.ToString(length) + " (" 
				+ Convert.ToString(count) + ")";			
		}
		private string handleQQRatioLength(ListDictionary typedParams)
		{
			LinkHydraulics linkHyd;
			int count;
			bool isFraction = (bool)typedParams["IsFraction"];
			linkHyd = (LinkHydraulics)linkResults[(int)typedParams["ScenarioID"]];
			count = linkHyd.CountByQQRatio(
				(double)typedParams["MinValue"],
				(double)typedParams["MaxValue"],
				(double)typedParams["MinPipeSize"],
				(double)typedParams["MaxPipeSize"]);
			return isFraction
				? Convert.ToString(
				Math.Round((double)count / this.charLinks.Count, 2) * 100 + "%")
				: Convert.ToString(count);		
		}
		private string handleAreaByLanduse(ListDictionary typedParams)
		{
			int count = 0;
			bool isFraction = (bool)typedParams["IsFraction"];
			Enumerators.LandUseTypes landUse;
			landUse = (Enumerators.LandUseTypes)typedParams["LandUse"];
			if (landUse != Enumerators.LandUseTypes.OTH)
			{
				count = this.charDSCs.AreaByLandUse(landUse,
					(Enumerators.TimeFrames)typedParams["TimeFrame"]);
			}
			else
			{
				int nonOTHCount = 0;
				foreach (Enumerators.LandUseTypes lu in Enum.GetValues(typeof(Enumerators.LandUseTypes)))
				{					
					nonOTHCount += this.charDSCs.AreaByLandUse(lu,
						(Enumerators.TimeFrames)typedParams["TimeFrame"]);
				}
				count = this.charDSCs.Values.Count - nonOTHCount;
				return "Not Implemented";
			}
			return isFraction
				? Convert.ToString(
				Math.Round((double)count / this.charDSCs.Count, 2) * 100 + "%") 
				: Convert.ToString(count);
		}
		private string handleSewerBackupRisk(ListDictionary typedParams)
		{
			DSCHydraulics dscHyd;	
			int count;
			bool isFraction = (bool)typedParams["IsFraction"];
			dscHyd = (DSCHydraulics)dscResults[(int)typedParams["ScenarioID"]];				
			count =	dscHyd.CountBySewerBackupRisk(							
				(double)typedParams["MinValue"],
				(double)typedParams["MaxValue"]);	
			return isFraction
				? Convert.ToString(
				Math.Round((double)count / this.charDSCs.Count, 2) * 100 + "%") 
				: Convert.ToString(count);
		}
		private string handleFalseBFRiskCount(ListDictionary typedParams)
		{
			bool isFraction = (bool)typedParams["IsFraction"];
			int count;
			count = this.charDSCs.CountByFalseBFRisk();
			return isFraction
				? Convert.ToString(
				Math.Round((double)count / this.charDSCs.Count, 2) * 100 + "%")
				: Convert.ToString(count);
		}
				 

		private string handlePipeSegmentCount(ListDictionary typedParams)
		{
			int count;
			bool isFraction = (bool)typedParams["IsFraction"];
			count = this.charLinks.PipeSegmentCount(
				(Enumerators.LinkTypes)typedParams["LinkType"]);
			return isFraction
				? Convert.ToString(
				Math.Round((double)count / this.charLinks.Count, 2) * 100 + "%") 
				: Convert.ToString(count);
		}
		private string handlePipeSegmentLength(ListDictionary typedParams)
		{
			int count;
			bool isFraction = (bool)typedParams["IsFraction"];
			count = this.charLinks.PipeSegmentLength(
				(Enumerators.LinkTypes)typedParams["LinkType"]) / 5280;
			return isFraction
				? Convert.ToString(
				Math.Round((double)count / this.charLinks.Count, 2) * 100 + "%") 
				: Convert.ToString(count);
		}
		
		private string handleMinPipeDiameter(ListDictionary typedParams)
		{
			return Convert.ToString(
				this.charLinks.GetMinPipeDiam(
				(Enumerators.LinkTypes)typedParams["LinkType"]));
		}

		private string handleMaxPipeDiameter(ListDictionary typedParams)
		{
			return Convert.ToString(
				this.charLinks.GetMaxPipeDiam(
				(Enumerators.LinkTypes)typedParams["LinkType"])) + "\"";
		}

		private string handlePipeDiameterRange(ListDictionary typedParams)
		{
			return 
				Convert.ToString(charLinks.GetMinPipeDiam(
				(Enumerators.LinkTypes)typedParams["LinkType"])) + 
				"\" to " +
				Convert.ToString(charLinks.GetMaxPipeDiam(
				(Enumerators.LinkTypes)typedParams["LinkType"]) + "\"");
		}

		private string handleDSCCount(ListDictionary typedParams)
		{
			return Convert.ToString(this.charDSCs.Count);
		}

		private string handleRoofDiscoCount(ListDictionary typedParams)
		{
			int count;
			bool isFraction = (bool)typedParams["IsFraction"];
			count = this.charDSCs.RoofDiscoCount(
				(double)typedParams["MinValue"],
				(double)typedParams["MaxValue"]);
			return isFraction
				? Convert.ToString(
				Math.Round((double)count / this.charDSCs.Count, 2) * 100 + "%") 
				: Convert.ToString(count);
		}

		private string handleRoofDiscoCountByLanduse(ListDictionary typedParams)
		{
			int count;
			bool isFraction = (bool)typedParams["IsFraction"];
			Enumerators.LandUseTypes landUse;
			landUse = (Enumerators.LandUseTypes)typedParams["LandUse"];
			if (landUse != Enumerators.LandUseTypes.OTH)
			{
				if ( this.charDSCs.CountByLandUse(landUse,
					(Enumerators.TimeFrames)typedParams["TimeFrame"]) == 0)
				{
					return "N/A";
				}
				count = this.charDSCs.RoofDiscoCountByLandUse(landUse,
					(Enumerators.TimeFrames)typedParams["TimeFrame"]);
				return isFraction
					? Convert.ToString(
					Math.Round((double)count / 
					this.charDSCs.CountByLandUse(
					(Enumerators.LandUseTypes)typedParams["LandUse"],
					(Enumerators.TimeFrames)typedParams["TimeFrame"]) ,
					2) * 100 + "%") 
					: Convert.ToString(count);
			}
			else
			{
				int nonOTHCount = 0;
				int nonOTHDiscoCount = 0;
				foreach (Enumerators.LandUseTypes lu in Enum.GetValues(typeof(Enumerators.LandUseTypes)))
				{	
					nonOTHDiscoCount += this.charDSCs.RoofDiscoCountByLandUse(lu,
						(Enumerators.TimeFrames)typedParams["TimeFrame"]);	
					nonOTHCount += this.charDSCs.CountByLandUse(lu, 
						(Enumerators.TimeFrames)typedParams["TimeFrame"]);
				}
				
				if (nonOTHCount == 0) return "N/A";
				return isFraction
					? Convert.ToString(
					Math.Round((double)nonOTHDiscoCount / 
					nonOTHCount,
					2) * 100 + "%") 
					: Convert.ToString(nonOTHDiscoCount);
			}
			
		}

		private string handleCountByDiscoClass(ListDictionary typedParams)
		{
			string discoClass = (string)typedParams["DiscoClass"];
			int count = this.charDSCs.CountByDiscoClass(discoClass);
			bool isFraction = (bool)typedParams["IsFraction"];
			return isFraction
				? Convert.ToString(
				Math.Round((double)count / this.charDSCs.Count, 2) * 100 + "%")
				: Convert.ToString(count);
		}
		private string handlePipeSurchargeSummary(ListDictionary typedParams)
		{
			LinkHydraulics linkHyd;
			int count;
			double length;
			linkHyd = (LinkHydraulics)linkResults[(int)typedParams["ScenarioID"]];
			length = Math.Round(linkHyd.PipeSurchargeLength(
				(double)typedParams["MinValue"],
				(double)typedParams["MaxValue"]) / 5280.0, 1);						
			count = (int)linkHyd.PipeSurchargeCount(
				(double)typedParams["MinValue"],
				(double)typedParams["MaxValue"]);
			return Convert.ToString(length) + " (" 
				+ Convert.ToString(count) + ")";
		}

		private string handlePipeSurchargeLength(ListDictionary typedParams)
		{
			LinkHydraulics linkHyd;
			double surchargeLength;
			double totalLength;

			bool isFraction = (bool)typedParams["IsFraction"];
			
			linkHyd = (LinkHydraulics)linkResults[(int)typedParams["ScenarioID"]];
			surchargeLength = linkHyd.PipeSurchargeLength(
				(double)typedParams["MinValue"],
				(double)typedParams["MaxValue"]) / 5280.0;
			totalLength = linkHyd.PipeSurchargeLength(
				System.Double.MinValue, System.Double.MaxValue) / 5280.0;

			return isFraction
				? Convert.ToString(
				Math.Round(
				surchargeLength / 
				(charLinks.PipeSegmentLength() / 5280.0)
				, 2) * 100) + "%"
				: Convert.ToString(Math.Round(surchargeLength / 5280.0, 2));	
		}

		private string handlePipeSurchargeTimeSummary(ListDictionary typedParams)
		{
			double length = 0;
			int count = 0;
			double min;
			double max;
			min = (double)typedParams["MinValue"];
			max = (double)typedParams["MaxValue"];

			NodeHydraulics nodeHyd;
			nodeHyd = (NodeHydraulics)nodeResults[(int)typedParams["ScenarioID"]];				
			
			foreach (Link l in this.charLinks.Values)
			{
				double usSurchargeTime = 0;
				double dsSurchargeTime = 0;
				if (nodeHyd.Contains(l.USNodeName))
				{
					usSurchargeTime = nodeHyd[l.USNodeName].SurchargeTime;
				}
				if (nodeHyd.Contains(l.DSNodeName))
				{
					dsSurchargeTime = nodeHyd[l.DSNodeName].SurchargeTime;
				}
				if ( (usSurchargeTime >= min && usSurchargeTime <= max) ||
					 (dsSurchargeTime >= min && dsSurchargeTime <= max) )
				{
					length += l.Length;
					count++;
				}				
			}
			return Convert.ToString(Math.Round(length / 5280, 1)) + " (" + Convert.ToString(count) + ")";
		}

		private string handlePipeSurchargeTimeLength(ListDictionary typedParams)
		{
			double length = 0;
			
			double min;
			double max;
			min = (double)typedParams["MinValue"];
			max = (double)typedParams["MaxValue"];

			bool isFraction = (bool)typedParams["IsFraction"];

			NodeHydraulics nodeHyd;
			nodeHyd = (NodeHydraulics)nodeResults[(int)typedParams["ScenarioID"]];	
			
			foreach (Link l in this.charLinks.Values)
			{
				double usSurchargeTime = 0;
				double dsSurchargeTime = 0;
				if (nodeHyd.Contains(l.USNodeName))
				{
					usSurchargeTime = nodeHyd[l.USNodeName].SurchargeTime;
				}
				if (nodeHyd.Contains(l.DSNodeName))
				{
					dsSurchargeTime = nodeHyd[l.DSNodeName].SurchargeTime;
				}
				if ( (usSurchargeTime >= min && usSurchargeTime <= max) ||
					(dsSurchargeTime >= min && dsSurchargeTime <= max) )
				{
					length += l.Length;					
				}				
			}
			return isFraction
				? Convert.ToString(
				Math.Round(( (double)length / this.charLinks.PipeSegmentLength()) , 2) * 100 + "%")
				: Convert.ToString(length / 5280);		
		}

		private string handleUICCount(ListDictionary typedParams)
		{
			//TODO: Move some of this AO code to a utils library...
			//Open a .lyr file as a GxFile, which can QI to GxLayer...
			ESRI.ArcGIS.Catalog.GxLayer sscGxLayer;
			sscGxLayer = new ESRI.ArcGIS.Catalog.GxLayerClass();
			ESRI.ArcGIS.Catalog.GxFile sscGxFile;
			sscGxFile = (ESRI.ArcGIS.Catalog.GxFile)sscGxLayer;
			sscGxFile.Path = this.mstSurfSC_FC;
			//Which can QI to an IFeatureLayer...
			ESRI.ArcGIS.Carto.IFeatureLayer sscFL;
			sscFL = (ESRI.ArcGIS.Carto.IFeatureLayer)sscGxLayer.Layer;			
			//Which can QI to an IFeatureSelection, which will allow 
			//for selecting individual objects...
			ESRI.ArcGIS.Carto.IFeatureSelection sscFeatureSelection;
			sscFeatureSelection = (ESRI.ArcGIS.Carto.IFeatureSelection)sscFL;
			//And IFeatureLayer can also provide an IFeatureCursor for iterating...
			ESRI.ArcGIS.Geodatabase.IFeatureCursor sscCursor;		
			sscCursor = sscFL.Search(null, false);

			ESRI.ArcGIS.Geodatabase.IFeature sscFeature;
			sscFeature = sscCursor.NextFeature();
			while (sscFeature != null)
			{
				string ngto;
				ngto = Convert.ToString(
					sscFeature.get_Value(sscFeature.Fields.FindField("NGTO_EX")));
				if (this.charNodes.Contains(ngto))
				{										
					sscFeatureSelection.SelectionSet.Add(sscFeature.OID);					
				}
				sscFeature = sscCursor.NextFeature();				
			}

			//Gnarly ArcObjects code... the intent is to generate an IEnumGeometry
			//containing all the geometries from the feature selection created above.
			//The IEnumGeometry contains a collection of geometries and can be used 
			//to output a single unified geometry via 
			//GeometryFactory.CreateGeometryFromEnumerator.
			ESRI.ArcGIS.Geometry.IEnumGeometry enumGeom;
			ESRI.ArcGIS.Geodatabase.IEnumGeometryBind enumGeomBind;
			enumGeom = new ESRI.ArcGIS.Geodatabase.EnumFeatureGeometryClass();
			enumGeomBind = (ESRI.ArcGIS.Geodatabase.IEnumGeometryBind)enumGeom;
			enumGeomBind.BindGeometrySource(null, sscFeatureSelection.SelectionSet);		
			
			ESRI.ArcGIS.Geometry.IGeometryFactory geomFactory;
			geomFactory = new ESRI.ArcGIS.Geometry.GeometryEnvironmentClass();
			
			ESRI.ArcGIS.Geometry.IGeometry geometryFilter;
			geometryFilter = geomFactory.CreateGeometryFromEnumerator(enumGeom);
			
			//Now that we have a Geometry representing the selected SSC objects
			//we can generate a spatial filter for use in spatial selection.
			ESRI.ArcGIS.Geodatabase.ISpatialFilter sscFilter;
			sscFilter = new ESRI.ArcGIS.Geodatabase.SpatialFilterClass();
			sscFilter.Geometry = geometryFilter;
			sscFilter.GeometryField = sscFL.FeatureClass.ShapeFieldName;
			sscFilter.SpatialRel = ESRI.ArcGIS.Geodatabase.esriSpatialRelEnum.esriSpatialRelContains;

			//Finally, create the UIC layer...
			ESRI.ArcGIS.Catalog.GxLayer uicGxLayer;
			uicGxLayer = new ESRI.ArcGIS.Catalog.GxLayerClass();
			ESRI.ArcGIS.Catalog.GxFile uicGxFile;
			uicGxFile = (ESRI.ArcGIS.Catalog.GxFile)uicGxLayer;
			uicGxFile.Path = this.uicDB;

			ESRI.ArcGIS.Carto.IFeatureLayer uicFL;
			uicFL = (ESRI.ArcGIS.Carto.IFeatureLayer)uicGxLayer.Layer;
			//.. and define an IFeatureSelection...
			ESRI.ArcGIS.Carto.IFeatureSelection uicFeatureSelection;
			uicFeatureSelection = (ESRI.ArcGIS.Carto.IFeatureSelection)uicFL;
			//...and select from it using the spatial filter
			uicFeatureSelection.SelectFeatures(sscFilter, 
				ESRI.ArcGIS.Carto.esriSelectionResultEnum.esriSelectionResultNew, false);
			//...and return the count of the UIC selection.
			return Convert.ToString(uicFeatureSelection.SelectionSet.Count);
			//TODO: Take some tylenol and put this in a library.
		}
		private string handleUICForReviewCount(ListDictionary typedParams)
		{
			//Open a .lyr file as a GxFile, which can QI to GxLayer...
			ESRI.ArcGIS.Catalog.GxLayer sscGxLayer;
			sscGxLayer = new ESRI.ArcGIS.Catalog.GxLayerClass();
			ESRI.ArcGIS.Catalog.GxFile sscGxFile;
			sscGxFile = (ESRI.ArcGIS.Catalog.GxFile)sscGxLayer;
			sscGxFile.Path = this.mstSurfSC_FC;
			//Which can QI to an IFeatureLayer...
			ESRI.ArcGIS.Carto.IFeatureLayer sscFL;
			sscFL = (ESRI.ArcGIS.Carto.IFeatureLayer)sscGxLayer.Layer;			
			//Which can QI to an IFeatureSelection, which will allow 
			//for selecting individual objects...
			ESRI.ArcGIS.Carto.IFeatureSelection sscFeatureSelection;
			sscFeatureSelection = (ESRI.ArcGIS.Carto.IFeatureSelection)sscFL;
			//And IFeatureLayer can also provide an IFeatureCursor for iterating...
			ESRI.ArcGIS.Geodatabase.IFeatureCursor sscCursor;		
			sscCursor = sscFL.Search(null, false);

			ESRI.ArcGIS.Geodatabase.IFeature sscFeature;
			sscFeature = sscCursor.NextFeature();
			while (sscFeature != null)
			{
				string ngto;
				ngto = Convert.ToString(
					sscFeature.get_Value(sscFeature.Fields.FindField("NGTO_EX")));
				if (this.charNodes.Contains(ngto))
				{										
					sscFeatureSelection.SelectionSet.Add(sscFeature.OID);					
				}
				sscFeature = sscCursor.NextFeature();				
			}

			//Gnarly ArcObjects code... the intent is to generate an IEnumGeometry
			//containing all the geometries from the feature selection created above.
			//The IEnumGeometry contains a collection of geometries and can be used 
			//to output a single unified geometry via 
			//GeometryFactory.CreateGeometryFromEnumerator.
			ESRI.ArcGIS.Geometry.IEnumGeometry enumGeom;
			ESRI.ArcGIS.Geodatabase.IEnumGeometryBind enumGeomBind;
			enumGeom = new ESRI.ArcGIS.Geodatabase.EnumFeatureGeometryClass();
			enumGeomBind = (ESRI.ArcGIS.Geodatabase.IEnumGeometryBind)enumGeom;
			enumGeomBind.BindGeometrySource(null, sscFeatureSelection.SelectionSet);		
			
			ESRI.ArcGIS.Geometry.IGeometryFactory geomFactory;
			geomFactory = new ESRI.ArcGIS.Geometry.GeometryEnvironmentClass();
			
			ESRI.ArcGIS.Geometry.IGeometry geometryFilter;
			geometryFilter = geomFactory.CreateGeometryFromEnumerator(enumGeom);
			
			//Now that we have a Geometry representing the selected SSC objects
			//we can generate a spatial filter for use in spatial selection.
			ESRI.ArcGIS.Geodatabase.ISpatialFilter sscFilter;
			sscFilter = new ESRI.ArcGIS.Geodatabase.SpatialFilterClass();
			sscFilter.Geometry = geometryFilter;
			sscFilter.GeometryField = sscFL.FeatureClass.ShapeFieldName;
			sscFilter.SpatialRel = ESRI.ArcGIS.Geodatabase.esriSpatialRelEnum.esriSpatialRelContains;

			//Finally, create the UIC layer...
			ESRI.ArcGIS.Catalog.GxLayer uicGxLayer;
			uicGxLayer = new ESRI.ArcGIS.Catalog.GxLayerClass();
			ESRI.ArcGIS.Catalog.GxFile uicGxFile;
			uicGxFile = (ESRI.ArcGIS.Catalog.GxFile)uicGxLayer;
			uicGxFile.Path = this.uicDB;

			ESRI.ArcGIS.Carto.IFeatureLayer uicFL;
			uicFL = (ESRI.ArcGIS.Carto.IFeatureLayer)uicGxLayer.Layer;
			//.. and define an IFeatureSelection...
			ESRI.ArcGIS.Carto.IFeatureSelection uicFeatureSelection;
			uicFeatureSelection = (ESRI.ArcGIS.Carto.IFeatureSelection)uicFL;
			//...and select from it using the spatial filter
			uicFeatureSelection.SelectFeatures(sscFilter, 
				ESRI.ArcGIS.Carto.esriSelectionResultEnum.esriSelectionResultNew, false);
			
			int count = 0;
			//Iterate through the UIC selection to find UICs matching the review criteria.
			ESRI.ArcGIS.Geodatabase.ICursor uicCursor;
			uicFeatureSelection.SelectionSet.Search(sscFilter, false, out uicCursor);
			ESRI.ArcGIS.Geodatabase.IRow uicRow;
			uicRow = uicCursor.NextRow();
			while (uicRow != null)
			{	
				double gw_sepdist;
				double nr_wellmin;
				double in_tot;
				gw_sepdist = Convert.ToDouble(uicRow.get_Value(uicRow.Fields.FindField("GW_SEPDIST")));
				nr_wellmin = Convert.ToDouble(uicRow.get_Value(uicRow.Fields.FindField("nr_wellmin")));
				in_tot = Convert.ToDouble(uicRow.get_Value(uicRow.Fields.FindField("in_tot")));
				if (nr_wellmin <= 500 || in_tot == 1 || gw_sepdist <= 10)
				{
					count++;
				}
				uicRow = uicCursor.NextRow();
			}
			
			bool isFraction = (bool)typedParams["IsFraction"];
			return isFraction ? Convert.ToString(
				Math.Round( count / 
				(uicFeatureSelection.SelectionSet.Count + System.Double.Epsilon),
				2 ) * 100) + "%"
				: Convert.ToString(count);
			
			
		}

		#endregion

		private void parseEmbeddedMultiRow(string xmlFragment, XmlReader xmlReader, XmlTextWriter xmlWriter)
		{										
			string multiRowKey = "";			

			XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlReader.NameTable);									
			//Create the XmlParserContext.
			XmlParserContext context = new XmlParserContext(xmlReader.NameTable, nsmgr, null, XmlSpace.None);
			XmlTextReader functionReader = new XmlTextReader(xmlFragment, XmlNodeType.Element, context);

			while (functionReader.Read())
			{
				if (functionReader.Name == "Row")
				{
					multiRowKey = functionReader.GetAttribute("multiRowKey");
					break;
				}
			}			
			switch (multiRowKey)
			{
				case "DivNodeList":
					handleDiversionMultiRow(xmlFragment, functionReader, xmlWriter);										
					break;
				default:
					break;
			}			
			//return returnFragment;
		}

		private void handleDiversionMultiRow(string xmlFragment, XmlTextReader functionReader, XmlTextWriter xmlWriter)
		{
			XmlNamespaceManager nsmgr = new XmlNamespaceManager(functionReader.NameTable);									
			//Create the XmlParserContext.
			XmlParserContext context = new XmlParserContext(functionReader.NameTable, nsmgr, null, XmlSpace.None);			

			DiversionStructures divStructs;
			divStructs = new DiversionStructures(charNodes);
						
			foreach (DiversionStructure div in divStructs.Values)
			{					
				functionReader = new XmlTextReader(xmlFragment, XmlNodeType.Element, context);				
				xmlWriter.WriteStartElement("Row");
				while (functionReader.Read())
				{
					if (functionReader.Name == "Cell" && 
						functionReader.NodeType != System.Xml.XmlNodeType.EndElement)
					{								
						xmlWriter.WriteStartElement("Cell");
						while (functionReader.MoveToNextAttribute())
						{
							if (functionReader.Name != "data")
							{
								xmlWriter.WriteAttributeString(functionReader.Name, functionReader.Value);
							}
						}
					}
					else if (functionReader.Name == "Function" && 
						functionReader.NodeType != System.Xml.XmlNodeType.EndElement)
					{
						string functionValue;
						string functionName = functionReader.GetAttribute("functionName");
						switch (functionName)
						{
							case "DivLocation":
								functionValue = div.Address;
								break;
							case "DivNodeID":
								functionValue = div.NodeName;
								break;								
							case "DivID":
								functionValue = div.DiversionName;
								break;
							case "DivNodeType":
								functionValue = "";
								break;
							case "DivStatus":
								functionValue = div.Status;
								break;
							case "DivOutfallID":
								if (div.Outfall == String.Empty)
								{
									functionValue = String.Empty;
								}
								else if (Char.IsDigit(div.Outfall[0]))
								{
									functionValue = "OF " + div.Outfall;									
								}
								else
								{
									functionValue = div.Outfall;
								}
								break;
							default:
								functionValue = "INVALID DivNodeList FUNCTION";
								break;

						}
						xmlWriter.WriteAttributeString("data", functionValue);
						xmlWriter.WriteEndElement();
					}
				}							
				xmlWriter.WriteEndElement();				
			}			
		}
		
		
		private void parseEmbeddedMultiTable(string xmlFragment, XmlTextReader xmlReader, XmlTextWriter xmlWriter)
		{
			string multiTableKey = "";			

			XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmlReader.NameTable);									
			//Create the XmlParserContext.
			XmlParserContext context = new XmlParserContext(xmlReader.NameTable, nsmgr, null, XmlSpace.None);
			XmlTextReader functionReader = new XmlTextReader(xmlFragment, XmlNodeType.Element, context);

			while (functionReader.Read())
			{
				if (functionReader.Name == "CharacterizationTable")
				{
					multiTableKey = functionReader.GetAttribute("multiTableKey");
					break;
				}
			}			
			switch (multiTableKey)
			{
				case "PumpStationList":
					handlePumpStationMultiTable(functionReader.ReadOuterXml(), functionReader, xmlWriter);										
					break;
				default:
					break;
			}			
			//return returnFragment;
		}
		
		private void handlePumpStationMultiTable(string xmlFragment, XmlTextReader functionReader, XmlTextWriter xmlWriter)
		{
			XmlNamespaceManager nsmgr = new XmlNamespaceManager(functionReader.NameTable);									
			//Create the XmlParserContext.
			XmlParserContext context = new XmlParserContext(functionReader.NameTable, nsmgr, null, XmlSpace.None);			

			PumpStations pumpStations;
			pumpStations = new PumpStations(charNodes, this.psDB);
	
			int psCount = pumpStations.Count;
			int psNum = 1;
			if (psCount == 0) //TODO: Write "This study area has no pump stations"
			{ 
				functionReader = new XmlTextReader(xmlFragment, XmlNodeType.Element, context);			
				functionReader.MoveToContent();
				string colCount = functionReader.GetAttribute("columnCount");
				xmlWriter.WriteStartElement("CharacterizationTable");				
				xmlWriter.WriteAttributes(functionReader, true);
				xmlWriter.WriteStartElement("Row");
				xmlWriter.WriteStartElement("Cell");
				xmlWriter.WriteAttributeString("cellType", "COLUMNNAME");
				xmlWriter.WriteAttributeString("data", "There are no pump stations present in this study area.");							
				xmlWriter.WriteAttributeString("colSpan", colCount);
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
				return;
			}
			foreach (PumpStation ps in pumpStations.Values)
			{	
				string tableQualifier = "";				
				if (psCount != 1)
				{
					char tableID = (char)(psNum+96);
					tableQualifier=Convert.ToString(tableID);
					psNum++;
				}
				functionReader = new XmlTextReader(xmlFragment, XmlNodeType.Element, context);				
				while (functionReader.Read())
				{
					if (functionReader.NodeType == System.Xml.XmlNodeType.Whitespace)
					{ continue; }
					else if (functionReader.NodeType == System.Xml.XmlNodeType.EndElement)
					{
						xmlWriter.WriteEndElement();
					}
					else if (functionReader.Name == "CharacterizationTable" &&
						functionReader.NodeType != System.Xml.XmlNodeType.EndElement)
					{
						xmlWriter.WriteStartElement("CharacterizationTable");
						while (functionReader.MoveToNextAttribute())
						{
							if (functionReader.Name == "tableNumber")
							{
								xmlWriter.WriteAttributeString(functionReader.Name, functionReader.Value + tableQualifier);
							}
							else
							{
								xmlWriter.WriteAttributeString(functionReader.Name, functionReader.Value);
							}
						}

					}
					else if (functionReader.Name == "Row" && functionReader.GetAttribute("isMultiRow") == "true" &&
						functionReader.NodeType != System.Xml.XmlNodeType.EndElement)
					{																				
						switch(functionReader.GetAttribute("multiRowKey"))
						{
							case "PSWetWellList":
								handleWetWellMultiRow(ps, functionReader.ReadInnerXml(), functionReader, xmlWriter);
								break;
							case "PSForceMainList":								
								handleForceMainMultiRow(ps, functionReader.ReadInnerXml(), functionReader, xmlWriter);
								break;
							case "PSPumpList":								
								handlePumpMultiRow(ps, functionReader.ReadInnerXml(), functionReader, xmlWriter);
								break;
							default:
								break;
						}						
						//functionReader.Skip();
					}
					else if (functionReader.Name == "Cell" && functionReader.GetAttribute("cellType") == "FUNCTION" &&
						functionReader.NodeType != System.Xml.XmlNodeType.EndElement)
					{
						xmlWriter.WriteStartElement("Cell");													
						while (functionReader.MoveToNextAttribute())
						{	
							if (functionReader.Name != "data")
							{
								xmlWriter.WriteAttributeString(functionReader.Name, functionReader.Value);									
							}								
						}
					}
					else if (functionReader.Name == "Function" && 
						functionReader.NodeType != System.Xml.XmlNodeType.EndElement)
					{
						string functionValue;
						switch(functionReader.GetAttribute("functionName"))
						{
							case "PSName":
								functionValue = ps.Name + " Pump Station";
								break;
							case "PSType":
								functionValue = ps.Type;
								break;
							case "PSLocation":
								functionValue = ps.Address;
								break;
							case "PSFirmCapacity":
								functionValue = Convert.ToString(ps.FirmCapacity) + " gpm";
								break;
							case "PSFullCapacity":
								functionValue = Convert.ToString(ps.FullCapacity) + " gpm";
								break;
							default:
								functionValue = "";
								break;
						}
						xmlWriter.WriteAttributeString("data", functionValue);
						functionReader.Skip();										
					}
					else 
					{
						xmlWriter.WriteStartElement(functionReader.Name);
						while (functionReader.MoveToNextAttribute())
						{
							xmlWriter.WriteAttributeString(functionReader.Name, functionReader.Value);
						}												
					}

				}															
			}			
		}
		
		private void handleWetWellMultiRow(PumpStation ps, string xmlFragment, XmlTextReader functionReader, XmlTextWriter xmlWriter)
		{						
			XmlNamespaceManager nsmgr = new XmlNamespaceManager(functionReader.NameTable);									
			//Create the XmlParserContext.
			XmlParserContext context = new XmlParserContext(functionReader.NameTable, nsmgr, null, XmlSpace.None);			
									
			foreach (WetWell ww in ps.WetWells)
			{					
				functionReader = new XmlTextReader(xmlFragment, XmlNodeType.Element, context);				
				xmlWriter.WriteStartElement("Row");

				while (functionReader.Read())
				{
					if (functionReader.Name == "Cell" && 
						functionReader.NodeType != System.Xml.XmlNodeType.EndElement)
					{								
						xmlWriter.WriteStartElement("Cell");
						while (functionReader.MoveToNextAttribute())
						{
							if (functionReader.Name != "data")
							{
								xmlWriter.WriteAttributeString(functionReader.Name, functionReader.Value);
							}
						}
					}
					else if (functionReader.Name == "Function" && 
						functionReader.NodeType != System.Xml.XmlNodeType.EndElement)
					{
						string functionValue;
						string functionName = functionReader.GetAttribute("functionName");
						switch (functionName)
						{
							case "PSWetWellNum":
								if (ps.WetWells.Count > 1)
								{
									functionValue = Convert.ToString(ww.Number);
								}
								else
								{
									functionValue = "";
								}
								break;
							case "PSWetWellDimensions":
								if (ww.Shape == "Rectangular")
								{
									functionValue = ww.Shape + " (" 
										+ ww.Dimension1 + " ft x " 
										+ ww.Dimension2 + " ft)";
								}
								else if (ww.Shape == "Circular")
								{
									functionValue = ww.Shape 
										+ " (r = " + ww.Dimension1 + " ft)";
								}
								else
								{
									functionValue = "Irregular";
								}
								break;								
							case "PSWetWellVolume":
								functionValue = Convert.ToString(ww.Volume) + " gal/ft";
								break;
							case "PSWetWellBypass":
								functionValue = "Bypass text";
								break;							
							default:
								functionValue = "INVALID WetWell FUNCTION";
								break;

						}
						xmlWriter.WriteAttributeString("data", functionValue);						
						functionReader.Skip();
					}
					else if (functionReader.NodeType == System.Xml.XmlNodeType.EndElement)
					{
						xmlWriter.WriteEndElement();
					}
				}
				xmlWriter.WriteEndElement();
			}				
		}
		private void handleForceMainMultiRow(PumpStation ps, string xmlFragment, XmlTextReader functionReader, XmlTextWriter xmlWriter)
		{
			XmlNamespaceManager nsmgr = new XmlNamespaceManager(functionReader.NameTable);									
			//Create the XmlParserContext.
			XmlParserContext context = new XmlParserContext(functionReader.NameTable, nsmgr, null, XmlSpace.None);
						
			int fmNum = 1;
			foreach (ForceMain fm in ps.ForceMains)
			{	
				functionReader = new XmlTextReader(xmlFragment, XmlNodeType.Element, context);				
				xmlWriter.WriteStartElement("Row");

				while (functionReader.Read())
				{
					if (functionReader.Name == "Cell" && 
						functionReader.NodeType != System.Xml.XmlNodeType.EndElement)
					{								
						xmlWriter.WriteStartElement("Cell");
						while (functionReader.MoveToNextAttribute())
						{
							if (functionReader.Name != "data")
							{
								xmlWriter.WriteAttributeString(functionReader.Name, functionReader.Value);
							}
						}
					}
					else if (functionReader.Name == "Function" && 
						functionReader.NodeType != System.Xml.XmlNodeType.EndElement)
					{
						string functionValue;
						string functionName = functionReader.GetAttribute("functionName");
						switch (functionName)
						{
							case "PSForceMainID":
								if (ps.ForceMains.Count > 1)
								{
									functionValue = Convert.ToString(fmNum);
									fmNum++;
								}
								else
								{
									functionValue = "";
								}
								break;
							case "PSForceMainDiameter":
								if (charLinks.FindByMLinkID(fm.MLinkID) != null)
								{
									functionValue = Convert.ToString(
										charLinks.FindByMLinkID(fm.MLinkID).Diameter) + " in";
								}
								else
								{
									functionValue = "Unknown";
								}														
								break;								
							case "PSForceMainLength":
								functionValue = Convert.ToString(fm.Length) + " ft";								
								break;
							case "PSForceMainHead":
								functionValue = Convert.ToString(fm.StaticHead) + " ft";								
								break;					
							default:
								functionValue = "INVALID ForceMain FUNCTION";
								break;

						}
						xmlWriter.WriteAttributeString("data", functionValue);					
						functionReader.Skip();
					}
					else if (functionReader.NodeType == System.Xml.XmlNodeType.EndElement)
					{
						xmlWriter.WriteEndElement();
					}
				}
				xmlWriter.WriteEndElement();											
			}
		}
		private void handlePumpMultiRow(PumpStation ps, string xmlFragment, XmlTextReader functionReader, XmlTextWriter xmlWriter)
		{
			XmlNamespaceManager nsmgr = new XmlNamespaceManager(functionReader.NameTable);									
			//Create the XmlParserContext.
			XmlParserContext context = new XmlParserContext(functionReader.NameTable, nsmgr, null, XmlSpace.None);			
							
			foreach (Pump pmp in ps.Pumps)
			{			
				functionReader = new XmlTextReader(xmlFragment, XmlNodeType.Element, context);				
				xmlWriter.WriteStartElement("Row");

				while (functionReader.Read())
				{
					if (functionReader.Name == "Cell" && 
						functionReader.NodeType != System.Xml.XmlNodeType.EndElement)
					{								
						xmlWriter.WriteStartElement("Cell");
						while (functionReader.MoveToNextAttribute())
						{
							if (functionReader.Name != "data")
							{
								xmlWriter.WriteAttributeString(functionReader.Name, functionReader.Value);
							}
						}
					}
					else if (functionReader.Name == "Function" && 
						functionReader.NodeType != System.Xml.XmlNodeType.EndElement)
					{
						string functionValue;
						string functionName = functionReader.GetAttribute("functionName");
						switch (functionName)
						{
							case "PSPumpNumber":
								if (pmp.PumpNumber == 1)
								{
									functionValue = "Lead Pump";									
								}
								else if (pmp.PumpNumber == 2)
								{
									functionValue = "Lag Pump"; 										
								}
								else
								{
									functionValue = "Lag " 
										+ Convert.ToString(pmp.PumpNumber - 1)
										+ " Pump";
								}
								break;
							case "PSPumpMotorSpeed":
								functionValue = Convert.ToString(
									pmp.MotorSpeed) + " rpm";
								break;								
							case "PSPumpHP":
								functionValue = Convert.ToString(pmp.HorsePower) + " hp";								
								break;
							case "PSPumpCapacity":
								functionValue = Convert.ToString(pmp.Capacity) + " gpm";								
								break;							
							default:
								functionValue = "INVALID ForceMain FUNCTION";
								break;

						}
						xmlWriter.WriteAttributeString("data", functionValue);
						functionReader.Skip();
					}
					else if (functionReader.NodeType == System.Xml.XmlNodeType.EndElement)
					{
						xmlWriter.WriteEndElement();
					}
				}														
				xmlWriter.WriteEndElement();
			}
			
		}
				
		/// <summary>
		/// Event that occurs when CharacterizationEngine reports that it's status has changed.
		/// </summary>
		public event OnStatusChangedEventHandler StatusChanged;
		
		/// <summary>
		/// Internally called function that fires the OnStatusChanged event.
		/// </summary>
		/// <param name="e">Parameter that contains the string describing the new state.</param>
		protected virtual void OnStatusChanged(StatusChangedArgs e) 
		{
			if (StatusChanged != null)
				StatusChanged(e);
		}			
	}
}

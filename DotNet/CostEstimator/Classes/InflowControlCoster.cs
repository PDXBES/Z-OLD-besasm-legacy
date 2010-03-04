// Project: Classes, File: InflowControlCoster.cs
// Namespace: CostEstimator.Classes, Class: InflowControlCoster
// Path: C:\Development\CostEstimatorV2\Classes, Author: Arnel
// Code lines: 30, Size of file: 412 Bytes
// Creation date: 3/6/2008 12:09 PM
// Last modified: 7/16/2008 2:34 PM

#region Using directives
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.XPath;
#endregion

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
	public enum InflowControl
	{
		GreenStreetPlanter,
		InfiltrationPlanter,
		VegetatedInfiltrationBasin,
		DownspoutDisconnection,
		Ecoroof,
		CurbExtension,
		StormwaterPlanter,
		FlowRestrictor
	} // enum InflowControl

	/// <summary>
	/// Inflow control coster
	/// </summary>
	public class InflowControlCoster
	{
		#region Variables
		private decimal _GreenStreetPlanterMinCost;
		private decimal _GreenStreetPlanterCostPerAcre;
		private decimal _InfiltrationPlanterMinCost;
		private decimal _InfiltrationPlanterCostPerAcre;
		private decimal _VegetatedInfiltrationBasinMinCost;
		private decimal _VegetatedInfiltrationBasinCostPerAcre;
		private decimal _DownspoutDisconnectionMinCost;
		private decimal _DownspoutDisconnectionCostPerAcre;
		private decimal _EcoroofMinCost;
		private decimal _EcoroofCostPerAcre;
		private decimal _CurbExtensionMinCost;
		private decimal _CurbExtensionCostPerCuFt;
		private double _CurbExtensionMinCuFt;
		private decimal _StormwaterPlanterMinCost;
		private decimal _StormwaterPlanterCostPerCuFt;
		private double _StormwaterPlanterMinCuFt;
		private decimal _FlowRestrictorCost;
		private int _BaseENR;

		private InflowControl _InflowControl;
		private double _FacilitySizeCuFt;
		private double _DrainageAreaSqFt;
		private bool _MinCostUsed;

		public SortedList<string, CosterParameter> CosterParameters = new SortedList<string, CosterParameter>();
		#endregion

		#region Constructors
		/// <summary>
		/// Create inflow control coster
		/// </summary>
		public InflowControlCoster()
		{
			LoadReference();
		} // InflowControlCoster()
		#endregion

		#region Properties
		public InflowControl InflowControl
		{
			get
      {
      	return _InflowControl;
      } // get
			set
      {
      	_InflowControl = value;
      } // set
		} // InflowControl

		/// <summary>
		/// Facility size cu ft
		/// </summary>
		/// <returns>Double</returns>
		public double FacilitySizeCuFt
		{
			get
			{
    		return _FacilitySizeCuFt;
			} // get
			set
      {
      	_FacilitySizeCuFt = value;
      } // set
		} // FacilitySizeCuFt

		public double DrainageAreaSqFt
		{
			get
      {
      	return _DrainageAreaSqFt;
      } // get
			set
      {
      	_DrainageAreaSqFt = value;
      } // set
		} // DrainageAreaSqFt

		/// <summary>
		/// Cost
		/// </summary>
		/// <returns>Decimal</returns>
		public decimal Cost
		{
			get
			{
				_MinCostUsed = false;
				switch (_InflowControl)
				{
					case Classes.InflowControl.GreenStreetPlanter:
						{
							decimal calcCost = (decimal)((double)_GreenStreetPlanterCostPerAcre * _DrainageAreaSqFt / 43560);
							_MinCostUsed = calcCost < _GreenStreetPlanterMinCost;
							return Math.Max(_GreenStreetPlanterMinCost, calcCost);
						} // block
					case Classes.InflowControl.InfiltrationPlanter:
						{
							decimal calcCost = (decimal)((double)_InfiltrationPlanterCostPerAcre * _DrainageAreaSqFt / 43560);
							_MinCostUsed = calcCost < _InfiltrationPlanterMinCost;
							return Math.Max(_InfiltrationPlanterMinCost, calcCost);
						} // block
					case Classes.InflowControl.VegetatedInfiltrationBasin:
						{
							decimal calcCost = (decimal)((double)_VegetatedInfiltrationBasinCostPerAcre * _DrainageAreaSqFt / 43560);
							_MinCostUsed = calcCost < _VegetatedInfiltrationBasinMinCost;
							return Math.Max(_VegetatedInfiltrationBasinMinCost, calcCost);
						} // block
					case Classes.InflowControl.DownspoutDisconnection:
						{
							decimal calcCost = (decimal)((double)_DownspoutDisconnectionCostPerAcre * _DrainageAreaSqFt / 43560);
							_MinCostUsed = calcCost < _DownspoutDisconnectionMinCost;
							return Math.Max(_DownspoutDisconnectionMinCost, calcCost);
						} // block
					case Classes.InflowControl.Ecoroof:
						{
							decimal calcCost = (decimal)((double)_EcoroofCostPerAcre * _DrainageAreaSqFt / 43560);
							_MinCostUsed = calcCost < _EcoroofMinCost;
							return Math.Max(_EcoroofMinCost, calcCost);
						} // block
					case Classes.InflowControl.CurbExtension:
						{
							decimal calcCost = (decimal)((double)_CurbExtensionCostPerCuFt * _FacilitySizeCuFt);
							_MinCostUsed = _FacilitySizeCuFt <= _CurbExtensionMinCuFt;
							return (_FacilitySizeCuFt <= _CurbExtensionMinCuFt) ?
								_CurbExtensionMinCost : calcCost;
						} // block
					case Classes.InflowControl.StormwaterPlanter:
						{
							decimal calcCost = (decimal)((double)_StormwaterPlanterCostPerCuFt * _FacilitySizeCuFt);
							_MinCostUsed = _FacilitySizeCuFt <= _StormwaterPlanterMinCuFt;
              return (_FacilitySizeCuFt <= _StormwaterPlanterMinCuFt) ?
							_StormwaterPlanterMinCost : calcCost;
						} // block
					case Classes.InflowControl.FlowRestrictor:
						return _FlowRestrictorCost;
				} // switch

				return 0;
			} // get
		} // Cost

		/// <summary>
		/// Base ENR
		/// </summary>
		/// <returns>Int</returns>
		public int BaseENR
		{
			get
			{
				return _BaseENR;
			} // get
		} // BaseENR

		/// <summary>
		/// Green street planter min cost
		/// </summary>
		/// <returns>Decimal</returns>
		public decimal GreenStreetPlanterMinCost
		{
			get
			{
				return _GreenStreetPlanterMinCost;
			} // get
		} // GreenStreetPlanterMinCost

		/// <summary>
		/// Infiltration planter min cost
		/// </summary>
		/// <returns>Decimal</returns>
		public decimal InfiltrationPlanterMinCost
		{
			get
			{
				return _InfiltrationPlanterMinCost;
			} // get
		} // InfiltrationPlanterMinCost

		/// <summary>
		/// Vegetated infiltration basin min cost
		/// </summary>
		/// <returns>Decimal</returns>
		public decimal VegetatedInfiltrationBasinMinCost
		{
			get
			{
				return _VegetatedInfiltrationBasinMinCost;
			} // get
		} // VegetatedInfiltrationBasinMinCost

		/// <summary>
		/// Downspout disconnection min cost
		/// </summary>
		/// <returns>Decimal</returns>
		public decimal DownspoutDisconnectionMinCost
		{
			get
			{
				return _DownspoutDisconnectionMinCost;
			} // get
		} // DownspoutDisconnectionMinCost

		/// <summary>
		/// Ecoroof min cost
		/// </summary>
		/// <returns>Decimal</returns>
		public decimal EcoroofMinCost
		{
			get
			{
				return _EcoroofMinCost;
			} // get
		} // EcoroofMinCost

		/// <summary>
		/// Curb extension min cu ft
		/// </summary>
		/// <returns>Double</returns>
		public double CurbExtensionMinCuFt
		{
			get
			{
				return _CurbExtensionMinCuFt;
			} // get
		} // CurbExtensionMinCuFt

		/// <summary>
		/// Curb extension min cost
		/// </summary>
		/// <returns>Double</returns>
		public decimal CurbExtensionMinCost
		{
			get
			{
				return _CurbExtensionMinCost;
			} // get
		} // CurbExtensionMinCost

		/// <summary>
		/// Stormwater planter min cu ft
		/// </summary>
		/// <returns>Double</returns>
		public double StormwaterPlanterMinCuFt
		{
			get
			{
				return _StormwaterPlanterMinCuFt;
			} // get
		} // StormwaterPlanterMinCuFt

		/// <summary>
		/// Stormwater planter min cost
		/// </summary>
		/// <returns>Decimal</returns>
		public decimal StormwaterPlanterMinCost
		{
			get
			{
				return _StormwaterPlanterMinCost;
			} // get
		} // StormwaterPlanterMinCost
		#endregion

		#region Methods
		/// <summary>
		/// Initialize coster parameters
		/// </summary>
		public void InitializeCosterParameters()
		{
			CosterParameter param;

			param = new CosterParameter("Green street planter minimum cost", "System.Decimal", _GreenStreetPlanterMinCost);
			CosterParameters.Add(param.Name, param);
			param = new CosterParameter("Green street planter cost per acre", "System.Decimal", _GreenStreetPlanterCostPerAcre);
			CosterParameters.Add(param.Name, param);
			param = new CosterParameter("Infiltration planter minimum cost", "System.Decimal", _InfiltrationPlanterMinCost);
			CosterParameters.Add(param.Name, param);
			param = new CosterParameter("Infiltration planter cost per acre", "System.Decimal", _InfiltrationPlanterCostPerAcre);
			CosterParameters.Add(param.Name, param);
			param = new CosterParameter("Vegetated infiltration basin minimum cost", "System.Decimal", _VegetatedInfiltrationBasinMinCost);
			CosterParameters.Add(param.Name, param);
			param = new CosterParameter("Vegetated infiltration basin cost per acre", "System.Decimal", _VegetatedInfiltrationBasinCostPerAcre);
			CosterParameters.Add(param.Name, param);
			param = new CosterParameter("Downspout disconnection minimum cost", "System.Decimal", _DownspoutDisconnectionMinCost);
			CosterParameters.Add(param.Name, param);
			param = new CosterParameter("Downspout disconnection cost per acre", "System.Decimal", _DownspoutDisconnectionCostPerAcre);
			CosterParameters.Add(param.Name, param);
			param = new CosterParameter("Ecoroof minimum cost", "System.Decimal", _EcoroofMinCost);
			CosterParameters.Add(param.Name, param);
			param = new CosterParameter("Ecoroof cost per acre", "System.Decimal", _EcoroofCostPerAcre);
			CosterParameters.Add(param.Name, param);
			param = new CosterParameter("Curb extension minimum cost", "System.Decimal", _CurbExtensionMinCost);
			CosterParameters.Add(param.Name, param);
			param = new CosterParameter("Curb extension cost per cu ft", "System.Decimal", _CurbExtensionCostPerCuFt);
			CosterParameters.Add(param.Name, param);
			param = new CosterParameter("Curb extension minimum cost equivalent cu ft", "System.Decimal", _CurbExtensionMinCuFt);
			CosterParameters.Add(param.Name, param);
			param = new CosterParameter("Stormwater planter minimum cost", "System.Decimal", _StormwaterPlanterMinCost);
			CosterParameters.Add(param.Name, param);
			param = new CosterParameter("Stormwater planter cost per cu ft", "System.Decimal", _StormwaterPlanterCostPerCuFt);
			CosterParameters.Add(param.Name, param);
			param = new CosterParameter("Stormwater planter minimum cost equivalent cu ft", "System.Decimal", _StormwaterPlanterMinCuFt);
			CosterParameters.Add(param.Name, param);
			param = new CosterParameter("Flow restrictor cost", "System.Decimal", _FlowRestrictorCost);
			CosterParameters.Add(param.Name, param);
			param = new CosterParameter("Base ENR", "System.Decimal", _BaseENR);
			CosterParameters.Add(param.Name, param);

		} // InitializeCosterParameters()

		/// <summary>
		/// Load reference
		/// </summary>
		/// <returns>Bool</returns>
		public bool LoadReference()
		{
			XmlReaderSettings xmlSettings = new XmlReaderSettings();
			xmlSettings.IgnoreWhitespace = true;
			xmlSettings.ValidationType = ValidationType.Schema;
			xmlSettings.ConformanceLevel = ConformanceLevel.Document;

			string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
			XmlReader pipeCostRef = XmlReader.Create(appPath + @"\InflowControlCostReference.xml", xmlSettings);

			XPathDocument pipeCostRefDoc = new XPathDocument(pipeCostRef, XmlSpace.None);
			XPathNavigator pipeCostRefNav = pipeCostRefDoc.CreateNavigator();

			string xpathQuery = "/InflowControlCostOptions/*";

			XPathNodeIterator xpathIter = pipeCostRefNav.Select(xpathQuery);
			while (xpathIter.MoveNext())
			{
				switch (xpathIter.Current.Name)
				{
					case "GreenStreetPlanterMinCost":
						_GreenStreetPlanterMinCost = (decimal)xpathIter.Current.ValueAsDouble;
						break;
					case "GreenStreetPlanterCostPerAcre":
						_GreenStreetPlanterCostPerAcre = (decimal)xpathIter.Current.ValueAsDouble;
						break;
					case "InfiltrationPlanterMinCost":
						_InfiltrationPlanterMinCost = (decimal)xpathIter.Current.ValueAsDouble;
						break;
					case "InfiltrationPlanterCostPerAcre":
						_InfiltrationPlanterCostPerAcre = (decimal)xpathIter.Current.ValueAsDouble;
						break;
					case "VegetatedInfiltrationBasinMinCost":
						_VegetatedInfiltrationBasinMinCost = (decimal)xpathIter.Current.ValueAsDouble;
						break;
					case "VegetatedInfiltrationBasinCostPerAcre":
						_VegetatedInfiltrationBasinCostPerAcre = (decimal)xpathIter.Current.ValueAsDouble;
						break;
					case "DownspoutDisconnectionMinCost":
						_DownspoutDisconnectionMinCost = (decimal)xpathIter.Current.ValueAsDouble;
						break;
					case "DownspoutDisconnectionCostPerAcre":
						_DownspoutDisconnectionCostPerAcre = (decimal)xpathIter.Current.ValueAsDouble;
						break;
					case "EcoroofMinCost":
						_EcoroofMinCost = (decimal)xpathIter.Current.ValueAsDouble;
						break;
					case "EcoroofCostPerAcre":
						_EcoroofCostPerAcre = (decimal)xpathIter.Current.ValueAsDouble;
						break;
					case "CurbExtensionMinCost":
						_CurbExtensionMinCost = (decimal)xpathIter.Current.ValueAsDouble;
						break;
					case "CurbExtensionCostPerCuFt":
						_CurbExtensionCostPerCuFt = (decimal)xpathIter.Current.ValueAsDouble;
						break;
					case "CurbExtensionMinCuFt":
						_CurbExtensionMinCuFt = xpathIter.Current.ValueAsDouble;
						break;
					case "StormwaterPlanterMinCost":
						_StormwaterPlanterMinCost = (decimal)xpathIter.Current.ValueAsDouble;
						break;
					case "StormwaterPlanterCostPerCuFt":
						_StormwaterPlanterCostPerCuFt = (decimal)xpathIter.Current.ValueAsDouble;
						break;
					case "StormwaterPlanterMinCuFt":
						_StormwaterPlanterMinCuFt = xpathIter.Current.ValueAsDouble;
						break;
					case "FlowRestrictorCost":
						_FlowRestrictorCost = (decimal)xpathIter.Current.ValueAsDouble;
						break;
					case "BaseENR":
						_BaseENR = xpathIter.Current.ValueAsInt;
						break;
				} // switch
			} // while

			InitializeCosterParameters();

			return true;
		} // LoadReference()

		#endregion
	}
}

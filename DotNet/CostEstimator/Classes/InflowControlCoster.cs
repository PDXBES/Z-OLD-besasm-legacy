// Project: Classes, File: InflowControlCoster.cs
// Namespace: CostEstimator.Classes, Class: InflowControlCoster
// Path: C:\Development\CostEstimatorV2\Classes, Author: Arnel
// Code lines: 30, Size of file: 412 Bytes
// Creation date: 3/6/2008 12:09 PM
// Last modified: 6/29/2010 1:29 PM

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
  /// Inflow control costing engine
  /// </summary>
  public class InflowControlCoster
  {
    #region Variables
    private float _GreenStreetPlanterMinCost;
    private float _GreenStreetPlanterCostPerAcre;
    private float _InfiltrationPlanterMinCost;
    private float _InfiltrationPlanterCostPerAcre;
    private float _VegetatedInfiltrationBasinMinCost;
    private float _VegetatedInfiltrationBasinCostPerAcre;
    private float _DownspoutDisconnectionMinCost;
    private float _DownspoutDisconnectionCostPerAcre;
    private float _EcoroofMinCost;
    private float _EcoroofCostPerAcre;
    private float _CurbExtensionMinCost;
    private float _CurbExtensionCostPerCuFt;
    private double _CurbExtensionMinCuFt;
    private float _CurbExtensionTrafficControl;
    private float _CurbExtensionAdaRamp;
    private float _CurbExtensionWaterLineSleeving;
    private float _StormwaterPlanterMinCost;
    private float _StormwaterPlanterCostPerCuFt;
    private double _StormwaterPlanterMinCuFt;
    private float _FlowRestrictorCost;
    private int _BaseENR;

    private InflowControl _InflowControl;
    private double _FacilitySizeCuFt;
    private double _DrainageAreaSqFt;
    private bool _MinCostUsed;

    public SortedList<string, CosterParameter> CosterParameters =
    new SortedList<string, CosterParameter>();
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
    /// <returns>float</returns>
    public float Cost
    {
      get
      {
        _MinCostUsed = false;
        switch (_InflowControl)
        {
          case Classes.InflowControl.GreenStreetPlanter:
            {
              float calcCost = (float)((double)_GreenStreetPlanterCostPerAcre *
              _DrainageAreaSqFt / Common.SQUARE_FEET_PER_ACRE);
              _MinCostUsed = calcCost < _GreenStreetPlanterMinCost;
              return Math.Max(_GreenStreetPlanterMinCost, calcCost);
            } // block
          case Classes.InflowControl.InfiltrationPlanter:
            {
              float calcCost = (float)((double)_InfiltrationPlanterCostPerAcre *
              _DrainageAreaSqFt / Common.SQUARE_FEET_PER_ACRE);
              _MinCostUsed = calcCost < _InfiltrationPlanterMinCost;
              return Math.Max(_InfiltrationPlanterMinCost, calcCost);
            } // block
          case Classes.InflowControl.VegetatedInfiltrationBasin:
            {
              float calcCost = (float)((double)_VegetatedInfiltrationBasinCostPerAcre *
              _DrainageAreaSqFt / Common.SQUARE_FEET_PER_ACRE);
              _MinCostUsed = calcCost < _VegetatedInfiltrationBasinMinCost;
              return Math.Max(_VegetatedInfiltrationBasinMinCost, calcCost);
            } // block
          case Classes.InflowControl.DownspoutDisconnection:
            {
              float calcCost = (float)((double)_DownspoutDisconnectionCostPerAcre *
              _DrainageAreaSqFt / Common.SQUARE_FEET_PER_ACRE);
              _MinCostUsed = calcCost < _DownspoutDisconnectionMinCost;
              return Math.Max(_DownspoutDisconnectionMinCost, calcCost);
            } // block
          case Classes.InflowControl.Ecoroof:
            {
              float calcCost = (float)((double)_EcoroofCostPerAcre *
              _DrainageAreaSqFt / Common.SQUARE_FEET_PER_ACRE);
              _MinCostUsed = calcCost < _EcoroofMinCost;
              return Math.Max(_EcoroofMinCost, calcCost);
            } // block
          case Classes.InflowControl.CurbExtension:
            {
              float calcCost = (float)((double)_CurbExtensionCostPerCuFt * _FacilitySizeCuFt);
              _MinCostUsed = _FacilitySizeCuFt <= _CurbExtensionMinCuFt;
              return (_FacilitySizeCuFt <= _CurbExtensionMinCuFt) ?
              _CurbExtensionMinCost : calcCost;
            } // block
          case Classes.InflowControl.StormwaterPlanter:
            {
              float calcCost = (float)((double)_StormwaterPlanterCostPerCuFt * _FacilitySizeCuFt);
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
    /// <returns>float</returns>
    public float GreenStreetPlanterMinCost
    {
      get
      {
        return _GreenStreetPlanterMinCost;
      } // get
    } // GreenStreetPlanterMinCost

    /// <summary>
    /// Infiltration planter min cost
    /// </summary>
    /// <returns>float</returns>
    public float InfiltrationPlanterMinCost
    {
      get
      {
        return _InfiltrationPlanterMinCost;
      } // get
    } // InfiltrationPlanterMinCost

    /// <summary>
    /// Vegetated infiltration basin min cost
    /// </summary>
    /// <returns>float</returns>
    public float VegetatedInfiltrationBasinMinCost
    {
      get
      {
        return _VegetatedInfiltrationBasinMinCost;
      } // get
    } // VegetatedInfiltrationBasinMinCost

    /// <summary>
    /// Downspout disconnection min cost
    /// </summary>
    /// <returns>float</returns>
    public float DownspoutDisconnectionMinCost
    {
      get
      {
        return _DownspoutDisconnectionMinCost;
      } // get
    } // DownspoutDisconnectionMinCost

    /// <summary>
    /// Ecoroof min cost
    /// </summary>
    /// <returns>float</returns>
    public float EcoroofMinCost
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
    public float CurbExtensionMinCost
    {
      get
      {
        return _CurbExtensionMinCost;
      } // get
    } // CurbExtensionMinCost

    /// <summary>
    /// Curb extension traffic control
    /// </summary>
    /// <returns>float</returns>
    public float CurbExtensionTrafficControl
    {
      get
      {
        return _CurbExtensionTrafficControl;
      } // get
    } // CurbExtensionTrafficControl

    /// <summary>
    /// Curb extension ada ramp
    /// </summary>
    /// <returns>float</returns>
    public float CurbExtensionAdaRamp
    {
      get
      {
        return _CurbExtensionAdaRamp;
      } // get
    } // CurbExtensionAdaRamp

    /// <summary>
    /// Curb extension water line sleeving
    /// </summary>
    /// <returns>float</returns>
    public float CurbExtensionWaterLineSleeving
    {
      get
      {
        return _CurbExtensionWaterLineSleeving;
      } // get
    } // CurbExtensionWaterLineSleeving

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
    /// <returns>float</returns>
    public float StormwaterPlanterMinCost
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

      param = new CosterParameter("Green street planter minimum cost", "System.float", _GreenStreetPlanterMinCost);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Green street planter cost per acre", "System.float", _GreenStreetPlanterCostPerAcre);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Infiltration planter minimum cost", "System.float", _InfiltrationPlanterMinCost);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Infiltration planter cost per acre", "System.float", _InfiltrationPlanterCostPerAcre);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Vegetated infiltration basin minimum cost", "System.float", _VegetatedInfiltrationBasinMinCost);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Vegetated infiltration basin cost per acre", "System.float", _VegetatedInfiltrationBasinCostPerAcre);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Downspout disconnection minimum cost", "System.float", _DownspoutDisconnectionMinCost);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Downspout disconnection cost per acre", "System.float", _DownspoutDisconnectionCostPerAcre);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Ecoroof minimum cost", "System.float", _EcoroofMinCost);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Ecoroof cost per acre", "System.float", _EcoroofCostPerAcre);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Curb extension minimum cost", "System.float", _CurbExtensionMinCost);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Curb extension cost per cu ft", "System.float", _CurbExtensionCostPerCuFt);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Curb extension minimum cost equivalent cu ft", "System.float", _CurbExtensionMinCuFt);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Curb extension traffic control", "System.float", _CurbExtensionTrafficControl);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Curb extension ADA ramp", "System.float", _CurbExtensionAdaRamp);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Curb extension water line sleeving", "System.float", _CurbExtensionWaterLineSleeving);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Stormwater planter minimum cost", "System.float", _StormwaterPlanterMinCost);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Stormwater planter cost per cu ft", "System.float", _StormwaterPlanterCostPerCuFt);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Stormwater planter minimum cost equivalent cu ft", "System.float", _StormwaterPlanterMinCuFt);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Flow restrictor cost", "System.float", _FlowRestrictorCost);
      CosterParameters.Add(param.Name, param);
      param = new CosterParameter("Base ENR", "System.float", _BaseENR);
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
            _GreenStreetPlanterMinCost = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "GreenStreetPlanterCostPerAcre":
            _GreenStreetPlanterCostPerAcre = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "InfiltrationPlanterMinCost":
            _InfiltrationPlanterMinCost = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "InfiltrationPlanterCostPerAcre":
            _InfiltrationPlanterCostPerAcre = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "VegetatedInfiltrationBasinMinCost":
            _VegetatedInfiltrationBasinMinCost = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "VegetatedInfiltrationBasinCostPerAcre":
            _VegetatedInfiltrationBasinCostPerAcre = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "DownspoutDisconnectionMinCost":
            _DownspoutDisconnectionMinCost = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "DownspoutDisconnectionCostPerAcre":
            _DownspoutDisconnectionCostPerAcre = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "EcoroofMinCost":
            _EcoroofMinCost = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "EcoroofCostPerAcre":
            _EcoroofCostPerAcre = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "CurbExtensionMinCost":
            _CurbExtensionMinCost = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "CurbExtensionCostPerCuFt":
            _CurbExtensionCostPerCuFt = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "CurbExtensionMinCuFt":
            _CurbExtensionMinCuFt = xpathIter.Current.ValueAsDouble;
            break;
          case "CurbExtensionTrafficControl":
            _CurbExtensionTrafficControl = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "CurbExtensionAdaRamp":
            _CurbExtensionAdaRamp = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "CurbExtensionWaterLineSleeving":
            _CurbExtensionWaterLineSleeving = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "StormwaterPlanterMinCost":
            _StormwaterPlanterMinCost = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "StormwaterPlanterCostPerCuFt":
            _StormwaterPlanterCostPerCuFt = (float)xpathIter.Current.ValueAsDouble;
            break;
          case "StormwaterPlanterMinCuFt":
            _StormwaterPlanterMinCuFt = xpathIter.Current.ValueAsDouble;
            break;
          case "FlowRestrictorCost":
            _FlowRestrictorCost = (float)xpathIter.Current.ValueAsDouble;
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

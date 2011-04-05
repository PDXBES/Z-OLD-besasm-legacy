// Project: SystemsAnalysis.Modeling, File: RoofTarget.cs
// Namespace: SystemsAnalysis.Modeling, Class: RoofTarget
// Path: C:\Development\Modeling, Author: Arnel
// Code lines: 48, Size of file: 1.37 KB
// Creation date: 9/17/2007 1:05 AM
// Last modified: 3/6/2008 11:08 PM

using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsAnalysis.Modeling
{
  public class RoofTarget : AbstractInflowControl
  {
    internal int _dsToBioretention;
    internal int _dsToDrywell;
    internal int _dsToEcoroof;
    internal int _dsToPlanter;
    internal int _dsToStreet;
    internal int _dsToVeg;

    public RoofTarget(DataAccess.ModelDataSet.MdlRoofTargetsRow row)
    {
      _ID = row.ICID;
      _ICType = Types.Enumerators.InflowControlTypes.Roof;
      _BuildModelIC = row.BuildModelIC;
      _Constructed = row.Constructed;

      _dsToBioretention = row.DStoBioretention;
      _dsToDrywell = row.DStoDrywell;
      _dsToEcoroof = row.DStoEcoroof;
      _dsToPlanter = row.DStoPlanter;
      _dsToStreet = row.DStoStreet;
      _dsToVeg = row.DStoVeg;

      double finalTargetRoofArea = row.EXRfAreaFt * row.FractionDrained - row.EXICFtRoof;
      _ControlledArea = finalTargetRoofArea * TotalDownspouts / row.DSTotal *
        (1.2 - row.Difficulty * 0.2);
    }

    public RoofTarget(RoofTarget originalTarget, string FocusArea)
    {
      _ID = originalTarget._ID;
      _ICType = originalTarget._ICType;
      _BuildModelIC = originalTarget._BuildModelIC;
      _Constructed = originalTarget._Constructed;
      _ControlledArea = originalTarget._ControlledArea;
      _FocusArea = FocusArea;
      _dsToBioretention = originalTarget._dsToBioretention;
      _dsToDrywell = originalTarget._dsToDrywell;
      _dsToEcoroof = originalTarget._dsToEcoroof;
      _dsToPlanter = originalTarget._dsToPlanter;
      _dsToStreet = originalTarget._dsToStreet;
      _dsToVeg = originalTarget._dsToVeg;
    }

    /// <summary>
    /// Total downspouts
    /// </summary>
    /// <returns>Int</returns>
    public int TotalDownspouts
    {
      get
      {
        return _dsToBioretention + _dsToDrywell + _dsToEcoroof + _dsToPlanter +
          _dsToStreet + _dsToVeg;
      } // get
    } // TotalDownspouts

    /// <summary>
    /// DSTo bioretention
    /// </summary>
    /// <returns>Int</returns>
    public int DSToBioretention
    {
      get
      {
        return _dsToBioretention;
      } // get
    } // DSToBioretention

    /// <summary>
    /// Area to bioretention
    /// </summary>
    /// <returns>Double</returns>
    public double AreaToBioretention
    {
      get
      {
        return ControlledArea * DSToBioretention / TotalDownspouts;
      } // get
    } // AreaToBioretention

    /// <summary>
    /// DSTo drywell
    /// </summary>
    /// <returns>Int</returns>
    public int DSToDrywell
    {
      get
      {
        return _dsToDrywell;
      } // get
    } // DSToDrywell

    /// <summary>
    /// Area to drywell
    /// </summary>
    /// <returns>Double</returns>
    public double AreaToDrywell
    {
      get
      {
        return ControlledArea * DSToDrywell / TotalDownspouts;
      } // get
    } // AreaToDrywell

    /// <summary>
    /// DSTo ecoroof
    /// </summary>
    /// <returns>Int</returns>
    public int DSToEcoroof
    {
      get
      {
        return _dsToEcoroof;
      } // get
    } // DSToEcoroof

    /// <summary>
    /// Area to ecoroof
    /// </summary>
    /// <returns>Double</returns>
    public double AreaToEcoroof
    {
      get
      {
        return ControlledArea * DSToEcoroof / TotalDownspouts;
      } // get
    } // AreaToEcoroof

    /// <summary>
    /// DSTo planter
    /// </summary>
    /// <returns>Int</returns>
    public int DSToPlanter
    {
      get
      {
        return _dsToPlanter;
      } // get
    } // DSToPlanter

    /// <summary>
    /// Area to planter
    /// </summary>
    /// <returns>Double</returns>
    public double AreaToPlanter
    {
      get
      {
        return ControlledArea * DSToPlanter / TotalDownspouts;
      } // get
    } // AreaToPlanter

    /// <summary>
    /// DSTo street
    /// </summary>
    /// <returns>Int</returns>
    public int DSToStreet
    {
      get
      {
        return _dsToStreet;
      } // get
    } // DSToStreet

    /// <summary>
    /// Area to street
    /// </summary>
    /// <returns>Double</returns>
    public double AreaToStreet
    {
      get
      {
        return ControlledArea * DSToStreet / TotalDownspouts;
      } // get
    } // AreaToStreet

    /// <summary>
    /// DSTo veg
    /// </summary>
    /// <returns>Int</returns>
    public int DSToVeg
    {
      get
      {
        return _dsToVeg;
      } // get
    } // DSToVeg

    /// <summary>
    /// Area to veg
    /// </summary>
    /// <returns>Double</returns>
    public double AreaToVeg
    {
      get
      {
        return ControlledArea * DSToVeg / TotalDownspouts;
      } // get
    } // AreaToVeg
  }
}

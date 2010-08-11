// Project: SystemsAnalysis.Modeling, File: StreetTarget.cs
// Namespace: SystemsAnalysis.Modeling, Class: StreetTarget
// Path: C:\Development\Modeling, Author: Arnel
// Code lines: 41, Size of file: 942 Bytes
// Creation date: 9/17/2007 6:46 AM
// Last modified: 3/6/2008 11:50 PM

using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsAnalysis.Modeling
{
  public class StreetTarget : AbstractInflowControl
  {
    internal string _typeCode;
    internal double _curbExtensionVol;

    public StreetTarget(DataAccess.ModelDataSet.MdlStreetTargetsRow row)
    {
      _ID = row.ICID;
      _ICType = Types.Enumerators.InflowControlTypes.Street;
      _BuildModelIC = row.BuildModelIC;
      _Constructed = row.Constructed;
      _ControlledArea = row.StreetArea;

      _typeCode = row.IsTypeCodeNull() ? "" : row.TypeCode;
      _curbExtensionVol = row.CurbExtensionVol;
    }

    public StreetTarget(StreetTarget originalTarget, string FocusArea)
    {
      _ID = originalTarget._ID;
      _ICType = originalTarget._ICType;
      _BuildModelIC = originalTarget._BuildModelIC;
      _Constructed = originalTarget._Constructed;
      _ControlledArea = originalTarget._ControlledArea;
      _FocusArea = FocusArea;
      _typeCode = originalTarget._typeCode;
      _curbExtensionVol = originalTarget._curbExtensionVol; ;
    }

    /// <summary>
    /// Type code
    /// </summary>
    /// <returns>String</returns>
    public string TypeCode
    {
      get
      {
        return _typeCode;
      } // get
    } // TypeCode

    /// <summary>
    /// Curb extension vol
    /// </summary>
    /// <returns>Double</returns>
    public double CurbExtensionVol
    {
      get
      {
        return _curbExtensionVol;
      } // get
    } // CurbExtensionVol
  }
}

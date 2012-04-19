using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsAnalysis.Modeling
{
  public abstract class AbstractInflowControl : InflowControl
  {
    internal int _ID;
    internal Types.Enumerators.InflowControlTypes _ICType;
    internal bool _BuildModelIC;
    internal int _Constructed;
    internal string _FocusArea;
    internal double _ControlledArea;

    public int ID
    {
      get
      {
        return _ID;
      }
      set
      {
        _ID = value;
      }
    }

    public Types.Enumerators.InflowControlTypes ICType
    {
      get
      {
        return _ICType;
      }
    }

    public bool InModel
    {
      get
      {
        return _BuildModelIC || _Constructed > 0;
      }
    }

    public bool ToBeBuilt
    {
      get
      {
        return _BuildModelIC;
      }
      set
      {
        _BuildModelIC = value;
      }
    }

    public string FocusArea
    {
      get
      {
        return _FocusArea;
      }
    }

    public double ControlledArea
    {
      get
      {
        return _ControlledArea;
      }
    }

    public int Constructed
    {
      get
      {
        return _Constructed;
      }
      set
      {
        _Constructed = value;
      }
    }
  }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace SystemsAnalysis.Modeling
{
  public class ParkingTarget : AbstractInflowControl
  {
    public ParkingTarget(DataAccess.ModelDataSet.MdlParkingTargetsRow row)
    {
      _ID = row.ICID;
      _ICType = Types.Enumerators.InflowControlTypes.Parking;
      _BuildModelIC = row.BuildModelIC;
      _Constructed = row.Constructed;
      _ControlledArea = row.ParkingTargetArea;
    }

    public ParkingTarget(ParkingTarget originalTarget, string FocusArea)
    {
      _ID = originalTarget._ID;
      _ICType = originalTarget._ICType;
      _BuildModelIC = originalTarget._BuildModelIC;
      _Constructed = originalTarget._Constructed;
      _ControlledArea = originalTarget._ControlledArea;
      _FocusArea = FocusArea;
    }
  }
}

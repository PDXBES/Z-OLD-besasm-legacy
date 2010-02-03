using System;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.DataAccess;

namespace SystemsAnalysis.Modeling
{
  /// <summary>
  /// Class for storing diversion structure information.
  /// </summary>
  public class DiversionStructure
  {
    private string nodeName;
    private string diversionName;
    private string address;
    private string outfall;
    private string status;
    private string diversionType;

    public DiversionStructure(DataAccess.DiversionStructureDataSet.StructuresRow diversionRow)
    {
      this.nodeName = diversionRow.HansenUnitID;
      this.diversionName = diversionRow.StructureNum;
      this.address = diversionRow.Location;
      this.outfall = diversionRow.OutfallID;
      this.status = diversionRow.Status;
      this.diversionType = "";
    }

    public String NodeName
    {
      get { return this.nodeName; }
    }
    public string DiversionName
    {
      get { return this.diversionName; }
    }
    public string Address
    {
      get { return this.address; }
    }
    public string Outfall
    {
      get { return this.outfall; }
    }
    public string Status
    {
      get { return this.status; }
    }
    public string DiversionType
    {
      get { return this.diversionType; }
    }

  }
}

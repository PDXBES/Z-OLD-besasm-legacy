using System;
using System.Collections.Generic;
using System.Data.OleDb;

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  public class Segment
  {
    public int ID { get; private set; }
    public int XbjectID { get; private set; }
    public int GlobalID { get; private set; }
    public int HansenCompKey { get; private set; }
    public string USNodeID { get; private set; }
    public string DSNodeID { get; private set; }
    public int SegUSNodeID { get; private set; }
    public int SegDSNodeID { get; private set; }
    public double LengthFt { get; private set; }
    public double USFtFromUSNode { get; private set; }
    public double DSFtFromUSNode { get; private set; }
    public int CutNumber { get; private set; }
    public string UnitID { get; private set; }
    public string UnitType { get; private set; }
    public int CompType { get; private set; }
    public string Ownership { get; private set; }
    public string ServiceStatus { get; private set; }
    public double USElevation { get; private set; }
    public double DSElevation { get; private set; }
    public double USDepth { get; private set; }
    public double DSDepth { get; private set; }
    public double PipeLength { get; private set; }
    public double PipeDiamWidth { get; private set; }
    public double PipeHeight { get; private set; }
    public string PipeShape { get; private set; }
    public string Material { get; private set; }
    public string JobNumber { get; private set; }
    public string InstallDate { get; private set; }

    public Segment(OleDbDataReader reader)
    {
      ID = Convert.ToInt32(reader["ID"]);
      XbjectID = Convert.ToInt32(reader["XBJECTID"]);
      GlobalID = Convert.ToInt32(reader["GLOBALID"]);
      HansenCompKey = Convert.ToInt32(reader["hansen_compkey"]);
      USNodeID = reader["us_node_id"].ToString();
      DSNodeID = reader["ds_node_id"].ToString();
      SegUSNodeID = Convert.ToInt32(reader["seg_us_node_id"]);
      SegDSNodeID = Convert.ToInt32(reader["seg_ds_node_id"]);
      LengthFt = Convert.ToDouble(reader["length"]);
      USFtFromUSNode = Convert.ToDouble(reader["fm"]);
      DSFtFromUSNode = Convert.ToDouble(reader["to_"]);
      CutNumber = Convert.ToInt32(reader["cutno"]);
      UnitID = reader["UNITID"].ToString();
      UnitType = reader["UNITTYPE"].ToString();
      CompType = Convert.ToInt32(reader["COMPTYPE"]);
      Ownership = reader["OWNRSHIP"].ToString();
      ServiceStatus = reader["servstat"].ToString();
      USElevation = Convert.ToDouble(reader["FRM_ELEV"]);
      DSElevation = Convert.ToDouble(reader["TO_ELEV"]);
      USDepth = Convert.ToDouble(reader["FRM_DEPTH"]);
      DSDepth = Convert.ToDouble(reader["TO_DEPTH"]);
      PipeLength = Convert.ToDouble(reader["ParentLength"]);
      PipeDiamWidth = Convert.ToDouble(reader["PIPESIZE"]);
      PipeHeight = Convert.ToDouble(reader["PIPEHEIGHT"]);
      PipeShape = reader["PIPESHPE"].ToString();
      Material = reader["MATERIAL"].ToString();
      JobNumber = reader["JOBNO"].ToString();
      InstallDate = reader["INSTALL_DATE"].ToString();
    }
  }
}

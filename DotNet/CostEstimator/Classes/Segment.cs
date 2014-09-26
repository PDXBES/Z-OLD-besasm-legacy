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
    public Single LengthFt { get; private set; }
    public Single USFtFromUSNode { get; private set; }
    public Single DSFtFromUSNode { get; private set; }
    public Int16 CutNumber { get; private set; }
    public Int16 NumCuts { get; private set; }
    public string UnitID { get; private set; }
    public string UnitType { get; private set; }
    public Int16 CompType { get; private set; }
    public string Ownership { get; private set; }
    public string ServiceStatus { get; private set; }
    public Single USElevation { get; private set; }
    public Single DSElevation { get; private set; }
    public Single USDepth { get; private set; }
    public Single DSDepth { get; private set; }
    public Single PipeLength { get; private set; }
    public Single PipeDiamWidth { get; private set; }
    public Single PipeHeight { get; private set; }
    public string PipeShape { get; private set; }
    public string Material { get; private set; }
    public string JobNumber { get; private set; }
    public string InstallDate { get; private set; }

    public Segment(OleDbDataReader reader)
    {
      ID = Convert.ToInt32(reader["ID"] != DBNull.Value ? reader["ID"] : 0);
      XbjectID = Convert.ToInt32(reader["XBJECTID"] != DBNull.Value ? reader["XBJECTID"] : 0);
      GlobalID = Convert.ToInt32(reader["GLOBALID"] != DBNull.Value ? reader["GLOBALID"] : 0);
      HansenCompKey = 
        Convert.ToInt32(reader["hansen_compkey"] != DBNull.Value ? reader["hansen_compkey"] : 0);
      USNodeID = reader["us_node_id"] != null ? reader["us_node_id"].ToString() : string.Empty;
      DSNodeID = reader["ds_node_id"] != null ? reader["ds_node_id"].ToString() : string.Empty;
      SegUSNodeID = 
        Convert.ToInt32(reader["seg_us_node_id"] != DBNull.Value ? reader["seg_us_node_id"] : 0);
      SegDSNodeID = 
        Convert.ToInt32(reader["seg_ds_node_id"] != DBNull.Value ? reader["seg_ds_node_id"] : 0);
      LengthFt = Convert.ToSingle(reader["length"] != DBNull.Value ? reader["length"] : 0.0);
      USFtFromUSNode = Convert.ToSingle(reader["fm"] != DBNull.Value ? reader["fm"] : 0.0);
      DSFtFromUSNode = Convert.ToSingle(reader["to_"] != DBNull.Value ? reader["to_"] : 0.0);
      CutNumber = Convert.ToInt16(reader["cutno"] != DBNull.Value ? reader["cutno"] : 0);
      NumCuts = Convert.ToInt16(reader["tot_segs"] != DBNull.Value ? reader["tot_segs"] : 0);
      UnitID = reader["UNITID"] != null ? reader["UNITID"].ToString() : string.Empty;
      UnitType = reader["UNITTYPE"] != null ? reader["UNITTYPE"].ToString() : string.Empty;
      CompType = Convert.ToInt16(reader["COMPTYPE"] != DBNull.Value ? reader["COMPTYPE"] : 0);
      Ownership = reader["OWNRSHIP"] != null? reader["OWNRSHIP"].ToString() : string.Empty;
      ServiceStatus = reader["servstat"] != null? reader["servstat"].ToString() : string.Empty;
      USElevation = Convert.ToSingle(reader["FRM_ELEV"] != DBNull.Value ? reader["FRM_ELEV"] : 0.0);
      DSElevation = Convert.ToSingle(reader["TO_ELEV"] != DBNull.Value ? reader["TO_ELEV"] : 0.0);
      USDepth = Convert.ToSingle(reader["FRM_DEPTH"] != DBNull.Value ? reader["FRM_DEPTH"] : 0.0);
      DSDepth = Convert.ToSingle(reader["TO_DEPTH"] != DBNull.Value ? reader["TO_DEPTH"] : 0.0);
      PipeLength = Convert.ToSingle(reader["ParentLength"] != DBNull.Value ? reader["ParentLength"] : 0.0);
      PipeDiamWidth = Convert.ToSingle(reader["PIPESIZE"] != DBNull.Value ? reader["PIPESIZE"] : 0.0);
      PipeHeight = Convert.ToSingle(reader["PIPEHEIGHT"] != DBNull.Value ? reader["PIPEHEIGHT"] : 0.0);
      PipeShape = reader["PIPESHPE"] != null ? reader["PIPESHPE"].ToString() : string.Empty;
      Material = reader["MATERIAL"] != null ? reader["MATERIAL"].ToString() : string.Empty;
      JobNumber = reader["JOBNO"] != null ? reader["JOBNO"].ToString() : string.Empty;
      InstallDate = reader["INSTALL_DATE"] !=
        null ? reader["INSTALL_DATE"].ToString() : string.Empty;
    }
  }
}

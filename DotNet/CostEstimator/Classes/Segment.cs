using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using DevExpress.CodeRush.StructuralParser;

namespace SystemsAnalysis.Analysis.CostEstimator.Classes
{
  public class Segment
  {
    int ID;
    int XbjectID;
    int GlobalID;
    int HansenCompKey;
    int USNodeID;
    int DSNodeID;
    int SegUSNodeID;
    int SegDSNodeID;
    double LengthFt;
    double USFtFromUSNode;
    double DSFtFromDSNode;
    int CutNumber;
    string UnitID;
    string UnitType;
    int CompType;
    string Ownership;
    string ServiceStatus;
    double USElevation;
    double DSElevation;
    double USDepth;
    double DSDepth;
    double PipeLength;
    double PipeDiamWidth;
    double PipeHeight;
    string PipeShape;
    string Material;
    string JobNumber;
    string InstallDate;

    public Segment(OleDbDataReader reader)
    {
      reader.Read();
      ID = reader["ID"];
      XbjectID = reader["XBJECTID"];
      GlobalID = reader["GLOBALID"];
      HansenCompKey = reader["hansen_compkey"];
      USNodeID = reader["us_node_id"];
      DSNodeID = reader["ds_node_id"];
      SegUSNodeID = reader["seg_us_node_id"];
      SegDSNodeID = reader["seg_ds_node_id"];
      LengthFt = reader["length"];
      USFtFromUSNode = reader["fm"];
      DSFtFromUSNode = reader["to_"];
      CutNumber = reader["cutno"];
      UnitID = reader["UNITID"];
      UnitType = reader["UNITTYPE"];
      CompType = reader["COMPTYPE"];
      Ownership = reader["OWNRSHIP"];
      ServiceStatus = reader["servstat"];
      USElevation = reader["FRM_ELEV"];
      DSElevation = reader["TO_ELEV"];
      USDepth = reader["FRM_DEPTH"];
      DSDepth = reader["TO_DEPTH"];
      PipeLength = reader["ParentLength"];
      PipeDiamWidth = reader["PIPESIZE"];
      PipeHeight = reader["PIPEHEIGHT"];
      PipeShape = reader["PIPESHPE"];
      Material = reader["MATERIAL"];
      JobNumber = reader["JOBNO"];
      InstallDate = reader["INSTALL_DATE"];
    }
  }
}

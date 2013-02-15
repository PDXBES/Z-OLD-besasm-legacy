using System;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.DataAccess;
using SystemsAnalysis.Types;
using System.Collections.Generic;

namespace SystemsAnalysis.Modeling
{
  /// <summary>
  /// Class for storing diversion structure information.
  /// </summary>
  public class FlowEstimationCatchment
  {
    public static Dictionary<int, FlowEstimationCatchment> GetFecsFromLinks(Links links)
    {
      Dictionary<int, FlowEstimationCatchment> fecs;
      fecs = new Dictionary<int, FlowEstimationCatchment>();

      DataAccess.FlowEstimationDataSet.FlowEstCatchmentsDataTable feDT;
      feDT = new DataAccess.FlowEstimationDataSet.FlowEstCatchmentsDataTable();

      DataAccess.FlowEstimationDataSetTableAdapters.FlowEstCatchmentsTableAdapter feTA;
      feTA = new DataAccess.FlowEstimationDataSetTableAdapters.FlowEstCatchmentsTableAdapter();
      try
      {
        feTA.Fill(feDT);
      }
      catch (Exception ex)
      {
        System.Data.DataRow[] rows;
        rows = feDT.GetErrors();
        foreach (System.Data.DataRow row in rows)
        {
          throw new Exception(row.RowError);
        }
      }

      foreach (FlowEstimationDataSet.FlowEstCatchmentsRow row in feDT)
      {
        if (links.FindByMLinkID(row.MLinkID) != null)
        {
          fecs.Add(row.FecID, new FlowEstimationCatchment(row));
        }
      }
      return fecs;
    }

    private List<int> stopLinks;
    private int fecID;
    private string fecName;
    private int mLinkID;
    private string basinID;
    private Enumerators.FlowEstimationMethods feMethod;
    //private string feMethod;
    private DataAccess.FlowEstimationDataSet.FECResultsRow fecResults;
    private bool hasFecResults;
    public FlowEstimationCatchment(DataAccess.FlowEstimationDataSet.FlowEstCatchmentsRow feRow)
    {
      this.fecID = feRow.FecID;
      this.fecName = feRow.CatchmentName;
      this.mLinkID = feRow.MLinkID;
      this.basinID = feRow.BasinID;
      this.stopLinks = new List<int>();
      DataAccess.FlowEstimationDataSetTableAdapters.FECStopLinksTableAdapter fecStopLinksTA;
      fecStopLinksTA = new DataAccess.FlowEstimationDataSetTableAdapters.FECStopLinksTableAdapter();
      DataAccess.FlowEstimationDataSet.FECStopLinksDataTable fecStopLinksDT;
      fecStopLinksDT = fecStopLinksTA.GetDataByFecID(this.fecID);

      foreach (DataAccess.FlowEstimationDataSet.FECStopLinksRow stopLinksRow in fecStopLinksDT)
      {
        stopLinks.Add(stopLinksRow.MLinkID);
      }
      this.feMethod = Enumerators.GetFlowEstimationMethodEnum(feRow.CatchmentType);
      DataAccess.FlowEstimationDataSetTableAdapters.FECResultsTableAdapter fecResultsTA;
      fecResultsTA = new DataAccess.FlowEstimationDataSetTableAdapters.FECResultsTableAdapter();
      DataAccess.FlowEstimationDataSet.FECResultsDataTable fecResultsTable;
      fecResultsTable = fecResultsTA.GetDataByFecID(fecID);
      if (fecResultsTable.Count != 0)
      {
        fecResults = fecResultsTable[0];
      }
    }

    public DataAccess.FlowEstimationDataSet.FECResultsRow FecResults
    {
      get
      {
        if (this.fecResults == null)
        {
          throw new Exception("This FEC does not have results.");
        }
        return this.fecResults;
      }
    }
    public int FecID
    {
      [System.Diagnostics.DebuggerStepThroughAttribute]
      get
      {
        return this.fecID;
      }
    }
    public string FecName
    {
      [System.Diagnostics.DebuggerStepThroughAttribute]
      get
      {
        return this.fecName;
      }
    }
    public int MLinkID
    {
      [System.Diagnostics.DebuggerStepThroughAttribute]
      get
      {
        return this.mLinkID;
      }
    }
    public string BasinID
    {
      [System.Diagnostics.DebuggerStepThroughAttribute]
      get
      {
        return this.basinID;
      }
    }
    public Enumerators.FlowEstimationMethods FEMethod
    {
      [System.Diagnostics.DebuggerStepThroughAttribute]
      get
      {
        return this.feMethod;
      }
    }
    public List<int> StopLinks
    {
      [System.Diagnostics.DebuggerStepThroughAttribute]
      get
      {
        return this.stopLinks;
      }
    }
  }
}

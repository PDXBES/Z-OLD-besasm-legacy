namespace SystemsAnalysis.DataAccess
{
  partial class FlowEstimationDataSet
  {
    partial class FECStopLinksDataTable
    {
    }
    partial class FlowEstCatchmentsDataTable
    {
      public FlowEstCatchmentsRow FindByFECName(string FecName)
      {
        foreach (FlowEstCatchmentsRow row in this)
        {
          if (row.CatchmentName == FecName)
          {
            return row;
          }
        }
        return null;
      }
    }
    partial class FECResultsDataTable
    {
      public FECResultsRow FindByFECID(int fecID)
      {
        foreach (FECResultsRow row in this)
        {
          if (row.FecID == fecID)
          {
            return row;
          }
        }
        return null;
      }
    }
  }
}

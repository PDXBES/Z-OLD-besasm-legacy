using System;
using System.Data.Linq;
using System.Collections.Generic;

//namespace DSCUpdater 
namespace DSCUpdater
{
  public partial class ProjectDataSet
  {
    partial class DscUpdaterDataTable
    {

    }
  
    partial class DSCEDITDataTable
    {
    }

    partial class MstDscDataTable
    {

    }
  }
}

namespace DSCUpdater.ProjectDataSetTableAdapters
{  
  public partial class MstDscTableAdapter
  {
    public int FillByDscIdList(ProjectDataSet.MstDscDataTable mstDscDataTable, IEnumerable<int> dscIDs)
    {
      string originalCommandText = CommandCollection[0].CommandText;
      int returnValue = 0;
      try
      {
        this.CommandCollection[0].CommandText += Utilities.CreateDscWhereClause(dscIDs);
        returnValue = this.Fill(mstDscDataTable);
      }
      finally
      {
        this.CommandCollection[0].CommandText = originalCommandText;
      }
      return returnValue;
    }
  }

  public partial class MstIcDiscoVegTableAdapter
  {
    public int FillByDscIdList(ProjectDataSet.MstIcDiscoVegDataTable mstDiscoVegDataTable, IEnumerable<int> dscIDs)
    {
      string originalCommandText = CommandCollection[0].CommandText;
      int returnValue = 0;
      try
      {
        this.CommandCollection[0].CommandText += Utilities.CreateDscWhereClause(dscIDs);
        returnValue = this.Fill(mstDiscoVegDataTable);
      }
      finally
      {
        this.CommandCollection[0].CommandText = originalCommandText;
      }
      return returnValue;
    }
  }

  public partial class MstIcDrywellTableAdapter
  {
    public int FillByDscIdList(ProjectDataSet.MstIcDrywellDataTable mstDrywellDataTable, IEnumerable<int> dscIDs)
    {
      string originalCommandText = CommandCollection[0].CommandText;
      int returnValue = 0;
      try
      {
        this.CommandCollection[0].CommandText += Utilities.CreateDscWhereClause(dscIDs);
        returnValue = this.Fill(mstDrywellDataTable);
      }
      finally
      {
        this.CommandCollection[0].CommandText = originalCommandText;
      }
      return returnValue;
    }
  }

  internal static class Utilities
  {
    internal static string CreateDscWhereClause(IEnumerable<int> dscIDs)
    {
      string whereClause;
      whereClause = " WHERE DSCID IN (";
      string delimter = String.Empty;
      foreach (int dscid in dscIDs)
      {
        whereClause += delimter + dscid;
        delimter = ",";
      }
      whereClause += ")";
      return whereClause;
    }
  }

}

using System;
using System.Collections.Generic;
using SystemsAnalysis.ModelConstruction;
using SystemsAnalysis.Utils.AccessUtils;

namespace SystemsAnalysis.ModelConstruction
{
  /// <summary>
  /// Intended to serve as the entry point for building EMGAATS models in .NET.
  /// </summary>
  public static class ModelBuilder
  {
    /// <summary>
    /// Relinks all linked tables in a model's required Access Databases.
    /// </summary>
    public static void RelinkModel(string modelRoot)
    {
      ModelConfigurationDataSet modelConfigDS;
      modelConfigDS = new ModelConfigurationDataSet(modelRoot);


      Dictionary<string, string> databases;
      databases = modelConfigDS.GetRequiredDatabases();

      foreach (string dbName in databases.Keys)
      {
        AccessHelper accessHelper = new AccessHelper(databases[dbName]);

        Dictionary<string, string> linkedTableSources;
        linkedTableSources = modelConfigDS.GetLinkedTableSources(dbName);

        foreach (string table in linkedTableSources.Keys)
        {
          accessHelper.LinkTable(table, linkedTableSources[table]);
        }
      }
    }

    /// <summary>
    /// Relinks all tables in DataAccess.mdb to point relative to modelPath
    /// </summary>
    public static void RefreshDataAccess(string modelRoot)
    {
      modelRoot += modelRoot.EndsWith("\\") ? "" : "\\";

      ModelConfigurationDataSet modelConfigDS;
      modelConfigDS = new ModelConfigurationDataSet(modelRoot);

      string dataAccessFileName = modelConfigDS.GetDataAccessFileName();
      AccessHelper accessHelper = new AccessHelper(modelRoot + dataAccessFileName);
      try
      {
        Dictionary<string, string[]> dataAccessTables;
        dataAccessTables = modelConfigDS.GetDataAccessTables();

        foreach (string linkName in dataAccessTables.Keys)
        {
          string tableName = dataAccessTables[linkName][0];
          string databasePath = modelRoot + dataAccessTables[linkName][1];

          accessHelper.LinkTable(tableName, databasePath, linkName);
        }
      }
      catch (Exception ex)
      {
        throw new Exception("Could not relink DataAccess tables.", ex);
      }
      finally
      {
        accessHelper.Dispose();
      }
    }
  }
}

// Project: SystemsAnalysis.DataAccess, File: ModelDataSet.cs
// Namespace: SystemsAnalysis.DataAccess.ModelDataSetTableAdapters, Class: ModelDataSet
// Path: C:\Development\DotNet\DataAccess, Author: Arnel
// Code lines: 113, Size of file: 3.05 KB
// Creation date: 6/17/2008 3:16 PM
// Last modified: 6/30/2008 6:25 PM

namespace SystemsAnalysis.DataAccess
{
    partial class ModelDataSet
    {
        partial class MdlPipXPDataTable
        {
        }
    }
}

namespace SystemsAnalysis.DataAccess.ModelDataSetTableAdapters
{
  public static class ModelAdapterSetup
  {
    private static string NormalizePath(string alternativePath)
    {
      return alternativePath + (alternativePath.EndsWith("\\") ? "" : "\\");
    }

    public static string GetAlternativePackageConnectionString(string alternativePath)
    {
      return GetConnectionString(alternativePath, "alternative_package.mdb");
    }

    public static string GetModelConnectionString(string modelPath)
    {
      return GetConnectionString(modelPath, "mdbs\\DataAccess.mdb");
    }

    public static string GetConnectionString(string databasePath, string databaseName)
    {
      return @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
      NormalizePath(databasePath) + databaseName;
    }
  }
}

namespace SystemsAnalysis.DataAccess.ModelDataSetTableAdapters
{
  public partial class MdlDscTableAdapter
  {
    public MdlDscTableAdapter(string modelPath)
      : this()
    {
      this.Connection.ConnectionString = ModelAdapterSetup.GetModelConnectionString(modelPath);
    }
  }

  public partial class MdlLinksTableAdapter
  {
    public MdlLinksTableAdapter(string modelPath)
      : this()
    {
      this.Connection.ConnectionString = ModelAdapterSetup.GetModelConnectionString(modelPath);
    }
  }

  public partial class MdlNodesTableAdapter
  {
    public MdlNodesTableAdapter(string modelPath)
      : this()
    {
      this.Connection.ConnectionString = ModelAdapterSetup.GetModelConnectionString(modelPath);
    }
  }

  public partial class MdlSpecLinksTableAdapter
  {
    public MdlSpecLinksTableAdapter(string modelPath)
      : this()
    {
      this.Connection.ConnectionString = ModelAdapterSetup.GetModelConnectionString(modelPath);
    }
  }

  public partial class MdlSpecLinkDataTableAdapter
  {
    public MdlSpecLinkDataTableAdapter(string modelPath)
      : this()
    {
      this.Connection.ConnectionString = ModelAdapterSetup.GetModelConnectionString(modelPath);
    }
  }

  public partial class MdlParkingTargetsTableAdapter
  {
    public MdlParkingTargetsTableAdapter(string modelPath)
      : this()
    {
      this.Connection.ConnectionString = ModelAdapterSetup.GetModelConnectionString(modelPath);
    }
  }

  public partial class MdlRoofTargetsTableAdapter
  {
    public MdlRoofTargetsTableAdapter(string modelPath)
      : this()
    {
      this.Connection.ConnectionString = ModelAdapterSetup.GetModelConnectionString(modelPath);
    }
  }

  public partial class MdlStreetTargetsTableAdapter
  {
    public MdlStreetTargetsTableAdapter(string modelPath)
      : this()
    {
      this.Connection.ConnectionString = ModelAdapterSetup.GetModelConnectionString(modelPath);
    }
  }

  /// <summary>
  /// Mdl pip XP table adapter
  /// </summary>
  public partial class MdlPipXPTableAdapter
  {
    /// <summary>
    /// Create mdl pip XP table adapter
    /// </summary>
    /// <param name="modelPath">Model path</param>
    public MdlPipXPTableAdapter(string modelPath)
      : this()
    {
      this.Connection.ConnectionString = ModelAdapterSetup.GetModelConnectionString(modelPath);
    } // MdlPipXPTableAdapter(modelPath)
  } // class MdlPipXPTableAdapter
}


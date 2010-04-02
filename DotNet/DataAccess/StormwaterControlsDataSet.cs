namespace SystemsAnalysis.DataAccess
{
  public partial class StormwaterControlsDataSet
  {
  }
}

namespace SystemsAnalysis.DataAccess.StormwaterControlsDataSetTableAdapters
{

  public static class StormwaterControlsAdapterSetup
  {
    private static string NormalizePath(string path)
    {
      return path + (path.EndsWith("\\") ? "" : "\\");
    }

    public static string GetModelConnectionString(string modelPath)
    {
      return GetConnectionString(modelPath, "mdbs\\StormwaterControls_v12.mdb");
    }

    public static string GetConnectionString(string databasePath, string databaseName)
    {
      return @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" +
        NormalizePath(databasePath) + databaseName;
    }
  }
}

namespace SystemsAnalysis.DataAccess.StormwaterControlsDataSetTableAdapters
{
  public partial class ICTargetRoofTableAdapter
  {
    public ICTargetRoofTableAdapter(string modelPath)
      : this()
    {
      this.Connection.ConnectionString = StormwaterControlsAdapterSetup.GetModelConnectionString(modelPath);
    } 
  }

  public partial class ICTargetParkTableAdapter
  {
    public ICTargetParkTableAdapter(string modelPath)
      : this()
    {
      this.Connection.ConnectionString = StormwaterControlsAdapterSetup.GetModelConnectionString(modelPath);
    } 
  }
  public partial class ICNodeTableAdapter
  {
    public ICNodeTableAdapter(string modelPath)
      : this()
    {
      this.Connection.ConnectionString = StormwaterControlsAdapterSetup.GetModelConnectionString(modelPath);
    } 
  }
}

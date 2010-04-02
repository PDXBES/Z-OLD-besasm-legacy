namespace SystemsAnalysis.DataAccess
{
  public partial class StormwaterControlsDataSet
  {
    public void InitStormwaterControlDataSet(string modelPath)
    {
      StormwaterControlsDataSetTableAdapters.ICNodeTableAdapter icNodeTA;
      icNodeTA = new StormwaterControlsDataSetTableAdapters.ICNodeTableAdapter(modelPath);      
      icNodeTA.Fill(this.ICNode);

      StormwaterControlsDataSetTableAdapters.ICTargetParkTableAdapter icTargetParkTA;
      icTargetParkTA = new StormwaterControlsDataSetTableAdapters.ICTargetParkTableAdapter(modelPath);
      icTargetParkTA.Fill(this.ICTargetPark);

      StormwaterControlsDataSetTableAdapters.ICTargetRoofTableAdapter icTargetRoofTA;
      icTargetRoofTA = new StormwaterControlsDataSetTableAdapters.ICTargetRoofTableAdapter(modelPath);
      icTargetRoofTA.Fill(this.ICTargetRoof);

      StormwaterControlsDataSetTableAdapters.mdl_SurfSC_acTableAdapter mdlSscTA;
      mdlSscTA = new StormwaterControlsDataSetTableAdapters.mdl_SurfSC_acTableAdapter(modelPath);
      mdlSscTA.Fill(this.mdl_SurfSC_ac);
    }

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

  public partial class mdl_SurfSC_acTableAdapter
  {
    public mdl_SurfSC_acTableAdapter(string modelPath)
      : this()
    {
      this.Connection.ConnectionString = StormwaterControlsAdapterSetup.GetModelConnectionString(modelPath);
    }
  }
}

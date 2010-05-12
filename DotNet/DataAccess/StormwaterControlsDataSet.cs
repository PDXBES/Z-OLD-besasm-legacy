using SystemsAnalysis.Utils.AccessUtils;
using System.IO;
using SystemsAnalysis.Modeling.ModelUtils.ResultsExtractor;
using System.Data;

namespace SystemsAnalysis.DataAccess
{
  public partial class StormwaterControlsDataSet
  {
    public void InitStormwaterControlDataSet(string modelPath)
    {      
      StormwaterControlsDataSetTableAdapters.mdl_dirsc_acTableAdapter mdlDscTA;
      mdlDscTA = new StormwaterControlsDataSetTableAdapters.mdl_dirsc_acTableAdapter(modelPath);

      StormwaterControlsDataSetTableAdapters.mdl_SurfSC_acTableAdapter mdlSscTA;
      mdlSscTA = new StormwaterControlsDataSetTableAdapters.mdl_SurfSC_acTableAdapter(modelPath);

      StormwaterControlsDataSetTableAdapters.ic_RoofTargetsTableAdapter icRoofTargetsTA;
      icRoofTargetsTA = new StormwaterControlsDataSetTableAdapters.ic_RoofTargetsTableAdapter(modelPath);

      StormwaterControlsDataSetTableAdapters.ic_StreetTargetsTableAdapter icStreetTargetsTA;
      icStreetTargetsTA = new StormwaterControlsDataSetTableAdapters.ic_StreetTargetsTableAdapter(modelPath);

      StormwaterControlsDataSetTableAdapters.ic_ParkingTargetsTableAdapter icParkingTargetsTA;
      icParkingTargetsTA = new StormwaterControlsDataSetTableAdapters.ic_ParkingTargetsTableAdapter(modelPath);

      AccessHelper accessHelper = new AccessHelper(modelPath + @"\mdbs\StormwaterControls_v12.mdb");
      try
      {
        accessHelper.LinkTable("mdl_DirSC_ac", modelPath + @"\dsc\mdl_DirSC_ac.mdb","mdl_dirsc_ac");
        mdlDscTA.Fill(this.mdl_dirsc_ac);

        accessHelper.LinkTable("mdl_surfsc_ac", modelPath + @"\surfsc\mdl_SurfSC_ac.mdb");
        mdlSscTA.Fill(this.mdl_SurfSC_ac);

        accessHelper.LinkTable("mst_ic_StreetTargets_ac", modelPath + @"\icalt\mst_ic_StreetTargets_ac.mdb","ic_StreetTargets");
        icStreetTargetsTA.Fill(this.ic_StreetTargets);

        accessHelper.LinkTable("mst_ic_RoofTargets_ac", modelPath + @"\icalt\mst_ic_RoofTargets_ac.mdb", "ic_RoofTargets");
        icRoofTargetsTA.Fill(this.ic_RoofTargets);

        accessHelper.LinkTable("mst_ic_ParkingTargets_ac", modelPath + @"\icalt\mst_ic_ParkingTargets_ac.mdb", "ic_ParkingTargets");
        icParkingTargetsTA.Fill(this.ic_ParkingTargets);
      }
      finally
      {
        accessHelper.Dispose();
      }

      StormwaterControlsDataSetTableAdapters.ICNodeTableAdapter icNodeTA;
      icNodeTA = new StormwaterControlsDataSetTableAdapters.ICNodeTableAdapter(modelPath);
      icNodeTA.Fill(this.ICNode);

      StormwaterControlsDataSetTableAdapters._mdl_roofTargetsTableAdapter mdlRoofTargetsTA;
      mdlRoofTargetsTA = new StormwaterControlsDataSetTableAdapters._mdl_roofTargetsTableAdapter(modelPath);
      mdlRoofTargetsTA.Fill(this._mdl_roofTargets);

      foreach (System.Data.DataRow row in this._mdl_roofTargets.GetErrors())
      {

      }

      StormwaterControlsDataSetTableAdapters._mdl_ParkingTargetsTableAdapter mdlParkingTargetsTA;
      mdlParkingTargetsTA = new StormwaterControlsDataSetTableAdapters._mdl_ParkingTargetsTableAdapter(modelPath);
      mdlParkingTargetsTA.Fill(this._mdl_ParkingTargets);      
    }

    public void InitAltTargetDataTables(string alternativePath)
    {
      StormwaterControlsDataSetTableAdapters.AltStreetTargetsTableAdapter altStreetTargetsTA;
      altStreetTargetsTA = new SystemsAnalysis.DataAccess.StormwaterControlsDataSetTableAdapters.AltStreetTargetsTableAdapter(alternativePath);
      altStreetTargetsTA.Fill(this.AltStreetTargets);

      StormwaterControlsDataSetTableAdapters.AltRoofTargetsTableAdapter altRoofTargetsTA;
      altRoofTargetsTA = new SystemsAnalysis.DataAccess.StormwaterControlsDataSetTableAdapters.AltRoofTargetsTableAdapter(alternativePath);
      altRoofTargetsTA.Fill(this.AltRoofTargets);

      StormwaterControlsDataSetTableAdapters.AltParkingTargetsTableAdapter altParkingTargetsTA;
      altParkingTargetsTA = new SystemsAnalysis.DataAccess.StormwaterControlsDataSetTableAdapters.AltParkingTargetsTableAdapter(alternativePath);
      altParkingTargetsTA.Fill(this.AltParkingTargets);
    }

    public void InitResultsDataTables(string swmmOutputFile)
    {
      XPSWMMResults xpSwmmResults = new XPSWMMResults(swmmOutputFile);
      xpSwmmResults.GetTableE18().CopyToDataTable(this.TableE18, LoadOption.PreserveChanges);
      xpSwmmResults.GetTableE19().CopyToDataTable(this.TableE19, LoadOption.PreserveChanges);
      
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

  public partial class mdl_dirsc_acTableAdapter
  {
    public mdl_dirsc_acTableAdapter(string modelPath)
      : this()
    {
      this.Connection.ConnectionString = StormwaterControlsAdapterSetup.GetModelConnectionString(modelPath);
    }
  }

  public partial class ic_StreetTargetsTableAdapter
  {
    public ic_StreetTargetsTableAdapter(string modelPath)
      : this()
    {
      this.Connection.ConnectionString = StormwaterControlsAdapterSetup.GetModelConnectionString(modelPath);
    }
  }

  public partial class ic_ParkingTargetsTableAdapter
  {
    public ic_ParkingTargetsTableAdapter(string modelPath)
      : this()
    {
      this.Connection.ConnectionString = StormwaterControlsAdapterSetup.GetModelConnectionString(modelPath);
    }
  }

  public partial class ic_RoofTargetsTableAdapter
  {
    public ic_RoofTargetsTableAdapter(string modelPath)
      : this()
    {
      this.Connection.ConnectionString = StormwaterControlsAdapterSetup.GetModelConnectionString(modelPath);
    }
  }

  public partial class _mdl_roofTargetsTableAdapter
  {
    public _mdl_roofTargetsTableAdapter(string modelPath)
      : this()
    {
      this.Connection.ConnectionString = StormwaterControlsAdapterSetup.GetModelConnectionString(modelPath);
    }
  }

  public partial class _mdl_ParkingTargetsTableAdapter
  {
    public _mdl_ParkingTargetsTableAdapter(string modelPath)
      : this()
    {
      this.Connection.ConnectionString = StormwaterControlsAdapterSetup.GetModelConnectionString(modelPath);
    }
  }

  public partial class AltStreetTargetsTableAdapter
  {
    public AltStreetTargetsTableAdapter(string alternativePath)
      : this()
    {
      alternativePath = alternativePath + (alternativePath.EndsWith(Path.DirectorySeparatorChar.ToString()) ? "" :
        Path.DirectorySeparatorChar.ToString());
      this.Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + alternativePath + "alternative_package.mdb";
    }
  }

  public partial class AltRoofTargetsTableAdapter
  {
    public AltRoofTargetsTableAdapter(string alternativePath)
      : this()
    {
      alternativePath = alternativePath + (alternativePath.EndsWith(Path.DirectorySeparatorChar.ToString()) ? "" :
        Path.DirectorySeparatorChar.ToString());
      this.Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + alternativePath + "alternative_package.mdb";
    }
  }

  public partial class AltParkingTargetsTableAdapter
  {
    public AltParkingTargetsTableAdapter(string alternativePath)
      : this()
    {
      alternativePath = alternativePath + (alternativePath.EndsWith(Path.DirectorySeparatorChar.ToString()) ? "" :
        Path.DirectorySeparatorChar.ToString());
      this.Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + alternativePath + "alternative_package.mdb";
    }
  }
}

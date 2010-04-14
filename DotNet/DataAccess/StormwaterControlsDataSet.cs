using SystemsAnalysis.Utils.AccessUtils;
using System.IO;

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

      StormwaterControlsDataSetTableAdapters.ic_RoofTargetsTableAdapter icRoofTargetsTA;
      icRoofTargetsTA = new StormwaterControlsDataSetTableAdapters.ic_RoofTargetsTableAdapter(modelPath);

      StormwaterControlsDataSetTableAdapters.ic_StreetTargetsTableAdapter icStreetTargetsTA;
      icStreetTargetsTA = new StormwaterControlsDataSetTableAdapters.ic_StreetTargetsTableAdapter(modelPath);

      StormwaterControlsDataSetTableAdapters.ic_ParkingTargetsTableAdapter icParkingTargetsTA;
      icParkingTargetsTA = new StormwaterControlsDataSetTableAdapters.ic_ParkingTargetsTableAdapter(modelPath);
      
      AccessHelper accessHelper = new AccessHelper(modelPath + @"\mdbs\StormwaterControls_v12.mdb");
      try
      {
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

// Project: SystemsAnalysis.DataAccess, File: AlternativeDataSet.cs
// Namespace: SystemsAnalysis.DataAccess.AlternativeDataSetTableAdapters, Class: AltDscTableAdapter
// Path: C:\Development\DotNet\DataAccess, Author: Arnel
// Code lines: 98, Size of file: 3.43 KB
// Creation date: 3/31/2008 2:29 PM
// Last modified: 6/2/2008 4:49 PM
using System.IO;

namespace SystemsAnalysis.DataAccess.AlternativeDataSetTableAdapters
{
}

namespace SystemsAnalysis.DataAccess.AlternativeDataSetTableAdapters
{
  public partial class AltDscTableAdapter
  {
    public AltDscTableAdapter(string alternativePath)
      : this()
    {
      alternativePath = alternativePath + (alternativePath.EndsWith(Path.DirectorySeparatorChar.ToString()) ? "" :
      Path.DirectorySeparatorChar.ToString());
      this.Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + alternativePath + "alternative_package.mdb";
    }
  }

  public partial class AltLinksTableAdapter
  {
    public AltLinksTableAdapter(string alternativePath)
      : this()
    {
      alternativePath = alternativePath + (alternativePath.EndsWith(Path.DirectorySeparatorChar.ToString()) ? "" :
      Path.DirectorySeparatorChar.ToString());
      this.Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + alternativePath + "alternative_package.mdb";
    }
  }

  public partial class AltNodesTableAdapter
  {
    public AltNodesTableAdapter(string alternativePath)
      : this()
    {
      alternativePath = alternativePath + (alternativePath.EndsWith(Path.DirectorySeparatorChar.ToString()) ? "" :
      Path.DirectorySeparatorChar.ToString());
      this.Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + alternativePath + "alternative_package.mdb";
    }
  }

  public partial class AltSpecLinksTableAdapter
  {
    public AltSpecLinksTableAdapter(string alternativePath)
      : this()
    {
      alternativePath = alternativePath + (alternativePath.EndsWith(Path.DirectorySeparatorChar.ToString()) ? "" :
      Path.DirectorySeparatorChar.ToString());
      this.Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + alternativePath + "alternative_package.mdb";
    }
  }

  public partial class AltSpecLinkDataTableAdapter
  {
    public AltSpecLinkDataTableAdapter(string alternativePath)
      : this()
    {
      alternativePath = alternativePath + (alternativePath.EndsWith(Path.DirectorySeparatorChar.ToString()) ? "" :
      Path.DirectorySeparatorChar.ToString());
      this.Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + alternativePath + "alternative_package.mdb";
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

  /// <summary>
  /// Alt pip xp table adapter
  /// </summary>
  public partial class AltPipXPTableAdapter
  {
    /// <summary>
    /// Create alt pip xp table adapter
    /// </summary>
    /// <param name="alternativePath">Alternative path</param>
    public AltPipXPTableAdapter(string alternativePath)
      : this()
    {
      alternativePath = alternativePath + (alternativePath.EndsWith(Path.DirectorySeparatorChar.ToString()) ? "" :
      Path.DirectorySeparatorChar.ToString());
      this.Connection.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + alternativePath + "alternative_package.mdb";
    } // AltPipXpTableAdapter(alternativePath)
  } // class AltPipXpTableAdapter
}


namespace SystemsAnalysis.DataAccess
{
  public partial class AlternativeDataSet
  {
  }
}

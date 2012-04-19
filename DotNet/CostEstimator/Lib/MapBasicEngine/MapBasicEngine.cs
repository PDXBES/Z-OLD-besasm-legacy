using System;
using System.Data.OleDb;
using System.IO;

//using MapInfo;
using SystemsAnalysis.Utils.MapInfoUtils;

namespace SystemsAnalysis.Utils.MapInfoUtils
{
  /// <summary>
  /// Facilitates access to the MapInfo OLE object. This class is a wrapper
  /// around a Visual Basic class (SystemsAnalysis.Utilities.MapInfoUtils.MapBasicWrapper).
  /// This is necessary since Visual Basic is much more graceful with regards to 
  /// late-binding OLE objects and C# has trouble type-casting certain MapInfo
  /// objects due to quirkiness in the MapBasicApplication implementation.	
  /// </summary>
  public class MapBasicEngine
  {
    //private MapInfo.MapInfoApplicationClass mi;
    private MapBasicWrapper miWrapper;
    private string library;
    private string libraryName;
    private bool switchedToMapInfo;

    /// <summary>
    /// Creates a new instance of the MapBasicEngine
    /// </summary>
    public MapBasicEngine()
    {
      this.miWrapper = new MapBasicWrapper();
      this.switchedToMapInfo = false;
    }

    /// <summary>
    /// Write a global variable that MapBasic programs can use for
    /// processing.
    /// </summary>
    /// <param name="globalName">The name of the global variable to write</param>
    /// <param name="globalVariable">The value of the global variable to write</param>
    public void WriteGlobal(string globalName, string globalVariable)
    {
      this.miWrapper.WriteGlobal(globalName, globalVariable);
    }
    /// <summary>
    /// Reads a global variable from a MapBasic programs.
    /// </summary>
    /// <param name="globalName">The name of the global variable to read</param>
    /// <returns>The value of the global variable</returns>
    /// 
    public string ReadGlobal(string globalName)
    {
      return this.miWrapper.ReadGlobal(globalName);
    }

    /// <summary>
    /// Executes an MapBasic executable (*.MBX file) within the current
    /// MapInfo session.
    /// </summary>
    /// <param name="library">The full path to the .MBX file to execute</param>
    public void ExecuteLibrary(string library)
    {
      if (this.library != library)
      {
        try
        {
          this.ExitCurrentLibrary();
        }
        catch
        {
          this.miWrapper = new MapBasicWrapper();
        }
        finally
        {
          this.library = Path.GetFullPath(library);
          this.libraryName = System.IO.Path.GetFileNameWithoutExtension(library);
        }
      }
      if (!File.Exists(this.library))
      {
        this.library = "";
        throw new Exception("MapBasic Library not found: " + this.library + "'");
      }
      this.miWrapper.ExecuteLibrary(this.library);
      //this.miWrapper.WriteGlobal("gReturnStatus", "INITIALIZED");					
      return;
    }

    /// <summary>
    /// Halts the currently running .MBX file. The current .MBX file is required
    /// to have a function menu item with ID=9999 that calls the MapInfo command
    /// "End Program".
    /// </summary>
    public void ExitCurrentLibrary()
    {
      this.miWrapper.RunMenuCommand("9999");
    }
    /// <summary>
    /// Executes the MapBasic menu command referred to by commandID.
    /// </summary>
    /// <param name="commandID">The command ID of the MapBasic command to execute</param>
    public void ExecuteLibraryFunction(string commandID)
    {
      this.miWrapper.RunMenuCommand(commandID);
    }

    /// <summary>
    /// Closes MapInfo. If the MapInfo session is not visible, the MapInfoW process
    /// will be terminated. If the MapInfo session is visible, then MapInfo will
    /// continue to execute, but it will no longer be tied to the calling application.
    /// </summary>
    public void CloseMapInfo()
    {
      this.miWrapper.CloseMapInfo();
      this.switchedToMapInfo = false;
    }

    /// <summary>
    /// Shows/hides the MapInfo user interface
    /// </summary>
    public bool Visible
    {
      get
      {
        return this.miWrapper.Visible;
      }
      set
      {
        this.miWrapper.Visible = value;
      }
    }

    /// <summary>
    /// If the MapInfo session is visible, this will maximize the MapInfo session
    /// </summary>
    public void SwitchToMapInfo()
    {
      this.miWrapper.SwitchToMapInfo();
      this.switchedToMapInfo = true;
    }

    /// <summary>
    /// Indicates if this MapInfo session has been given focus and maximized
    /// </summary>
    public bool SwitchedToMapInfo
    {
      get
      {
        return this.switchedToMapInfo;
      }
    }

    public string GetError()
    {
      return this.miWrapper.GetError();
    }
  }
}

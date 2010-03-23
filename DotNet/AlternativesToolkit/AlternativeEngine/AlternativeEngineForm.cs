using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using System.Data.Odbc;
using SystemsAnalysis.Utils.StatusTextBox;
using SystemsAnalysis.ModelConstruction.AlternativesToolkit;
using SystemsAnalysis.ModelConstruction;
using SystemsAnalysis.Utils.MapInfoUtils;
using SystemsAnalysis.Modeling.ModelResults;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Utils.AccessUtils;
using SystemsAnalysis.Modeling.Alternatives;
using SystemsAnalysis.Types;

namespace SystemsAnalysis.ModelConstruction.AlternativesToolkit
{
  public partial class AlternativeEngineForm : Form
  {
    private string templateDirectory;
    private bool debugMode;
    private IGISExecuter mbExecuter;
    private string applicationDirectory;
    private string configPath;
    private AlternativeConfiguration altConfig;
    private string version = AlternativeConfiguration.MasterVersion;
    private DataSet aggregatorDS;
    private List<AlternativePackage> alternativePackages;

    private AltEngineConfiguration config;

    /// <summary>
    /// Constructor for the AlternativeEngine. Loads the Splash Screen and
    /// performs some minor instatiation tasks.
    /// </summary>        
    public AlternativeEngineForm()
    {
      this.Cursor = Cursors.WaitCursor;
      this.Visible = false;
      System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ThreadStart(DoSplash));
      th.Start();

      //
      // Required for Windows Form Designer support
      //
      InitializeComponent();

      this.debugMode = false;
      this.applicationDirectory = Application.StartupPath + "\\"; // Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\BES_SystemsAnalysis\\Alternatives_Toolkit\\";

      if (!Directory.Exists(this.applicationDirectory))
      {
        Directory.CreateDirectory(this.applicationDirectory);
      }

      engineOutput.AddStatus("Entering AlternativeEngine.", SeverityLevel.Info);

      LoadConfig(false);

      th.Abort();
      this.Visible = true;
      this.Refresh();
      this.Cursor = Cursors.Default;
      if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
      {
        try
        {
          engineOutput.AddStatus("Running from ClickOnce mode.");
          if (System.Deployment.Application.ApplicationDeployment.CurrentDeployment.IsFirstRun)
          {
            engineOutput.AddStatus("First run of new version... copying shortcut file.");
            File.Copy(Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + "\\Programs\\BES Systems Analysis\\Alternatives Toolkit.appref-ms", Environment.GetFolderPath(Environment.SpecialFolder.StartMenu) + "\\EMGAATS\\Alternatives Toolkit.appref-ms", true);
          }
        }
        catch (System.Deployment.Application.InvalidDeploymentException ex)
        {
          engineOutput.AddStatus("Deployment exception: " + ex.Message);
        }
        catch (Exception ex)
        {
          engineOutput.AddStatus(ex.Message, SeverityLevel.Warning);
        }
      }
      else
      {
        engineOutput.AddStatus("Application not run from ClickOnce mode.");
      }

    }

    /// <summary>
    /// Loads the configuration file from user's application directory.  If the default 
    /// configuration file cannot be found in this location, it will be created using the
    /// embedded resource "default_settings.xml". If debugMode is enabled the settings file
    /// will use the hard-coded "debug_settings.xml".  Also binds MRU lists to history block
    /// in the onfiguration file.
    /// </summary>
    private void LoadConfig(bool debugSettings)
    {
      try
      {
        if (debugSettings)
        {
          this.configPath = @"Debug_Settings.xml";
        }
        else
        {
          this.configPath = this.applicationDirectory + "Default_Settings.xml";
          if (!File.Exists(this.configPath))
          {
            this.engineOutput.AddStatus("Could not find settings file. Reading embedded version...", SeverityLevel.Warning);
            Stream configStream;

            configStream = GetType().Assembly.GetManifestResourceStream(
              //"SystemsAnalysis.ModelConstruction.AlternativesToolkit.Default_Settings.xml");                    
                "SystemsAnalysis.ModelConstruction.AlternativesToolkit.Default_Settings.xml");
            StreamReader streamReader = new System.IO.StreamReader(configStream);

            StreamWriter streamWriter;
            streamWriter = new System.IO.StreamWriter(this.configPath, false);

            streamWriter.Write(streamReader.ReadToEnd());
            streamWriter.Close();
          }
        }

        //If the configuration file cannot be found, read the embedded config file and write
        //it to disk with the default name.                
        this.engineOutput.AddStatus("Reading Settings File from: " + this.configPath, SeverityLevel.Info);

        this.config = new AltEngineConfiguration();

        config.ReadXml(this.configPath);

        if (config.FileHistory.Count != 1)
        {
          config.FileHistory.Clear();
          config.FileHistory.AddFileHistoryRow(4);
        }

        this.templateDirectory = config.ProgramSettings[0].AlternativeTemplate;
        this.engineOutput.AddUpdate("Alternative Template located at:\n	" + this.templateDirectory + "\n");
        this.cmbBaseModel.DataSource = this.config.BaseModel;
        this.cmbOutputModel.DataSource = this.config.OutputModel;
        this.txtFocusAreas.Text = this.config.ProgramSettings[0].FocusAreasTable;

        mbExecuter = new MapBasicExecuter(this.config, this.debugMode, this.applicationDirectory);
        this.mbExecuter.StatusChanged += new SystemsAnalysis.Utils.Events.OnStatusChangedEventHandler(mbExecuter_StatusChanged);

        lblCurrentConfigFile.Text = "Current Settings File: " + this.configPath;

        this.engineOutput.AddStatus("Alternative Engine configuration loaded succesfully.");
      }
      catch (Exception ex)
      {
        this.engineOutput.AddStatus(ex.ToString(), SeverityLevel.Error);
        this.engineOutput.AddStatus("Error loading configuration file. See status log for details.", SeverityLevel.Error);
      }
    }

    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new AlternativeEngineForm());
    }

    private static void DoSplash()
    {
      DoSplash(false);
    }

    /// <summary>
    /// Loads the Splash Screen during start-up.
    /// </summary>
    private static void DoSplash(bool waitForClick)
    {
      string versionText = "x.x.x.x";
      string dateText = "1/1/1900";
      if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
      {
        Version v = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
        versionText = v.Major + "." + v.Minor + "." + v.Build + "." + v.Revision;
        dateText = System.IO.File.GetLastWriteTime(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString("MMMM dd yyyy");
      }
      else
      {
        string version = System.Reflection.Assembly.GetExecutingAssembly().GetName(false).Version.ToString();
        int minorVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName(false).Version.Minor;
        int majorVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName(false).Version.Major;
        int build = System.Reflection.Assembly.GetExecutingAssembly().GetName(false).Version.Build;
        int revision = System.Reflection.Assembly.GetExecutingAssembly().GetName(false).Version.Revision;

        versionText = majorVersion + "." + minorVersion + "." + build + "." + revision;
        FileInfo fi = new FileInfo("AlternativesToolkit.exe");
        dateText = fi.CreationTime.Date.ToString("MMMM dd yyyy");
      }

      SplashScreen sp = new SplashScreen(versionText, dateText);
      sp.ShowDialog(waitForClick);
    }

    #region MapBasic execution functions
    /// <summary>
    /// Creates a new, empty alternative by copying all alternative files from the
    /// alternative template directory.  Also creates the alternative configuration file.
    /// </summary>
    /// <param name="baseModel">The path to the EMGAATS model within which to create a new alternative.</param>
    /// <param name="alternativeName">The name of the new alternative to create.</param>
    /// <returns>Returns true if the new alternative was succesfully created, otherwise false.</returns>
    private bool NewAlternative(string baseModel, string alternativeName)
    {
      try
      {
        this.Cursor = Cursors.WaitCursor;

        this.engineOutput.AddStatus("Attempting to create new alternative...");
        this.engineOutput.AddStatus("Base Model: 'file:" + baseModel + "'");
        this.engineOutput.AddStatus("Alternative Name: '" + alternativeName + "'");

        string alternativePath = GetAlternativePath(baseModel, alternativeName);
        if (File.Exists(alternativePath + "alternative_configuration.xml"))
        {
          this.engineOutput.AddStatus(
              "An alternative with the specified name already exists... ",
              SeverityLevel.Warning);
          if (MessageBox.Show("An alternative with that name already exists... Overwrite?",
              "Alternative Exists!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
          {
            this.engineOutput.AddUpdate("Declined to overwrite.\n", SeverityLevel.Warning);
            return false;
          }
          System.IO.Directory.Delete(alternativePath, true);
          this.engineOutput.AddUpdate("Overwriting existing alternative.\n", SeverityLevel.Warning);
        }

        CopyDirectory(this.templateDirectory, alternativePath, true);

        altConfig = new AlternativeConfiguration(alternativeName, baseModel, this.txtDefaultNodeName.Text);
        altConfig.AddHistory("CREATED");

        this.mbExecuter.ExecuteFunctionGroup("Create", baseModel, alternativeName, "", false);

        this.engineOutput.AddStatus("Alternative '" + alternativeName + "' Created!", SeverityLevel.Attention);
      }
      catch (Exception ex)
      {
        this.engineOutput.AddStatus("Error creating new alternative: " +
            ex.ToString(), SeverityLevel.Error);
        return false;
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
      return true;

    }

    /// <summary>
    /// Launches the MapInfo Workbench for interactively editing an alternative.
    /// </summary>
    /// <param name="baseModel">The path to the EMGAATS model containing the alternative to be edited.</param>
    /// <param name="alternativeName">The name of the already existing alternative to edit.</param>
    private void EditAlternative(string baseModel, string alternativeName)
    {
      try
      {
        string DefaultNodeSuffix;
        DefaultNodeSuffix = this.txtDefaultNodeName.Text;

        string alternativePath = GetAlternativePath(baseModel, alternativeName);

        if (!File.Exists(alternativePath + "alternative_configuration.xml"))
        {
          this.engineOutput.AddUpdate(
              "Specified alternative does not exist... ",
              SeverityLevel.Warning);
        }
        foreach (string f in Directory.GetFiles(templateDirectory))
        {
          if (!File.Exists(alternativePath + Path.GetFileName(f)))
          {
            File.Copy(f, alternativePath + Path.GetFileName(f));
            this.engineOutput.AddStatus("Could not find alt file: " + Path.GetFileName(f) + "; Copying from template.", SeverityLevel.Warning);
          }
        }

        this.mbExecuter.ExecuteFunctionGroup("Edit", baseModel, alternativeName, "", true);

        altConfig.DefaultNodeSuffix = DefaultNodeSuffix;
        altConfig.AddHistory("EDITED");
      }
      catch (Exception ex)
      {
        this.engineOutput.AddStatus(ex.ToString(), SeverityLevel.Error);
        this.engineOutput.AddStatus("Error editing alternative. See status log for details.", SeverityLevel.Error);
      }
      config.UpdateBaseModelHistory(baseModel);
      return;
    }

    /// <summary>
    /// Creates a new EMGAATS model that includes all changes specified by the selected alternative.
    /// </summary>
    /// <param name="baseModel">The path to the EMGAATS model containing the alternative to apply.</param>
    /// <param name="alternativeName">The name of the existing alternative that will be used to create a new EMGAATS model.</param>
    /// <param name="outputModel">The to the directory that will contain the EMGAATS model representing the alternative design.</param>
    private void ApplyAlternative(string baseModel, string alternativeName, string outputModel)
    {
      //1) Delete any data in the outputModel directory
      //2) Validate the alternative, using QC from linkappend tool
      //  - If fail, write errors to MapInfo table and enter editing session
      //3) Copy the necessary files from the baseModel to the outputModel via the CopyModel function
      //4) Run the MapBasic function to perform the alternative operations
      //5) Validate the new model, using EMGAATS QC workspace
      //  - If fail, write errors to MapInfo table and enter editing session
      //6) Relink the new model tables

      string alternativePath = GetAlternativePath(baseModel, alternativeName);

      try
      {
        this.Cursor = Cursors.WaitCursor;

        this.engineOutput.AddStatus("Attempting to apply alternative...");
        this.engineOutput.AddStatus("Base Model: 'file:" + baseModel + "'");
        this.engineOutput.AddStatus("Alternative Name: '" + alternativeName + "'");
        this.engineOutput.AddStatus("Output Model: 'file:" + outputModel + "'");
        int existingFileCount = Directory.GetFiles(outputModel, "*", SearchOption.AllDirectories).Length;
        if (existingFileCount > 0)
        {
          this.engineOutput.AddStatus(
              "The Output Model directory already exists... ", SeverityLevel.Warning);
          if (MessageBox.Show("The Output Model directory contains " + existingFileCount + " files. Any model files will be overwritten. Continue?",
              "Overwrite Model Files?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
          {
            this.engineOutput.AddUpdate("Declined to overwrite.\n", SeverityLevel.Warning);
            return;
          }
          try
          {
            DeleteModel(outputModel);
          }
          catch (System.IO.IOException ex)
          {
            this.engineOutput.AddStatus(ex.ToString(), SeverityLevel.Error);
            this.engineOutput.AddStatus("Error overwriting existing data. See status log for details.", SeverityLevel.Error);
            return;
          }
          this.engineOutput.AddUpdate("Overwriting existing directory.\n", SeverityLevel.Warning);
        }

        CopyModel(baseModel, outputModel);

        try
        {
          this.engineOutput.AddStatus("Relinking Model...");
          ModelBuilder.RelinkModel(outputModel);
        }
        catch (Exception ex)
        {
          this.engineOutput.AddStatus(ex.ToString(), SeverityLevel.Error);
          this.engineOutput.AddStatus("Error relinking model. See status log for details.", SeverityLevel.Error);
          return;
        }
        this.engineOutput.AddStatus("Updating Model Configuration...");

        //Update the alternative configuration file                                                               
        ModelConfigurationDataSet modelConfigDS = new ModelConfigurationDataSet(outputModel);
        modelConfigDS.AddAlternativeRow(alternativeName, baseModel);
        //ConfigurationHelper modelConfigHelper;
        //modelConfigHelper = new ConfigurationHelper(outputModel);                
        //modelConfigHelper.AddAlternativeRow(alternativeName, baseModel);

        if (Directory.Exists(alternativePath + "error_check"))
        {
          Directory.Delete(alternativePath + "error_check", true);
        }
        Directory.CreateDirectory(alternativePath + "error_check");

        this.mbExecuter.ExecuteFunctionGroup("Apply", baseModel, alternativeName, outputModel, false);

        SystemsAnalysis.Utils.INIFile modelIni;
        modelIni = new SystemsAnalysis.Utils.INIFile(outputModel + "model.ini");

        modelIni.WriteINIString("Admin", "CreatedVia", "Alternative");
        //modelIni.WriteINIString("Admin", "AlternativeApplied", System.DateTime.Now.ToString(@"MM/dd/yyyy hh:mm:ss tt"));
        modelIni.WriteINIString("Admin", "CreationDetail", baseModel + alternativeName);
        modelIni.WriteINIString("Admin", "RefreshMDBs", "False");

        altConfig.AddHistory("APPLIED to: " + outputModel);

        this.engineOutput.AddStatus("Output model available at: file:" + outputModel);
        this.engineOutput.AddStatus("Alternative Succesfully Applied.", SeverityLevel.Attention);
      }

      catch (Exception ex)
      {
        this.engineOutput.AddStatus("Error applying alternative: " +
            ex.ToString(), SeverityLevel.Error);
        this.engineOutput.SaveFile(alternativePath + "error_check\\errorlog.txt");
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }

      try
      {
        if (Directory.GetFiles(alternativePath + "error_check").Length != 0)
        {
          this.engineOutput.AddStatus("Errors and/or warnings were found in your alternative. Examine the files in file:" + alternativePath + "error_check\" to view and correct these errors.", SeverityLevel.Error);
          MessageBox.Show("Errors and/or warnings were found in your alternative. Examine the files in\n" + alternativePath + "error_check \nto view and correct these errors.", "Alternative Failed QC Check", MessageBoxButtons.OK, MessageBoxIcon.Error);
          this.engineOutput.AddStatus("Errors and/or warning found in alternative.", SeverityLevel.Error);
        }
        else
        {
          this.engineOutput.AddStatus("No alternative errors found!");
          MessageBox.Show("No warnings or errors were found in your alternative. Although no errors or warnings were found, manual QC is an essential part of the alternative build process. For further QC, open the Output Model in EMGAATS and select 'Generate QC Workspaces'. Examine the Output Model until you are confident it represents the intended system configuration.  At this point you may deploy the Output Model and perform all standard modeling operations on it.", "Applied Alternative OK!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

        }
      }
      catch (Exception ex)
      {
        this.engineOutput.AddStatus("Error reading alternative QC files: " +
            ex.ToString(), SeverityLevel.Error);
      }

      config.UpdateBaseModelHistory(baseModel);
      config.UpdateOutputModelHistory(outputModel);
      return;
    }

    #endregion

    #region Auto-Conveyance functions



    #endregion

    #region Utilities
    private void CopyDirectory(string src, string dst, bool includeFiles)
    {
      string[] files;
      if (!dst.EndsWith(System.IO.Path.DirectorySeparatorChar.ToString()))
      {
        dst += System.IO.Path.DirectorySeparatorChar;
      }
      if (!System.IO.Directory.Exists(dst))
      {
        System.IO.Directory.CreateDirectory(dst);
      }
      files = System.IO.Directory.GetFileSystemEntries(src);
      foreach (string s in files)
      {
        if (System.IO.Directory.Exists(s))
        {
          CopyDirectory(s, dst + System.IO.Path.GetFileName(s), true);
        }
        else if (includeFiles)
        {
          System.IO.File.Copy(s, dst + System.IO.Path.GetFileName(s), true);
        }
      }
      return;
    }

    private void CopyModel(string src, string dst)
    {
      foreach (AltEngineConfiguration.BaseDirectoryRow dRow in this.config.BaseModelComponents[0].GetBaseDirectoryRows())
      {
        bool recurse = dRow.Recurse;
        bool includeFiles = dRow.IncludeFiles;
        string dir = dRow.BaseDirectory_text;
        dir += dir.EndsWith("\\") ? "" : "\\";
        /*if (!Directory.Exists(src + dir))
        {
            this.engineOutput.AddStatus("Model directory '" + src + dir + "' does not exist.", SeverityLevel.Warning);
            continue;
        }*/
        if (recurse && includeFiles)
        {
          CopyDirectory(src + dir, dst + dir, true);
        }
        else if (!recurse && includeFiles)
        {
          System.IO.Directory.CreateDirectory(dst + dir);
          foreach (string f in System.IO.Directory.GetFiles(src + dir))
          {
            if (System.IO.File.Exists(f))
            {
              System.IO.File.Copy(f, dst + dir + System.IO.Path.GetFileName(f), true);
            }
          }
        }
        else if (!recurse && !includeFiles)
        {
          System.IO.Directory.CreateDirectory(dst + dir);
        }
        else if (recurse && !includeFiles)
        {
          CopyDirectory(src, dst, false);
        }
      }

      foreach (AltEngineConfiguration.BaseFileRow fRow in this.config.BaseModelComponents[0].GetBaseFileRows())
      {
        int filePatternIndex = fRow.BaseFile_text.LastIndexOf("\\");
        string filePattern = fRow.BaseFile_text.Substring(filePatternIndex + 1);
        string dir = filePatternIndex >= 0 ?
            fRow.BaseFile_text.Substring(0, filePatternIndex + 1) : "";
        if (!Directory.Exists(src + dir))
        {
          this.engineOutput.AddStatus("Model directory '" + src + dir + "' does not exist.", SeverityLevel.Warning);
          continue;
        }
        string[] files = System.IO.Directory.GetFiles(src + dir, filePattern);
        foreach (string f in files)
        {
          System.IO.File.Copy(f, dst + dir + System.IO.Path.GetFileName(f), true);
          System.IO.File.SetAttributes(dst + dir + System.IO.Path.GetFileName(f),
              System.IO.File.GetAttributes(f) & ~System.IO.FileAttributes.ReadOnly);
        }
      }

      return;
    }

    private void DeleteModel(string model)
    {
      foreach (AltEngineConfiguration.BaseFileRow fRow in this.config.BaseModelComponents[0].GetBaseFileRows())
      {
        int filePatternIndex = fRow.BaseFile_text.LastIndexOf("\\");
        string filePattern = fRow.BaseFile_text.Substring(filePatternIndex + 1);
        string dir = filePatternIndex >= 0 ?
            fRow.BaseFile_text.Substring(0, filePatternIndex + 1) : "";
        if (!Directory.Exists(model + dir))
        {
          continue;
        }
        string[] files = System.IO.Directory.GetFiles(model + dir, filePattern);
        foreach (string f in files)
        {
          if (File.Exists(f))
          {
            try
            {
              File.Delete(f);
            }
            catch (Exception ex)
            {
              engineOutput.AddStatus("Failed to delete file '" + f + "': " + ex.Message, SeverityLevel.Warning);
            }

          }
        }
      }

      foreach (AltEngineConfiguration.BaseDirectoryRow dRow in this.config.BaseModelComponents[0].GetBaseDirectoryRows())
      {
        bool recurse = dRow.Recurse;
        bool includeFiles = dRow.IncludeFiles;
        string dir = dRow.BaseDirectory_text;
        dir += dir.EndsWith("\\") ? "" : "\\";
        if (Directory.Exists(model + dir))
        {
          try
          {
            Directory.Delete(model + dir, true);
          }
          catch (Exception ex)
          {
            engineOutput.AddStatus("Failed to delete directory '" + model + dir + "': " + ex.Message, SeverityLevel.Warning);
          }
          //TODO: Need to check for read-only files and not crash when deleting them
          //File.SetAttributes(model + dir, FileAttributes.Normal);
        }
      }
    }

    private void UpdateAlternativeList()
    {
      try
      {
        grdAlternativeHistory.DataSource = null;
        grdFocusAreaChooser.DataSource = new DataTable();
        alternativePackages = new List<AlternativePackage>();

        string baseModel = this.cmbBaseModel.Text;
        baseModel += baseModel.EndsWith("\\") ? "" : "\\";
        if (!ValidateBaseModelInput(baseModel))
        {
          return;
        }
        this.cmbAlternativeList.DataSource = this.GetAlternativeList(baseModel);
        cmbAlternativeList.SelectedIndex = -1;
        cmbAlternativeList.Text = "";
      }
      catch (Exception ex)
      {
        engineOutput.AddStatus(ex.ToString(), SeverityLevel.Error);
        engineOutput.AddStatus("Error updating alternative list. See status log for details.", SeverityLevel.Error);
      }
    }

    private List<string> GetAlternativeList(string baseModelPath)
    {
      List<string> altList;
      altList = new List<string>();
      string[] files;
      try
      {
        files = System.IO.Directory.GetFileSystemEntries(baseModelPath + "alternatives\\");
        engineOutput.AddStatus("Reading " + files.Length + " alternatives from BaseModel '" + baseModelPath + "'.", SeverityLevel.Info);
      }
      catch
      {
        return altList;
      }
      foreach (string s in files)
      {
        string altFile = s + "\\alternative_configuration.xml";
        if (File.Exists(altFile))
        {
          try
          {
            AlternativeConfiguration alt = new AlternativeConfiguration(altFile);
            //Patch alternative data structure if versions don't match
            if (alt.AlternativeVersion != this.version)
            {
              MapBasicEngine mbEngine = new MapBasicEngine();
              mbEngine.ExecuteLibrary(this.config.UpdateVersionPatchMBX);
              mbEngine.WriteGlobal("gTemplateDirectory", templateDirectory);
              mbEngine.WriteGlobal("gVersion", this.version);
              mbEngine.WriteGlobal("gBaseModelPath", baseModelPath);
              mbEngine.WriteGlobal("gAlternativePath", s);
              mbEngine.ExecuteLibraryFunction("10000");
              mbEngine.ExitCurrentLibrary();
              mbEngine.CloseMapInfo();

              alt.UpdateVersion(this.version);
            }
            altList.Add(alt.AlternativeName);
          }
          catch (Exception ex)
          {
            engineOutput.AddStatus(ex.Message, SeverityLevel.Warning);
            engineOutput.AddStatus("Could not process alternative '" + s + "'", SeverityLevel.Warning);
          }
        }
      }
      return altList;
    }

    private void SwitchToDefaultView()
    {
      this.tabControl.SelectedTab = this.tabControl.Tabs["EngineStatus"];
    }

    private bool ValidateBaseModelInput(string baseModel)
    {
      if (baseModel == "" || baseModel == null || baseModel == "\\")
      {
        MessageBox.Show("Please specify a base model.",
            "Base model not specified");
      }
      else if (!Directory.Exists(baseModel))
      {
        MessageBox.Show("The base model directory does not exist, please respecify the base model.",
            "Could not find base model");
      }
      else if (!File.Exists(baseModel + "model.INI"))
      {
        MessageBox.Show("The base model does not contain a model.ini file, please respecify the base model.",
            "Base model is invalid");
      }
      else if (baseModel.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
      {
        MessageBox.Show("Base model name contains invalid characters.",
            "Base model is invalid",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
      }
      else
      {
        return true;
      }
      return false;
    }
    private bool ValidateAlternativeInput(string baseModel, string alternativeName)
    {
      if (!ValidateBaseModelInput(baseModel))
      {
        return false;
      }

      if (alternativeName == null || alternativeName == "")
      {
        MessageBox.Show("Please specify a name for the alternative",
            "Alternative name not specified");
      }
      else if (alternativeName.ToLower() == "aggregate")
      {
        MessageBox.Show("The name 'aggregate' is reserved for internal use.", "Invalid Alternative Name");
      }
      else if (alternativeName.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
      {
        MessageBox.Show("Alternative name contains invalid characters.",
            "Alternative name is invalid",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
      }
      else if (this.txtDefaultNodeName.Text.Length != 3)
      {
        MessageBox.Show("Please specify a 3 character default node name.", "Default Node Name Not Specified");
      }
      else
      {
        return true;
      }
      return false;
    }
    private bool ValidateOutputModelInput(string baseModel, string alternativeName, string outputModel)
    {
      if (!ValidateAlternativeInput(baseModel, alternativeName))
      {
        return false;
      }

      if (baseModel == outputModel)
      {
        MessageBox.Show("Output Model must not be the same as the Base Model.",
            "Output Model is invalid",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
        return false;
      }

      if (outputModel == null || outputModel == "" || outputModel == "\\")
      {
        MessageBox.Show("Please specify a directory for the Output Model.",
            "Output Model is invalid",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
      }
      else if (outputModel.IndexOfAny(Path.GetInvalidPathChars()) > 0)
      {
        MessageBox.Show("Output Model directory contains invalid characters.",
            "Output Model is invalid",
            MessageBoxButtons.OK,
            MessageBoxIcon.Error);
      }
      else if (!Directory.Exists(outputModel))
      {
        try
        {
          Directory.CreateDirectory(outputModel);
          return true;
        }
        catch
        {
          MessageBox.Show("Could not create output model directory.",
         "Output Model is invalid",
         MessageBoxButtons.OK,
         MessageBoxIcon.Error);
        }
      }
      else
      {
        return true;
      }
      return false;
    }

    #endregion

    #region Miscellaneous Events

    private void engineOutput_LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start(e.LinkText);
    }
    private void engineOutput_TextChanged(object sender, EventArgs e)
    {
      this.statusBar.Panels[0].Text = engineOutput.CurrentStatus;
    }

    private void txtDebugMode_CheckedChanged(object sender, System.EventArgs e)
    {

      this.debugMode = txtDebugMode.Checked;
      if (debugMode)
      {
        this.configPath = @"Debug_Settings.xml";
      }
      else
      {
        this.configPath = this.applicationDirectory + "Default_Settings.xml";
      }
      lblCurrentConfigFile.Text = this.configPath;
    }

    #endregion

    #region UI Event Handlers
    private void alternativeExplorerBar_ItemClick(object sender, Infragistics.Win.UltraWinExplorerBar.ItemEventArgs e)
    {
      switch (e.Item.Key)
      {
        case "Exit":
          this.Close();
          break;
        default:
          this.tabControl.SelectedTab = this.tabControl.Tabs[e.Item.Key];
          break;
      }
    }

    private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
    {
      //SplashScreen sp = new SplashScreen();
      //sp.ShowDialog(true);
      DoSplash(true);
    }

    private void cmbBaseModel_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateAlternativeList();
      cmbAlternativeList.SelectedIndex = -1;
    }

    private void mbExecuter_StatusChanged(SystemsAnalysis.Utils.Events.StatusChangedArgs e)
    {
      this.engineOutput.AddStatus(e.NewStatus, SeverityLevel.Info);
    }

    private void cmbAlternativeList_SelectionChanged(object sender, EventArgs e)
    {
      if (cmbAlternativeList.SelectedIndex == -1)
      {
        this.grdAlternativeHistory.DataSource = null;
        return;
      }
      string baseModel = this.cmbBaseModel.Text;
      baseModel += baseModel.EndsWith("\\") ? "" : "\\";
      string alternativeName = (string)cmbAlternativeList.Value;

      string alternativePath = GetAlternativePath(baseModel, alternativeName);

      altConfig = new AlternativeConfiguration(alternativeName, baseModel);

      this.txtDefaultNodeName.Text = altConfig.DefaultNodeSuffix;// GetDefaultNodeSuffix(alternativePath);
      this.grdAlternativeHistory.DataSource = altConfig;
      this.grdAlternativeHistory.DataMember = "History";
    }

    private void btnEditAlternative_Click(object sender, EventArgs e)
    {
      string baseModel;
      string alternativeName;

      baseModel = this.cmbBaseModel.Text;
      baseModel += baseModel.EndsWith("\\") ? "" : "\\";
      alternativeName = this.cmbAlternativeList.Text;

      if (GetAlternativeList(baseModel).Count == 0)
      {
        MessageBox.Show("The selected base model does not contain any alternatives.", "No Alternatives found");
        return;
      }

      if (ValidateAlternativeInput(baseModel, alternativeName))
      {
        SwitchToDefaultView();
        EditAlternative(baseModel, alternativeName);
      }
    }

    private void btnNewAlternative_Click(object sender, EventArgs e)
    {
      string alternativeName = (string)cmbAlternativeList.Text;
      string baseModel = this.cmbBaseModel.Text;
      baseModel += baseModel.EndsWith("\\") ? "" : "\\";

      string alternativePath = GetAlternativePath(baseModel, alternativeName);

      if (!ValidateAlternativeInput(baseModel, alternativeName))
      {
        return;
      }
      if (!NewAlternative(baseModel, alternativeName))
      {
        return;
      }
      UpdateAlternativeList();


      if (MessageBox.Show("Begin editing alternative '" + alternativeName + "'?\n(This will launch the MapInfo Workbench)", "Begin Editing?",
          MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
      {
        EditAlternative(baseModel, alternativeName);
        SwitchToDefaultView();
      }
      UpdateAlternativeList();
      config.UpdateBaseModelHistory(baseModel);
      return;
    }

    private void btnApplyAlternative_Click(object sender, EventArgs e)
    {
      string baseModel = "";
      string outputModel = "";
      string alternativeName = "";

      try
      {
        baseModel = this.cmbBaseModel.Text;
        baseModel += baseModel.EndsWith("\\") ? "" : "\\";
        outputModel = this.cmbOutputModel.Text;
        outputModel += outputModel.EndsWith("\\") ? "" : "\\";
        alternativeName = this.cmbAlternativeList.Text;

        if (ValidateOutputModelInput(baseModel, alternativeName, outputModel))
        {
          ApplyAlternative(baseModel, alternativeName, outputModel);
          SwitchToDefaultView();
        }

      }
      catch (Exception ex)
      {
        MessageBox.Show("Error applying alternative. See status log for details.", "Error Applying Alternative", MessageBoxButtons.OK, MessageBoxIcon.Error);
        engineOutput.AddStatus("Error generating conveyance alternative '" + alternativeName + "'.", SeverityLevel.Error);
        engineOutput.AddStatus("    Base Model: file:" + baseModel);
        engineOutput.AddStatus("    Output Model: file:" + outputModel);
        engineOutput.AddStatus(ex.ToString(), SeverityLevel.Error);
      }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      SwitchToDefaultView();
    }

    private void btnDeleteAlternative_Click(object sender, EventArgs e)
    {
      string baseModel = this.cmbBaseModel.Text;
      baseModel += baseModel.EndsWith("\\") ? "" : "\\";
      string alternativeName = this.cmbAlternativeList.Text;
      if (MessageBox.Show("Are you sure you wish to delete the selected alternative?",
          "Delete Alternative?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) != DialogResult.OK)
      {
        return;
      }
      DeleteAlternative(baseModel, alternativeName);
      UpdateAlternativeList();
    }

    private void DeleteAlternative(string baseModel, string alternativeName)
    {
      string alternativePath = GetAlternativePath(baseModel, alternativeName);

      if (!Directory.Exists(alternativePath))
      {
        engineOutput.AddStatus("Attempted to delete non-existent alternative '" + alternativeName + "' from model " + baseModel);
        return;
      }

      engineOutput.AddStatus("Deleting alternative '" + alternativeName + "' from model " + baseModel, SeverityLevel.Info);
      try
      {
        Directory.Delete(alternativePath, true);
        engineOutput.AddStatus("Successfully deleted alternative '" + alternativeName + "'.");
      }
      catch (Exception ex)
      {
        engineOutput.AddStatus("Error deleting alternative: " + ex.ToString(), SeverityLevel.Error);
      }
    }

    private void btnBrowseOutput_Click(object sender, EventArgs e)
    {
      if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
      {
        string newOutputModel;
        newOutputModel = this.folderBrowserDialog1.SelectedPath;
        if (!newOutputModel.EndsWith("\\"))
        {
          newOutputModel += "\\";
        }
        this.cmbOutputModel.Text = newOutputModel;
      }
    }

    private void btnBrowseBaseModel_Click(object sender, EventArgs e)
    {
      if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
      {
        string newBaseModel;
        newBaseModel = this.folderBrowserDialog1.SelectedPath;
        if (!newBaseModel.EndsWith("\\"))
        {
          newBaseModel += "\\";
        }
        if (!ValidateBaseModelInput(newBaseModel))
        {
          return;
        }
        this.cmbBaseModel.Text = newBaseModel;
        this.cmbAlternativeList.DataSource = this.GetAlternativeList(newBaseModel);
      }
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
      this.Close();
    }

    private void btnCopyAlternative_Click(object sender, EventArgs e)
    {
      string baseModel = "";
      string alternativeName = "";


      baseModel = this.cmbBaseModel.Text;
      baseModel += baseModel.EndsWith("\\") ? "" : "\\";
      alternativeName = (string)cmbAlternativeList.Text;

      InputBoxResult result;
      result = InputBox.Show("Enter a name for the new alternative:", "Copy Alternative");
      if (result.ReturnCode != DialogResult.OK)
      {
        if (!ValidateAlternativeInput(baseModel, result.Text))
        {
          return;
        }
        return;
      }

      string newAltName = result.Text;
      CopyAlternative(baseModel, alternativeName, newAltName);
      UpdateAlternativeList();
    }

    private void CopyAlternative(string baseModel, string alternativeName, string newAltName)
    {
      string newAltPath;
      newAltPath = GetAlternativePath(baseModel, newAltName);

      string alternativePath = GetAlternativePath(baseModel, alternativeName);

      try
      {
        engineOutput.AddStatus("Copying alternative.");


        Cursor = Cursors.WaitCursor;
        CopyDirectory(alternativePath, newAltPath, true);
        AlternativeConfiguration copiedAltConfig = new AlternativeConfiguration(newAltName, baseModel);

        engineOutput.AddStatus("Succesfully copied alternative.");
      }
      catch (Exception ex)
      {
        engineOutput.AddStatus(ex.ToString(), SeverityLevel.Error);
        engineOutput.AddStatus("Error copying alternative. See status log for details.", SeverityLevel.Error);
      }
      finally
      {
        Cursor = Cursors.Default;
      }
    }

    private void btnRestoreDefault_Click(object sender, EventArgs e)
    {
      LoadConfig(false);
    }

    private void btnLoadDebugSettings_Click(object sender, EventArgs e)
    {
      LoadConfig(true);
    }

    private void btnEditAutoConveyance_Click(object sender, EventArgs e)
    {
      if (autoConveyanceInterface.AutoConveyanceMode)
      {
        DisableAutoConveyance();
        return;
      }

      string baseModel;
      baseModel = cmbBaseModel.Text;

      baseModel += baseModel.EndsWith("\\") ? "" : "\\";
      if (!ValidateBaseModelInput(baseModel))
      {
        return;
      }

      EnableAutoConveyance(baseModel);
    }

    private void btnGenerateConveyance_Click(object sender, EventArgs e)
    {
      string baseModel = "";
      string alternativeName = "";

      try
      {
        alternativeName = (string)cmbAlternativeList.Text;
        baseModel = this.cmbBaseModel.Text;
        baseModel += baseModel.EndsWith("\\") ? "" : "\\";
        string alternativePath = GetAlternativePath(baseModel, alternativeName);

        if (ValidateAlternativeInput(baseModel, alternativeName))
        {
          if (!NewAlternative(baseModel, alternativeName))
          {
            return;
          }
          UpdateAlternativeList();
          autoConveyanceInterface.AutoConveyance(baseModel, alternativeName);
          this.mbExecuter.ExecuteFunctionGroup("AutoConveyance", baseModel, alternativeName, "", false);
          this.altConfig.AddHistory("Auto-Generated from conveyance grid");
          config.UpdateBaseModelHistory(baseModel);
          this.engineOutput.AddStatus("Succesfully generated conveyance alternative '" + alternativeName + "'.");
          DisableAutoConveyance();
        }
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error generating conveyance alternative. See status log for details.", "Error Generating Conveyance Alternative", MessageBoxButtons.OK, MessageBoxIcon.Error);
        engineOutput.AddStatus("    Base Model: file:" + baseModel, SeverityLevel.Error);
        engineOutput.AddStatus(ex.ToString(), SeverityLevel.Error);
        engineOutput.AddStatus("Error generating conveyance alternative '" + alternativeName + "'.", SeverityLevel.Error);
      }
    }

    private void EnableAutoConveyance(string baseModel)
    {
      btnEditAutoConveyance.Text = "Exit Conveyance Mode";
      cmbBaseModel.Enabled = false;
      autoConveyanceInterface.BaseModel = baseModel;
      autoConveyanceInterface.EnableAutoConveyanceMode();
      btnGenerateConveyance.Enabled = true;

      autoConveyanceInterface.BaseModel = baseModel;
      engineOutput.AddStatus("Entering auto-conveyance mode. Base model: " + baseModel);
    }

    private void DisableAutoConveyance()
    {
      btnEditAutoConveyance.Text = "Prepare Conveyance Alternative";
      cmbBaseModel.Enabled = true;
      autoConveyanceInterface.DisableAutoConveyanceMode();
      btnGenerateConveyance.Enabled = false;
      engineOutput.AddStatus("Exiting auto-conveyance mode.");
      return;
    }

    private void tabControl_SelectedTabChanging(object sender, Infragistics.Win.UltraWinTabControl.SelectedTabChangingEventArgs e)
    {
      if (autoConveyanceInterface.AutoConveyanceMode)
      {
        if (MessageBox.Show("You are navigating away from an Auto-Conveyance editing session. Unsaved changes will be lost. Continue?", "Exit Auto-Conveyance?", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
        {
          DisableAutoConveyance();
        }
        else
        {
          e.Cancel = true;
        }
      }
      return;
    }

    #endregion

    private void cmbBaseModel_ValueChanged(object sender, EventArgs e)
    {
      UpdateAlternativeList();
      //engineOutput.AddUpdate(".");
    }

    private void btnReadFocusAreas_Click(object sender, EventArgs e)
    {


      string baseModel;

      baseModel = this.cmbBaseModel.Text;
      baseModel += baseModel.EndsWith("\\") ? "" : "\\";
      List<string> alternativeList = this.GetAlternativeList(baseModel);

      if (alternativeList.Count == 0)
      {
        MessageBox.Show("The selected base model does not contain any alternatives.", "No Alternatives found");
        return;
      }

      List<string> focusAreaList = new List<string>();
      aggregatorDS = new DataSet();
      alternativePackages = new List<AlternativePackage>();

      try
      {
        this.Cursor = Cursors.WaitCursor;

        foreach (string alternativeName in alternativeList)
        {
          string alternativePath = GetAlternativePath(baseModel, alternativeName);

          try
          {
            AlternativePackage ap = new AlternativePackage(alternativePath);
            alternativePackages.Add(ap);

            foreach (string focusArea in ap.FocusAreaList)
            {
              if (!focusAreaList.Contains(focusArea))
              {
                focusAreaList.Add(focusArea);
              }
            }
          }
          catch (Exception ex)
          {
            this.Cursor = Cursors.Default;
            MessageBox.Show(ex.Message);
            engineOutput.AddStatus(ex.Message, SeverityLevel.Error);
            return;
          }
        }
        focusAreaList.Sort();

        DataTable dt = new DataTable();

        dt.Columns.Add("Focus Area", typeof(string));
        dt.Columns[0].ReadOnly = true;

        foreach (string altName in alternativeList)
        {
          dt.Columns.Add(altName, typeof(bool));
        }

        dt.Columns.Add("(Exclude)", typeof(bool));

        foreach (string focusAreaName in focusAreaList)
        {
          DataRow r = dt.NewRow();
          r[0] = focusAreaName;
          r[1] = true;
          for (int i = 2; i < alternativeList.Count + 2; i++)
          {
            r[i] = false;
          }
          dt.Rows.Add(r);
        }
        engineOutput.AddStatus("Aggregtor read focus areas succesfully.", SeverityLevel.Info);

        grdFocusAreaChooser.DataSource = dt;
      }
      catch (Exception ex)
      {
        MessageBox.Show("Error reading focus areas: " + ex.Message);
        engineOutput.AddStatus("Error reading focus areas: " + ex.Message, SeverityLevel.Error);
      }
      finally
      {
        this.Cursor = Cursors.Default;
      }
    }

    /// <summary>
    /// Forces radio-button functionality (1 and only 1 selected) in a check-box (0 or more selected) interface
    /// </summary>        
    private void grdFocusAreaChooser_CellChange(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e)
    {
      if (e.Cell.Text == "False")
      {
        e.Cell.Value = true;
      }
      else
      {
        foreach (Infragistics.Win.UltraWinGrid.UltraGridCell c in e.Cell.Row.Cells)
        {
          if (c.Column.Index == 0) continue;
          if (e.Cell.Column.Key != c.Column.Key)
          {
            c.Value = false;
          }
        }
      }
    }

    private void btnAggregate_Click(object sender, EventArgs e)
    {
      if (alternativePackages.Count == 0)
      {
        MessageBox.Show("No alternatives loaded - Use 'Read Focus Area' on a base model with 1 or more alternatives.", "No Alternatives loaded");
        return;
      }

      string baseModel;

      baseModel = this.cmbBaseModel.Text;
      baseModel += baseModel.EndsWith("\\") ? "" : "\\";

      InputBoxResult result;
      string newAltName;

      result = InputBox.Show("Please enter a name for the new alternative:", "New Alternative Name");

      if (result.ReturnCode != DialogResult.OK)
      {
        return;
      }
      newAltName = result.Text;

      try
      {
        if (!ValidateAlternativeInput(baseModel, newAltName))
        {
          return;
        }

        if (this.GetAlternativeList(baseModel).Contains(newAltName))
        {
          MessageBox.Show("An alternative named '" + newAltName + "' already exists. Please specify a unique name.", "Alternative Name Already Exists");
          return;
        }

        if (this.GetAlternativeList(baseModel).Contains("aggregate"))
        {
          DeleteAlternative(baseModel, "aggregate");
        }
        if (!NewAlternative(baseModel, "aggregate"))
        {
          this.engineOutput.AddStatus("Unable to delete system alternative 'aggregate'. Please delete this alternative manually. Check for any hung processes.", SeverityLevel.Error);
          return;
        }

        DataTable dt = (DataTable)grdFocusAreaChooser.DataSource;

        this.Cursor = Cursors.WaitCursor;

        foreach (DataRow row in dt.Rows)
        {
          string focusArea = (string)row[0];

          if ((bool)row[dt.Columns.Count - 1])
          {
            continue;
          }

          for (int i = 1; i < row.ItemArray.Length; i++)
          {
            //If focus area is not selected, then keep searching for a checked entry
            if (!(bool)row[i])
            {
              continue;
            }
            //Otherwise aggregate the focus area
            string alternativeName = dt.Columns[i].Caption;

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("gInputAlternativePath", GetAlternativePath(baseModel, alternativeName));
            parameters.Add("gFocusArea", focusArea);
            parameters.Add("gOutputAlternativePath", GetAlternativePath(baseModel, "aggregate"));
            parameters.Add("gRemoveRIK", chkRemoveRIK.Checked.ToString());
            mbExecuter.ExecuteFunctionGroup("Aggregate", parameters);

          }

        }

        CopyAlternative(baseModel, "aggregate", newAltName);

        MessageBox.Show("The selected Focus Areas were aggregated succesfully!", "Success!", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
      catch (Exception ex)
      {
        engineOutput.AddStatus("Unable to Aggregate Alternative: " + ex.Message, SeverityLevel.Error);
        if (this.GetAlternativeList(baseModel).Contains(newAltName))
        {
          DeleteAlternative(baseModel, newAltName);
        }
        MessageBox.Show("Unable to Aggregate Alternative: " + ex.Message, "Error Aggregating Alternative", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      finally
      {
        this.Cursor = Cursors.Default;
        if (this.GetAlternativeList(baseModel).Contains("aggregate"))
        {
          DeleteAlternative(baseModel, "aggregate");
        }
        UpdateAlternativeList();
      }


    }

    private void btnBrowseFocusAreas_Click(object sender, EventArgs e)
    {
      if (this.folderBrowserDialog1.ShowDialog() == DialogResult.OK)
      {
        string focusArea;
        focusArea = this.folderBrowserDialog1.SelectedPath;
        if (!focusArea.EndsWith("\\"))
        {
          focusArea += "\\";
        }

        this.txtFocusAreas.Text = focusArea;
      }
    }

    private void btnRefreshFocusAreas_Click(object sender, EventArgs e)
    {
      string alternativeName = (string)cmbAlternativeList.Text;
      string baseModel = this.cmbBaseModel.Text;
      baseModel += baseModel.EndsWith("\\") ? "" : "\\";
      string alternativePath = GetAlternativePath(baseModel, alternativeName);

      if (!ValidateAlternativeInput(baseModel, alternativeName))
      {
        return;
      }

      try
      {
        Dictionary<string, string> parameters = new Dictionary<string, string>();
        parameters.Add("gAlternativePath", alternativePath);
        parameters.Add("gIncludeLinksAndNodes", chkFocusAreaLinks.Checked.ToString());
        parameters.Add("gIncludeCatchments", chkFocusAreaCatchments.Checked.ToString());
        parameters.Add("gIncludeICTargets", chkFocusAreaTargets.Checked.ToString());
        parameters.Add("gFocusAreas", txtFocusAreas.Text);
        mbExecuter.ExecuteFunctionGroup("UpdateFocusAreas", parameters);
      }
      catch (Exception ex)
      {
        this.engineOutput.AddStatus("Error updating focus areas: " + ex.Message, SeverityLevel.Error);
      }

      return;
    }

    private void btnMoveAlternative_Click(object sender, EventArgs e)
    {
      string baseModelPath = this.cmbBaseModel.Text;
      baseModelPath += baseModelPath.EndsWith("\\") ? "" : "\\";

      string alternativeName = (string)cmbAlternativeList.Text;

      string newModelPath;
      newModelPath = this.txtNewModel.Text;

      InputBoxResult result;
      string newAltName;
      result = InputBox.Show("Please enter a name for the new alternative:", "New Alternative Name");

      if (result.ReturnCode != DialogResult.OK)
      {
        return;
      }
      newAltName = result.Text;

      if (!ValidateAlternativeInput(baseModelPath, alternativeName))
      {
        return;
      }
      if (!ValidateAlternativeInput(newModelPath, newAltName))
      {
        return;
      }

      if (this.GetAlternativeList(baseModelPath).Contains(newAltName))
      {
        MessageBox.Show("An alternative named '" + newAltName + "' already exists. Please specify a unique name.", "Alternative Name Already Exists");
        return;
      }

      try
      {
        this.Cursor = Cursors.WaitCursor;

        string alternativePath = GetAlternativePath(baseModelPath, alternativeName);
        string newAlternativePath = GetAlternativePath(newModelPath, newAltName);

        Model baseModel = new Model(baseModelPath);
        Model newModel = new Model(newModelPath);

        AlternativePackage ap = new AlternativePackage(alternativePath);

        ap.Copy(newModel);
        

        MessageBox.Show("The alternative '" + alternativeName + "' was succesfully moved.");
      }
      catch (Exception ex)
      {
        engineOutput.AddStatus("Unable to Move Alternative: " + ex.Message, SeverityLevel.Error);

        MessageBox.Show("Unable to Move Alternative: " + ex.Message, "Error Moving Alternative", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
      finally
      {
        this.Cursor = Cursors.Default;

      }
    }

    public string GetAlternativePath(string baseModel, string alternativeName)
    {
      string alternativePath;
      alternativePath = baseModel + "alternatives\\" + alternativeName + "\\";
      return alternativePath;
    }

  }
}
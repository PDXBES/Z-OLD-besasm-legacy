using System;
using SystemsAnalysis.Utils.MapInfoUtils;
using SystemsAnalysis.Utils.Events;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using SystemsAnalysis.Modeling.Alternatives;

namespace SystemsAnalysis.ModelConstruction.AlternativesToolkit
{
	/// <summary>
	/// Summary description for AlternativeEngine.
	/// </summary>
	public class MapBasicExecuter : IGISExecuter
	{
		private MapBasicEngine mbEngine;	
		private AltEngineConfiguration config;
		private bool debugMode = false;
        private System.Timers.Timer timer;
        private WorkbenchRunning workbenchRunning;
        private string applicationDirectory;

		/*public MapBasicExecuter(AltEngineConfiguration config)
		{					
			this.mbEngine = new MapBasicEngine();
			this.config = config;
			this.debugMode = false;            
            // 
            // timer
            // 
            this.timer = new System.Timers.Timer();
            this.timer.AutoReset = false;
            this.timer.Interval = 250;
            //this.timer.SynchronizingObject = this;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Elapsed);
            //this.StatusChanged += new SystemsAnalysis.Utils.Events.OnStatusChangedEventHandler(mbExecuter_StatusChanged);            
		}*/

		public MapBasicExecuter(AltEngineConfiguration config, bool debugMode, string applicationDirectory)
		{					
			this.config = config;
			this.debugMode = debugMode;
            this.applicationDirectory = applicationDirectory;
            // 
            // timer
            // 
            this.timer = new System.Timers.Timer();
            this.timer.AutoReset = false;
            this.timer.Interval = 250;
            //this.timer.SynchronizingObject = this;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Elapsed);            
		}

		public void ExecuteLibrary(string libraryName, IDictionary parameterList)
		{
			if (debugMode) { this.mbEngine.Visible = true; }
            if (this.mbEngine == null)
            {
                this.mbEngine = new MapBasicEngine();
            }
			foreach (AltEngineConfiguration.LibraryRow lib in config.MapBasicFramework[0].GetLibraryRows())
			{						
				//Execute all libraries in the specifed execution group
				if (lib.LibraryName != libraryName)
				{
					continue;
				}
									
				LoadLibrary(lib);	
				SetParameters(lib, parameterList);
				ExecuteFunctions(lib);
																																				
				this.SetStatus("MapBasic Library executed OK!");	
			}
		}
		
		private void LoadLibrary(AltEngineConfiguration.LibraryRow lib)
		{
            string libraryLocation;
            libraryLocation = lib.Location;
            if (libraryLocation.Contains("%Application%"))
            {
                libraryLocation = libraryLocation.Replace("%Application%", Application.StartupPath);
            }
            this.SetStatus("Executing Library: '" + lib.LibraryName + "'.");
            this.SetStatus("Library location: " + libraryLocation);
            this.mbEngine.ExecuteLibrary(libraryLocation);
		}
		private void SetParameters(AltEngineConfiguration.LibraryRow lib, IDictionary parameterList)
		{
			foreach (string parameterName in parameterList.Keys)
			{
				this.mbEngine.WriteGlobal(parameterName, (string)parameterList[parameterName]);
				this.SetStatus("	Set parameter " + parameterName + " = '" + (string)parameterList[parameterName] + "'.");
			}
			this.mbEngine.WriteGlobal("gDebugMode", this.debugMode ? "TRUE" : "FALSE");
		}

		private void ExecuteFunctions(AltEngineConfiguration.LibraryRow lib)
		{
			//Execute all functions within the current library	
			foreach (AltEngineConfiguration.FunctionRow f in lib.GetFunctionRows())
			{	
                this.SetStatus("    Executing MapBasic function '" + f.Name + "' from library '" + f.LibraryName + "'.");
				if (this.debugMode) 
				{ 							
					MessageBox.Show("Execute Function '" + f.Name + "' from MapInfo Function Menu, then click 'OK' to continue.", 
						"Debug Mode", MessageBoxButtons.OK); 					
					continue;
				} 
				mbEngine.ExecuteLibraryFunction(f.CommandID);
				this.SetStatus("	" + this.mbEngine.ReadGlobal("gReturnStatus"));				
			}
		}
				
		public void ShowGIS()
		{
			this.mbEngine.Visible = true;			
			this.mbEngine.SwitchToMapInfo();			
		}
		public void HideGIS()
		{
			try
			{
				this.mbEngine.Visible = false;
			}
			catch
			{
			}
		}

		public bool WaitingForGIS
		{		
			get 
			{
				try
				{
					bool waiting;				
					waiting = this.mbEngine.Visible; 								
					return waiting;
				}
				catch
				{					
					throw new System.Runtime.InteropServices.COMException("MapInfo has crashed!");
				}
			}
		}
		public void CloseGIS()
		{
			this.mbEngine.CloseMapInfo();
		}

		public string GetError()
		{
			return this.mbEngine.GetError();
		}

		/// <summary>
		/// Event that occurs when CharacterizationEngine reports that it's status has changed.
		/// </summary>
		public event OnStatusChangedEventHandler StatusChanged;
		
		/// <summary>
		/// Internally called function that fires the OnStatusChanged event.
		/// </summary>
		/// <param name="e">Parameter that contains the string describing the new state.</param>
		protected virtual void OnStatusChanged(StatusChangedArgs e) 
		{
			if (StatusChanged != null)
				StatusChanged(e);
		}

		private void SetStatus(string status)
		{
			this.OnStatusChanged(new StatusChangedArgs(status));				
		}

        /// <summary>
        /// Executes a group of functions as specifed by execGroup in the settings files.
        /// </summary>
        /// <param name="execGroup"></param>
        /// <param name="interactive"></param>
        //TODO: Refactor to EngineSettings partial class.
        public void ExecuteFunctionGroup(string execGroup, string baseModel, string alternativeName, string outputModel, bool interactive)
        {                        
            //Execute all libraries in the specified Execution Group
            try
            {
                this.mbEngine = new MapBasicEngine();

                if (interactive) { this.ShowGIS(); }
                foreach (AltEngineConfiguration.LibraryRow lib in config.MapBasicFramework[0].GetLibraryRows())
                {
                    if (lib.ExecGroup == execGroup)
                    {
                        IDictionary parameterList;
                        parameterList = this.GetGISParameters(lib, baseModel, alternativeName, outputModel);
                        //mbExecuter.ExecuteLibrary(lib.LibraryName, parameterList);
                        this.ExecuteLibrary(lib.LibraryName, parameterList);
                    }
                }

                //Some library calls will require user input through the MapInfo GUI
                if (!interactive) { return; }

                //mbExecuter.ShowGIS();									
                this.workbenchRunning = new WorkbenchRunning();

                timer.Start();
                switch (this.workbenchRunning.ShowDialog())
                {
                    case DialogResult.OK:
                        //this.engineOutput.AddStatus("Returned from Alternative Workbench OK!", SeverityLevel.Attention);
                        this.SetStatus("Returned from Alternative Workbench OK!");
                        break;
                    case DialogResult.Cancel:
                        //this.engineOutput.AddStatus("User canceled Alternative Workbench!", SeverityLevel.Warning);
                        this.SetStatus("User canceled Alternative Workbench!");
                        break;
                    case DialogResult.Abort:
                        MessageBox.Show("MapInfo has crashed!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        //this.engineOutput.AddStatus("Alternative Workbench terminated abnormally!", SeverityLevel.Error);
                        this.SetStatus("Alternative Workbench terminated abnormally!");
                        break;
                    default:
                        //this.engineOutput.AddStatus("Alternative Workbench terminated impossibly!", SeverityLevel.Error);
                        this.SetStatus("Alternative Workbench terminated impossibly!");
                        break;
                }
            }
            catch (System.Runtime.InteropServices.COMException)
            {
                throw new Exception("Error executing MapBasic Library: " + this.GetError());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                timer.Stop();
                //this.mbExecuter.HideGIS();
                //this.mbExecuter.CloseGIS();
                this.HideGIS();
                this.CloseGIS();
                //this.Show();
            }
            return;
        }

        private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                if (!this.WaitingForGIS)
                {
                    timer.Stop();

                    this.workbenchRunning.DialogResult = DialogResult.OK;
                }
                else
                {
                    timer.Start();
                }
            }
            catch
            {
                timer.Stop();
                this.workbenchRunning.DialogResult = DialogResult.Abort;
            }
        }

        //TODO: Move data acces portions of this fucntion to EngineSettings.cs
        private IDictionary GetGISParameters(AltEngineConfiguration.LibraryRow lib, string baseModel, string alternativeName, string outputModel)
        {
            //string baseModel;
            //string outputModel;
            //string alternativeName;

            //baseModel = this.cmbBaseModel.Text;
            baseModel += baseModel.EndsWith("\\") ? "" : "\\";
            //outputModel = this.cmbOutputModel.Text;
            outputModel += outputModel.EndsWith("\\") ? "" : "\\";
            //alternativeName = this.cmbAlternativeList.Text;

            System.Collections.Specialized.ListDictionary globalList;
            globalList = new System.Collections.Specialized.ListDictionary();
            string globalVariable = "";

            foreach (AltEngineConfiguration.GlobalVariableRow global in lib.GetGlobalVariableRows())
            {
                switch (global.RelativeTo)
                {
                    case "Alternative":
                        globalVariable = baseModel + "alternatives\\" +
                            alternativeName + "\\";
                        break;
                    case "BaseModel":
                        globalVariable = baseModel;
                        break;
                    case "OutputModel":
                        globalVariable = outputModel;
                        break;
                    case "AlternativeConfiguration":
                        AlternativeConfiguration altConfig = new AlternativeConfiguration();
                        altConfig.ReadXml(baseModel + "alternatives\\" +
                        	alternativeName + "\\alternative_configuration.xml");
                        switch (global.Name)
                        {
                            case "gDefaultNodeSuffix":
                                globalVariable = altConfig.DefaultNodeSuffix;// AlternativeConfiguration[0].DefaultNodeSuffix;
                                break;
                            default:
                                globalVariable = "";
                                break;
                        }
                        break;
                    case "ModelConfiguration":
                        ModelConfigurationDataSet modelConfigDS = new ModelConfigurationDataSet();
                        modelConfigDS.ReadXml(outputModel + "Model.xml");
                        switch (global.Name)
                        {
                            case "gAltID":
                                foreach (ModelConfigurationDataSet.AlternativeRow row in modelConfigDS.IncludedAlternatives[0].GetAlternativeRows())
                                {
                                    if (alternativeName == row.Name)
                                    {
                                        globalVariable = row.AltID.ToString();
                                        break;
                                    }
                                }
                                break;
                        }
                        break;
                    case "SystemTemp":
                        globalVariable = this.applicationDirectory + "Temp\\";
                        if (!Directory.Exists(globalVariable + global.Value)) Directory.CreateDirectory(globalVariable + global.Value);
                        break;
                    case "Application":
                        globalVariable = Application.StartupPath + "\\";
                        break;          
                    default:
                        globalVariable = "";
                        break;
                }
                globalVariable += global.Value;
                globalList.Add(global.Name, globalVariable);
            }
            return globalList;
        }
		
	}
}

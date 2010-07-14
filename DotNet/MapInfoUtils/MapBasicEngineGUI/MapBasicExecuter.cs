using System;
using SystemsAnalysis.Utils.MapInfoUtils;
using SystemsAnalysis.Utils.Events;
using System.Windows.Forms;
using System.Collections;

namespace SystemsAnalysis.ModelConstruction.AlternativesBuilder
{
	/// <summary>
	/// Summary description for AlternativeEngine.
	/// </summary>
	public class MapBasicExecuter : IGISExecuter
	{
		private MapBasicEngine mbEngine;			
		private EngineConfiguration config;
		private bool debugMode = false;	

		public MapBasicExecuter(EngineConfiguration config)
		{					
			this.mbEngine = new MapBasicEngine();
			this.config = config;
			this.debugMode = false;			
		}

		public MapBasicExecuter(EngineConfiguration config, bool debugMode)
		{		
			this.mbEngine = new MapBasicEngine();
			this.config = config;
			this.debugMode = debugMode;
		}

		public void ExecuteLibrary(string libraryName, IDictionary parameterList)
		{
			if (debugMode) { this.mbEngine.Visible = true; }

			foreach (EngineConfiguration.LibraryRow lib in config.MapBasicFramework[0].GetLibraryRows())
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
		
		private void LoadLibrary(EngineConfiguration.LibraryRow lib)
		{
			this.SetStatus("Executing Library: '" + lib.LibraryName + "'.");				
			this.mbEngine.ExecuteLibrary(lib.Location);
		}
		private void SetParameters(EngineConfiguration.LibraryRow lib, IDictionary parameterList)
		{
			foreach (string parameterName in parameterList.Keys)
			{
				this.mbEngine.WriteGlobal(parameterName, (string)parameterList[parameterName]);
				this.SetStatus("	Set parameter " + parameterName + " = '" + (string)parameterList[parameterName] + "'.");
			}
			this.mbEngine.WriteGlobal("gDebugMode", this.debugMode ? "TRUE" : "FALSE");
		}

		private void ExecuteFunctions(EngineConfiguration.LibraryRow lib)
		{
			//Execute all functions within the current library	
			foreach (EngineConfiguration.FunctionRow f in lib.GetFunctionRows())
			{	
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
					MessageBox.Show("MapInfo has crashed!");
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
		
		
	}
}

using System;
using System.Drawing;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using SystemsAnalysis.Utils.StatusTextBox;
using SystemsAnalysis.ModelConstruction.AlternativesBuilder;
using SystemsAnalysis.Utils.MapInfoUtils;

namespace SystemsAnalysis.ModelConstruction.AlternativesBuilder
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class mbEngineGUIForm : System.Windows.Forms.Form
	{	
		private bool debugMode;		
		private bool interactiveMode;
		private IGISExecuter mbExecuter;
				
		private WorkbenchRunning workbenchRunning;
		private EngineConfiguration config;		

		private SystemsAnalysis.Utils.StatusTextBox.StatusTextBox engineOutput;
		private System.Windows.Forms.Panel panel1;
		private Infragistics.Win.Misc.UltraButton btnExit;
		private System.Timers.Timer timer;
		private Infragistics.Win.UltraWinEditors.UltraCheckEditor txtDebugMode;
		private Infragistics.Win.Misc.UltraButton btnExecuteLibrary;
		private Infragistics.Win.UltraWinEditors.UltraCheckEditor txtInteractiveMode;
		private Infragistics.Win.Misc.UltraButton btnReloadConfig;
		private Infragistics.Win.UltraWinEditors.UltraComboEditor cmbLibraries;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public mbEngineGUIForm()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//TODO: Remove debugMode
			this.debugMode = false;			

			this.engineOutput.AddStatus("Entering AlternativeEngine.", SeverityLevel.Info);			
			try
			{
				ReloadConfig();                
			}
			catch (Exception ex)
			{
				this.engineOutput.AddStatus("Error during initialization: " + ex.ToString(), SeverityLevel.Error);
			}									
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			System.IO.Stream configStream = GetType().Assembly.GetManifestResourceStream("SystemsAnalysis.ModelConstruction.AlternativesBuilder.Default_Settings.xml");				
			this.config.WriteXml("Default_Settings.xml");
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mbEngineGUIForm));
            this.btnExecuteLibrary = new Infragistics.Win.Misc.UltraButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnReloadConfig = new Infragistics.Win.Misc.UltraButton();
            this.cmbLibraries = new Infragistics.Win.UltraWinEditors.UltraComboEditor();
            this.txtInteractiveMode = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.txtDebugMode = new Infragistics.Win.UltraWinEditors.UltraCheckEditor();
            this.btnExit = new Infragistics.Win.Misc.UltraButton();
            this.engineOutput = new SystemsAnalysis.Utils.StatusTextBox.StatusTextBox();
            this.timer = new System.Timers.Timer();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLibraries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.timer)).BeginInit();
            this.SuspendLayout();
            // 
            // btnExecuteLibrary
            // 
            this.btnExecuteLibrary.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExecuteLibrary.Location = new System.Drawing.Point(24, 8);
            this.btnExecuteLibrary.Name = "btnExecuteLibrary";
            this.btnExecuteLibrary.Size = new System.Drawing.Size(117, 37);
            this.btnExecuteLibrary.TabIndex = 0;
            this.btnExecuteLibrary.Text = "Execute MBX Library";
            this.btnExecuteLibrary.Click += new System.EventHandler(this.btnExecuteLibrary_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnReloadConfig);
            this.panel1.Controls.Add(this.cmbLibraries);
            this.panel1.Controls.Add(this.txtInteractiveMode);
            this.panel1.Controls.Add(this.txtDebugMode);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.btnExecuteLibrary);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 296);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(835, 104);
            this.panel1.TabIndex = 0;
            // 
            // btnReloadConfig
            // 
            this.btnReloadConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnReloadConfig.Location = new System.Drawing.Point(720, 8);
            this.btnReloadConfig.Name = "btnReloadConfig";
            this.btnReloadConfig.Size = new System.Drawing.Size(104, 37);
            this.btnReloadConfig.TabIndex = 8;
            this.btnReloadConfig.Text = "Reload Engine Configuration";
            this.btnReloadConfig.Click += new System.EventHandler(this.btnReloadConfig_Click);
            // 
            // cmbLibraries
            // 
            this.cmbLibraries.Location = new System.Drawing.Point(144, 8);
            this.cmbLibraries.Name = "cmbLibraries";
            this.cmbLibraries.Size = new System.Drawing.Size(184, 24);
            this.cmbLibraries.TabIndex = 7;
            // 
            // txtInteractiveMode
            // 
            this.txtInteractiveMode.Checked = true;
            this.txtInteractiveMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.txtInteractiveMode.Location = new System.Drawing.Point(568, 32);
            this.txtInteractiveMode.Name = "txtInteractiveMode";
            this.txtInteractiveMode.Size = new System.Drawing.Size(144, 16);
            this.txtInteractiveMode.TabIndex = 6;
            this.txtInteractiveMode.Text = "Interactive Mode?";
            this.txtInteractiveMode.CheckedChanged += new System.EventHandler(this.txtInteractiveMode_CheckedChanged);
            // 
            // txtDebugMode
            // 
            this.txtDebugMode.Location = new System.Drawing.Point(568, 8);
            this.txtDebugMode.Name = "txtDebugMode";
            this.txtDebugMode.Size = new System.Drawing.Size(144, 16);
            this.txtDebugMode.TabIndex = 5;
            this.txtDebugMode.Text = "Debug Mode?";
            this.txtDebugMode.CheckedChanged += new System.EventHandler(this.txtDebugMode_CheckedChanged);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Location = new System.Drawing.Point(720, 57);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(106, 37);
            this.btnExit.TabIndex = 4;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // engineOutput
            // 
            this.engineOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.engineOutput.Location = new System.Drawing.Point(0, 0);
            this.engineOutput.Name = "engineOutput";
            this.engineOutput.Size = new System.Drawing.Size(835, 296);
            this.engineOutput.TabIndex = 1;
            this.engineOutput.Text = "";
            this.engineOutput.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.engineOutput_LinkClicked);
            // 
            // timer
            // 
            this.timer.AutoReset = false;
            this.timer.Interval = 250;
            this.timer.SynchronizingObject = this;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.timer_Elapsed);
            // 
            // mbEngineGUIForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(835, 400);
            this.Controls.Add(this.engineOutput);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "mbEngineGUIForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MapBasic Engine GUI";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cmbLibraries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.timer)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new mbEngineGUIForm());
		}
						
		private void btnExit_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}
		
		
		private void ExecuteMapBasicLibrary(EngineConfiguration.LibraryRow libraryRow)
		{																
			//Execute all libraries in the specified Execution Group
			try
			{
                mbExecuter = new MapBasicExecuter(this.config, this.debugMode);
                mbExecuter.StatusChanged += new SystemsAnalysis.Utils.Events.OnStatusChangedEventHandler(mbExecuter_StatusChanged);

                IDictionary parameterList;
                
                //Must show MapInfo prior to executing functions, or else toolpads do not get set correctly
                if (interactiveMode) 
                {
                    mbExecuter.ShowGIS();
                }

                parameterList = this.GetGISParameters(libraryRow);
                mbExecuter.ExecuteLibrary(libraryRow.LibraryName, parameterList);			
																									
				//Some library calls will require user input through the MapInfo GUI
				if (!interactiveMode) 
				{					
					return;
				} 
												
				this.workbenchRunning = new WorkbenchRunning();						
				timer.Start();								

				switch (this.workbenchRunning.ShowDialog())
				{
					case DialogResult.OK:
						this.engineOutput.AddStatus("Returned from Alternative Workbench OK!", SeverityLevel.Attention);
						break;
					case DialogResult.Cancel:						
						this.engineOutput.AddStatus("User canceled Alternative Workbench!", SeverityLevel.Warning);
						break;
					case DialogResult.Abort:
						this.engineOutput.AddStatus("Alternative Workbench terminated abnormally!", SeverityLevel.Error);
						break;
					default:
						this.engineOutput.AddStatus("Alternative Workbench terminated impossibly!", SeverityLevel.Error);
						break;				
				}						
			}
			catch (System.Runtime.InteropServices.COMException)
			{											
				throw new Exception("Error executing MapBasic Library: " + mbExecuter.GetError());
			}
			catch (Exception ex)
			{
				throw ex;
			}
			finally
			{	
				timer.Stop();				
				mbExecuter.HideGIS();
				mbExecuter.CloseGIS();						
				this.Show();
			}			
			return;
		}
		
		private IDictionary GetGISParameters(EngineConfiguration.LibraryRow lib)
		{
			//this.config.sc
			System.Collections.Specialized.ListDictionary globalList;
			globalList = new System.Collections.Specialized.ListDictionary();
			string globalVariable;

			foreach (EngineConfiguration.GlobalVariableRow global in lib.GetGlobalVariableRows())
			{
				switch (global.RelativeTo)
				{
					//If items in the EngineConfig file must be modified by
					//to include run-time information, add a case: here to handle
					default:
						globalVariable = "";
						break;
				}
				globalVariable += global.Value;
				globalList.Add(global.Name, globalVariable);
			}
			return globalList;
		}					
		
		private void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			try
			{
				if (!this.mbExecuter.WaitingForGIS)
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

		private void engineOutput_LinkClicked(object sender, System.Windows.Forms.LinkClickedEventArgs e)
		{
				System.Diagnostics.Process.Start(e.LinkText);
		}

		private void txtDebugMode_CheckedChanged(object sender, System.EventArgs e)
		{
			this.debugMode = txtDebugMode.Checked;
		}
		private void txtInteractiveMode_CheckedChanged(object sender, System.EventArgs e)
		{
			this.interactiveMode = txtInteractiveMode.Checked;
		}

		private void mbExecuter_StatusChanged(SystemsAnalysis.Utils.Events.StatusChangedArgs e)
		{
			this.engineOutput.AddStatus(e.NewStatus, SeverityLevel.Info);
		}
		
		private void btnExecuteLibrary_Click(object sender, System.EventArgs e)
		{
            DataRowView selectedView = (DataRowView)this.cmbLibraries.SelectedItem.ListObject;
                EngineConfiguration.LibraryRow libraryRow = (EngineConfiguration.LibraryRow)selectedView.Row;
                ExecuteMapBasicLibrary(libraryRow);
		}

		private void btnReloadConfig_Click(object sender, System.EventArgs e)
		{
			ReloadConfig();
		}

		private void ReloadConfig()
		{
			if (!File.Exists("Default_Settings.xml"))
			{
				Stream configStream = GetType().Assembly.GetManifestResourceStream("SystemsAnalysis.Utils.MapInfoUtils.Default_Settings.xml");					
				StreamReader streamReader = new System.IO.StreamReader(configStream);

				StreamWriter streamWriter;										
				streamWriter = new System.IO.StreamWriter("Default_Settings.xml", false);
					
				streamWriter.Write(streamReader.ReadToEnd());
				streamWriter.Close();
			}					

			this.engineOutput.AddStatus("Reading Default Settings...", SeverityLevel.Info);
								
			this.config = new EngineConfiguration();
				
			config.ReadXml("Default_Settings.xml");							

			//this.cmbGroup.Items.Clear();
			this.cmbLibraries.DataSource = config.Library;            
			this.cmbLibraries.DisplayMember = "LibraryName";		
		}

		
	}
}

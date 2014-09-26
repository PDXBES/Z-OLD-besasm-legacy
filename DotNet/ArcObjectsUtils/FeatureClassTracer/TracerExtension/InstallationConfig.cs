using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration.Install;
using System.Runtime.InteropServices;

namespace SystemsAnalysis.Tracer
{
	/// <summary>
	/// Summary description for InstallationConfig.
	/// </summary>
	[RunInstaller(true)]
	public class InstallationConfig : System.Configuration.Install.Installer
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public InstallationConfig()
		{
			// This call is required by the Designer.
			InitializeComponent();

			// TODO: Add any initialization after the InitializeComponent call
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		public override void Install(IDictionary stateSaver)
		{			
			try
			{
				base.Install (stateSaver);
				RegistrationServices regsrv = new RegistrationServices();
				if (!regsrv.RegisterAssembly(base.GetType().Assembly, AssemblyRegistrationFlags.SetCodeBase))
				{
					throw new InstallException("Failed to Register for COM");
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message, "Error during installation");
			}
		}

		public override void Uninstall(IDictionary savedState)
		{
			try
			{
				base.Uninstall (savedState);
				RegistrationServices regsrv = new RegistrationServices();
				if (!regsrv.UnregisterAssembly(base.GetType().Assembly))
				{
					throw new InstallException("Failed to Register for COM");
				}
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.Message, "Error during uninstallation");
			}
		}

		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
		#endregion
	}
}

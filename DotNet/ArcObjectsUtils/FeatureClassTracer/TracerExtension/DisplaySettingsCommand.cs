using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.Framework;

using System;
using System.Runtime.InteropServices;
namespace SystemsAnalysis.Tracer
{
	
	[Guid("90f566d2-55bf-4c53-841f-bce4ff5ee9d9")]
	[ClassInterface(ClassInterfaceType.None)]
	[ProgId("SystemsAnalysis.FeatureClassTracer.DisplaySettingsCommand")]
	public class DisplaySettingsCommand : ICommand
	{
		private ESRI.ArcGIS.ArcMapUI.IMxApplication m_MxApp;
		private ESRI.ArcGIS.ArcMapUI.IMxDocument m_MxDoc;
		private ESRI.ArcGIS.esriSystem.IExtension m_Ext;

		#region COM Registration Function(s)
		[ComRegisterFunction()]
		[ComVisible(false)]
		static void RegisterFunction(Type registerType)
		{
			// Required for ArcGIS Component Category Registrar support
			ArcGISCategoryRegistration(registerType);

			//
			// TODO: Add any COM registration code here
			//
		}

		[ComUnregisterFunction()]
		[ComVisible(false)]
		static void UnregisterFunction(Type registerType)
		{
			// Required for ArcGIS Component Category Registrar support
			ArcGISCategoryUnregistration(registerType);

			//
			// TODO: Add any COM unregistration code here
			//
		}

		#region ArcGIS Component Category Registrar generated code
		/// <summary>
		/// Required method for ArcGIS Component Category registration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryRegistration(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			ControlsCommands.Register(regKey);
			MxCommands.Register(regKey);

		}
		/// <summary>
		/// Required method for ArcGIS Component Category unregistration -
		/// Do not modify the contents of this method with the code editor.
		/// </summary>
		private static void ArcGISCategoryUnregistration(Type registerType)
		{
			string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
			ControlsCommands.Unregister(regKey);
			MxCommands.Unregister(regKey);

		}

		#endregion
		#endregion

		public DisplaySettingsCommand()
		{
		}

	
		#region ICommand Implementations
		public void OnClick()
		{
			try
			{
				this.OnClick(new EventArgs());
			}
			catch (Exception ex)
			{
				System.Windows.Forms.MessageBox.Show(ex.ToString(), "ERROR");
			}

		}

		public string Message
		{
			get
			{
				// TODO:	Add DisplaySettingsCommand.Message getter implementation
				return null;
			}
		}

		public int Bitmap
		{
			get
			{
				// TODO:	Add DisplaySettingsCommand.Bitmap getter implementation
				return 0;
			}
		}

		public void OnCreate(object hook)
		{
			IApplication m_App;
			if (hook.GetType() == typeof(ESRI.ArcGIS.ArcMapUI.IMxApplication))
			{
				m_MxApp = (ESRI.ArcGIS.ArcMapUI.IMxApplication)hook;
				m_App = (ESRI.ArcGIS.Framework.IApplication)m_MxApp;
				m_MxDoc = (ESRI.ArcGIS.ArcMapUI.IMxDocument)m_App.Document;

				ESRI.ArcGIS.esriSystem.UID u;
				u = new ESRI.ArcGIS.esriSystem.UIDClass();
				u.Value = "SystemsAnalysis.FeatureClassTracer.TracerExtension";	
				m_Ext = m_App.FindExtensionByCLSID(u);
			}		
		}

		public string Caption
		{
			get
			{				
				return "Settings";
			}
		}

		public string Tooltip
		{
			get
			{				
				return "Show FeatureClass Tracer settings.";
			}
		}

		public int HelpContextID
		{
			get
			{
				// TODO:	Add DisplaySettingsCommand.HelpContextID getter implementation
				return 0;
			}
		}

		public string Name
		{
			get
			{
				return "Feature Class Tracer Settings";								
			}
		}

		public bool Checked
		{
			get
			{				
				return false;
			}
		}

		public bool Enabled
		{
			get
			{
				// TODO:	Add DisplaySettingsCommand.Enabled getter implementation
				return true;
			}
		}

		public string HelpFile
		{
			get
			{
				// TODO:	Add DisplaySettingsCommand.HelpFile getter implementation
				return null;
			}
		}

		public string Category
		{
			get
			{				
				return "Feature Class Tracer";
			}
		}
		#endregion

		public event OnClickEventHandler Click;

		protected virtual void OnClick(EventArgs e) 
		{			
			if (Click != null)
			{				
				Click(this, e);
			}
		}


	}
}

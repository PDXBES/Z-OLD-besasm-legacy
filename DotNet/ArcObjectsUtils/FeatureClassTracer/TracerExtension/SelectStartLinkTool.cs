using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Framework;
using System;
using System.Runtime.InteropServices;
namespace SystemsAnalysis.Tracer
{
	
	[Guid("57f9d82b-ed4b-42c6-9f9d-48206c742684")]
	[ClassInterface(ClassInterfaceType.None)]
	[ProgId("SystemsAnalysis.FeatureClassTracer.SelectStartLinkTool")]
	public class SelectStartLinkTool : ICommand, ITool
	{
		private ESRI.ArcGIS.ArcMapUI.IMxApplication m_MxApp;
		private ESRI.ArcGIS.ArcMapUI.MxDocument m_MxDoc;
		private ESRI.ArcGIS.esriSystem.IExtension m_Ext;

		private System.Drawing.Bitmap m_Bitmap;
		private System.IntPtr m_hBitmap;
		private System.Windows.Forms.Cursor m_Cursor;
		private System.IntPtr m_hCursor;

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
		
		public SelectStartLinkTool()
		{
			m_Bitmap = new System.Drawing.Bitmap(GetType().Assembly.GetManifestResourceStream("SystemsAnalysis.Tracer.StartLinkIcon.bmp"));
			m_Bitmap.MakeTransparent(m_Bitmap.GetPixel(0,0));
			m_hBitmap = m_Bitmap.GetHbitmap();

			m_Cursor = new System.Windows.Forms.Cursor(GetType().Assembly.GetManifestResourceStream("SystemsAnalysis.Tracer.SelectStartLink.cur"));				
			
			if (m_Cursor != null)
			{
				m_hCursor = m_Cursor.Handle;
			}

		}

		#region ITool Implementations
		public void OnMouseDown(int button, int shift, int x, int y)
		{
			if (button == 1)
			{
				this.OnMouseDown(new ClickEventArgs(button, shift, x, y));
			}
		}

		public void OnMouseUp(int button, int shift, int x, int y)
		{
			this.Deactivate();
		}

		public void OnKeyDown(int keyCode, int shift)
		{
			// TODO:	Add MyClass1.OnKeyDown implementation
		}

		public void OnKeyUp(int keyCode, int shift)
		{
			// TODO:	Add MyClass1.OnKeyUp implementation
		}

		public void OnMouseMove(int button, int shift, int x, int y)
		{
			// TODO:	Add MyClass1.OnMouseMove implementation
		}

		public int Cursor
		{
			get
			{				
				return this.m_hCursor.ToInt32();
			}
		}

		public bool OnContextMenu(int x, int y)
		{
			// TODO:	Add MyClass1.OnContextMenu implementation
			return true;
		}

		public bool Deactivate()
		{			
			return true;
		}

		public void Refresh(int hdc)
		{
			// TODO:	Add MyClass1.Refresh implementation
		}

		public void OnDblClick()
		{
			// TODO:	Add MyClass1.OnDblClick implementation
		}
		#endregion

	
		#region ICommand Implementations
		public void OnClick()
		{			
		}

		public string Message
		{
			get
			{				
				return "Select a pipe to add to the list of start pipes.";
			}
		}

		public int Bitmap
		{
			get
			{				
				return m_hBitmap.ToInt32();
			}
		}

		public void OnCreate(object hook)
		{
			IApplication m_App ;
			if (hook.GetType() == typeof(ESRI.ArcGIS.ArcMapUI.IMxApplication))
			{
				m_MxApp = (ESRI.ArcGIS.ArcMapUI.IMxApplication)hook;
				m_App = (ESRI.ArcGIS.Framework.IApplication)m_MxApp;
				m_MxDoc = (ESRI.ArcGIS.ArcMapUI.MxDocument)m_App.Document;

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
				return "Select a pipe to add to the list of start pipes.";
			}
		}

		public string Tooltip
		{
			get
			{
				return "Select a start pipe";				
			}
		}

		public int HelpContextID
		{
			get
			{
				// TODO:	Add SelectStartLinkCommand.HelpContextID getter implementation
				return 0;
			}
		}

		public string Name
		{
			get
			{
				return "Select Start Link Command";
			}
		}

		public bool Checked
		{
			get
			{
				// TODO:	Add SelectStartLinkCommand.Checked getter implementation
				return false;
			}
		}

		public bool Enabled
		{
			get
			{
				// TODO:	Add SelectStartLinkCommand.Enabled getter implementation
				return true;
			}
		}

		public string HelpFile
		{
			get
			{
				// TODO:	Add SelectStartLinkCommand.HelpFile getter implementation
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

		public event OnMouseDownEventHandler MouseDown;

		protected virtual void OnMouseDown(ClickEventArgs e) 
		{			
			if (MouseDown != null)
			{				
				MouseDown(this, e);
			}
		}

	}
}

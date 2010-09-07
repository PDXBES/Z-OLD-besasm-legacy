using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;

namespace SanitaryAnalysisTools
    {
    /// <summary>
    /// Summary description for LayerManagerCommand.
    /// </summary>
    [Guid ( "cc35b4b4-b67d-4101-88f1-caa5e3af449a" )]
    [ClassInterface ( ClassInterfaceType.None )]
    [ProgId ( "SanitaryAnalysisTools.LayerManagerCommand" )]
    public class LayerManagerCommand : BaseCommand
        {
        private IMxApplication m_MxApp;
        private IMxDocument m_MxDoc;
        private IExtension m_Ext;

        #region COM Registration Function(s)
        [ComRegisterFunction ()]
        [ComVisible ( false )]
        static void RegisterFunction ( Type registerType )
            {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryRegistration ( registerType );

            //
            // TODO: Add any COM registration code here
            //
            }

        [ComUnregisterFunction ()]
        [ComVisible ( false )]
        static void UnregisterFunction ( Type registerType )
            {
            // Required for ArcGIS Component Category Registrar support
            ArcGISCategoryUnregistration ( registerType );

            //
            // TODO: Add any COM unregistration code here
            //
            }

        #region ArcGIS Component Category Registrar generated code
        /// <summary>
        /// Required method for ArcGIS Component Category registration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryRegistration ( Type registerType )
            {
            string regKey = string.Format ( "HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID );
            MxCommands.Register ( regKey );

            }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration ( Type registerType )
            {
            string regKey = string.Format ( "HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID );
            MxCommands.Unregister ( regKey );

            }

        #endregion
        #endregion

        private IApplication m_application;
        public LayerManagerCommand ()
            {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "Sanitary Analysis Tools"; //localizable text
            base.m_caption = "Layer Settings";  //localizable text
            base.m_message = "Manages Layer Settings for RDII Utility";  //localizable text 
            base.m_toolTip = "Manages Layer Settings for RDII Utility";  //localizable text 
            base.m_name = "Sanitary Analysis Layer Settings";   //unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

            //Commented out to force the use of the text (caption)
            //try
            //    {
            //    //
            //    // TODO: change bitmap name if necessary
            //    //
            //    string bitmapResourceName = GetType ().Name + ".bmp";
            //    base.m_bitmap = new Bitmap ( GetType (), bitmapResourceName );
            //    }
            //catch ( Exception ex )
            //    {
            //    System.Diagnostics.Trace.WriteLine ( ex.Message, "Invalid Bitmap" );
            //    }
            }

        #region Overriden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate ( object hook )
            {
            if ( hook == null )
                return;

            m_application = hook as IApplication;

            //Disable if it is not ArcMap
            if ( hook is IMxApplication )
                base.m_enabled = true;
            else
                base.m_enabled = false;

            // TODO:  Add other initialization code
            if ( hook.GetType () == typeof ( ESRI.ArcGIS.ArcMapUI.IMxApplication ) )
                {
                m_MxApp = ( IMxApplication ) hook;
                m_application = ( IApplication ) m_MxApp;
                m_MxDoc = ( IMxDocument ) m_application.Document;

                UID u;
                u = new UIDClass ();
                u.Value = "SanitaryAnalysisTools.RdiiDisconnectExtension";
                m_Ext = m_application.FindExtensionByCLSID ( u );
                }
            }
        public event OnClickEventHandler Click;

        protected virtual void OnClick ( EventArgs e )
            {
            if ( Click != null )
            { Click ( this, e ); }
            }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick ()
            {
            // TODO: Add LayerManagerCommand.OnClick implementation
            try
                {
                this.OnClick ( new EventArgs () );
                }
            catch ( Exception ex )
                {
                System.Windows.Forms.MessageBox.Show(ex.ToString(), "LayerManagerCommand.OnClick");
                }
            }

        #endregion
        }
    }

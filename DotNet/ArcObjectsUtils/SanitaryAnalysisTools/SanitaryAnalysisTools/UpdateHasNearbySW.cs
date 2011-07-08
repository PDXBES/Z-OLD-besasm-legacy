using System;
using System.Drawing;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.SystemUI;
using ESRI.ArcGIS.esriSystem;
using ReferenceLibrary;

namespace SanitaryAnalysisTools
    {
    /// <summary>
    /// Summary description for UpdateHasNearbySW.
    /// </summary>
    [Guid ( "c01e48f6-e1b2-4871-bf5c-8943fc04ee5d" )]
    [ClassInterface ( ClassInterfaceType.None )]
    [ProgId ( "SanitaryAnalysisTools.UpdateHasNearbySW" )]
    public class UpdateHasNearbySW : BaseCommand
        {
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

        private IActiveView m_ActiveView;
        private IMxApplication m_MxApp;
        private IMxDocument m_MxDoc;
        private IExtension m_Ext;

        //private int indexDSC;

        private IApplication m_application;
        public UpdateHasNearbySW ()
            {
            //
            // TODO: Define values for the public properties
            //
            base.m_category = "Sanitary Analysis Tools"; //localizable text
            base.m_caption = "DSC Has Nearby Storm";  //localizable text
            base.m_message = "Sets the NearbySW to Yes";  //localizable text 
            base.m_toolTip = "DSC Has Nearby Storm";  //localizable text 
            base.m_name = "SanA_DSCHasSW";   //unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

            try
                {
                //
                // TODO: change bitmap name if necessary
                //
                string bitmapResourceName = GetType ().Name + ".bmp";
                base.m_bitmap = new Bitmap ( GetType (), bitmapResourceName );
                }
            catch ( Exception ex )
                {
                System.Diagnostics.Trace.WriteLine ( ex.Message, "Invalid Bitmap" );
                }
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
            m_MxDoc = ( IMxDocument ) m_application.Document;
            m_ActiveView = ( IActiveView ) m_MxDoc.ActiveView;

            ArcObjectLibrary arcObjectLibrary = new ArcObjectLibrary ();

            //Disable if it is not ArcMap
            if ( hook is IMxApplication )
                base.m_enabled = true;
            else
                base.m_enabled = false;

            // TODO:  Add other initialization code

            // Verify that the right layers are loaded
            //indexDSC = arcObjectLibrary.GetIndexNumberOfLayerName ( m_activeView,
            //    "MODELING_DEV.MODELADMIN.mst_dsc" );
            //if ( indexDSC < 0 )
            //    base.m_enabled = false;
            //else
            //    base.m_enabled = true;
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
            // TODO: Add UpdateHasNearbySW.OnClick implementation
            try
                {
                this.OnClick ( new EventArgs () );
                }
            catch ( Exception ex )
                {
                System.Windows.Forms.MessageBox.Show 
                    ( ex.ToString (), "UpdateHasNearbySW OnClick Error" );
                }
            }    
        #endregion
        }
    }

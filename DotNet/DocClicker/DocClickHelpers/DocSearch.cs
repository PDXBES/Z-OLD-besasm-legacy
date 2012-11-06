using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using ESRI.ArcGIS.ADF.BaseClasses;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;

using DocManager;
using ExtnHelpers;


namespace DocClickHelpers
{
    /// <summary>
    /// Summary description for DocSearch.
    /// </summary>
    [Guid("cfcecdf2-3238-4e1f-ab43-0c35867fb271")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("DocClickHelpers.DocSearch")]
    public sealed class DocSearch : BaseCommand
    {
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
            MxCommands.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommands.Unregister(regKey);

        }

        #endregion
        #endregion

        private IApplication m_application;
        private DocManager.DocMgrSearch m_docSearch;
        private ESRI.ArcGIS.esriSystem.IExtension m_Ext;
        private ESRI.ArcGIS.esriSystem.IExtensionConfig m_ExtConfig;
        private Logger m_Logger;

        public DocSearch()
        {

            //
            // TODO: Define values for the public properties
            //
            base.m_category = ""; //localizable text
            base.m_caption = "";  //localizable text
            base.m_message = "";  //localizable text 
            base.m_toolTip = "";  //localizable text 
            base.m_name = "";   //unique id, non-localizable (e.g. "MyCategory_ArcMapCommand")

            try
            {
                //
                // TODO: change bitmap name if necessary
                //
                string bitmapResourceName = GetType().Name + ".bmp";
                base.m_bitmap = new Bitmap(GetType(), bitmapResourceName);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.WriteLine(ex.Message, "Invalid Bitmap");
            }
        }

        #region Overriden Class Methods

        /// <summary>
        /// Occurs when this command is created
        /// </summary>
        /// <param name="hook">Instance of the application</param>
        public override void OnCreate(object hook)
        {
            if (hook == null)
                return;

            m_application = hook as IApplication;

            //Disable if it is not ArcMap
            if (hook is IMxApplication)
                base.m_enabled = true;
            else
                base.m_enabled = false;

            ESRI.ArcGIS.esriSystem.UID u;
            u = new ESRI.ArcGIS.esriSystem.UIDClass();
            u.Value = "DocClicker.DocClicker";
            m_Ext = m_application.FindExtensionByCLSID(u);
            m_ExtConfig = (ESRI.ArcGIS.esriSystem.IExtensionConfig)m_Ext;

            if (m_Logger == null)
            {
                m_Logger = new Logger(m_application);
            }
            //m_Logger.Show();
            m_Logger.Log("Search Tool Created", 5);


            // TODO:  Add other initialization code
        }

        /// <summary>
        /// Occurs when this command is clicked
        /// </summary>
        public override void OnClick()
        {
            //MessageBox.Show("Search gadget present");
            string searchName = "", startDate = "", endDate = "";
            TOCDocClicker TOCcl;
            ESRI.ArcGIS.ArcMapUI.IMxDocument pMxDoc;
            pMxDoc = (ESRI.ArcGIS.ArcMapUI.IMxDocument)m_application.Document;

            //Find the contents view tab to put the log message in.  
            for (int i = 0; i <= pMxDoc.ContentsViewCount; i++)
            {
              //todo: replace the string with a var
              string tempStr = pMxDoc.get_ContentsView(i).Name;
              if (tempStr == "DocClicker")
              {
                TOCcl = (TOCDocClicker)pMxDoc.get_ContentsView(i);
                searchName = TOCcl.docName;
                startDate = TOCcl.startDate;
                endDate = TOCcl.endDate;
                break;
              }
            }

            if (m_docSearch == null)
            {
                m_docSearch = new DocMgrSearch(m_Logger);
            }
            //if (searchName != "")
            //{
              m_docSearch.DoSearch(searchName, startDate, endDate);
            
            //}

            ESRI.ArcGIS.esriSystem.UID pUID;
            pUID = new ESRI.ArcGIS.esriSystem.UIDClass();
  
            ICommandItem pCmdSelect;
            ICommandBars pCommandBars;

            pUID.Value = "esriArcMapUI.SelectByGraphicsCommand"; //{57610895-4F78-11D2-AAAB-000000000000}
            pCommandBars = m_application.Document.CommandBars;
            
            pCmdSelect = pCommandBars.Find(pUID, false, false);
            pCmdSelect.Execute();

        }
        /// <summary>
        /// Called to see whether the command is enabled or not.
        /// </summary>
        public override bool Enabled
        {
            get
            {
                //return base.Enabled;
                if (m_ExtConfig.State == ESRI.ArcGIS.esriSystem.esriExtensionState.esriESEnabled)
                    return true;
                else
                    return false;
            }
        }

        #endregion
    }
}

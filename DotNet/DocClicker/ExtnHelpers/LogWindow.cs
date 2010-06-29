using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.esriSystem;


namespace ExtnHelpers
{
    [Guid("714f8459-7fdc-4cbd-8ddc-1af76e361075")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("ExtnHelpers.LogWindow")]
    public class LogWindow : ESRI.ArcGIS.Framework.IDockableWindowDef
    {
        #region Member variables
        private IApplication _ArcMAP;

        /// <summary>
        /// The user control that is "hosted" in the dockable window
        /// </summary>
        private ctlLog _ctlLog;

        #endregion

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
            MxDockableWindows.Register(regKey);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxDockableWindows.Unregister(regKey);

        }

        #endregion
        #endregion

        #region "IDockableWindowDef Implementations"
        public string Caption
        {
          get
          {
            return ExtnHelpers.ExtnHelperResources.ResourceManager.GetString("WindowCaption");
          }
        }
        /// <summary>
        /// Gets a held to the form's list box. This list box is what appears
        /// in the dockable window.
        /// </summary>
        public int ChildHWND
        {
          get
          {
            //return _LogForm.LogListBox.Handle.ToInt32();
            return _ctlLog.Handle.ToInt32();
            //this.Handle.ToInt32();
          }
        }

        public string Name
        {
          get
          {
            return ExtnHelpers.ExtnHelperResources.ResourceManager.GetString("WindowName");
          }
        }

        /// <summary>
        /// When creating the dockable window, care the form that is hosted...
        /// </summary>
        /// <param name="hook"></param>
        public void OnCreate(object hook)
        {
          _ArcMAP = hook as IApplication;
          _ctlLog = new ctlLog();
        }

        public void OnDestroy()
        {
          _ArcMAP = null;
          _ctlLog = null;
        }
        /// <summary>
        /// Gets a reference to the Form that is hosted in the dockable window;
        /// This is how the extension gets access to the form to write into
        /// The log.
        /// </summary>
        public object UserData
        {
          get
          {
            return _ctlLog;
          }
        }
        #endregion

      }
}

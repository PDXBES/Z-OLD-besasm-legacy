using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.SystemUI;
using System;
using System.Runtime.InteropServices;
namespace SystemsAnalysis.Tracer
{

    [Guid("9a68abe0-bbe6-42a3-8d18-1c8939f8c5f9")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("FeatureClassTracer.TracerToolbar")]
    public class TracerToolbar : IToolBarDef
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
            ControlsToolbars.Register(regKey);
            MxCommandBars.Register(regKey);

            MxCommandBars_PremierToolbar(true, registerType);

        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            ControlsToolbars.Unregister(regKey);
            MxCommandBars.Unregister(regKey);

            MxCommandBars_PremierToolbar(false, registerType);

        }
        /// <summary>
        /// Required MxCommandBars Premier Toolbar registration method -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void MxCommandBars_PremierToolbar(bool reg, Type t)
        {
            if (reg)
            {
                Microsoft.Win32.RegistryKey settingsKey =
                    Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\ESRI", true);
                if (settingsKey != null)
                {
                    Microsoft.Win32.RegistryKey premierKey = settingsKey.CreateSubKey(@"ArcMap\Settings\PremierToolbar\{" + t.GUID.ToString() + "}");
                    premierKey.Close();
                    settingsKey.Close();
                }
            }
            else
            {
                Microsoft.Win32.RegistryKey toolbarKey =
                    Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\ESRI\ArcMap\Settings\PremierToolbar\{" + t.GUID.ToString() + "}", true);
                if (toolbarKey != null)
                {
                    toolbarKey.DeleteSubKeyTree(string.Empty);
                    toolbarKey.Close();
                }
            }
        }

        #endregion
        #endregion

        public TracerToolbar()
        {

        }


        #region IToolBarDef Implementations
        public void GetItemInfo(int pos, IItemDef itemDef)
        {
            switch (pos)
            {
                case 0:
                    itemDef.ID = "SystemsAnalysis.FeatureClassTracer.SelectStartLinkTool";
                    itemDef.Group = false;
                    break;
                case 1:
                    itemDef.ID = "SystemsAnalysis.FeatureClassTracer.SelectStopLinkTool";
                    itemDef.Group = false;
                    break;
                case 2:
                    itemDef.ID = "SystemsAnalysis.Tracer.FeatureClassTracer.ClearLinksCommand";
                    itemDef.Group = false;
                    break;
                case 3:
                    itemDef.ID = "SystemsAnalysis.FeatureClassTracer.DisplaySettingsCommand";
                    itemDef.Group = false;
                    break;
            }
        }

        public string Caption
        {
            get
            {
                return "Feature Class Tracer";
            }
        }

        public string Name
        {
            get
            {
                return "ArcGIS Feature Class Tracer";
            }
        }

        public int ItemCount
        {
            get
            {
                return 4;
            }
        }
        #endregion

    }
}

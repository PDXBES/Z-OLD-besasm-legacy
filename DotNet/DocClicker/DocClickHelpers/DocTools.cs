using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace DocClickHelpers
{
    /// <summary>
    /// Summary description for DocTools.
    /// </summary>
    [Guid("9914dfde-7a05-4739-8692-fcdacb785d16")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("DocClickHelpers.DocTools")]
    public sealed class DocTools : BaseToolbar
    {


        #region COM Registration Function(s)
        // Do not change any thing here -- DP

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
            MxCommandBars.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxCommandBars.Unregister(regKey);
        }

        #endregion
        #endregion
        /// <summary>
        /// Constructor for the Doc Manager toolbar.
        /// Add the commands that are associated with the Document Management Extension.
        /// </summary>
        public DocTools()
        {
            AddItem("{b4ad9ce6-df57-463e-9c03-6d8bf80aee97}", 1); // document inspector
            AddItem("{4ba3c02c-f643-49f9-90c9-c4ea0fcafbac}", 2); // Sloppy selector
            AddItem("{cfcecdf2-3238-4e1f-ab43-0c35867fb271}", 3); // search test gizmo
        }

        public override string Caption
        {
            get
            {
                //TODO: Replace bar caption
                return DocClickHelpers.ResourceManager.GetString("Document Management Tools");
            }
        }
        public override string Name
        {
            get
            {
                //TODO: Replace bar ID
                return "DocTools";
            }
        }


    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.ADF.CATIDs;
using ESRI.ArcGIS.ADF.BaseClasses;

namespace SanitaryAnalysisTools
    {
    /// <summary>
    /// Summary description for SanToolbar.
    /// </summary>
    [Guid ( "ed425e5a-ef9f-4c9d-971d-4d4e301efb5f" )]
    [ClassInterface ( ClassInterfaceType.None )]
    [ProgId ( "SanitaryAnalysisTools.SanToolbar" )]
    public sealed class SanToolbar : BaseToolbar
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
            MxCommandBars.Register ( regKey );
            }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration ( Type registerType )
            {
            string regKey = string.Format ( "HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID );
            MxCommandBars.Unregister ( regKey );
            }

        #endregion
        #endregion

        public SanToolbar ()
            {
            //
            // TODO: Define your toolbar here by adding items
            //
            AddItem ( "SanitaryAnalysisTools.UpdateHasNearbySW" );
            AddItem ( "SanitaryAnalysisTools.UpdateNoNearbySW" );
            AddItem ( "SanitaryAnalysisTools.UpdateMaybeNearbySW" );
            BeginGroup(); //Separator
            AddItem ( "SanitaryAnalysisTools.LayermanagerCommand" );
            
            //AddItem("{FBF8C3FB-0480-11D2-8D21-080009EE4E51}", 1); //undo command
            }

        public override string Caption
            {
            get
                {
                //TODO: Replace bar caption
                return "Sanitary Analysis Tools";
                }
            }
        public override string Name
            {
            get
                {
                //TODO: Replace bar ID
                return "SanToolbar";
                }
            }
        }
    }
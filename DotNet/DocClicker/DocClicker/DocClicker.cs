using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Data;


using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ADF.CATIDs;

//using DocClickHelpers;
using ExtnHelpers;

namespace DocClicker 
{
    [Guid("040b57c0-52da-41c6-a9dd-7ae37f7c0907")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("DocClicker.DocClicker")]
    public class DocClicker : IExtension, IExtensionConfig
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
            MxExtension.Register(regKey);
            SxExtensions.Register(regKey);
            GxExtensions.Register(regKey);
            GMxExtensions.Register(regKey);
        }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration(Type registerType)
        {
            string regKey = string.Format("HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID);
            MxExtension.Unregister(regKey);
            SxExtensions.Unregister(regKey);
            GxExtensions.Unregister(regKey);
            GMxExtensions.Unregister(regKey);

        }

        #endregion
        #endregion
        private IApplication m_application;
        private esriExtensionState m_enableState;
        private Logger m_Logger;
        
       
        private esriExtensionState offState = esriExtensionState.esriESDisabled;
        private bool firstTime = true;

        private const esriLicenseProductCode RequiredProductCode = esriLicenseProductCode.esriLicenseProductCodeArcView;
        private const esriLicenseProductCode RequiredProductCode2 = esriLicenseProductCode.esriLicenseProductCodeArcEditor;
        private const esriLicenseProductCode RequiredProductCode3 = esriLicenseProductCode.esriLicenseProductCodeArcInfo;
        #region IExtension Members

        string IExtension.Name
        {
            get { return "Doc Mgt Interface"; }
        }
        
        void IExtension.Shutdown()
        {
            m_Logger = null;
            m_application = null;
        }

        void IExtension.Startup(ref object initializationData)
        {
            m_application = initializationData as IApplication;
            if (m_application == null)
                return;

        }

        #endregion

        public Logger extensionLogger
        {
            get { return m_Logger; }
        }
        #region IExtensionConfig Members

        string IExtensionConfig.Description
        {
            get { return "Document Management Integration interface\r\n" +
                        "Copyright 2007 CH2M HILL and the City of Portland, Oregon"; }
        }

        string IExtensionConfig.ProductName
        {
            get { return "Document Management Integration"; }
        }

        esriExtensionState IExtensionConfig.State
        {
            get
            {
                return m_enableState;
            }
            set
            {

                // on startup, make sure extension is disabled
                if (this.firstTime == true)
                {
                    this.firstTime = false;
                    this.m_enableState = this.offState;
                    // turn of TOC too -- Do not do any thing here otherwise it will ask to save a file when open the MXD file
                    // TOCONorOFF(false);
                    // ToolBarONorOFF(false);
                    return;
                }
                if (m_enableState != 0 && value == m_enableState)
                    return;

                //Check if ok to enable or disable extension
                esriExtensionState requestState = value;
                if (requestState == esriExtensionState.esriESEnabled)
                {
                    //Cannot enable if it's already in unavailable state
                    if (m_enableState == esriExtensionState.esriESUnavailable)
                    {
                        throw new COMException("Not running the appropriate product license.");
                    }

                    //Determine if state can be changed
                    esriExtensionState checkState = StateCheck(true);
                    m_enableState = checkState;
                    if (m_enableState == esriExtensionState.esriESUnavailable)
                    {
                        throw new COMException("Not running the appropriate product license.");
                    }
                    else
                    {
                        if (m_Logger == null)
                        {
                            m_Logger = new Logger(m_application);
                        }
                        //m_Logger.Show();
                        m_Logger.Log("Extension enabled.", 5);
                        TOCONorOFF(true );
                        ToolBarONorOFF(true );
                        

                    }
                }
                else if (requestState == 0 || requestState == esriExtensionState.esriESDisabled)
                {
                    //Determine if state can be changed
                    esriExtensionState checkState = StateCheck(false);
                    if (checkState != m_enableState)
                    {
                        m_enableState = checkState;
                        TOCONorOFF(false);
                        ToolBarONorOFF(false);
                        if (m_Logger != null)
                        {
                            //m_Logger.Hide();
                        }
                       
                    }
                }

            }
        }
        /// <summary>
        /// Determine extension state 
        /// </summary>
        /// <param name="requestEnable">true if to enable; false to disable</param>
        private esriExtensionState StateCheck(bool requestEnable)
        {
            //Turn on or off extension directly
            if (requestEnable)
            {
                //Check if the correct product is licensed
                IAoInitialize aoInitTestProduct = new AoInitializeClass();
                esriLicenseProductCode prodCode = aoInitTestProduct.InitializedProduct();
                if (prodCode == RequiredProductCode
                    || prodCode == RequiredProductCode2
                    || prodCode == RequiredProductCode3)
                    return esriExtensionState.esriESEnabled;

                return esriExtensionState.esriESUnavailable;
            }
            else
                return esriExtensionState.esriESDisabled;
        }
        private void TOCONorOFF(bool state)
        {
            ESRI.ArcGIS.ArcMapUI.IMxDocument pMxDoc;
            pMxDoc = (ESRI.ArcGIS.ArcMapUI.IMxDocument)m_application.Document;
            ESRI.ArcGIS.ArcMapUI.IContentsView  TOCDOC;
            for (int i = 0; i <= pMxDoc.ContentsViewCount; i++)
            {
                
                //todo: replace the string with a var
                string tempStr = pMxDoc.get_ContentsView(i).Name;
                if (tempStr == "DocClicker")
                {
                    TOCDOC = (ESRI.ArcGIS.ArcMapUI.IContentsView)pMxDoc.get_ContentsView(i);
                   
                    TOCDOC.Visible = state;
                    pMxDoc.UpdateContents();
                    
                    break;
                }
            }
        }
        private void ToolBarONorOFF(bool state)
        {
            ICommandBars pbars;
            // ESRI.ArcGIS.ArcMapUI.IMxDocument pMxDoc;
            pbars = m_application.Document.CommandBars;
            UID puid = new UID() ;
            puid.Value ="DocClickHelpers.DocTools";
            ICommandBar pbar;
            pbar = (ICommandBar)pbars.Find(puid, false , false);
            if (state)
            {
                ICommandBar missing = null;
                pbar.Dock(esriDockFlags.esriDockShow, missing);
            }
            else
            {
                ICommandBar missing = null;
                pbar.Dock(esriDockFlags.esriDockHide, missing);
            }

           

        }
        #endregion
    }
}

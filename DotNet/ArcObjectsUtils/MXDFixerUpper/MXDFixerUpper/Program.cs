using System;
using System.Collections.Generic;
using System.Windows.Forms;
using ESRI.ArcGIS.esriSystem;
using ConceptCave.WaitCursor;

namespace SystemsAnalysis.GIS.SDETools
    {
    static class Program
        {
        private static LicenseInitializer m_AOLicenseInitializer = new SystemsAnalysis.GIS.SDETools.LicenseInitializer ();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main ()
            {
            //ESRI License Initializer generated code.
            m_AOLicenseInitializer.InitializeApplication ( new esriLicenseProductCode[] { esriLicenseProductCode.esriLicenseProductCodeArcInfo },
            new esriLicenseExtensionCode[] { } );
            Application.EnableVisualStyles ();
            Application.SetCompatibleTextRenderingDefault ( false );

            ApplicationWaitCursor.Cursor = Cursors.WaitCursor;
            ApplicationWaitCursor.Delay  = new TimeSpan(0, 0, 0, 0, 100);


            Application.Run ( new MainForm () );
            //Application.Run ( new frmChangeSDE () );
            //Application.Run ( new frmSDEconnection () );

            //ESRI License Initializer generated code.
            //Do not make any call to ArcObjects after ShutDownApplication()
            m_AOLicenseInitializer.ShutdownApplication ();
            }
        }
    }
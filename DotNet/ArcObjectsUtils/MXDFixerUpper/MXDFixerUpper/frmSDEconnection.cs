using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.esriSystem;
using System.IO;
using ReferenceLibrary;

namespace SystemsAnalysis.GIS.SDETools
    {
    public partial class frmSDEconnection : Form
        {
        ReferenceLibrary.ArcConnectionLibrary arcConnectionLibrary = new ReferenceLibrary.ArcConnectionLibrary();

        private string sdeConnectionFolder = "";
        private MainForm parentForm;
        //private StatusStrip statusStrip;

        public frmSDEconnection ()
            {
            InitializeComponent ();

            // Fill the list box with SDE connections
            connectionStatus.Text = "Initializing...";

            listSDEConnections.Items.Clear();
            listSDEConnections.Items.Add ( "Viola-ModelAdmin" );
            listSDEConnections.Items.Add ( "Viola-ModelEditor" );
            listSDEConnections.Items.Add ( "Viola-ModelView" );
            listSDEConnections.Items.Add ( "SirToby-ModelAdmin" );
            listSDEConnections.Items.Add ( "SitToby-ModelEditor" );
            listSDEConnections.Items.Add ( "SirToby-ModelView" );
            listSDEConnections.Items.Add ( "SirToby-OSA" );
            listSDEConnections.Items.Add ( "PDOT Assets" );
            listSDEConnections.Items.Add ( "PWB Water" );
            listSDEConnections.Items.Add ( "EGH Public (CGIS Layers)" );
            listSDEConnections.Items.Add ( "EGH Raster (CGIS Raster)" );

            connectionStatus.Text = "Find existing connections...";

            labelCurrentConnections.Text = "Existing SDE Connections for " + Environment.UserName.ToString ();
            sdeConnectionFolder = "C:\\Documents and Settings\\" + Environment.UserName.ToString () + "\\Application Data\\ESRI\\ArcCatalog\\";

            FillCurrentConnections ( sdeConnectionFolder );
            connectionStatus.Text = "Find existing connections...";
            }

        public frmSDEconnection ( MainForm parentForm )
            {
            InitializeComponent ();

            this.MdiParent = parentForm;

            connectionStatus.Text = "Initializing...";

            // Fill the list box with SDE connections

            connectionStatus.Text = "Adding available SDE Connections...";
            listSDEConnections.Items.Clear ();
            listSDEConnections.Items.Add ( "Viola-ModelAdmin" );
            listSDEConnections.Items.Add ( "Viola-ModelEditor" );
            listSDEConnections.Items.Add ( "Viola-ModelView" );
            listSDEConnections.Items.Add ( "SirToby-ModelAdmin" );
            listSDEConnections.Items.Add ( "SitToby-ModelEditor" );
            listSDEConnections.Items.Add ( "SirToby-ModelView" );
            listSDEConnections.Items.Add ( "SirToby-OSA" );
            connectionStatus.Text = "";

            connectionStatus.Text = "Find existing connections...";
            sdeConnectionFolder = "C:\\Documents and Settings\\" + Environment.UserName.ToString () + "\\Application Data\\ESRI\\ArcCatalog\\";

            FillCurrentConnections ( sdeConnectionFolder );
            connectionStatus.Text = "Ready";
            }

        private void btnGo_Click ( object sender, EventArgs e )
            {
            //// Test getting the current User Name
            //string userName = Environment.UserName.ToString ();
            //textUserName.Text = userName;

            try
                {
                // Check to see if something is selected
                string sdeConnection = listSDEConnections.SelectedItem.ToString ();
                if ( sdeConnection == null )
                    {
                    MessageBox.Show("No SDE Connection Selected", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                    }

                    // Define ESRI variables to make the connection
                    ESRI.ArcGIS.esriSystem.IPropertySet propertySet = new ESRI.ArcGIS.esriSystem.PropertySetClass ();
                    ESRI.ArcGIS.Geodatabase.IWorkspaceFactory2 workspaceFactory;
                    workspaceFactory = ( ESRI.ArcGIS.Geodatabase.IWorkspaceFactory2 ) new ESRI.ArcGIS.DataSourcesGDB.SdeWorkspaceFactoryClass ();

                    connectionStatus.Text = "Checking old connection...";
                    if ( File.Exists ( sdeConnectionFolder + @"\" + sdeConnection + ".sde" ) )
                        {
                        File.Delete ( sdeConnectionFolder + @"\" + sdeConnection + ".sde" );
                        }

                    connectionStatus.Text = "Creating new SDE connection...";
                    switch ( sdeConnection )
                        {
                        case "Viola-ModelAdmin":
                            propertySet = arcConnectionLibrary.create_ArcSDE_Connection ( "VIOLA", "5175", "MODELING_DEV", "ModelAdmin", "daW7horn", "SDE.default" );
                            workspaceFactory.Create ( sdeConnectionFolder, sdeConnection + ".sde", propertySet, 0 );
                            break;
                        case "Viola-ModelEditor":
                            propertySet = arcConnectionLibrary.create_ArcSDE_Connection ( "VIOLA", "5175", "MODELING_DEV", "ModelEditor", "5tribbles", "SDE.default" );
                            workspaceFactory.Create ( sdeConnectionFolder, sdeConnection + ".sde", propertySet, 0 );
                            break;
                        case "Viola-ModelView":
                            propertySet = arcConnectionLibrary.create_ArcSDE_Connection ( "VIOLA", "5175", "MODELING_DEV", "ModelView", "#lenrA", "SDE.default" );
                            workspaceFactory.Create ( sdeConnectionFolder, sdeConnection + ".sde", propertySet, 0 );
                            break;
                        case "SirToby-ModelAdmin":
                            propertySet = arcConnectionLibrary.create_ArcSDE_Connection ( "SIRTOBY", "5175", "MODELING_DEV", "ModelAdmin", "daW7horn", "SDE.default" );
                            workspaceFactory.Create ( sdeConnectionFolder, sdeConnection + ".sde", propertySet, 0 );
                            break;
                        case "SirToby-ModelEditor":
                            propertySet = arcConnectionLibrary.create_ArcSDE_Connection ( "SIRTOBY", "5175", "MODELING_DEV", "ModelEditor", "5tribbles", "SDE.default" );
                            workspaceFactory.Create ( sdeConnectionFolder, sdeConnection + ".sde", propertySet, 0 );
                            break;
                        case "SirToby-ModelView":
                            propertySet = arcConnectionLibrary.create_ArcSDE_Connection ( "SIRTOBY", "5175", "MODELING_DEV", "ModelView", "#lenrA", "SDE.default" );
                            workspaceFactory.Create ( sdeConnectionFolder, sdeConnection + ".sde", propertySet, 0 );
                            break;
                        case "SirToby-OSA":
                            //propertySet = arcConnectionLibrary.create_ArcSDE_Connection ( "SIRTOBY", "sde", "MODELING_DEV", "SDE.DEFAULT", "OSA" );
                            break;
                        case "PDOT Assets":
                            File.Copy ( @"W:\ArcGIS_Tools\SDE_Connections\PDOT.sde", sdeConnectionFolder + "PDOT.sde");
                            break;
                        case "PWB Water":
                            File.Copy ( @"W:\ArcGIS_Tools\SDE_Connections\PWB_water.sde", sdeConnectionFolder + "PWB_water.sde" );
                            break;
                        case "EGH Public (CGIS Layers)":
                            File.Copy ( @"W:\ArcGIS_Tools\SDE_Connections\CGIS_Layer.sde", sdeConnectionFolder + "CGIS_Layer.sde" );
                            break;
                        case "EGH Raster (CGIS Raster)":
                            File.Copy ( @"W:\ArcGIS_Tools\SDE_Connections\CGIS_raster.sde", sdeConnectionFolder + "CGIS_raster.sde" );
                            break;


                        }
                    FillCurrentConnections ( sdeConnectionFolder );
                    }
            catch ( DirectoryNotFoundException ex )
                {
                connectionStatus.Text = "SDE File Directory not found error...";
                MessageBox.Show ( "/nSDE File Directory not found/n" + sdeConnectionFolder + "/nnot found/nVerify the location of your *.sde files", "User SDE Directory Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error );
                DirSearch ( @"c:\" );
                }
            connectionStatus.Text = "Done.";
            }

        private void DirSearch(string sDir)
            {
            try
                {
                foreach ( string d in Directory.GetDirectories ( sDir ) )
                    {
                    foreach ( string f in Directory.GetFiles ( d, "*.sde" ) )
                        {
                        listCurrentConnections.Items.Add ( f );
                        }
                    DirSearch ( d );
                    }
                }
            catch ( System.Exception excpt )
                {
                MessageBox.Show ( excpt.Message );
                }
            }

        private void FillCurrentConnections (string sdeConnectionFolder)
            {
            listCurrentConnections.Items.Clear();
            string[] dirs = Directory.GetFiles ( sdeConnectionFolder, "*.sde" );
            foreach ( string dir in dirs )
                {
                int slashLocation = dir.LastIndexOf ( @"\" );
                if ( slashLocation == -1 )
                    {
                    listCurrentConnections.Items.Add ( dir );
                    }
                else
                    {
                    listCurrentConnections.Items.Add ( dir.Substring ( slashLocation + 1 ) );
                    }
                }
            }

        private void btnExit_Click ( object sender, EventArgs e )
            {
            this.Close ();
            }

        private void frmSDEconnection_Activated ( object sender, EventArgs e )
            {
            
            }
        }
    }
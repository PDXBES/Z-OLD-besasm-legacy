using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using System.IO;
using System.Collections;

namespace SystemsAnalysis.GIS.SDETools
    {
    public partial class frmChangeSDE : Form
        {
        string sdeConnectionFolder = "";
        private MainForm parentForm;
        //private parentForm.statusStrip1 parentForm.statusStrip1;

        public frmChangeSDE ()
            {
            InitializeComponent ();

            connectionStatus.Text = "Initializing...";
            
            sdeConnectionFolder = @"C:\Documents and Settings\" + Environment.UserName.ToString () + @"\Application Data\ESRI\ArcCatalog\";
            FillCurrentConnections ( sdeConnectionFolder );

            connectionStatus.Text = "Ready...";
            }
        /// <summary>
        /// Constructor for the form
        /// </summary>
        /// <param name="parentForm">The main form that opened this form.  
        /// Used to control the StatusStrip</param>
        public frmChangeSDE ( MainForm parentForm )
            {
            InitializeComponent ();

            //this.MdiParent = parentForm;
            
            //connectionStatus.Text = "Initializing...";

            sdeConnectionFolder = @"C:\Documents and Settings\" + Environment.UserName.ToString () + @"\Application Data\ESRI\ArcCatalog\";
            FillCurrentConnections ( sdeConnectionFolder );

            //connectionStatus.Text = "Ready...";
            }

        /// <summary>
        /// Updates the Connection Properties for each SDE Layer with a source
        /// that was selected in listExistingSDE to the one in listSDEConnections
        /// </summary>
        /// <param name="sPath">Full path and name of the MXD document to be converted</param>
        private void UpdateMapDocument ( string sPath )
            {
            IMapDocument mapDocument;
            IMap map;

            mapDocument = new MapDocumentClass ();
            if ( mapDocument.get_IsMapDocument ( sPath ) )
                {
                try
                    {
                    connectionStatus.Text = "Verifying selections...";

                    // Verify a selection
                    string sdeConnection = listSDEConnections.SelectedItem.ToString ();
                    if (listExistingSDE.SelectedItems.Count == 0 )
                        {
                        MessageBox.Show ( "Verify MXD Connections ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                        return;
                        }

                    // Verify the file exists
                    if ( File.Exists ( sdeConnectionFolder + @"\" + sdeConnection) == false)
                        {
                        MessageBox.Show ( "SDE Connection file does not exist!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                        return;
                        }

                    connectionStatus.Text = "Getting SDE Connection Properties...";

                    //This function puts the information from an ArcSDE connection file 
                    // (*.sde) into an IPropertySet
                    ESRI.ArcGIS.esriSystem.IPropertySet propertySet = new ESRI.ArcGIS.esriSystem.PropertySetClass ();
                    IWorkspaceFactory workspaceFactory = new ESRI.ArcGIS.DataSourcesGDB.SdeWorkspaceFactoryClass ();
                    propertySet = workspaceFactory.ReadConnectionPropertiesFromFile ( sdeConnectionFolder + @"\" + sdeConnection);

                    connectionStatus.Text = "Opening Map Document...";
                    // Open the map document
                    mapDocument.Open ( sPath, "" );

                    for ( int l = 0; l <= mapDocument.MapCount - 1; l++ )
                        {
                        map = mapDocument.get_Map ( l );

                        // Update map layers
                        for ( int j = 0; j <= map.LayerCount - 1; j++ )
                            {
                            ILayer thisLayer = ( ILayer ) map.get_Layer ( j );
                            CheckLayer ( thisLayer, propertySet, sdeConnection );
                            }

                        // Update Standalone Tables
                        IStandaloneTableCollection saTableCollection = ( IStandaloneTableCollection ) map;
                        for ( int i = 0; i <= saTableCollection.StandaloneTableCount - 1; i++ )
                            {
                            IStandaloneTable saTable = ( IStandaloneTable ) saTableCollection.get_StandaloneTable ( i );
                            IDataLayer2 dataLayer2 = ( IDataLayer2 ) saTable;
                            UpdateLayerSDEConnection(dataLayer2, propertySet, sdeConnection);
                            }
                        }
                    connectionStatus.Text = "Saving Map...";

                    
                    mapDocument.Save ( false, false );
                    mapDocument.Close ();

                    connectionStatus.Text = "Refreshing MXD Connections...";

                    GetMXD_SDEConnections ();

                    connectionStatus.Text = "Done...";
                    }
                catch ( System.NullReferenceException  )
                    {
                    connectionStatus.Text = "Error...";
                    MessageBox.Show ( "Verify MXD and Available Connections ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                    }
                catch ( Exception ex )
                    {
                    connectionStatus.Text = "Error...";
                    MessageBox.Show ( ex.Message, "frmChangeSDE.UpdateMapDocument()", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    }
                }
            }

        private void btnGo_Click ( object sender, EventArgs e )
            {
            UpdateMapDocument ( textMXDfile.Text );
            }

        private void btnOpenMXDdialog_Click ( object sender, EventArgs e )
            {
            OpenFileDialog openFileDialog1 = new OpenFileDialog ();

            connectionStatus.Text = "Reading MXD Layers...";

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "mxd files (*.mxd)|*.mxd";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if ( openFileDialog1.ShowDialog () == DialogResult.OK )
                {
                textMXDfile.Text = openFileDialog1.FileName;
                }
            }

        private void textMXDfile_TextChanged ( object sender, EventArgs e )
            {
            GetMXD_SDEConnections ();
            }

        private void FillCurrentConnections ( string sdeConnectionFolder )
            {
            listSDEConnections.Items.Clear ();
            string[] dirs = Directory.GetFiles ( sdeConnectionFolder, "*.sde" );
            foreach ( string dir in dirs )
                {
                int slashLocation = dir.LastIndexOf ( @"\" );
                if ( slashLocation == -1 )
                    {
                    listSDEConnections.Items.Add ( dir );
                    }
                else
                    {
                    listSDEConnections.Items.Add ( dir.Substring ( slashLocation + 1 ) );
                    }
                }
            }

        /// <summary>
        /// Changes the SDE Connection properties for the passed Data Layer if
        /// its PathName matches the ones we want to convert in listExistingSDE
        /// It can change any layer that implements IDataLayer2
        /// Cast the layer to IDataLayer2 before sending
        /// </summary>
        /// <param name="dataLayer2">Passed layer must implement IDataLayer2</param>
        /// <param name="propertySet">Set of new connection properties</param>
        /// <param name="sdeConnection">Name of the connection to update to</param>
        private void UpdateLayerSDEConnection ( IDataLayer2 dataLayer2, IPropertySet propertySet, string sdeConnection )
            {
            IDatasetName datasetName = ( IDatasetName ) dataLayer2.DataSourceName;
            IWorkspaceName workspaceName = ( IWorkspaceName ) datasetName.WorkspaceName;

            // Verify that dataLayer2's PathName matched the ones we want to convert (in listExistingSDE)
            // Need to to a foreach because multiple SDE Connections can be updated at once.
            foreach ( object o in listExistingSDE.SelectedItems )
                {
                if ( o.ToString () == workspaceName.PathName.ToString () )
                    {
                    // Match...update the ConnectionProperties
                    workspaceName.ConnectionProperties = propertySet;
                    workspaceName.PathName = sdeConnectionFolder + sdeConnection;
                    }
                }
            }

        /// <summary>
        /// Checks the passed layer, verifies the Type, and updates the Connection Properties
        /// by sending the layer over to UpdateLayerSDEConnection where its PathName is checked
        /// </summary>
        /// <param name="thisLayer">A generic layer that works with all layer types</param>
        /// <param name="propertySet">Set of new connection properties</param>
        /// <param name="sdeConnection">Name of the connection to update to</param>
        private void CheckLayer (ILayer thisLayer, IPropertySet propertySet, string sdeConnection)
            {
            if ( thisLayer is IFeatureLayer )
                {
                IFeatureLayer featureLayer = ( IFeatureLayer ) thisLayer;
                if ( featureLayer.DataSourceType == "SDE Feature Class" )
                    {
                    IDataLayer2 dataLayer2 = ( IDataLayer2 ) featureLayer;
                    UpdateLayerSDEConnection ( dataLayer2, propertySet, sdeConnection );
                    }
                }
            else if ( thisLayer is IRasterLayer )
                {
                // Have to assume that any Raster Layers are SDE-based
                // Currently cannot check the DataSourceType of Rasters.
                IRasterLayer rasterLayer = (IRasterLayer) thisLayer;
                IDataLayer2 dataLayer2 = ( IDataLayer2 ) rasterLayer;
                UpdateLayerSDEConnection ( dataLayer2, propertySet, sdeConnection );
                }
            else if ( thisLayer is IGroupLayer )
                {
                ICompositeLayer compositeLayer = ( ICompositeLayer ) thisLayer;
                for ( int k = 0; k <= compositeLayer.Count - 1; k++ )
                    {
                    // Since any number of layer types can be in a Group Layer,
                    // we need to send each layer of the Group Layer through CheckLayer (again) to be handled
                    ILayer compSubLayer = ( ILayer ) compositeLayer.get_Layer(k);
                    CheckLayer ( compSubLayer, propertySet, sdeConnection );
                    }
                }
            else
                {
                // An unhandled DataType (e.g. TinLayer, CadLayer, TopologyLayer, NetworkLayer)
                // Unlikely any of these are currently present in SDE
                MessageBox.Show (thisLayer.Name + " was not converted", "Unhandled Data Type", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

        private void btnExit_Click ( object sender, EventArgs e )
            {
            this.Close ();
            }

        private void frmChangeSDE_Activated ( object sender, EventArgs e )
            {

            }

        /// <summary>
        /// Fills listExistingSDEConnections with all the different SDE connections that
        /// are in the MXD
        /// </summary>
        private void GetMXD_SDEConnections()
            {
            IMapDocument mapDocument;
            IMap map;
            UID pUID = new UID ();
            // Allows iteration through a set of layers
            IEnumLayer enumLayer;
            IFeatureLayer featureLayer;
            // Provides access to additional members that control the data source properties of a layer
            IDataLayer2 dataLayer2;
            // Provides access to members that supply dataset name information.
            IDatasetName datasetName;
            //Use IWorkspaceName when you are browsing for workspaces and 
            // do not want the overhead of instantiating Workspace objects
            IWorkspaceName workspaceName;

            listExistingSDE.Items.Clear ();

            connectionStatus.Text = "Reading MXD Layers...";

            mapDocument = new MapDocumentClass ();
            if ( mapDocument.get_IsMapDocument ( textMXDfile.Text ) )
                {
                try
                    {
                    // Open the map document
                    mapDocument.Open ( textMXDfile.Text, "" );

                    // Process each Map in the Document
                    for ( int i = 0; i <= mapDocument.MapCount - 1; i++ )
                        {
                        map = mapDocument.get_Map ( i );

                        // Get the layers in this map
                        // Use...
                        //{6CA416B1-E160-11D2-9F4E-00C04F6BC78E} IDataLayer
                        //{40A9E885-5533-11d0-98BE-00805F7CED21} IFeatureLayer
                        //{E156D7E5-22AF-11D3-9F99-00C04F6BC78E} IGeoFeatureLayer
                        //{34B2EF81-F4AC-11D1-A245-080009B6F22B} IGraphicsLayer
                        //{5CEAE408-4C0A-437F-9DB3-054D83919850} IFDOGraphicsLayer
                        //{0C22A4C7-DAFD-11D2-9F46-00C04F6BC78E} ICoverageAnnotationLayer
                        //{EDAD6644-1810-11D1-86AE-0000F8751720} IGroupLayer
                        pUID.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}";
                        enumLayer = map.get_Layers ( pUID, true );

                        // Get information from only the SDE Layers
                        enumLayer.Reset ();
                        featureLayer = ( IFeatureLayer ) enumLayer.Next ();
                        while ( featureLayer != null )
                            {
                            if ( featureLayer.DataSourceType == "SDE Feature Class" )
                                {
                                dataLayer2 = ( IDataLayer2 ) featureLayer;
                                datasetName = ( IDatasetName ) dataLayer2.DataSourceName;
                                // We're going to skip the CGIS Hub data.
                                workspaceName = datasetName.WorkspaceName;
                                // We're going to skip the CGIS Hub data.
                                if ( workspaceName.PathName.ToString () != "" )
                                    {
                                    if ( listExistingSDE.Items.Contains ( workspaceName.PathName.ToString () ) == false )
                                        {
                                        // CGIS SDE layers do not use local connection properties
                                        // It will return an empty string so we need to back out and
                                        // check versus the datasetname.Name
                                        listExistingSDE.Items.Add ( workspaceName.PathName.ToString () );
                                        }
                                    }
                                }
                            featureLayer = ( IFeatureLayer ) enumLayer.Next ();
                            }
                        // Get the Standalone Tables in this map
                        IStandaloneTableCollection saTableCollection = ( IStandaloneTableCollection ) map;
                        for ( i = 0; i <= saTableCollection.StandaloneTableCount - 1; i++ )
                            {
                            IStandaloneTable saTable = ( IStandaloneTable ) saTableCollection.get_StandaloneTable ( i );
                            // StandaloneTable also implements IDataLayer2 
                            dataLayer2 = ( IDataLayer2 ) saTable;
                            datasetName = ( IDatasetName ) dataLayer2.DataSourceName;
                            workspaceName = datasetName.WorkspaceName;
                            if ( workspaceName.PathName.ToString () != "" )
                                {
                                if ( listExistingSDE.Items.Contains ( workspaceName.PathName.ToString () ) == false )
                                    {
                                    listExistingSDE.Items.Add ( workspaceName.PathName.ToString () );
                                    }
                                }
                            }
                        }
                    mapDocument.Close ();
                    }
                catch ( Exception ex )
                    {
                    MessageBox.Show ( ex.Message, "frmChangeSDE.UpdateMapDocument()", MessageBoxButtons.OK, MessageBoxIcon.Error );
                    }
                }
            connectionStatus.Text = "Ready...";
            }

        }
    }
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
// Added References
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;
using ReferenceLibrary;

namespace SanitaryAnalysisTools
    {
    /// <summary>
    /// LayerManagerGUI has the user select the layers necessary
    /// to run the RDII tools.
    /// </summary>
    public partial class LayerManagerGUI : Form
        {
        private IFeatureLayer dscFL;
        private ITable discoTable;
        private IStandaloneTable discoSATable;
        //private bool joinByDSC = false;
        private ArcObjectLibrary aOLibrary;
        private ESRI.ArcGIS.ArcMapUI.IMxDocument m_MxDoc; 

        public LayerManagerGUI ()
            {
            InitializeComponent ();
            }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumDataset"></param>
        public void SetupForm ( IEnumDataset enumDataset, ESRI.ArcGIS.ArcMapUI.IMxDocument mxDoc )
            {
            //When re-entering the settings form, we do not know for sure if the
            // previously selected FeatureClasses still exist in the dicument.
            // However, if it does, it is necessary to remember it.
            IFeatureLayer dscHolder = this.dscFL;
            ITable discoTableHolder = this.discoTable;
            IStandaloneTable discoSATableHolder = this.discoSATable;

            // Create an instance of the ArcObjectLibrary
            aOLibrary = ( ArcObjectLibrary ) new ArcObjectLibrary ();
            // Create an instance of the MxDoc
            m_MxDoc = mxDoc;
            
            this.dscFL = null;
            this.discoTable = null;

            this.cboDSCLayer.SelectedItem = null;
            this.cboDSCLayer.Items.Clear ();

            this.cboDiscoTable.SelectedItem = null;
            this.cboDiscoTable.Items.Clear ();

            // Add the appropriate FeatureClass(es) to each combo box
            IDataset ds;
            ds = enumDataset.Next ();
            while ( ds != null )
                {
                try
                    {
                    if ( ds.Type == esriDatasetType.esriDTFeatureClass )
                        {
                        IFeatureLayer fl;
                        fl = ( IFeatureLayer ) ds;
                        IFeatureClass fc;
                        fc = fl.FeatureClass;

                        if ( fc.ShapeType == esriGeometryType.esriGeometryPolygon )
                            {
                            this.cboDSCLayer.Items.Add ( new FeatureLayerWrapper ( fl ) );
                            }
                        }
                    if ( ds.Type == esriDatasetType.esriDTTable )
                        {
                        // Is this going to work???
                        // Need IStandaloneTable instead??
                        ITable table;
                        table = ( ITable ) ds;
                        //IStandaloneTable saTable;
                        //saTable = ( IStandaloneTable ) table;
                        //string name = saTable.Name;
                        this.cboDiscoTable.Items.Add ( new TableWrapper ( table ) );
                        }
                    }
                catch
                    {
                    }
                finally
                    {
                    ds = enumDataset.Next ();
                    }
                }

            // If possible, update the ComboBox with the FLayer/Table that was previously
            // selected
            // DSC
            try
                {
                foreach ( FeatureLayerWrapper flw in this.cboDSCLayer.Items )
                    {
                    if ( flw.FL == dscHolder )
                        {
                        this.cboDSCLayer.SelectedItem = flw;
                        this.dscFL = dscHolder;
                        break;
                        }
                    }
                }
            catch
                {
                // Select the 1st item in the list
                if ( this.cboDSCLayer.Items.Count > 0 )
                { this.cboDSCLayer.SelectedIndex = 0; }
                }
            // RDII Table
            try
                {
                foreach ( TableWrapper tw in this.cboDiscoTable.Items )
                    {
                    if ( tw.SATable == discoSATableHolder )
                        {
                        this.cboDiscoTable.SelectedItem = tw;
                        this.discoSATable = discoSATableHolder;
                        break;
                        }
                    }
                }
            catch
                {
                // Select 1st item on the list
                if ( this.cboDiscoTable.Items.Count > 0 )
                { this.cboDiscoTable.SelectedIndex = 0; }
                }
            return;
            }
        /// <summary>
        /// Provides a wrapper around IFeatureClass that allows us to add
        /// IFeatureClass to a ListBox and retrieve a meaningful ToString(),
        /// since ESRI's ToString implementation is shit (i.e. returns
        /// "COM_OBJECT" for all IFeatureClass objects.)
        /// </summary>
        protected class FeatureLayerWrapper
            {
            private IFeatureClass fc;
            private IFeatureLayer fl;
            private string description;

            public FeatureLayerWrapper ( IFeatureLayer fl )
                {
                this.fl = fl;
                this.fc = fl.FeatureClass;
                string name = fl.Name;
                int dotLocation = name.LastIndexOf ( "." );
                if ( dotLocation == -1 )
                    {
                    this.description = fl.Name;
                    }
                else
                    {
                    this.description = name.Substring ( dotLocation + 1 );
                    }
                }
            public override string ToString ()
                {
                return this.description;
                }
            public IFeatureClass FC
                {
                get { return this.fc; }
                }
            public IFeatureLayer FL
                {
                get { return this.fl; }
                }
            }

        private void okChanges_Click ( object sender, EventArgs e )
            {
            try
                {
                // Need to add a NULL selection trap in here
                FeatureLayerWrapper flw;
                flw = ( FeatureLayerWrapper ) this.cboDSCLayer.SelectedItem;
                this.dscFL = flw.FL;

                TableWrapper tw;
                tw = ( TableWrapper ) this.cboDiscoTable.SelectedItem;
                this.discoSATable = tw.SATable;

                #region Table to Feature Layer join
                // Make the join if necessary
                if ( ckJoinByDSC.Checked)
                    {
                    // Since the object in the combobox is a feature class, it is not directly connected
                    // to the layer in the map TOC.
                    // We need to go from the object in the combobox back to the TOC
                    // Get the index in the TOX of the selected layer
                    int dscIndex = aOLibrary.GetIndexNumberOfLayerName ( m_MxDoc.ActiveView, this.dscFL.Name );
                    // Get the IFeatureLayer at that index
                    IFeatureLayer dscFLayerTOC = aOLibrary.GetIFeatureLayerFromLayerIndexNumber ( m_MxDoc.ActiveView, dscIndex );
                    IGeoFeatureLayer dscGFLayer = ( IGeoFeatureLayer ) dscFLayerTOC;

                    Type t2 = Type.GetTypeFromProgID ( "esriCarto.FeatureLayer" );
                    System.Object obj2 = Activator.CreateInstance ( t2 );
                    IGeoFeatureLayer dscGFLayer2 = ( IGeoFeatureLayer ) obj2;

                    // Repeat with the Table
                    int discoIndex = aOLibrary.GetIndexNumberOfStandaloneTable ( m_MxDoc.ActiveView, this.discoSATable.Name );
                    ITable discoTableTOC = aOLibrary.GetStandaloneTableFromTableIndexNumber ( m_MxDoc.ActiveView, discoIndex );

                    //Type t2 = Type.GetTypeFromProgID ( "esriGeodatabase.Table" );
                    //System.Object obj2 = Activator.CreateInstance ( t2 );
                    //ITable discoTableTOC = aOLibrary.GetStandaloneTableFromTableIndexNumber ( m_MxDoc.ActiveView, discoIndex );
                    
                    //IMemoryRelationshipClassFactory memoryRelClassFactory = new MemoryRelationshipClassFactoryClass ();
                    //Type t = Type.GetTypeFromProgID ( c );
                    
                    //ESRI.ArcGIS.esriSystem.UID memoryRelClassFactoryClassID = new ESRI.ArcGIS.esriSystem.UID();
                    //IMemoryRelationshipClassFactory memRCF = new MemoryRelationshipClassFactoryClass();
                    //memoryRelClassFactoryClassID = MemoryRelationshipClassFactoryClass.GetClassID;
                    //object classIDValue = memoryRelClassFactoryClassID.Value;
                    //string s = classIDValue.ToString();
                    ESRI.ArcGIS.esriSystem.UID memoryRelClassFactoryClassID = new ESRI.ArcGIS.esriSystem.UID ();
                    memoryRelClassFactoryClassID.Value = "esriGeoDatabase.Feature";
                    object classIDValue = memoryRelClassFactoryClassID.Value;
                    string s = classIDValue.ToString ();

                    //Type t3 = Type.

                    Type t = Type.GetTypeFromProgID ( "esriGeoDatabase.MemoryRelationshipClassFactoryClass" );
                    System.Object obj = Activator.CreateInstance ( t );
                    IMemoryRelationshipClassFactory memoryRelClassFactory = obj as IMemoryRelationshipClassFactory;
                    IRelationshipClass relationshipClass = memoryRelClassFactory.Open ( "testJoin", 
                                                                (IObjectClass)discoTableTOC, 
                                                                "DSCID", 
                                                                dscGFLayer.DisplayFeatureClass, 
                                                                "DSCID", 
                                                                "forward", 
                                                                "backward", 
                                                                esriRelCardinality.esriRelCardinalityOneToOne );

                    IDisplayRelationshipClass displayRelClass = ( IDisplayRelationshipClass ) dscGFLayer;
                    displayRelClass.DisplayRelationshipClass ( relationshipClass, esriJoinType.esriLeftOuterJoin );
                #endregion
                    }

                this.DialogResult = DialogResult.OK;
                }
            catch ( Exception ex )
                {
                MessageBox.Show ( "Could not save settings: " + ex.ToString (),
                    "Error Saving Settings" );
                this.DialogResult = DialogResult.Abort;
                }
            }

        private void cancelButton_Click ( object sender, EventArgs e )
            {
            this.DialogResult = DialogResult.Cancel;
            }

        public IFeatureLayer DscFL
            {
            get { return this.dscFL; }
            }

        public IStandaloneTable DiscoSATable
            {
            get { return this.discoSATable; }
            }

        //public bool JoinByDSC
        //    {
        //    get { return this.joinByDSC; }
        //    }

        protected class TableWrapper
            {
            private ITable t;
            private IStandaloneTable saT;
            private string description;

            public TableWrapper ( ITable t )
                {
                this.t = t;
                this.saT = ( IStandaloneTable ) t;
                string name = saT.Name;
                int dotLocation = name.LastIndexOf ( "." );
                if ( dotLocation == -1 )
                    {
                    this.description = saT.Name;
                    }
                else
                    {
                    this.description = name.Substring ( dotLocation + 1 );
                    }
                }

            public override string ToString ()
                {
                return this.description;
                }

            public IStandaloneTable SATable
                {
                get { return this.saT; }
                }
            public ITable T
                {
                get { return this.t; }
                }
            }

        private void cboDSCLayer_SelectedIndexChanged ( object sender, EventArgs e )
            {
            if ( this.cboDSCLayer.SelectedItem == null )
            { return; }

            FeatureLayerWrapper flw = ( FeatureLayerWrapper ) this.cboDSCLayer.SelectedItem;
            IFeatureClass fc = flw.FC;
            }

        private void cboDiscoTable_SelectedIndexChanged ( object sender, EventArgs e )
            {
            if ( this.cboDiscoTable.SelectedItem == null )
            { return; }

            TableWrapper t = ( TableWrapper ) this.cboDiscoTable.SelectedItem;
            IStandaloneTable sat = t.SATable;
            }
        }// close public partial class LayerManagerGUI : Form
    } // close namespace SanitaryAnalysisTools
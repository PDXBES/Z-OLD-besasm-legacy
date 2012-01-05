// Standard References
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ADF.CATIDs;
// Added references
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Editor;
using System.Windows.Forms;
using ReferenceLibrary;

namespace SanitaryAnalysisTools
    {
    // These events are here to...?
    public delegate void OnClickEventHandler(object sender, EventArgs e);
    public delegate void OnMouseDownEventHandler(object sender, ClickEventArgs e);
    public delegate void OnMouseMoveEventHandler(object sender, ClickEventArgs e);
    public delegate void OnMouseUpEventHandler(object sender, ClickEventArgs e);

    [Guid ( "5eb48fb2-5b8e-416d-b7e5-36402128663b" )]
    [ClassInterface ( ClassInterfaceType.None )]
    [ProgId ( "SanitaryAnalysisTools.RdiiDisconnectExtension" )]
    public class RdiiDisconnectExtension : IExtension, IExtensionConfig, IPersistVariant
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
            MxExtension.Register ( regKey );

            }
        /// <summary>
        /// Required method for ArcGIS Component Category unregistration -
        /// Do not modify the contents of this method with the code editor.
        /// </summary>
        private static void ArcGISCategoryUnregistration ( Type registerType )
            {
            string regKey = string.Format ( "HKEY_CLASSES_ROOT\\CLSID\\{{{0}}}", registerType.GUID );
            MxExtension.Unregister ( regKey );

            }

        #endregion
        #endregion
        // Standard
        private IApplication m_application;
        private esriExtensionState m_enableState;
        // Added
        private IMxDocument m_MxDoc;
        private IMap map;

        private LayerManagerGUI layerManagerGUI;
        private IFeatureLayer dscFL;
        private IStandaloneTable discoSATable;
        //private bool joinByDSC;

        private ICommandBars commandBars;
        private LayerManagerCommand layerManagerCommand;
        private UpdateHasNearbySW updateHasNearbySW;
        private UpdateMaybeNearbySW updateMaybeNearbySW;
        private UpdateNoNearbySW updateNoNearbySW;

        private ArcObjectLibrary aOLibrary;

        private bool commandBarLoaded;

        #region IDocumentEvents Event Handlers
        // Add others as needed
        private static IDocumentEvents_OpenDocumentEventHandler odHandler;
        private static IDocumentEvents_NewDocumentEventHandler ndHandler;
        // mcHandler...MAY need to use this one to listen if the maps have been changed
        // cmHandler
        // avHandler
        // bcHandler
        // cdHandler
        #endregion

        #region IExtension Members
        public RdiiDisconnectExtension ()
            {
            this.dscFL = null;
            this.discoSATable = null;
            this.commandBarLoaded = false;
            aOLibrary = new ArcObjectLibrary ();
            }

        private void ExtensionSettings ( object sender, EventArgs e )
            {
            IDocumentDatasets documentDatasets;
            try
                {
                documentDatasets = ( IDocumentDatasets ) m_MxDoc;
                if ( documentDatasets.Datasets == null )
                    {
                    throw new Exception ( "No Datasets Loaded." );
                    }
                this.layerManagerGUI.SetupForm ( documentDatasets.Datasets, m_MxDoc );

                if ( this.layerManagerGUI.ShowDialog () == System.Windows.Forms.DialogResult.OK )
                    {
                    dscFL = this.layerManagerGUI.DscFL;
                    discoSATable = this.layerManagerGUI.DiscoSATable;
                    //joinByDSC = this.layerManagerGUI.JoinByDSC;
                    }
                else
                    {
                    // DialogResult <> OK...I don't know how to handle this yet.
                    }
                }
            catch ( Exception ex )
                {
                if(this.layerManagerGUI.Visible)
                    {this.layerManagerGUI.Hide();}

                System.Windows.Forms.MessageBox.Show(ex.ToString(), "ExtensionSettings Error");
                }
            }
        /// <summary>
        /// Used by the tools to verify that the correct map FLayers have
        /// been identified in the Layer Settings form.
        /// Also gets the map ready to use the tool.
        /// I'm not convinced I need to do this...
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VerifyMapSettings ( object sender, EventArgs e )
            {
            m_application.StatusBar.set_Message ( 0, "Map Prep..." );

            // Check if all the layers needed for the utility have been assigned
            if ( this.dscFL == null )
                {
                System.Windows.Forms.MessageBox.Show ( "DSC layer has not been assigned.\n"
                + "Click Layer Settings to select the DSC Feature Layer." );
                return;
                }
            // The DSC table must be selected too
            if ( this.discoSATable == null )
                {
                System.Windows.Forms.MessageBox.Show ( "Stormwater Data Table has not been assigned.\n"
                + "Click Layer Settings to select the table." );
                return;
                }

            // Set the DSC layer as selectable.
            }
        /// <summary>
        /// Used to set the DiscoAssessment field in SP_RDII_Disconnections to 1 (Yes)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HasSW ( object sender, EventArgs e )
            {
            // First let's test for the things that have been selected.
            map = ( IMap ) m_MxDoc.ActiveView;
            if ( map.SelectionCount == 0 )
                {System.Windows.Forms.MessageBox.Show ( "No objects selected" );}
            //else
            //    {System.Windows.Forms.MessageBox.Show ( map.SelectionCount +" objects selected");}
            
            // ISelectionSet???
            IFeatureSelection dscFSelection;
            dscFSelection = ( IFeatureSelection ) dscFL;
            if ( dscFSelection.SelectionSet.Count == 0 )
            { System.Windows.Forms.MessageBox.Show ( "No dscFL selected" ); }
            //else
            //{ System.Windows.Forms.MessageBox.Show ( dscFSelection.SelectionSet.Count + " DSCs selected" ); }
            
            // use IEnumIDs to read the SelectionSet IDs
            IEnumIDs enumIDs = (IEnumIDs) dscFSelection.SelectionSet.IDs;
            int fieldIndex = dscFL.FeatureClass.FindField ( "DSCID" );
            //string s = "\r\nID - DSCID";
            //IFeature feature;

            int iD = enumIDs.Next ();
            //while ( iD != -1 ) // -1 is returned after the last valid ID has been reached
            //    {
            //    feature = dscFL.FeatureClass.GetFeature ( iD );
            //    s += "\r\n" + iD + ": " + feature.get_Value ( fieldIndex );
            //    iD = enumIDs.Next ();
            //    }
            //System.Windows.Forms.MessageBox.Show("dscFSelection contains: " +
            //    dscFSelection.SelectionSet.Count + "\r\nWith the following DSCs" +
            //    s, "Selection Results");

            // Update the DiscoAssessment value for the matching rows in the discoSATable
            UpdateDiscoSetting ( 1 );
            }



        /// <summary>
        /// Name of extension. Do not exceed 31 characters
        /// </summary>
        public string Name
            {
            get
                {
                //TODO: Modify string to uniquely identify extension
                return "RdiiDisconnectionExtension";
                }
            }

        public void Shutdown ()
            {
            //TODO: Clean up resources

            m_application = null;
            m_MxDoc = null;
            }

        public void Startup ( ref object initializationData )
            {
            m_application = initializationData as IApplication;
            if ( m_application == null )
                return;

            //TODO: Add code to initialize the extension

            // Start listening for the MxDocument events.
            //  Must implicitly cast IMxDoc as an IApp
            m_MxDoc = ( IMxDocument ) m_application.Document;

            #region Event Handlers
            // TODO
            ndHandler = new IDocumentEvents_NewDocumentEventHandler ( NewDocument );
            ( ( IDocumentEvents_Event ) m_MxDoc ).NewDocument += ndHandler;

            odHandler = new IDocumentEvents_OpenDocumentEventHandler ( OpenDocument );
            ( ( IDocumentEvents_Event ) m_MxDoc ).OpenDocument += odHandler;
            #endregion
            }

        #endregion

        #region IExtensionConfig Members

        public string Description
            {
            get
                {
                //TODO: Replace description (use \r\n for line break)
                return "RDII Disconnection Extension\r\n" +
                    "Copyright © City of Portland 2009\r\n\r\n" +
                    "by: Mike Szwaya - BES/Asset Systems Management\r\n"+
                    "This extension is created from a .Net template.";
                }
            }

        /// <summary>
        /// Friendly name shown in the Extension dialog
        /// </summary>
        public string ProductName
            {
            get
                {
                //TODO: Replace
                return "RDII Disconnection Extension";
                }
            }

        public esriExtensionState State
            {
            get
                {
                return m_enableState;
                }
            set
                {
                if ( m_enableState != 0 && value == m_enableState )
                    return;

                //Check if ok to enable or disable extension
                esriExtensionState requestState = value;
                if ( requestState == esriExtensionState.esriESEnabled )
                    {
                    //Cannot enable if it's already in unavailable state
                    if ( m_enableState == esriExtensionState.esriESUnavailable )
                        {
                        throw new COMException ( "Cannot enable extension" );
                        }

                    //Determine if state can be changed
                    esriExtensionState checkState = StateCheck ( true );
                    m_enableState = checkState;
                    }
                else if ( requestState == 0 || requestState == esriExtensionState.esriESDisabled )
                    {
                    //Determine if state can be changed
                    esriExtensionState checkState = StateCheck ( false );
                    if ( checkState != m_enableState )
                        m_enableState = checkState;
                    }

                }
            }

        #endregion

        /// <summary>
        /// Determine extension state 
        /// </summary>
        /// <param name="requestEnable">true if to enable; false to disable</param>
        private esriExtensionState StateCheck ( bool requestEnable )
            {
            //TODO: Replace with advanced extension state checking if needed
            //Turn on or off extension directly
            if ( requestEnable )
                return esriExtensionState.esriESEnabled;
            else
                return esriExtensionState.esriESDisabled;
            }

        #region IPersistVariant Members

        public UID ID
            {
            get
                {
                UID typeID = new UIDClass ();
                typeID.Value = GetType ().GUID.ToString ( "B" );
                return typeID;
                }
            }

        public void Load ( IVariantStream Stream )
            {
            //TODO: Load persisted data from document stream

            Marshal.ReleaseComObject ( Stream );
            }

        public void Save ( IVariantStream Stream )
            {
            //TODO: Save extension related data to document stream

            Marshal.ReleaseComObject ( Stream );
            }

        #endregion

        #region IDocumentEvents members
        public void NewDocument ()
            {
            if ( this.commandBarLoaded )
            { return; }

            IDocument m_Doc;
            m_Doc = ( IDocument ) m_MxDoc;

            #region Main Toolbar Development
            commandBars = m_Doc.CommandBars;

            UID u;
            ICommandItem commandItem;

            u = new UIDClass ();
            u.Value = "SanitaryAnalysisTools.LayerManagerCommand";
            commandItem = commandBars.Find ( u, false, false );
            this.layerManagerCommand= ( LayerManagerCommand) commandItem.Command;

            u = new ESRI.ArcGIS.esriSystem.UIDClass ();
            u.Value = "SanitaryAnalysisTools.UpdateNoNearbySW";
            commandItem = commandBars.Find ( u, false, false );
            this.updateNoNearbySW = ( UpdateNoNearbySW ) commandItem.Command;

            u = new ESRI.ArcGIS.esriSystem.UIDClass ();
            u.Value = "SanitaryAnalysisTools.UpdateMaybeNearbySW";
            commandItem = commandBars.Find ( u, false, false );
            this.updateMaybeNearbySW = ( UpdateMaybeNearbySW) commandItem.Command;

            u = new ESRI.ArcGIS.esriSystem.UIDClass ();
            u.Value = "SanitaryAnalysisTools.UpdateHasNearbySW";
            commandItem = commandBars.Find ( u, false, false );
            this.updateHasNearbySW = ( UpdateHasNearbySW ) commandItem.Command;

            // Create the Layer Manager Form
            layerManagerGUI = new LayerManagerGUI ();

            // Add all the tool & command methods here
            // ...mildly tedious
            this.layerManagerCommand.Click += new OnClickEventHandler ( this.ExtensionSettings );
            this.updateHasNearbySW.Click += new OnClickEventHandler ( this.HasSW );
            this.updateMaybeNearbySW.Click += new OnClickEventHandler ( this.MaybeSW );
            this.updateNoNearbySW.Click += new OnClickEventHandler ( this.NoSW );

            # endregion
            return;
            }

        public void OpenDocument ()
            {
            // Treat opening an existing document the same as creating a new one
            this.NewDocument ();
            return;
            }
        #endregion
        /// <summary>
        /// Updates the DiscoAssessment value in the discoSATable to the 
        /// parameter value passed by the calling method
        /// </summary>
        /// <param name="discoAssessment">
        /// Equal to the following:
        /// 0: No
        /// 1: Yes
        /// 2: Maybe</param>
        public void UpdateDiscoSetting (int discoAssessment)
            {
            //throw new System.NotImplementedException ();
            // Get the editor
            IEditor2 pEditor = aOLibrary.GetESRIEditorFrom ( (IMxApplication) m_application);
            if ( pEditor.Map == null )
                {
                System.Windows.Forms.MessageBox.Show ( "Please start edit session" );
                return;
                }
            if(pEditor.SelectionCount==0)
                {
                System.Windows.Forms.MessageBox.Show ( "Nothing Selected" );
                return;
                }

            // Create an edit operation
            pEditor.StartOperation ();

            IFeatureSelection dscFSelection;
            dscFSelection = ( IFeatureSelection ) dscFL;

            // use IEnumIDs to read the SelectionSet IDs
            IEnumIDs enumIDs = ( IEnumIDs ) dscFSelection.SelectionSet.IDs;
            int fieldIndex = dscFL.FeatureClass.FindField ( "DSCID" );
            int discoAssIndex = discoSATable.Table.FindField ( "DiscoAssessment" );
            IFeature feature;
            //string s = "DSC : disco Table";
            
            // Enumerate through the selected DSCs by using the ID
            int iD = enumIDs.Next ();
            while ( iD != -1 ) // -1 is returned after the last valid ID has been reached
                {
                // Get the current feature
                feature = dscFL.FeatureClass.GetFeature ( iD );
                // Create the query filter
                IQueryFilter queryByDSCID = new QueryFilterClass ();
                // Set the WHERE clause
                queryByDSCID.WhereClause = "DSCID = " + feature.get_Value ( fieldIndex );
                // Execute the query filter
                ICursor discoCursor = discoSATable.Table.Update ( queryByDSCID, false );
                // Retrieve the results
                IRow discoRow = discoCursor.NextRow ();
                while ( discoRow != null )
                    {
                    //s += "\r\n " + queryByDSCID.WhereClause + " : " + discoRow.get_Value ( 0 );
                    discoRow.set_Value ( discoAssIndex, discoAssessment );
                    discoCursor.UpdateRow ( discoRow );
                    discoRow = discoCursor.NextRow ();
                    }
                System.Runtime.InteropServices.Marshal.ReleaseComObject ( discoCursor );
                iD = enumIDs.Next ();
                }
            // Stop the Edit Operation
            pEditor.StopOperation ( "Update DiscoAssessment" );
            // Refresh the map to redisplay the features
            pEditor.Display.Invalidate ( null, true, ( short ) esriScreenCache.esriAllScreenCaches );
            
            //System.Windows.Forms.MessageBox.Show ( s, "Query Summary" );
            return;
            }
        /// <summary>
        /// Creates a IDisplayRelationshipClass between the Feature Layer and the Standalone
        /// Table.
        /// </summary>
        /// <param name="fl"></param>
        /// <param name="sat"></param>
        private void JoinFLayerToSATable ( IFeatureLayer fl, IStandaloneTable sat )
            {
            
            }

        /// <summary>
        /// Used to set the DiscoAssessment field in SP_RDII_Disconnections to 2 (Maybe)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaybeSW ( object sender, EventArgs e )
            {
            // First let's test for the things that have been selected.
            map = ( IMap ) m_MxDoc.ActiveView;
            if ( map.SelectionCount == 0 )
            { System.Windows.Forms.MessageBox.Show ( "No objects selected" ); }
            //else
            //    {System.Windows.Forms.MessageBox.Show ( map.SelectionCount +" objects selected");}

            // ISelectionSet???
            IFeatureSelection dscFSelection;
            dscFSelection = ( IFeatureSelection ) dscFL;
            if ( dscFSelection.SelectionSet.Count == 0 )
            { System.Windows.Forms.MessageBox.Show ( "No dscFL selected" ); }
            //else
            //{ System.Windows.Forms.MessageBox.Show ( dscFSelection.SelectionSet.Count + " DSCs selected" ); }

            // use IEnumIDs to read the SelectionSet IDs
            IEnumIDs enumIDs = ( IEnumIDs ) dscFSelection.SelectionSet.IDs;
            int fieldIndex = dscFL.FeatureClass.FindField ( "DSCID" );
            //string s = "\r\nID - DSCID";
            //IFeature feature;

            int iD = enumIDs.Next ();
            //while ( iD != -1 ) // -1 is returned after the last valid ID has been reached
            //    {
            //    feature = dscFL.FeatureClass.GetFeature ( iD );
            //    s += "\r\n" + iD + ": " + feature.get_Value ( fieldIndex );
            //    iD = enumIDs.Next ();
            //    }
            //System.Windows.Forms.MessageBox.Show("dscFSelection contains: " +
            //    dscFSelection.SelectionSet.Count + "\r\nWith the following DSCs" +
            //    s, "Selection Results");

            // Update the DiscoAssessment value for the matching rows in the discoSATable
            UpdateDiscoSetting ( 2 );
            }

        private void NoSW ( object sender, EventArgs e )
            {
            // First let's test for the things that have been selected.
            map = ( IMap ) m_MxDoc.ActiveView;
            if ( map.SelectionCount == 0 )
            { System.Windows.Forms.MessageBox.Show ( "No objects selected" ); }
            //else
            //    {System.Windows.Forms.MessageBox.Show ( map.SelectionCount +" objects selected");}

            // ISelectionSet???
            IFeatureSelection dscFSelection;
            dscFSelection = ( IFeatureSelection ) dscFL;
            if ( dscFSelection.SelectionSet.Count == 0 )
            { System.Windows.Forms.MessageBox.Show ( "No dscFL selected" ); }
            //else
            //{ System.Windows.Forms.MessageBox.Show ( dscFSelection.SelectionSet.Count + " DSCs selected" ); }

            // use IEnumIDs to read the SelectionSet IDs
            IEnumIDs enumIDs = ( IEnumIDs ) dscFSelection.SelectionSet.IDs;
            int fieldIndex = dscFL.FeatureClass.FindField ( "DSCID" );
            //string s = "\r\nID - DSCID";
            //IFeature feature;

            int iD = enumIDs.Next ();
            //while ( iD != -1 ) // -1 is returned after the last valid ID has been reached
            //    {
            //    feature = dscFL.FeatureClass.GetFeature ( iD );
            //    s += "\r\n" + iD + ": " + feature.get_Value ( fieldIndex );
            //    iD = enumIDs.Next ();
            //    }
            //System.Windows.Forms.MessageBox.Show("dscFSelection contains: " +
            //    dscFSelection.SelectionSet.Count + "\r\nWith the following DSCs" +
            //    s, "Selection Results");

            // Update the DiscoAssessment value for the matching rows in the discoSATable
            UpdateDiscoSetting ( 0 );
            }
        }
    }
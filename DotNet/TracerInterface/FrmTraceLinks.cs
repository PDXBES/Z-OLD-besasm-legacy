using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SystemsAnalysis.Modeling;
using SystemsAnalysis.Utils;
using ESRI.ArcGIS.Carto;


namespace SystemsAnalysis.Tracer
{
    /// <summary>
    /// Modal Dialog for returning a Links collection by tracing a Links network
    /// </summary>
    public partial class FrmTraceLinks : Form
    {
        Links linkNetwork;
        FeatureLayer networkFL;
        //Dscs dscs;
        private Links startLinks;
        private Links stopLinks;

        private Links tracedLinks;

        private SystemsAnalysis.Utils.ArcObjects.MapControl.SelectFeatures selectCommand;
        private SystemsAnalysis.Utils.ArcObjects.MapControl.ClearFeatureSelection clearCommand;
        private SystemsAnalysis.Utils.ArcObjects.MapControl.Pan panCommand;
        private SystemsAnalysis.Utils.ArcObjects.MapControl.ZoomIn zoomInCommand;
        private SystemsAnalysis.Utils.ArcObjects.MapControl.ZoomOut zoomOutCommand;
        private SystemsAnalysis.Tracer.SelectStartLinkTool selectStartLinkTool;
        private SystemsAnalysis.Tracer.SelectStopLinkTool selectStopLinkTool;
        private SystemsAnalysis.Tracer.TracerExtension tracerExtension;
        
        /// <summary>
        /// Creates a new TracerInterface for tracing links within the provided link network
        /// </summary>
        /// <param name="linkNetwork">A Links collection representing a traceable network</param>
        /// <param name="networkLayer">The path to an ArcGIS .lyr file containing the same links as the provided Links collection linkNetwork</param>
        /// <param name="usNodeFieldName">The field within linkNetwork and networkLayer that contains the upstream node of a Link</param>
        /// /// <param name="dsNodeFieldName">The field within linkNetwork and networkLayer that contains the downstream node of a Link</param>
        public FrmTraceLinks(Links linkNetwork, string networkLayer, string usNodeFieldName, string dsNodeFieldName)
        {
            InitializeComponent();

            this.linkNetwork = linkNetwork;
            this.tracedLinks = new Links();
            SetupMapControl();
            try
            {                
                networkFL = (FeatureLayer)LoadLayerFile(networkLayer);
                tracerExtension.SetupTracer(linkNetwork.LinkNetwork,
                    networkFL, usNodeFieldName, dsNodeFieldName, axMapControl.Map);
                networkFL.Selectable = true;                

                ESRI.ArcGIS.Geometry.Point point;
                point = new ESRI.ArcGIS.Geometry.PointClass();
                point.X = 7643708.753;
                point.Y = 681634.261;
                axMapControl.CenterAt(point);
                axMapControl.MapScale = 10000.0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            InitializeLinkDisplayBoxes();
        }

        /// <summary>
        /// Adds a unselectable layer to the control to provide landmarks (e.g. street names, node names, 
        /// basin boundaries) aiding the user in selecting appropriate Links.
        /// </summary>
        /// <param name="referenceLayer">The path to an ArcGIS .lyr file containing the reference layer to be added to the control</param>
        /// <param name="position">The relative position of the layer to be added (1 = topmost layer)</param>
        public void AddReferenceLayer(string referenceLayer, int position)
        {
                IFeatureLayer pFL;
                pFL = (FeatureLayer)LoadLayerFile(referenceLayer, position);
                pFL.Selectable = false;                                
        }

        #region Map panel command button actions
        private void SetupMapControl()
        {
            selectCommand =
                new SystemsAnalysis.Utils.ArcObjects.MapControl.SelectFeatures();
            selectCommand.OnCreate(axMapControl.Object);
            axMapControl.CurrentTool = selectCommand;
            clearCommand =
                new SystemsAnalysis.Utils.ArcObjects.MapControl.ClearFeatureSelection();
            clearCommand.OnCreate(axMapControl.Object);
            panCommand =
                new SystemsAnalysis.Utils.ArcObjects.MapControl.Pan();
            panCommand.OnCreate(axMapControl.Object);
            zoomInCommand =
                new SystemsAnalysis.Utils.ArcObjects.MapControl.ZoomIn();
            zoomInCommand.OnCreate(axMapControl.Object);
            zoomOutCommand =
                new SystemsAnalysis.Utils.ArcObjects.MapControl.ZoomOut();
            zoomOutCommand.OnCreate(axMapControl.Object);
            selectStartLinkTool =
                new SystemsAnalysis.Tracer.SelectStartLinkTool();
            selectStartLinkTool.OnCreate(axMapControl.Object);
            selectStopLinkTool =
                new SystemsAnalysis.Tracer.SelectStopLinkTool();
            selectStopLinkTool.OnCreate(axMapControl.Object);
            tracerExtension = new SystemsAnalysis.Tracer.TracerExtension();

            this.selectStopLinkTool.MouseDown += new SystemsAnalysis.Tracer.OnMouseDownEventHandler(CallTracerExtension);
            this.selectStartLinkTool.MouseDown += new SystemsAnalysis.Tracer.OnMouseDownEventHandler(CallTracerExtension);            
        }
        private void btnSelectFeatures_Click(object sender, System.EventArgs e)
        {
            axMapControl.CurrentTool = selectCommand;
        }
        private void btnClearSelection_Click(object sender, System.EventArgs e)
        {
            clearCommand.OnClick();
        }
        private void btnZoomIn_Click(object sender, System.EventArgs e)
        {
            axMapControl.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerZoomIn;
            axMapControl.CurrentTool = zoomInCommand;
        }
        private void btnZoomOut_Click(object sender, System.EventArgs e)
        {
            axMapControl.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerZoomOut;
            axMapControl.CurrentTool = zoomOutCommand;
        }
        private void btnPan_Click(object sender, System.EventArgs e)
        {
            axMapControl.MousePointer = ESRI.ArcGIS.Controls.esriControlsMousePointer.esriPointerPan;
            axMapControl.CurrentTool = panCommand;
        }
        private void btnSelectStartLink_Click(object sender, EventArgs e)
        {
            axMapControl.CurrentTool = selectStartLinkTool;
        }

        private void btnSelectStopLink_Click(object sender, EventArgs e)
        {
            axMapControl.CurrentTool = selectStopLinkTool;
        }

        public ILayer LoadLayerFile(string fullPath)
        {
            return LoadLayerFile(fullPath, 0);
        }
        /// <summary>
        /// Loads a layerfile into the supplied ESRI map control at index 0 and returns a reference to the layer
        /// </summary>
        /// <param name="fullPath">Path to the layerfile</param>
        /// <param name="mapControl">An ESRI map control object</param>
        /// <returns>An ESRI ILayer object</returns>
        public ILayer LoadLayerFile(string fullPath, int position)
        {
            if (System.IO.File.Exists(fullPath) == false)
                return null;

            try
            {
                axMapControl.AddLayerFromFile(fullPath, position);
                //this.OnStatusChanged(new StatusChangedArgs("Added layer to MapControl: '" + System.IO.Path.GetFileName(fullPath) + "'"));
                ILayer layer = axMapControl.get_Layer(0);

                return layer;
            }
            catch (Exception ex)
            {
                //this.OnStatusChanged(new StatusChangedArgs("Error loading layer: " + System.IO.Path.GetFileName(fullPath), StatusChangeType.Warning));
                throw ex;
            }
        }
        #endregion

        #region Methods to manage start link and stop link list boxes
        private void InitializeLinkDisplayBoxes()
        {
            startLinks = new Links();
            startLinks.OnAddedLink +=
                new OnAddLinkEventHandler(this.startLinks_OnAddedLink);
            startLinks.OnRemovedLink +=
                new OnRemoveLinkEventHandler(this.startLinks_OnRemovedLink);
            stopLinks = new Links();
            stopLinks.OnAddedLink +=
                new OnAddLinkEventHandler(this.stopLinks_OnAddedLink);
            stopLinks.OnRemovedLink +=
                new OnRemoveLinkEventHandler(this.stopLinks_OnRemovedLink);
            lstStartLinks.DisplayMember = "MLinkID";
            lstStopLinks.DisplayMember = "MLinkID";
        }
        private void startLinks_OnAddedLink(object sender, Links.LinkChangedEventArgs e)
        {
            Link addedLink = (Link)e.ChangedLink;
            lstStartLinks.Items.Add(addedLink);
        }
        private void startLinks_OnRemovedLink(object sender, Links.LinkChangedEventArgs e)
        {
            Link removedLink = (Link)e.ChangedLink;
            lstStartLinks.Items.Remove(removedLink);
        }
        private void stopLinks_OnAddedLink(object sender, Links.LinkChangedEventArgs e)
        {
            Link addedLink = (Link)e.ChangedLink;
            lstStopLinks.Items.Add(addedLink);
        }
        private void stopLinks_OnRemovedLink(object sender, Links.LinkChangedEventArgs e)
        {
            Link removedLink = (Link)e.ChangedLink;
            lstStopLinks.Items.Remove(removedLink);
        }
        private void btnRemoveStartLink_Click(object sender, System.EventArgs e)
        {
            while (lstStartLinks.SelectedItems.Count != 0)
            {
                Link linkToRemove = (Link)lstStartLinks.SelectedItems[0];
                startLinks.Remove(linkToRemove.LinkID);
            }
        }
        private void btnAddStartLink_Click(object sender, System.EventArgs e)
        {
            InputBoxResult result;
            result = InputBox.Show("Enter an MLinkID:", "Add StartLink", "", null, -1, -1);
            int mLinkID;
            if (result.OK)
            {
                try
                {
                    mLinkID = Int32.Parse(result.Text);
                    Link l = linkNetwork.FindByMLinkID(mLinkID);
                    if (l != null)
                    {
                        startLinks.Add(l);
                    }
                    else
                    {
                        MessageBox.Show("Could not find MLinkID in mstLinks.", "MLinkID Not Found");
                    }
                }
                catch
                { MessageBox.Show("You must enter a valid MLinkID.", "Invalid Input"); }
            }
        }
        private void btnRemoveStopLink_Click(object sender, System.EventArgs e)
        {
            //Link linkToRemove = (Link)lstStopLinks.SelectedItem;
            while (lstStopLinks.SelectedItems.Count != 0)
            {
                Link linkToRemove = (Link)lstStopLinks.SelectedItems[0];
                stopLinks.Remove(linkToRemove.LinkID);
            }
        }
        private void btnAddStopLink_Click(object sender, System.EventArgs e)
        {
            InputBoxResult result;
            result = InputBox.Show("Enter an MLinkID:", "Add StopLink", "", null, -1, -1);
            int mLinkID;
            if (result.OK)
            {
                try
                {
                    mLinkID = Int32.Parse(result.Text);
                    Link l = linkNetwork.FindByMLinkID(mLinkID);
                    if (l != null)
                    {
                        stopLinks.Add(l);
                    }
                    else
                    {
                        MessageBox.Show("Could not find MLinkID in mstLinks.", "MLinkID Not Found");
                    }
                }
                catch
                { MessageBox.Show("You must enter a valid MLinkID.", "Invalid Input"); }
            }

        }
        #endregion

        private void CallTracerExtension(object sender, SystemsAnalysis.Tracer.ClickEventArgs e)
        {
            try
            {
                SystemsAnalysis.Tracer.IGraphEdge edge;
                edge = tracerExtension.SelectLinkExternal(sender, e);
                if (sender.GetType().ToString() == "SystemsAnalysis.Tracer.SelectStartLinkTool")
                {
                    Link l = linkNetwork[edge.EdgeID];
                    startLinks.Add(l);
                }
                else if (sender.GetType().ToString() == "SystemsAnalysis.Tracer.SelectStopLinkTool")
                {
                    Link l = linkNetwork[edge.EdgeID];
                    stopLinks.Add(l);
                }
                tracedLinks = TraceNetwork();
            }
            catch (Exception ex)
            {
                //OnStatusChanged(new StatusChangedArgs("Could not select link: " + ex.Message));
                MessageBox.Show(ex.Message);
            }
        }

        private Links TraceNetwork()
        {
            Links links = new Links();
            try
            {
                links = linkNetwork.Trace(startLinks, stopLinks);
                //IFeatureLayer pFeatLayer;
                //pFeatLayer = (IFeatureLayer)axMapControl.get_Layer(1);

                int[] selectedEdgeIDArray = linkNetwork.GetSelectedEdgeIDArray();
                if (chkZoomToTrace.Checked)
                {                    
                    SystemsAnalysis.Utils.ArcObjects.MapControl.MapControlHelper.ZoomToOIDArray(
                        axMapControl, networkFL, selectedEdgeIDArray);
                }
                else
                {
                    SelectFromLayer(networkFL, selectedEdgeIDArray);
                }

                //this.OnStatusChanged(new StatusChangedArgs("Succesfully previewed Network Trace. Found " + selectedEdgeIDArray.Length + " links.", StatusChangeType.Info));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //this.OnStatusChanged(new StatusChangedArgs(ex.Message, StatusChangeType.Error));
            }
            return links;
        }

        /// <summary>
        /// Retrieves the traced Links after the Modal Dialog has closed.
        /// </summary>
        /// <returns>A collection of Links traced by Form</returns>
        public Links GetTracedLinks()
        {
            return this.tracedLinks;
        }

        private void btnExportLinks_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnRefreshTrace_Click(object sender, EventArgs e)
        {
            tracedLinks = TraceNetwork();
        }

        private void SelectFromLayer(ILayer pLayer, int[] SelectedOIDArray)
        {
            IFeatureLayer pFeatLayer;
            pFeatLayer = (IFeatureLayer)pLayer;
            
            IFeatureSelection pFeatSelection;
            pFeatSelection = (IFeatureSelection)pFeatLayer;
            pFeatSelection.Clear();
            if (SelectedOIDArray.Length <= 0)
            {
                return;
            }            
            pFeatSelection.SelectionSet.AddList(SelectedOIDArray.Length, ref SelectedOIDArray[0]);
            axMapControl.ActiveView.Refresh();

        }

    }
}
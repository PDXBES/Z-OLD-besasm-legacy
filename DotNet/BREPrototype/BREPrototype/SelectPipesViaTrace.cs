using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SystemsAnalysis.Modeling;
using ESRI.ArcGIS.Carto;

namespace BREPrototype
{
    public partial class SelectPipesViaTrace : Form
    {
        Links mstLinks;
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
        

        public SelectPipesViaTrace(Links mstLinks)
        {
            InitializeComponent();

            this.mstLinks = mstLinks;
            this.tracedLinks = new Links();
            SetupMapControl();
            InitializeLinkDisplayBoxes();
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

            try
            {
                IFeatureLayer pFL;
                pFL = (FeatureLayer)LoadLayerFile(Application.StartupPath + "\\lyr\\sewer_basins.lyr");
                pFL.Selectable = false;

                ESRI.ArcGIS.Geometry.Point point;
                point = new ESRI.ArcGIS.Geometry.PointClass();
                point.X = 7643708.753;
                point.Y = 681634.261;
                axMapControl.CenterAt(point);
                axMapControl.MapScale = 10000.0;

                LoadLayerFile(Application.StartupPath + "\\lyr\\cgis_streets.lyr");
                LoadLayerFile(Application.StartupPath + "\\lyr\\sp_bf_risk.lyr");

                pFL = (FeatureLayer)LoadLayerFile(Application.StartupPath + "\\lyr\\mst_links.lyr");
                tracerExtension.SetupTracer(mstLinks.LinkNetwork, pFL, "USNode", "DSNode", axMapControl.Map);
                pFL.Selectable = true;

                pFL = (FeatureLayer)LoadLayerFile(Application.StartupPath + "\\lyr\\mst_nodes.lyr");
                pFL.Selectable = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        /// <summary>
        /// Loads a layerfile into the supplied ESRI map control at index 0 and returns a reference to the layer
        /// </summary>
        /// <param name="fullPath">Path to the layerfile</param>
        /// <param name="mapControl">An ESRI map control object</param>
        /// <returns>An ESRI ILayer object</returns>
        public ILayer LoadLayerFile(string fullPath)
        {
            if (System.IO.File.Exists(fullPath) == false)
                return null;

            try
            {
                axMapControl.AddLayerFromFile(fullPath, 0);
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
                    Link l = mstLinks.FindByMLinkID(mLinkID);
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
                    Link l = mstLinks.FindByMLinkID(mLinkID);
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
                    Link l = mstLinks[edge.EdgeID];
                    startLinks.Add(l);
                }
                else if (sender.GetType().ToString() == "SystemsAnalysis.Tracer.SelectStopLinkTool")
                {
                    Link l = mstLinks[edge.EdgeID];
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
                links = mstLinks.Trace(startLinks, stopLinks);
                IFeatureLayer pFeatLayer;
                pFeatLayer = (IFeatureLayer)axMapControl.get_Layer(1);

                int[] selectedEdgeIDArray = mstLinks.GetSelectedEdgeIDArray();
                if (chkZoomToTrace.Checked)
                {                    
                    SystemsAnalysis.Utils.ArcObjects.MapControl.MapControlHelper.ZoomToOIDArray(
                        axMapControl, pFeatLayer, selectedEdgeIDArray);
                }
                else
                {
                    SelectFromLayer(pFeatLayer, selectedEdgeIDArray);
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

        public Links GetTrace()
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
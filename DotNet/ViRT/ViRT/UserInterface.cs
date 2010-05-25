using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.ControlCommands;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Utility;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.SystemUI;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using translator;

namespace ViRT
{
    public partial class UserInterface : Form
    {
        private IMap m_pMap;
        private IMapControl3 m_MapControl;
        private ILayer m_pCurrentLayer;
        private NEPTUNEDataSet nEPTUNEDataSet;
        private BindingSource nEPTUNEDataSetBindingSource;
        SortedList QuarterSectionList = new SortedList();

        private IAoInitialize ArcLicense;

        DateTime d1;
        DateTime d2;
        long NumberOfGages;
        long NumberOfTimeStepsBetweenStartAndEnd = 0;
        bool FileLocationChosen = false;
        gageinfo[][] QuarterSectionGageInfo;
        string QSListString;
        RainTools rainTools = new RainTools();
        private ViRT.RainDSClass rainDS;
        private System.Data.OleDb.OleDbConnection oleDbConnection2;
        private System.Data.OleDb.OleDbDataAdapter oleDbDataAdapter1;
        private System.Data.SqlClient.SqlConnection sqlConnection1;
        private System.Data.SqlClient.SqlDataAdapter sqlDataAdapter1;
        //private ViRT.NEPTUNEDataSetTableAdapters.RAIN_SENSORTableAdapter rAIN_SENSORTableAdapter;
        private ViRT.NEPTUNEDataSetTableAdapters.STATIONTableAdapter sTATIONTableAdapter;
        private ViRT.qsDS qsDS1;
        double[][] largeRainArray;
        DataTable graphData = new DataTable();

        private esriLicenseStatus CheckOutArcLicense(esriLicenseProductCode productCode)
        {
            ArcLicense = new AoInitializeClass();
            /*esriLicenseStatus licenseStatus =
                ArcLicense.IsProductCodeAvailable(productCode);
            if (licenseStatus == esriLicenseStatus.esriLicenseAvailable)*/
            esriLicenseStatus licenseStatus = ArcLicense.Initialize(productCode);

            return licenseStatus;
        }


        public UserInterface()
        {
            InitializeComponent();
            esriLicenseStatus licenseStatus =
                CheckOutArcLicense(esriLicenseProductCode.esriLicenseProductCodeArcView);
        }

        private void buttonZoomOut_Click(object sender, EventArgs e)
        {
            // Zoom out
            IActiveView pActiveView = (IActiveView)m_pMap;
            IEnvelope pEnvelope = pActiveView.Extent;
            ESRI.ArcGIS.Geometry.Point pnt = new ESRI.ArcGIS.Geometry.Point();
            IPoint iPnt = pnt;
            iPnt.X = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.XMin + (pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.Width / 2);
            iPnt.Y = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.YMin + (pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.Height / 2);
            pEnvelope.CenterAt(iPnt);
            pEnvelope.Expand(1.1, 1.1, true);
            pActiveView.Extent = pEnvelope;
            pActiveView.Refresh();
        }

        private static void AddText(System.IO.FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
        private static void AddBinaryString(System.IO.FileStream fs, string value)
        {
            BinaryWriter bw = new BinaryWriter(fs);
            foreach (char c in value.ToCharArray())
            {
                bw.Write(c);
            }
        }
        private static void AddBinaryInt32(System.IO.FileStream fs, Int32 value)
        {
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(value);
        }
        private static void AddBinaryInt16(System.IO.FileStream fs, Int16 value)
        {
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(value);
        }
        private static void AddBinarySingle(System.IO.FileStream fs, Single value)
        {
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(value);
        }
        private void UserInterface_Load(object sender, System.EventArgs e)
        {
            // TODO: This line of code loads data into the 'nEPTUNEDataSet.ENVIRON_SENSOR' table. You can move, or remove it, as needed.
            this.eNVIRON_SENSORTableAdapter.Fill(this.nEPTUNEDataSet.ENVIRON_SENSOR);
            // TODO: This line of code loads data into the 'nEPTUNEDataSet.RAIN_SENSOR' table. You can move, or remove it, as needed.
            this.rAIN_SENSORTableAdapter.Fill(this.nEPTUNEDataSet.RAIN_SENSOR);
            // TODO: This line of code loads data into the 'nEPTUNEDataSet.STATION' table. You can move, or remove it, as needed.
            this.sTATIONTableAdapter.Fill(this.nEPTUNEDataSet.STATION);
            // TODO: This line of code loads data into the 'nEPTUNEDataSet.RAIN_SENSOR' table. You can move, or remove it, as needed.
            //this.rAIN_SENSORTableAdapter.Fill(this.nEPTUNEDataSet.RAIN_SENSOR);
            // Setup the application
            m_MapControl = (IMapControl3)axMapControl.GetOcx();
            m_pMap = m_MapControl.Map;
            LoadLayers();
        }
        private void UserInterface_Unload(object sender, System.EventArgs e)
        {
            //Release COM objects 
            ESRI.ArcGIS.Utility.COMSupport.AOUninitialize.Shutdown();
        }

        private void LoadLayers()
        {
            m_pMap.ClearLayers();
            string sAssemblyPath = Assembly.GetExecutingAssembly().Location;
            string sAssemblyFolder = System.IO.Path.GetDirectoryName(sAssemblyPath);
            string sFilePath1 = "W:\\Model_Programs\\ViRT\\Data\\Quarter_Sections.lyr";
            string sFilePath2 = "W:\\Model_Programs\\ViRT\\Data\\Sewer_Basins_PDX.lyr";

            ESRI.ArcGIS.Catalog.GxLayer sscGxLayer1;
            ESRI.ArcGIS.Catalog.GxLayer sscGxLayer2;
            sscGxLayer1 = new ESRI.ArcGIS.Catalog.GxLayerClass();
            sscGxLayer2 = new ESRI.ArcGIS.Catalog.GxLayerClass();

            ESRI.ArcGIS.Catalog.GxFile sscGxFile1;
            ESRI.ArcGIS.Catalog.GxFile sscGxFile2;
            sscGxFile1 = (ESRI.ArcGIS.Catalog.GxFile)sscGxLayer1;
            sscGxFile2 = (ESRI.ArcGIS.Catalog.GxFile)sscGxLayer2;
            sscGxFile1.Path = sFilePath1;
            sscGxFile2.Path = sFilePath2;

            //Which can QI to an IFeatureLayer...
            ESRI.ArcGIS.Carto.IFeatureLayer sscFL1;
            ESRI.ArcGIS.Carto.IFeatureLayer sscFL2;
            sscFL1 = (ESRI.ArcGIS.Carto.IFeatureLayer)sscGxLayer1.Layer;
            sscFL2 = (ESRI.ArcGIS.Carto.IFeatureLayer)sscGxLayer2.Layer;

            

            m_pMap.AddLayer(sscFL1);
            m_pMap.AddLayer(sscFL2);
            m_pCurrentLayer = m_pMap.get_Layer(1);

            ESRI.ArcGIS.Carto.IFeatureLayer QSLayer;
            ESRI.ArcGIS.Carto.IFeatureLayer BasinLayer;

            QSLayer = (ESRI.ArcGIS.Carto.IFeatureLayer)m_pMap.get_Layer(1);
            BasinLayer = (ESRI.ArcGIS.Carto.IFeatureLayer)m_pMap.get_Layer(0);

            QSLayer.Selectable = false;
            BasinLayer.Selectable = false;
        }

        private void axMapControl_OnMouseUp(object sender, ESRI.ArcGIS.Controls.IMapControlEvents2_OnMouseUpEvent e)
        {

            ESRI.ArcGIS.Carto.IFeatureLayer QSLayer;
            ESRI.ArcGIS.Carto.IFeatureLayer BasinLayer;

            QSLayer = (ESRI.ArcGIS.Carto.IFeatureLayer)m_pMap.get_Layer(1);
            BasinLayer = (ESRI.ArcGIS.Carto.IFeatureLayer)m_pMap.get_Layer(0);

            if (QSLayer.Selectable == true)
                SelectMouseDown(e.x, e.y);
            else if (BasinLayer.Selectable == true)
                SelectMouseDownByBasin(e.x, e.y);
        }

        //SelectMouseDown is called when the user is selecing quartersections individually
        //with the mouse.
        private void SelectMouseDown(int x, int y)
        {
            ISelectionSet ArcQuarterSections;
            m_pCurrentLayer = m_pMap.get_Layer(1);
            // Searches the map for features at the given point in the current layer
            // and adds them to the selection (or removes them if they are already selected).
            //if we dont have a current layer, exit this function
            if (m_pCurrentLayer == null) return;
            //if there are no geometry features in the current layer, exit this function
            if ((IGeoFeatureLayer)m_pCurrentLayer == null) return;

            // Get the feature layer and class of the current layer
            IFeatureLayer pFeatureLayer = (IFeatureLayer)m_pCurrentLayer;
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            //if there are no featureclasses in the featurelayer, exit this function
            if (pFeatureClass == null) return;

            // Get the mouse down position in map coordinates
            IActiveView pActiveView = (IActiveView)m_pMap;
            IPoint pPoint = pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            IGeometry pGeometry = pPoint;

            // Use a four pixel buffer around the cursor for feature search
            double length = ConvertPixelsToMapUnits(pActiveView, 4);
            ITopologicalOperator pTopo = (ITopologicalOperator)pGeometry;
            IGeometry pBuffer = pTopo.Buffer(length);
            pGeometry = (IGeometry)pBuffer.Envelope;

            //call up a Filter specific to this layer  
            ISpatialFilter pSpatialFilter = new SpatialFilter();
            pSpatialFilter.Geometry = pGeometry;
            pSpatialFilter.GeometryField = "SHAPE";

            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

            //the geometry field will apparently be the name of the shapefield
            //query all of the objects with the shapetype
            IQueryFilter pFilter = pSpatialFilter;

            // place all of the objects of this shapetype into the cursor
            IFeatureCursor pCursor = pFeatureLayer.Search(pFilter, false);

            //load up the first object
            IFeature pFeature = pCursor.NextFeature();

            //as long as there are real objects in the cursor
            while (pFeature != null)
            {
                //if the shape/object now in pCursor is also in the quartersectionlist
                //then the qtrno must be removed from the quartersection list.
                if (QuarterSectionList.Contains(pFeature.get_Value(4).ToString()))
                {
                    QuarterSectionList.Remove(pFeature.get_Value(4).ToString());
                }
                //if the shape now in the pCursor is not in the quartersection list
                //then the qtrno must be added to the quartersection list.
                else
                {
                    QuarterSectionList.Add(pFeature.get_Value(4).ToString(), pFeature.get_Value(4).ToString());
                }

                txtQSBulkInputEntry(int.Parse((string)pFeature.get_Value(4)));
                pFeature = pCursor.NextFeature();
            }
            m_pMap.ClearSelection();
            IFeature pFeature2;
            pCursor.Flush();
            string searchString = "QTRNO = '999999' ";
            foreach (string s in QuarterSectionList.Values)
            {
                searchString = searchString + " OR QTRNO = '" + s + "'";
            }
            IQueryFilter queryFilter2 = new QueryFilterClass();
            queryFilter2.SubFields = "QTRNO";
            queryFilter2.WhereClause = searchString;

            pCursor = pFeatureLayer.Search(queryFilter2, false);
            pFeature2 = pCursor.NextFeature();
            while (pFeature2 != null)
            {
                m_pMap.SelectFeature(m_pCurrentLayer, pFeature2);

                pFeature2 = pCursor.NextFeature();
            }
            pActiveView.Refresh();
        }

        private void SelectMouseDownByBasin(int x, int y)
        {
            ISelectionSet ArcQuarterSections;
            //layer 0 should be the layer of basins
            m_pCurrentLayer = m_pMap.get_Layer(0);
            // Searches the map for features at the given point in the current layer
            // and adds them to the selection (or removes them if they are already selected).
            //if we dont have a current layer, exit this function
            if (m_pCurrentLayer == null) return;
            //if there are no geometry features in the current layer, exit this function
            if ((IGeoFeatureLayer)m_pCurrentLayer == null) return;

            // Get the feature layer and class of the current layer
            IFeatureLayer pFeatureLayer = (IFeatureLayer)m_pCurrentLayer;
            IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
            //if there are no featureclasses in the featurelayer, exit this function
            if (pFeatureClass == null) return;

            // Get the mouse down position in map coordinates
            IActiveView pActiveView = (IActiveView)m_pMap;
            IPoint pPoint = pActiveView.ScreenDisplay.DisplayTransformation.ToMapPoint(x, y);
            IGeometry pGeometry = pPoint;

            // Use a four pixel buffer around the cursor for feature search
            double length = ConvertPixelsToMapUnits(pActiveView, 4);
            ITopologicalOperator pTopo = (ITopologicalOperator)pGeometry;
            IGeometry pBuffer = pTopo.Buffer(length);
            pGeometry = (IGeometry)pBuffer.Envelope;

            //call up a Filter specific to this layer  
            ISpatialFilter pSpatialFilter = new SpatialFilter();
            pSpatialFilter.Geometry = pGeometry;
            //pSpatialFilter.GeometryField = "SHAPE";
            pSpatialFilter.GeometryField = pFeatureClass.ShapeFieldName;
            switch (pFeatureClass.ShapeType)
            {
                case esriGeometryType.esriGeometryPoint:
                    pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
                    break;
                case esriGeometryType.esriGeometryPolyline:
                    pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    break;
                case esriGeometryType.esriGeometryPolygon:
                    pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
                    break;
            }

            //pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

            //the geometry field will apparently be the name of the shapefield
            //query all of the objects with the shapetype
            IQueryFilter pFilter = pSpatialFilter;

            // place all of the objects of this shapetype into the cursor
            IFeatureCursor pCursor = pFeatureLayer.Search(pFilter, false);

            //load up the first object
            IFeature pFeature = pCursor.NextFeature();


            ///This is where the quartersections that intersect the basin should be selected.
            ///****************************************
            ///****************************************
            m_pCurrentLayer = m_pMap.get_Layer(1);
            // Searches the map for features at the given point in the current layer
            // and adds them to the selection (or removes them if they are already selected).
            //if we dont have a current layer, exit this function
            if (m_pCurrentLayer == null) return;
            //if there are no geometry features in the current layer, exit this function
            if ((IGeoFeatureLayer)m_pCurrentLayer == null) return;

            // Get the feature layer and class of the current layer
            IFeatureLayer pFeatureLayer2 = (IFeatureLayer)m_pCurrentLayer;
            IFeatureClass pFeatureClass2 = pFeatureLayer2.FeatureClass;
            //if there are no featureclasses in the featurelayer, exit this function
            if (pFeatureClass2 == null) return;

            // Get the mouse down position in map coordinates
            //IActiveView pActiveView = (IActiveView)m_pMap;
            //ESRI.ArcGIS.Geometry.IPolygon pPolygon = pFeature.Shape;
            IGeometry pGeometry2 = pFeature.Shape;//pPolygon;

            //call up a Filter specific to this layer  
            ISpatialFilter pSpatialFilter2 = new SpatialFilter();
            pSpatialFilter2.Geometry = pGeometry2;
            pSpatialFilter2.GeometryField = "SHAPE";

            pSpatialFilter2.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

            //the geometry field will apparently be the name of the shapefield
            //query all of the objects with the shapetype
            IQueryFilter pFilter2 = pSpatialFilter2;

            // place all of the objects of this shapetype into the cursor
            IFeatureCursor pCursor2 = pFeatureLayer2.Search(pFilter2, false);

            //load up the first object
            IFeature pFeature2 = pCursor2.NextFeature();
            ///*********************************************************
            ///*********************************************************


            //as long as there are real objects in the cursor
            while (pFeature2 != null)
            {
                //if the shape/object now in pCursor is also in the quartersectionlist
                //then the qtrno must be removed from the quartersection list.
                if (QuarterSectionList.Contains(pFeature2.get_Value(4).ToString()))
                {
                    QuarterSectionList.Remove(pFeature2.get_Value(4).ToString());
                }
                //if the shape now in the pCursor is not in the quartersection list
                //then the qtrno must be added to the quartersection list.
                else
                {
                    QuarterSectionList.Add(pFeature2.get_Value(4).ToString(), pFeature2.get_Value(4).ToString());
                }

                txtQSBulkInputEntry(int.Parse((string)pFeature2.get_Value(4)));
                pFeature2 = pCursor2.NextFeature();
            }
            m_pMap.ClearSelection();
            IFeature pFeature3;
            pCursor2.Flush();
            string searchString = "QTRNO = '999999' ";
            foreach (string s in QuarterSectionList.Values)
            {
                searchString = searchString + " OR QTRNO = '" + s + "'";
            }
            IQueryFilter queryFilter3 = new QueryFilterClass();
            queryFilter3.SubFields = "QTRNO";
            queryFilter3.WhereClause = searchString;

            pCursor2 = pFeatureLayer2.Search(queryFilter3, false);
            pFeature3 = pCursor2.NextFeature();
            while (pFeature3 != null)
            {
                m_pMap.SelectFeature(m_pCurrentLayer, pFeature3);

                pFeature3 = pCursor2.NextFeature();
            }
            pActiveView.Refresh();
        }

        private IFeatureCursor GetSelectedFeatures()
        {
            if (m_pCurrentLayer == null) return null;

            // If there are no features selected let the user know
            IFeatureSelection pFeatSel = (IFeatureSelection)m_pCurrentLayer;
            ISelectionSet pSelectionSet = pFeatSel.SelectionSet;
            if (pSelectionSet.Count == 0)
            {
                MessageBox.Show("No features are selected in the '" + m_pCurrentLayer.Name + "' layer", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
            // Otherwise get all of the features back from the selection
            ICursor pCursor;
            pSelectionSet.Search(null, false, out pCursor);
            return (IFeatureCursor)pCursor;
        }

        private bool TestGeometryHit(double tolerance, IPoint pPoint, IFeature pFeature, ref IPoint pHitPoint, ref double hitDist, ref int partIndex, ref int vertexIndex, ref int vertexOffset, ref bool vertexHit)
        {
            // Function returns true if a feature's shape is hit and further defines
            // if a vertex lies within the tolerance
            bool bRetVal = false;
            IGeometry pGeom = (IGeometry)pFeature.Shape;

            IHitTest pHitTest = (IHitTest)pGeom;
            pHitPoint = new ESRI.ArcGIS.Geometry.Point();
            bool bTrue = true;
            // First check if a vertex was hit
            if (pHitTest.HitTest(pPoint, tolerance, esriGeometryHitPartType.esriGeometryPartVertex, pHitPoint, ref hitDist, ref partIndex, ref vertexIndex, ref bTrue))
            {
                bRetVal = true;
                vertexHit = true;
            }
            // Secondly check if a boundary was hit
            else if (pHitTest.HitTest(pPoint, tolerance, esriGeometryHitPartType.esriGeometryPartBoundary, pHitPoint, ref hitDist, ref partIndex, ref vertexIndex, ref bTrue))
            {
                bRetVal = true;
                vertexHit = false;
            }

            // Calculate offset to vertexIndex for multipatch geometries
            if (partIndex > 0)
            {
                IGeometryCollection pGeomColn = (IGeometryCollection)pGeom;
                vertexOffset = 0;
                for (int i = 0; i < partIndex; i++)
                {
                    IPointCollection pPointColn = (IPointCollection)pGeomColn.get_Geometry(i);
                    vertexOffset = vertexOffset + pPointColn.PointCount;
                }
            }

            return bRetVal;
        }

        private double ConvertPixelsToMapUnits(IActiveView pActiveView, double pixelUnits)
        {
            // Uses the ratio of the size of the map in pixels to map units to do the conversion
            IPoint p1 = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.UpperLeft;
            IPoint p2 = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.UpperRight;
            int x1, x2, y1, y2;
            pActiveView.ScreenDisplay.DisplayTransformation.FromMapPoint(p1, out x1, out y1);
            pActiveView.ScreenDisplay.DisplayTransformation.FromMapPoint(p2, out x2, out y2);
            double pixelExtent = x2 - x1;
            double realWorldDisplayExtent = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.Width;
            double sizeOfOnePixel = realWorldDisplayExtent / pixelExtent;
            return pixelUnits * sizeOfOnePixel;
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            d1 = dtpStartDate.Value.Date.AddMinutes(dtpStartDate.Value.TimeOfDay.TotalMinutes - dtpStartDate.Value.TimeOfDay.Minutes % 5);
            d2 = dtpEndDate.Value.Date.AddMinutes(dtpEndDate.Value.TimeOfDay.TotalMinutes + (5 - dtpEndDate.Value.TimeOfDay.Minutes % 5));

            d1 = d1.AddSeconds(- d1.TimeOfDay.Seconds);
            d2 = d2.AddSeconds(- d2.TimeOfDay.Seconds);
            d1 = d1.AddMilliseconds(-d1.TimeOfDay.Milliseconds);
            d2 = d2.AddMilliseconds(-d2.TimeOfDay.Milliseconds);
            //smoother = 0;

            if (d1 > d2)
            {
                MessageBox.Show("Please make sure your start date is earlier than your end date.");
            }
            else
            {
                if (FileLocationChosen == false)
                {
                    MessageBox.Show("Please choose an output file.");
                }
                else
                {
                    txtQSBulkInputParse();
                    if (QuarterSectionList.Count == 0)
                    {
                        MessageBox.Show("Please select at least one quartersection.");
                    }
                    else
                    {
                        this.Cursor = Cursors.WaitCursor;
                        rainTools.virtmain(d1,
                                            d2,
                                            NumberOfGages,
                                            NumberOfTimeStepsBetweenStartAndEnd,
                                            QuarterSectionList,
                                            QuarterSectionGageInfo,
                                            QSListString,
                                            oleDbConnection2,
                                            oleDbDataAdapter1,
                                            sqlConnection1,
                                            sqlDataAdapter1,
                                            rainDS,
                                            qsDS1,
                                            txtOutputFileDestination.Text,
                                            largeRainArray,
                                            checkBinaryOutput.Checked,
                                            graphData);
                        this.Cursor = Cursors.Default;
                    }
                }
            }
        }

        
        private void txtQSBulkInputEntry(int QSNumber)
        {
            //get the number of characters contained in txtQSBulkInput
            string sParseString = txtQSBulkInput.Text;

            //Search the main string for a match of the QSNumber
            //If the number already exists, get rid of it.
            //If it doesnt exist, add it
            if (sParseString.Contains("," + QSNumber.ToString() + ","))
            {
                sParseString = sParseString.Replace("," + QSNumber.ToString() + ",", ",");
            }
            else if (sParseString.Contains(QSNumber.ToString() + ","))
            {
                sParseString = sParseString.Replace(QSNumber.ToString() + ",", "");
            }
            else if (sParseString.Contains("," + QSNumber.ToString()))
            {
                sParseString = sParseString.Replace("," + QSNumber.ToString(), "");
            }
            else if (sParseString.Contains(QSNumber.ToString()))
            {
                sParseString = sParseString.Replace(QSNumber.ToString(), "");
            }
            else if (sParseString.Length < 1)
            {
                sParseString = QSNumber.ToString();
            }
            else
            {
                sParseString = sParseString + "," + QSNumber.ToString();
            }

            txtQSBulkInput.Text = sParseString;

            return;
        }

        public void txtQSBulkInputParse()
        {
            //get the numbers from txtQSBulkInput and place them
            //into QuartersectionList.  The numbers should be allowed
            //to be seperated by any non-digit character, so
            //commas, newlines, spaces should all be allowed in
            //whatever combination.  Simply search for the next 
            //set of digits and assume they are in proper quartersection
            //format.
            //get the number of characters contained in txtQSBulkInput
            string sParseString = txtQSBulkInput.Text;
            string sQuartersectionString = "";
            int iNumberOfCharactersToParse = sParseString.Length;
            int iQuartersection = 0;
            bool bFirstNumberFound = false;

            QSListString = "";

            QuarterSectionList.Clear();

            for (int i = 0; i < iNumberOfCharactersToParse; i++)
            {
                //If we have found a digit, then it must be the start of a
                //Quartersection identifier
                if (Char.IsDigit(sParseString[i]))
                {
                    //parse four digits into an integer and place the integer
                    //into QuartersectionList.  Also, we must advance the
                    //variable 'i' 4 places to intercept the next number.
                    sQuartersectionString = sParseString.Substring(i, 4);
                    iQuartersection = int.Parse(sQuartersectionString);
                    QuarterSectionList.Add(sQuartersectionString, sQuartersectionString);
                    i = i + 3;
                    if (bFirstNumberFound == false)
                    {
                        QSListString = "QTRNO = '" + sQuartersectionString + "' ";
                    }
                    else
                    {
                        QSListString = QSListString + " OR QTRNO = '" + sQuartersectionString + "' ";
                    }

                    bFirstNumberFound = true;
                }
                //if the character is not a digit, then nothing needs to be done.
                //the loop parameters will advance i and check the next character
                //to see if it is a digit.

            }
            return;
        }

        private void buttonZoomIn_Click(object sender, EventArgs e)
        {
            // Zoom in
            IActiveView pActiveView = (IActiveView)m_pMap;
            IEnvelope pEnvelope = pActiveView.Extent;
            ESRI.ArcGIS.Geometry.Point pnt = new ESRI.ArcGIS.Geometry.Point();
            IPoint iPnt = pnt;
            iPnt.X = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.XMin + (pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.Width / 2);
            iPnt.Y = pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.YMin + (pActiveView.ScreenDisplay.DisplayTransformation.VisibleBounds.Height / 2);
            pEnvelope.CenterAt(iPnt);
            pEnvelope.Expand(.9, .9, true);
            pActiveView.Extent = pEnvelope;
            pActiveView.Refresh();
        }

        private void btnFileBrowser_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "Specify Destination Filename";
            saveFileDialog1.Filter = "Text Files|*.txt";
            saveFileDialog1.FilterIndex = 1;
            saveFileDialog1.OverwritePrompt = true;

            if (saveFileDialog1.ShowDialog() != DialogResult.Cancel)
            {
                txtOutputFileDestination.Text = saveFileDialog1.FileName;
                FileLocationChosen = true;
            }
        }

        /// <summary>
        /// setZeroRainPeriod will place zero values for rainfall during the time indicated
        /// if there is no more downtime associated with a raingage, no matching record 
        /// in the rainfall that has been registered, and the current time is still before
        /// the end time of the raingage
        /// </summary>
        /// <param name="DownTimeIndex">This identifies the position of the static raingage array we are looking at</param>
        /// <param name="thisRow">thisRow is an integer that identifies the current rainfall record we are looking at </param>
        /// <param name="x">x is the time inteval, which is a start date and end date, of a period of downtime</param>
        /// <param name="h2Index">h2Index is an integer which identifies which gage we are considering.  This 
        /// value identifies the index of the gage in the h2List query, and not the h2Number itself</param>
        /// <param name="currentGagesInfo">currentgagesInfo is a gageinfo object which is simply used as a 
        /// memory trick to save access times and to make the code look cleaner</param>
        bool setZeroRainPeriod(ref int DownTimeIndex, ref int thisRow, ref TimeInterval x, ref int h2Index, ref gageinfo currentGagesInfo)
        {
            bool returnValue = false;

            if (DownTimeIndex + 1 >= currentGagesInfo.DownTimeList.Count)
            {
                largeRainArray[h2Index][thisRow] = 0.0;
                thisRow++;
                DownTimeIndex = 0;
                returnValue = true; ;
            }
            else
            {
                x = (TimeInterval)currentGagesInfo.DownTimeList.GetByIndex(++DownTimeIndex);
            }

            return returnValue;
        }

        void basicDownTimeSearch(ref DateTime dateTimeIterator, ref gageinfo currentGagesInfo, ref int h2Index, ref int thisRow)
        {
            int DownTimeIndex = 0;
            TimeInterval x;

            x = (TimeInterval)currentGagesInfo.DownTimeList.GetByIndex(DownTimeIndex);
            //if the date is not between the start date and end dates of the gage, then set the reading to 200
            if (dateTimeIterator < currentGagesInfo.startDate || dateTimeIterator > currentGagesInfo.endDate)
            {
                largeRainArray[h2Index][thisRow] = 200.0;
                thisRow++;
            }
            else
            {
                //if the gage cannot be down (no reading, but the downtime index is 
                //greater than the number of downtimes) then the reading must be zero.
                if (DownTimeIndex >= currentGagesInfo.DownTimeList.Count)
                {
                    largeRainArray[h2Index][thisRow] = 0.0;
                    thisRow++;
                }

                //if the gage might be down, check the downtime list for that gage
                //and if the date falls during a downtime, then set the reading to 200.
                //Otherwise, set the reading to zero.
                intenseDownTimeSearch(ref DownTimeIndex, ref currentGagesInfo, ref dateTimeIterator, ref x, ref h2Index, ref thisRow);
            }
        }

        /// <summary>
        /// intenseDownTimeSearch should probably be renamed.  This function does a full search of the
        /// downtimes for the gage in question and applies zeros (for no rain) or 200's (an identifier for downtime)
        /// to the largeRainArray.
        /// </summary>
        /// <param name="DownTimeIndex">This identifies the position of the static raingage array we are looking at</param>
        /// <param name="currentGagesInfo">currentgagesInfo is a gageinfo object which is simply used as a 
        /// memory trick to save access times and to make the code look cleaner</param>
        /// <param name="dateTimeIterator">dateTimeIterator is a dateTime value that represents the current time step.</param>
        /// <param name="x">x is the time inteval, which is a start date and end date, of a period of downtime</param>
        /// <param name="h2Index">h2Index is an integer which identifies which gage we are considering.  This 
        /// value identifies the index of the gage in the h2List query, and not the h2Number itself</param>
        /// <param name="thisRow">thisRow is an integer that identifies the current rainfall record we are looking at</param>
        void intenseDownTimeSearch(ref int DownTimeIndex, ref gageinfo currentGagesInfo, ref DateTime dateTimeIterator, ref TimeInterval x, ref int h2Index, ref int thisRow)
        {
            while (DownTimeIndex < currentGagesInfo.DownTimeList.Count)
            {
                //if the current time step is during a downtime, enter 200 into the array position
                if (dateTimeIterator >= x.IntervalStart)
                {
                    if (dateTimeIterator <= x.IntervalEnd)
                    {
                        largeRainArray[h2Index][thisRow] = 200.0;
                        thisRow++;
                        break;
                    }
                    // or keep looking if the dateTimeIterator is greater than the downtime
                    else
                    {
                        //if there aren't any other downtimes, then this is a zero rain period
                        if (setZeroRainPeriod(ref DownTimeIndex, ref thisRow, ref x, ref h2Index, ref currentGagesInfo) == true)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    largeRainArray[h2Index][thisRow] = 0.0;
                    DownTimeIndex = 0;
                    thisRow++;
                    break;
                }
            }
        }

        private void btnRealRainProcess_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DateTime d1 = dtpStartDay.Value.Date.AddMinutes(dtpStartDay.Value.TimeOfDay.TotalMinutes - dtpStartDay.Value.TimeOfDay.Minutes % 5);
                DateTime d2 = dtpEndDay.Value.Date.AddMinutes(dtpEndDay.Value.TimeOfDay.TotalMinutes + (5 - dtpEndDay.Value.TimeOfDay.Minutes % 5));

                if (d1 > d2)
                {
                    MessageBox.Show("Please make sure your start date is earlier than your end date.");
                }
                else
                {
                    int test = 0;
                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
                        //see if the user has selected a row for the sensor
                        test = (int)dataGridViewRainGages.SelectedRows[0].Cells[0].Value;
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("Please select a valid station.  Click on the grey box to the left of the station_id column and ensure that the entire row is highlighted.");
                        test = -1;
                    }
                    if (test != -1)
                    {
                        try
                        {
                            //get rid of the 'seconds' portion of the datetime
                            ExcelHelper.CreateRainReport(saveFileDialog1.FileName, d1.AddSeconds(-d1.Second), d2.AddSeconds(-d2.Second), cbReportZeroRain.Checked, (int)nudTimeStep.Value, dataGridViewRainGages, dataGridView1);
                            MessageBox.Show("Process complete!");
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show("An error occurred while attempting to load the file. The error is:"
                                            + System.Environment.NewLine + exp.ToString() + System.Environment.NewLine);
                        }
                    }
                    this.Cursor = Cursors.Arrow;
                }
            }
        }

        private void CreateRainReport(string path1, DateTime sdate, DateTime edate)
        {
            //Data set and table adapter for the variable minute rain gauges
            NEPTUNEDataSetTableAdapters.DataTable1TableAdapter theRainTableAdapter =
                new ViRT.NEPTUNEDataSetTableAdapters.DataTable1TableAdapter();
            NEPTUNEDataSet.DataTable1DataTable theRainTable =
                new NEPTUNEDataSet.DataTable1DataTable();
            int timeStep = (int)nudTimeStep.Value;

            sdate = sdate.AddMinutes(-(sdate.Minute % timeStep));

            NEPTUNEDataSetTableAdapters.V_MODEL_RAIN_DOWNTIMETableAdapter theDownTimeTableAdapter =
                new ViRT.NEPTUNEDataSetTableAdapters.V_MODEL_RAIN_DOWNTIMETableAdapter();
            NEPTUNEDataSet.V_MODEL_RAIN_DOWNTIMEDataTable theDownTimeTable =
                new NEPTUNEDataSet.V_MODEL_RAIN_DOWNTIMEDataTable();

            theRainTableAdapter.Fill(theRainTable, edate, timeStep, (int)dataGridViewRainGages.SelectedRows[0].Cells[0].Value, sdate);
            theDownTimeTableAdapter.FillByStationID(theDownTimeTable, (int)dataGridViewRainGages.SelectedRows[0].Cells[0].Value, sdate, edate);

            DateTime DateStep;
            int RainRow = 0;
            int DownRow = 0;
            int maxRows = theRainTable.Rows.Count;
            int DownRows = theDownTimeTable.Rows.Count;
            bool DataFound = false;
            System.Data.DataRow theRainRow;
            System.Data.DataRow theDownRow;

            if (File.Exists(path1))
            {
                File.Delete(path1);
            }

            System.IO.FileStream fs = File.Create(path1);

            //for each time step
            DateStep = sdate;

            System.TimeSpan diff1 = edate.Subtract(sdate);
            long NumberOfTimeStepsBetweenStartAndEnd = (((long)diff1.TotalMinutes) / timeStep) + 1;

            if (maxRows != 0)
            {
                theRainRow = theRainTable.Rows[0];
                for (int j = 0; j < NumberOfTimeStepsBetweenStartAndEnd; j++)
                {
                    DataFound = false;
                    //if the datetime in the rainDS is the same as the datetime in DateStep
                    //then get the rainfall sum and increment DateStep
                    if (RainRow < maxRows)
                    {
                        theRainRow = theRainTable.Rows[RainRow];
                    }
                    //if the current DateStep value is during one of the station's
                    //downtimes, then print "DOWN" as the value of the rainfall
                    //if 'ThereIsDownTime' == true, then print "DOWN"
                    //This means I will need to select the downtime for this
                    //raingage and place it in a new dataset?

                    if (String.Compare((string.Format("{0:MM/dd/yyyy HH:mm},", theRainRow["date_time"])), (string.Format("{0:MM/dd/yyyy HH:mm},", DateStep))) == 0)
                    {

                        AddText(fs, (string.Format("{0:MM/dd/yyyy HH:mm},", DateStep))
                            + theRainRow["sum_inches"].ToString() + ",\r\n");

                        RainRow++;
                        DataFound = true;
                    }
                    else if (DownRows > 0)
                    {
                        for (DownRow = 0; DownRow < DownRows; DownRow++)
                        {
                            if (DateTime.Compare(DateStep, (DateTime)theDownTimeTable.Rows[DownRow]["start_date"]) >= 0 &&
                                DateTime.Compare(DateStep, (DateTime)theDownTimeTable.Rows[DownRow]["end_date"]) <= 0 &&
                                DataFound == false)
                            {
                                AddText(fs, (string.Format("{0:MM/dd/yyyy HH:mm},", DateStep))
                                    + "DOWN,\r\n");
                                DataFound = true;
                            }
                        }
                    }
                    if (DataFound == false)
                    {
                        if (cbReportZeroRain.Checked)
                        {
                            AddText(fs, (string.Format("{0:MM/dd/yyyy HH:mm},", DateStep))
                                + "0,\r\n");
                        }
                    }
                    DateStep = DateStep.AddMinutes(timeStep);
                }
                fs.Close();

                //CreateExcelFile(sdate, edate, maxRows, path1);
            }
            else
            {
                MessageBox.Show("There is no rainfall recorded by that monitor during the specified interval");
            }
        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            //frmMapEditing.textBox2.Text = frmMapEditing.saveFileDialog1.FileName;
            textBox1.Text = saveFileDialog1.FileName;
        }

        private void btnExportToExcel_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DateTime d1 = dtpStartDay.Value.Date.AddMinutes(dtpStartDay.Value.TimeOfDay.TotalMinutes - dtpStartDay.Value.TimeOfDay.Minutes % 5);
                DateTime d2 = dtpEndDay.Value.Date.AddMinutes(dtpEndDay.Value.TimeOfDay.TotalMinutes + (5 - dtpEndDay.Value.TimeOfDay.Minutes % 5));

                if (d1 > d2)
                {
                    MessageBox.Show("Please make sure your start date is earlier than your end date.");
                }
                else
                {
                    int testGages = 0;
                    int testSensors = 0;
                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
                        //see if the user has selected a row for the sensor
                        testGages = (int)dataGridViewRainGages.SelectedRows[0].Cells[0].Value;
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("Please select a valid station.  Click on the grey box to the left of the station_id column and ensure that the entire row is highlighted.");
                        testGages = -1;
                    }
                    try
                    {
                        //see if the user has selected a row for the sensor
                        testSensors = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                    }
                    catch (Exception exp2)
                    {
                        MessageBox.Show("Please select a valid sensor.  Click on the grey box to the left of the station_id column and ensure that the entire row is highlighted.");
                        testSensors = -1;
                    }
                    if (testGages != -1 && testSensors != -1)
                    {
                        try
                        {
                            //get rid of the 'seconds' portion of the datetime
                            ViRT.ExcelHelper.CreateRainAndDepthReport(saveFileDialog1.FileName, d1.AddSeconds(-d1.Second), d2.AddSeconds(-d2.Second), cbReportZeroRain.Checked, (int)nudTimeStep.Value, dataGridViewRainGages, dataGridView1);
                            MessageBox.Show("Process complete!");
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show("An error occurred while attempting to load the file. The error is:"
                                            + System.Environment.NewLine + exp.ToString() + System.Environment.NewLine);
                        }
                    }
                    this.Cursor = Cursors.Arrow;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DateTime d1 = dtpStartDay.Value.Date.AddMinutes(dtpStartDay.Value.TimeOfDay.TotalMinutes - dtpStartDay.Value.TimeOfDay.Minutes % 5);
                DateTime d2 = dtpEndDay.Value.Date.AddMinutes(dtpEndDay.Value.TimeOfDay.TotalMinutes + (5 - dtpEndDay.Value.TimeOfDay.Minutes % 5));

                if (d1 > d2)
                {
                    MessageBox.Show("Please make sure your start date is earlier than your end date.");
                }
                else
                {
                    int test = 0;
                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
                        //see if the user has selected a row for the sensor
                        test = (int)dataGridView1.SelectedRows[0].Cells[0].Value;
                    }
                    catch (Exception exp)
                    {
                        MessageBox.Show("Please select a valid station.  Click on the grey box to the left of the station_id column and ensure that the entire row is highlighted.");
                        test = -1;
                    }
                    if (test != -1)
                    {
                        try
                        {
                            //get rid of the 'seconds' portion of the datetime
                            ExcelHelper.CreateDepthReport(saveFileDialog1.FileName, d1.AddSeconds(-d1.Second), d2.AddSeconds(-d2.Second), cbReportZeroRain.Checked, (int)nudTimeStep.Value, dataGridViewRainGages, dataGridView1);
                            MessageBox.Show("Process complete!");
                        }
                        catch (Exception exp)
                        {
                            MessageBox.Show("An error occurred while attempting to load the file. The error is:"
                                            + System.Environment.NewLine + exp.ToString() + System.Environment.NewLine);
                        }
                    }
                    this.Cursor = Cursors.Arrow;
                }
            }
        }

        private void buttonChangeLayer_Click(object sender, EventArgs e)
        {
            //this function may be larger than it needs to be.
            //consider shortening it and meshing it with the AddLayers()
            //function.  It is possible this could call the addlayers
            //function with the proper rewrite.
            m_pMap.ClearLayers();
            string sAssemblyPath = Assembly.GetExecutingAssembly().Location;
            string sAssemblyFolder = System.IO.Path.GetDirectoryName(sAssemblyPath);
            string sFilePath1 = "W:\\Model_Programs\\ViRT\\Data\\Quarter_Sections.lyr";
            string sFilePath2 = "W:\\Model_Programs\\ViRT\\Data\\Sewer_Basins_PDX.lyr";
            if (openFileDialogBasinLayer.ShowDialog()  == DialogResult.OK)
            {
                sFilePath2 = openFileDialogBasinLayer.FileName;
            }
            


            ESRI.ArcGIS.Catalog.GxLayer sscGxLayer1;
            ESRI.ArcGIS.Catalog.GxLayer sscGxLayer2;
            sscGxLayer1 = new ESRI.ArcGIS.Catalog.GxLayerClass();
            sscGxLayer2 = new ESRI.ArcGIS.Catalog.GxLayerClass();

            ESRI.ArcGIS.Catalog.GxFile sscGxFile1;
            ESRI.ArcGIS.Catalog.GxFile sscGxFile2;
            sscGxFile1 = (ESRI.ArcGIS.Catalog.GxFile)sscGxLayer1;
            sscGxFile2 = (ESRI.ArcGIS.Catalog.GxFile)sscGxLayer2;
            sscGxFile1.Path = sFilePath1;
            sscGxFile2.Path = sFilePath2;

            //Which can QI to an IFeatureLayer...
            ESRI.ArcGIS.Carto.IFeatureLayer sscFL1;
            ESRI.ArcGIS.Carto.IFeatureLayer sscFL2;
            sscFL1 = (ESRI.ArcGIS.Carto.IFeatureLayer)sscGxLayer1.Layer;
            sscFL2 = (ESRI.ArcGIS.Carto.IFeatureLayer)sscGxLayer2.Layer;

            m_pMap.AddLayer(sscFL1);
            try
            {
                m_pMap.AddLayer(sscFL2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Your selected layer file is unreadable.");
                sFilePath2 = "W:\\Model_Programs\\ViRT\\Data\\Sewer_Basins_PDX.lyr";
                sscGxFile2.Path = sFilePath2;
                sscFL2 = (ESRI.ArcGIS.Carto.IFeatureLayer)sscGxLayer2.Layer;
                m_pMap.AddLayer(sscFL2);
            }
            m_pCurrentLayer = m_pMap.get_Layer(1);
        }

        private void buttonImportMapInfoLinks_Click(object sender, EventArgs e)
        {
            string MapInfoConvertFile = "";
            if (openFileDialogMapInfoFile.ShowDialog() == DialogResult.OK)
            {
                MapInfoConvertFile = openFileDialogMapInfoFile.FileName;

                //Create text file in the c:temp directory 
                //identifying the location of the MapInfoConvertFile
                if (File.Exists("c:\\temp\\MILFile.txt"))
                {
                    File.Delete("c:\\temp\\MILFile.txt");
                }
                if (File.Exists("c:\\temp\\MILError.txt"))
                {
                    File.Delete("c:\\temp\\MILError.txt");
                }
                if (File.Exists("c:\\temp\\linksFile.tab"))
                {
                    File.Delete("c:\\temp\\linksFile.tab");
                }

                System.IO.FileStream fs = File.Create("c:\\temp\\MILFile.txt");
                AddText(fs, openFileDialogMapInfoFile.FileName);

                fs.Close();

                Process myProc;
                //Start up the mbx that works on the mapinfo file
                
                //System.Threading.Thread.Sleep(20000);

                //Check the output directory until the fused links file has been created 
                //or the error file has been created
                FileSystemWatcher watcher = new FileSystemWatcher(@"c:\\temp\\", "*.*");
                watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
                                        | NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Size;
                watcher.Changed += new FileSystemEventHandler(OnChanged);
                watcher.Created += new FileSystemEventHandler(OnChanged);
                watcher.Deleted += new FileSystemEventHandler(OnChanged);
                
                watcher.EnableRaisingEvents = true;

                myProc = Process.Start("W:\\Model_Programs\\ViRT\\MBX\\CombinePipes.mbx");
                //if the error file is created, then do not translate it, and exit this 
                //method.

                //if the fused file has been created, translate it to ArcGIS .lyr format
                while (CombineSuccessful != "yes" && CombineSuccessful != "no")
                {

                }

                myProc.Close();
                string processName = "MAPINFOW";
                Process[] processes = Process.GetProcessesByName(processName);

                foreach (Process process in processes)
                {
                    process.Kill();
                }

                

                translator.GISTranslator gisTranslator = new GISTranslator();

                //create a pgdb 
                gisTranslator.TranslateOneFileToLyr("c:\\temp\\linksFile.tab", "c:\\temp\\", /*"c:\\temp\\*/"linksFile.mdb", true);
                //create a layer file from the pgdb

                //this function may be larger than it needs to be.
                //consider shortening it and meshing it with the AddLayers()
                //function.  It is possible this could call the addlayers
                //function with the proper rewrite.
                m_pMap.ClearLayers();
                string sAssemblyPath = Assembly.GetExecutingAssembly().Location;
                string sAssemblyFolder = System.IO.Path.GetDirectoryName(sAssemblyPath);
                string sFilePath1 = "W:\\Model_Programs\\ViRT\\Data\\Quarter_Sections.lyr";
                string sFilePath2 = "W:\\Model_Programs\\ViRT\\Data\\Sewer_Basins_PDX.lyr";
                if (File.Exists("c:\\temp\\pipes.lyr"))
                {
                    sFilePath2 = "c:\\temp\\pipes.lyr";
                }



                ESRI.ArcGIS.Catalog.GxLayer sscGxLayer1;
                ESRI.ArcGIS.Catalog.GxLayer sscGxLayer2;
                sscGxLayer1 = new ESRI.ArcGIS.Catalog.GxLayerClass();
                sscGxLayer2 = new ESRI.ArcGIS.Catalog.GxLayerClass();

                ESRI.ArcGIS.Catalog.GxFile sscGxFile1;
                ESRI.ArcGIS.Catalog.GxFile sscGxFile2;
                sscGxFile1 = (ESRI.ArcGIS.Catalog.GxFile)sscGxLayer1;
                sscGxFile2 = (ESRI.ArcGIS.Catalog.GxFile)sscGxLayer2;
                sscGxFile1.Path = sFilePath1;
                sscGxFile2.Path = sFilePath2;

                //Which can QI to an IFeatureLayer...
                ESRI.ArcGIS.Carto.IFeatureLayer sscFL1;
                ESRI.ArcGIS.Carto.IFeatureLayer sscFL2;
                sscFL1 = (ESRI.ArcGIS.Carto.IFeatureLayer)sscGxLayer1.Layer;
                sscFL2 = (ESRI.ArcGIS.Carto.IFeatureLayer)sscGxLayer2.Layer;

                m_pMap.AddLayer(sscFL1);
                try
                {
                    m_pMap.AddLayer(sscFL2);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Your selected layer file is unreadable.");
                    sFilePath2 = "W:\\Model_Programs\\ViRT\\Data\\Sewer_Basins_PDX.lyr";
                    sscGxFile2.Path = sFilePath2;
                    sscFL2 = (ESRI.ArcGIS.Carto.IFeatureLayer)sscGxLayer2.Layer;
                    m_pMap.AddLayer(sscFL2);
                }
                m_pCurrentLayer = m_pMap.get_Layer(1);
                
                MessageBox.Show("DONE!");
                
            }
        }

        // Define the event handlers.
        private void OnChanged(object source, FileSystemEventArgs e)
        {
            // Specify what is done when a file is changed, created, or deleted.
            if (e.FullPath == "c:\\\\temp\\\\MILError.txt")
            {
                CombineSuccessful = "no";
            }
            if(e.FullPath == "C:\\\\temp\\\\linksFile.TAB")
            {
                CombineSuccessful = "yes";
            }
        }

        private void buttonPan_Click(object sender, EventArgs e)
        {
            ESRI.ArcGIS.Carto.IFeatureLayer QSLayer;
            ESRI.ArcGIS.Carto.IFeatureLayer BasinLayer;

            QSLayer = (ESRI.ArcGIS.Carto.IFeatureLayer)m_pMap.get_Layer(0);
            BasinLayer = (ESRI.ArcGIS.Carto.IFeatureLayer)m_pMap.get_Layer(1);

            QSLayer.Selectable = false;
            BasinLayer.Selectable = false;

            ICommand command = new ESRI.ArcGIS.Controls.ControlsMapPanToolClass();
            command.OnCreate(axMapControl.Object);
            if (command.Enabled == true)
            {
                axMapControl.CurrentTool = (ITool)command;
            }
        }

        private void buttonSelectByQuartersection_Click(object sender, EventArgs e)
        {
            ESRI.ArcGIS.Carto.IFeatureLayer QSLayer;
            ESRI.ArcGIS.Carto.IFeatureLayer BasinLayer;

            QSLayer = (ESRI.ArcGIS.Carto.IFeatureLayer)m_pMap.get_Layer(1);
            BasinLayer = (ESRI.ArcGIS.Carto.IFeatureLayer)m_pMap.get_Layer(0);

            QSLayer.Selectable = true;
            BasinLayer.Selectable = false;

            ICommand command2 = new ESRI.ArcGIS.Controls.ControlsSelectFeaturesToolClass();
            command2.OnCreate(axMapControl.Object);
            if (command2.Enabled == true)
            {
                axMapControl.CurrentTool = (ITool)command2;
                
            }

            
        }

        private void buttonSelectByBasin_Click(object sender, EventArgs e)
        {
            ESRI.ArcGIS.Carto.IFeatureLayer QSLayer;
            ESRI.ArcGIS.Carto.IFeatureLayer BasinLayer;

            QSLayer = (ESRI.ArcGIS.Carto.IFeatureLayer)m_pMap.get_Layer(1);
            BasinLayer = (ESRI.ArcGIS.Carto.IFeatureLayer)m_pMap.get_Layer(0);

            QSLayer.Selectable = false;
            BasinLayer.Selectable = true;

            ICommand command2 = new ESRI.ArcGIS.Controls.ControlsSelectFeaturesToolClass();
            command2.OnCreate(axMapControl.Object);
            if (command2.Enabled == true)
            {
                axMapControl.CurrentTool = (ITool)command2;

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }


    }
}

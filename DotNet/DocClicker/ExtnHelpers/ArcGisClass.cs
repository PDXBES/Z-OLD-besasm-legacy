using System;
using System.Data ;
using System.Data.SqlClient ;
using System.Collections.Generic;
using System.Text;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Carto;
using System.Runtime.InteropServices;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;

namespace ExtnHelpers
{
    class ArcGisClass
    {


        #region "Display HourGlass Mouse"
        public IMouseCursor g_pMouseCursor;
        public void BusyMouse(bool bBusy)
        {
            if (g_pMouseCursor == null)
            {
                g_pMouseCursor = new MouseCursorClass();
            }
            if (bBusy)
            {
                g_pMouseCursor.SetCursor(2);
            }
            else
            {
                g_pMouseCursor.SetCursor(0);
            }
        }


        #endregion

        #region "Get All Features from Point Search in GeoFeatureLayer"
        // ArcGIS Snippet Title: 
        // Get All Features from Point Search in GeoFeatureLayer
        //
        // Add the following references to the project:
        // ESRI.ArcGIS.Carto
        // ESRI.ArcGIS.Geometry
        // ESRI.ArcGIS.Geodatabase
        // ESRI.ArcGIS.System
        //
        // Intended ArcGIS Products for this snippet:
        // ArcGIS Desktop
        // ArcGIS Engine
        // ArcGIS Server
        //
        // Required ArcGIS Extensions:
        // (NONE)
        //
        // Notes:
        // This snippet is intended to be inserted at the base level of a Class.
        // It is not intended to be nested within an existing Method.
        //
        // Use the following XML documentation comments to use this snippet:
        /// <summary>Finds all the features in a GeoFeature layer by supplying a point. The point could come from a mouse click on the map.</summary>
        ///
        /// <param name="searchTolerance">A System.Double that is the number of map units to search. Example: 25</param>
        /// <param name="point">An IPoint interface in map units where the user clicked on the map</param>
        /// <param name="geoFeatureLayer">An ILayer interface to search upon</param>
        /// <param name="activeView">An IActiveView interface</param>
        /// 
        /// <returns>An IFeatureCursor interface is returned containing all the selected features found in the GeoFeatureLayer.</returns>
        /// 
        /// <remarks></remarks>
        public ESRI.ArcGIS.Geodatabase.IFeatureCursor GetIFeatureCursorFromIGeoFeatureLayer(System.Double searchTolerance, ESRI.ArcGIS.Geometry.IPoint point, ESRI.ArcGIS.Carto.IGeoFeatureLayer geoFeatureLayer, ESRI.ArcGIS.Carto.IActiveView activeView)
        {

            if (searchTolerance < 0 || point == null || geoFeatureLayer == null || activeView == null)
            {
                return null;
            }
            ESRI.ArcGIS.Carto.IMap map = activeView.FocusMap;

            // Expand the points envelope to give better search results    
            ESRI.ArcGIS.Geometry.IEnvelope envelope = point.Envelope;
            envelope.Expand(searchTolerance, searchTolerance, false);

            ESRI.ArcGIS.Geodatabase.IFeatureClass featureClass = geoFeatureLayer.FeatureClass;
            string shapeFieldName = featureClass.ShapeFieldName;

            // Create a new spatial filter and use the new envelope as the geometry    
            ESRI.ArcGIS.Geodatabase.ISpatialFilter spatialFilter = new ESRI.ArcGIS.Geodatabase.SpatialFilterClass();
            spatialFilter.Geometry = envelope;
            spatialFilter.SpatialRel = ESRI.ArcGIS.Geodatabase.esriSpatialRelEnum.esriSpatialRelEnvelopeIntersects;
            spatialFilter.set_OutputSpatialReference(shapeFieldName, map.SpatialReference);
            spatialFilter.GeometryField = shapeFieldName;

            // Do the search
            ESRI.ArcGIS.Geodatabase.IFeatureCursor featureCursor = featureClass.Search(spatialFilter, false);

            return featureCursor;
        }
        #endregion
        
        #region "Get FeatureLayer from Layer Index Number"
        // ArcGIS Snippet Title: 
        // Get FeatureLayer from Layer Index Number
        //
        // Add the following references to the project:
        // ESRI.ArcGIS.Carto
        // 
        // Intended ArcGIS Products for this snippet:
        // ArcGIS Desktop
        // ArcGIS Engine
        // ArcGIS Server
        //
        // Required ArcGIS Extensions:
        // (NONE)
        //
        // Notes:
        // This snippet is intended to be inserted at the base level of a Class.
        // It is not intended to be nested within an existing Method.
        //
        // Use the following XML documentation comments to use this snippet:
        /// <summary>Get the FeatureLayer by specifying the Layer Index Number.</summary>
        ///
        /// <param name="activeView">An IActiveView interface</param>
        /// <param name="layerIndex">An System.Int32 that is the layer index number in the map (zero based). Example: 3</param>
        /// 
        /// <returns>An IFeatureLayer interface (if found) or null (if not found)</returns>
        /// 
        /// <remarks></remarks>
        public ESRI.ArcGIS.Carto.IFeatureLayer GetIFeatureLayerFromLayerIndexNumber(ESRI.ArcGIS.Carto.IActiveView activeView, System.Int32 layerIndex)
        {

            if (activeView == null || layerIndex < 0)
            {
                return null;
            }
            ESRI.ArcGIS.Carto.IMap map = activeView.FocusMap;
            if (layerIndex < map.LayerCount && map.get_Layer(layerIndex) is ESRI.ArcGIS.Carto.IFeatureLayer)
            {
                return (ESRI.ArcGIS.Carto.IFeatureLayer)activeView.FocusMap.get_Layer(layerIndex); // Explicit Cast
            }
            else
            {
                return null;
            }
        }
        #endregion
        
        #region "Perform Attribute Query"
        // ArcGIS Snippet Title: 
        // Perform Attribute Query
        // 
        // Add the following references to the project:
        // ESRI.ArcGIS.Geodatabase
        // ESRI ArcGIS.System
        //
        // Intended ArcGIS Products for this snippet:
        // ArcGIS Desktop
        // ArcGIS Engine
        // ArcGIS Server
        //
        // Required ArcGIS Extensions:
        // (NONE)
        //
        // Notes:
        // This snippet is intended to be inserted at the base level of a Class.
        // It is not intended to be nested within an existing Method.
        //
        // Use the following XML documentation comments to use this snippet:
        /// <summary>Creates an attribute query based on a supplied table and where clause.</summary>
        ///
        /// <param name="table">An ESRI.ArcGIS.Geodatabase.ITable, this could be a table or feature class</param>
        /// <param name="whereClause">A System.String, (e.g., "city_name = 'Redlands'").</param>
        /// 
        /// <returns>An ICursor holding the results of the query will be returned.</returns>
        /// 
        /// <remarks>The SQL syntax used to specify the where clause is the same as that of the 
        /// underlying database holding the data. If you would like to return everything in the 
        /// table supply "" for the where clause.</remarks>
        public ESRI.ArcGIS.Geodatabase.ICursor AttributeQuery(ESRI.ArcGIS.Geodatabase.ITable table, System.String whereClause)
        {
            ESRI.ArcGIS.Geodatabase.IQueryFilter queryFilter = new ESRI.ArcGIS.Geodatabase.QueryFilterClass();
            queryFilter.WhereClause = whereClause; // create the where clause statement

            // query the table passed into the function and use a cursor to hold the results
            ESRI.ArcGIS.Geodatabase.ICursor cursor = table.Search(queryFilter, false);

            return cursor;
        }
        #endregion
            
        #region "Clear Selected Map Features"
        // ArcGIS Snippet Title: 
        // Clear Selected Map Features
        //
        // Add the following references to the project:
        // ESRI.ArcGIS.Carto
        // ESRI.ArcGIS.Geometry
        // 
        // Intended ArcGIS Products for this snippet:
        // ArcGIS Desktop
        // ArcGIS Engine
        // ArcGIS Server
        //
        // Required ArcGIS Extensions:
        // (NONE)
        //
        // Notes:
        // This snippet is intended to be inserted at the base level of a Class.
        // It is not intended to be nested within an existing Method.
        //
        // Use the following XML documentation comments to use this snippet:
        /// <summary>Clear the selected features in the IActiveView for a specified IFeatureLayer.</summary>
        ///
        /// <param name="activeView">An IActiveView interface</param>
        /// <param name="featureLayer">An IFeatureLayer</param>
        ///
        /// <remarks></remarks>
        public void ClearSelectedMapFeatures(ESRI.ArcGIS.Carto.IActiveView activeView, ESRI.ArcGIS.Carto.IFeatureLayer featureLayer)
        {
            if (activeView == null || featureLayer == null)
            {
                return;
            }
            ESRI.ArcGIS.Carto.IFeatureSelection featureSelection = featureLayer as ESRI.ArcGIS.Carto.IFeatureSelection; // Dynamic Cast

            // Invalidate only the selection cache. Flag the original selection
            activeView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGeoSelection, null, null);

            // Clear the selection
            featureSelection.Clear();

            // Flag the new selection
            activeView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGeoSelection, null, null);
        }
        #endregion
                

        #region "Select Map Features by Attribute Query"
        // ArcGIS Snippet Title: 
        // Select Map Features by Attribute Query
        //
        // Add the following references to the project:
        // ESRI.ArcGIS.Carto
        // ESRI.ArcGIS.Geodatabase
        // ESRI.ArcGIS.Geometry
        // ESRI.ArcGIS.System
        // 
        // Intended ArcGIS Products for this snippet:
        // ArcGIS Desktop
        // ArcGIS Engine
        // ArcGIS Server
        //
        // Required ArcGIS Extensions:
        // (NONE)
        //
        // Notes:
        // This snippet is intended to be inserted at the base level of a Class.
        // It is not intended to be nested within an existing Method.
        //
        // Use the following XML documentation comments to use this snippet:
        /// <summary>Select features in the IActiveView by an attribute query using a SQL syntax in a where clause.</summary>
        ///
        /// <param name="activeView">An IActiveView interface</param>
        /// <param name="featureLayer">An IFeatureLayer interface to select upon</param>
        /// <param name="whereClause">A System.String that is the SQL where clause syntax to select features. Example: "CityName = 'Redlands'"</param>
        /// 
        /// <remarks>Providing and empty string "" will return all records.</remarks>
        public void SelectMapFeaturesByAttributeQuery(ESRI.ArcGIS.Carto.IActiveView activeView, ESRI.ArcGIS.Carto.IFeatureLayer featureLayer, System.String whereClause)
        {
            if (activeView == null || featureLayer == null || whereClause == null)
            {
                return;
            }
            ESRI.ArcGIS.Carto.IFeatureSelection featureSelection = featureLayer as ESRI.ArcGIS.Carto.IFeatureSelection; // Dynamic Cast

            // Set up the query
            ESRI.ArcGIS.Geodatabase.IQueryFilter queryFilter = new ESRI.ArcGIS.Geodatabase.QueryFilterClass();
            queryFilter.WhereClause = whereClause;

            // Invalidate only the selection cache. Flag the original selection
            activeView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGeoSelection, null, null);

            // Perform the selection
            featureSelection.SelectFeatures(queryFilter, ESRI.ArcGIS.Carto.esriSelectionResultEnum.esriSelectionResultNew, false);

            // Flag the new selection
            activeView.PartialRefresh(ESRI.ArcGIS.Carto.esriViewDrawPhase.esriViewGeoSelection, null, null);
        }
        #endregion


        #region "Get Index Number from Layer Name"
        // ArcGIS Snippet Title: 
        // Get Index Number from Layer Name
        //
        // Add the following references to the project:
        // ESRI.ArcGIS.Carto
        // 
        // Intended ArcGIS Products for this snippet:
        // ArcGIS Desktop
        // ArcGIS Engine
        // ArcGIS Server
        //
        // Required ArcGIS Extensions:
        // (NONE)
        //
        // Notes:
        // This snippet is intended to be inserted at the base level of a Class.
        // It is not intended to be nested within an existing Method.
        //
        // Use the following XML documentation comments to use this snippet:
        /// <summary>Get the index number for the specified layer name.</summary>
        ///
        /// <param name="activeView">An IActiveView interface</param>
        /// <param name="layerName">A System.String that is the layer name in the active view. Example: "states"</param>
        /// 
        /// <returns>An System.Int32 representing a layer number</returns>
        /// 
        /// <remarks>Returns a System.Int32 where values of 0 and greater are valid layers. A return value of -1 means the layer name was not found.</remarks>
        public System.Int32 GetIndexNumberOfLayerName(ESRI.ArcGIS.Carto.IActiveView activeView, System.String layerName)
        {
            if (activeView == null || layerName == null)
            {
                return -1;
            }
            ESRI.ArcGIS.Carto.IMap map = activeView.FocusMap;

            // Get the number of layers
            int numberOfLayers = map.LayerCount;

            // Loop through the layers and get the correct layer index
            for (System.Int32 i = 0; i < numberOfLayers; i++)
            {
                if (layerName == map.get_Layer(i).Name)
                {

                    // Layer was found
                    return i;
                }
            }

            // No layer was found
            return -1;
        }
        #endregion
          

        #region "Get Path for a Layer"
        // ArcGIS Snippet Title: 
        // Get Path for a Layer
        //
        // Add the following references to the project:
        // ESRI.ArcGIS.Carto
        // ESRI.ArcGIS.Geodatabase
        // ESRI.ArcGIS.System
        // 
        // Intended ArcGIS Products for this snippet:
        // ArcGIS Desktop
        // ArcGIS Engine
        // ArcGIS Server
        //
        // Required ArcGIS Extensions:
        // (NONE)
        //
        // Notes:
        // This snippet is intended to be inserted at the base level of a Class.
        // It is not intended to be nested within an existing Method.
        //
        // Use the following XML documentation comments to use this snippet:
        /// <summary>Get the full path and filename of a layer.</summary>
        ///
        /// <param name="layer">An ILayer interface.</param>
        /// 
        /// <returns>A System.String that is the full path and filename for a layer</returns>
        /// 
        /// <remarks></remarks>
        public System.String GetLayerPath(ESRI.ArcGIS.Carto.ILayer layer)
        {
            if (layer == null || !(layer is ESRI.ArcGIS.Geodatabase.IDataset))
            {
                return null;
            }
            ESRI.ArcGIS.Geodatabase.IDataset dataset = (ESRI.ArcGIS.Geodatabase.IDataset)(layer); // Explicit Cast
            //if (dataset.Category.IndexOf("Shapefile") == 0)
            //{
            //    return (dataset.Workspace.PathName + "\\" + dataset.Name + ".shp");
            //}
            //else
            //{
            //    return (dataset.Workspace.PathName + "\\" + dataset.Name );
            //}

            int  n = dataset.Category.IndexOf("Shapefile");
            switch (n)
            {
                case 0: // Yes shape file
                    return dataset.Name + ".shp";
                    // break;
                
                default:
                    if (dataset.Category.IndexOf("Geodatabase") > 0)
                    {
                        return ( System.IO.Path.GetFileName(dataset.Workspace.PathName) + "\\" + dataset.Name );
                    }
                    else
                    {
                        return ( dataset.Name);
                    }
                    
                    // break;
            }

        }
        #endregion

       

        #region "Get Map from ArcMap"
        // ArcGIS Snippet Title: 
        // Get Map from ArcMap
        //
        // Add the following references to the project:
        // ESRI.ArcGIS.ArcMapUI
        // ESRI.ArcGIS.Carto
        // ESRI.ArcGIS.Framework
        // ESRI.ArcGIS.System
        // 
        // Intended ArcGIS Products for this snippet:
        // ArcGIS Desktop
        //
        // Required ArcGIS Extensions:
        // (NONE)
        //
        // Notes:
        // This snippet is intended to be inserted at the base level of a Class.
        // It is not intended to be nested within an existing Method.
        //
        // Use the following XML documentation comments to use this snippet:
        /// <summary>Get Map from ArcMap.</summary>
        ///
        /// <param name="application">An IApplication interface that is the ArcMap application.</param>
        /// 
        /// <returns>An IMap interface.</returns>
        /// 
        /// <remarks></remarks>
        public ESRI.ArcGIS.Carto.IMap GetMap(ESRI.ArcGIS.Framework.IApplication application)
        {
            if (application == null)
            {
                return null;
            }
            ESRI.ArcGIS.ArcMapUI.IMxDocument mxDocument = ((ESRI.ArcGIS.ArcMapUI.IMxDocument)(application.Document)); // Explicit Cast
            ESRI.ArcGIS.Carto.IActiveView activeView = mxDocument.ActiveView;
            ESRI.ArcGIS.Carto.IMap map = activeView.FocusMap;

            return map;

        }
        #endregion                
        #region "Get BasicMap from Map"
        // ArcGIS Snippet Title: 
        // Get BasicMap from Map
        //
        // Add the following references to the project:
        // ESRI.ArcGIS.Carto
        // ESRI.ArcGIS.System
        // 
        // Intended ArcGIS Products for this snippet:
        // ArcGIS Desktop
        // ArcGIS Engine
        // ArcGIS Server
        //
        // Required ArcGIS Extensions:
        // (NONE)
        //
        // Notes:
        // This snippet is intended to be inserted at the base level of a Class.
        // It is not intended to be nested within an existing Method.
        //
        // GUID:
        // {7498A760-9476-4a02-9771-0C434FC8401D}
        //
        // Use the following XML documentation comments to use this snippet:
        /// <summary>Get BasicMap from Map.</summary>
        ///
        /// <param name="map">An IMap interface.</param>
        /// 
        /// <returns>An IBasicMap interface.</returns>
        /// 
        /// <remarks>IBasicMap is a subset of IMap that provides support for ArcScene and ArcGlobe. The Map (2D), Scene (3D), and Globe (3D) coclasses implement this interface. Components used by ArcMap, ArcScene, and ArcGlobe, (such as the IdentifyDialog) utilize IBasicMap rather than IMap.</remarks>
        public ESRI.ArcGIS.Carto.IBasicMap GetBasicMapFromMap(ESRI.ArcGIS.Carto.IMap map)
        {
            ESRI.ArcGIS.Carto.IBasicMap basicMap = map as ESRI.ArcGIS.Carto.IBasicMap; // Dynamic Cast

            return basicMap;
        }
        #endregion                
        #region "Loop Through Layers of Specific UID"
        // ArcGIS Snippet Title: 
        // Loop Through Layers of Specific UID
        //
        // Add the following references to the project:
        // ESRI.ArcGIS.Carto
        // ESRI.ArcGIS.System
        // 
        // Intended ArcGIS Products for this snippet:
        // ArcGIS Desktop
        // ArcGIS Engine
        // ArcGIS Server
        //
        // Required ArcGIS Extensions:
        // (NONE)
        //
        // Notes:
        // This snippet is intended to be inserted at the base level of a Class.
        // It is not intended to be nested within an existing Method.
        //
        // Use the following XML documentation comments to use this snippet:
        /// <summary>Stub code to loop through layers in a map where a specific UID is supplied.</summary>
        ///
        /// <param name="map">An IMap interface in which the layers reside.</param>
        /// <param name="layerCLSID">A System.String that is the layer GUID type. For example: "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}" is the IGeoFeatureLayer.</param>
        /// 
        /// <remarks>In order of the code to be useful the user needs to write their own code to use the layer in the TODO section.
        ///
        /// The different layer GUID's and Interface's are:
        /// "{AD88322D-533D-4E36-A5C9-1B109AF7A346}" = IACFeatureLayer
        /// "{74E45211-DFE6-11D3-9FF7-00C04F6BC6A5}" = IACLayer
        /// "{495C0E2C-D51D-4ED4-9FC1-FA04AB93568D}" = IACImageLayer
        /// "{65BD02AC-1CAD-462A-A524-3F17E9D85432}" = IACAcetateLayer
        /// "{4AEDC069-B599-424B-A374-49602ABAD308}" = IAnnotationLayer
        /// "{DBCA59AC-6771-4408-8F48-C7D53389440C}" = IAnnotationSublayer
        /// "{E299ADBC-A5C3-11D2-9B10-00C04FA33299}" = ICadLayer
        /// "{7F1AB670-5CA9-44D1-B42D-12AA868FC757}" = ICadastralFabricLayer
        /// "{BA119BC4-939A-11D2-A2F4-080009B6F22B}" = ICompositeLayer
        /// "{9646BB82-9512-11D2-A2F6-080009B6F22B}" = ICompositeGraphicsLayer
        /// "{0C22A4C7-DAFD-11D2-9F46-00C04F6BC78E}" = ICoverageAnnotationLayer
        /// "{6CA416B1-E160-11D2-9F4E-00C04F6BC78E}" = IDataLayer
        /// "{0737082E-958E-11D4-80ED-00C04F601565}" = IDimensionLayer
        /// "{48E56B3F-EC3A-11D2-9F5C-00C04F6BC6A5}" = IFDOGraphicsLayer
        /// "{40A9E885-5533-11D0-98BE-00805F7CED21}" = IFeatureLayer
        /// "{605BC37A-15E9-40A0-90FB-DE4CC376838C}" = IGdbRasterCatalogLayer
        /// "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}" = IGeoFeatureLayer
        /// "{34B2EF81-F4AC-11D1-A245-080009B6F22B}" = IGraphicsLayer
        /// "{EDAD6644-1810-11D1-86AE-0000F8751720}" = IGroupLayer
        /// "{D090AA89-C2F1-11D3-9FEF-00C04F6BC6A5}" = IIMSSubLayer
        /// "{DC8505FF-D521-11D3-9FF4-00C04F6BC6A5}" = IIMAMapLayer
        /// "{34C20002-4D3C-11D0-92D8-00805F7C28B0}" = ILayer
        /// "{E9B56157-7EB7-4DB3-9958-AFBF3B5E1470}" = IMapServerLayer
        /// "{B059B902-5C7A-4287-982E-EF0BC77C6AAB}" = IMapServerSublayer
        /// "{82870538-E09E-42C0-9228-CBCB244B91BA}" = INetworkLayer
        /// "{D02371C7-35F7-11D2-B1F2-00C04F8EDEFF}" = IRasterLayer
        /// "{AF9930F0-F61E-11D3-8D6C-00C04F5B87B2}" = IRasterCatalogLayer
        /// "{FCEFF094-8E6A-4972-9BB4-429C71B07289}" = ITemporaryLayer
        /// "{5A0F220D-614F-4C72-AFF2-7EA0BE2C8513}" = ITerrainLayer
        /// "{FE308F36-BDCA-11D1-A523-0000F8774F0F}" = ITinLayer
        /// "{FB6337E3-610A-4BC2-9142-760D954C22EB}" = ITopologyLayer
        /// "{005F592A-327B-44A4-AEEB-409D2F866F47}" = IWMSLayer
        /// "{D43D9A73-FF6C-4A19-B36A-D7ECBE61962A}" = IWMSGroupLayer
        /// "{8C19B114-1168-41A3-9E14-FC30CA5A4E9D}" = IWMSMapLayer
        ///</remarks>
        public void LoopThroughLayers(DataTable  DT,  System.Windows.Forms.CheckedListBox clbLayers, ESRI.ArcGIS.Carto.IMap map, System.String layerCLSID)
        {
            if (map == null || layerCLSID == null)
            {
                return;
            }
            
            

            ESRI.ArcGIS.esriSystem.IUID uid = new ESRI.ArcGIS.esriSystem.UIDClass();
            uid.Value = layerCLSID; // Example: "{E156D7E5-22AF-11D3-9F99-00C04F6BC78E}" = IGeoFeatureLayer
            try
            {
                
                
                ESRI.ArcGIS.Carto.IEnumLayer enumLayer = map.get_Layers(((ESRI.ArcGIS.esriSystem.UID)(uid)), true); // Explicit Cast 
                enumLayer.Reset();
                ESRI.ArcGIS.Carto.ILayer layer = enumLayer.Next();
                while (!(layer == null))
                {
                    // Loop thro table 0
                    foreach (DataRow datarow in DT.Rows)
                    {
                        string sLayerName = (string)datarow["LayerName"];
                        string sLayerPath = (string)datarow["LayerPath"];
                        if ((layer.Name == sLayerName) && (sLayerPath == GetLayerPath(layer)))
                        {
                            clbLayers.Items.Add(layer.Name); //  + " - " + GetLayerPath(layer));
                            break;

                        }

                    }


                    layer = enumLayer.Next();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error: " + ex.Message.ToString());
            }
        }
        #endregion
                
        #region "Get ActiveView from ArcMap"
        // ArcGIS Snippet Title: 
        // Get ActiveView from ArcMap
        //
        // Add the following references to the project:
        // ESRI.ArcGIS.ArcMapUI
        // ESRI.ArcGIS.Carto
        // ESRI.ArcGIS.Framework
        // ESRI.ArcGIS.System
        // 
        // Intended ArcGIS Products for this snippet:
        // ArcGIS Desktop
        //
        // Required ArcGIS Extensions:
        // (NONE)
        //
        // Notes:
        // This snippet is intended to be inserted at the base level of a Class.
        // It is not intended to be nested within an existing Method.
        //
        // Use the following XML documentation comments to use this snippet:
        /// <summary>Get ActiveView from ArcMap.</summary>
        ///
        /// <param name="application">An IApplication interface that is the ArcMap application.</param>
        /// 
        /// <returns>An IActiveView interface.</returns>
        /// 
        /// <remarks></remarks>
        public ESRI.ArcGIS.Carto.IActiveView GetActiveView(ESRI.ArcGIS.Framework.IApplication application)
        {
            if (application == null)
            {
                return null;
            }
            ESRI.ArcGIS.ArcMapUI.IMxDocument mxDocument = application.Document as ESRI.ArcGIS.ArcMapUI.IMxDocument; // Dynamic Cast
            ESRI.ArcGIS.Carto.IActiveView activeView = mxDocument.ActiveView;
            
            return activeView;
        }
        #endregion                
        #region "Flash Geometry"
        // ArcGIS Snippet Title: 
        // Flash Geometry
        //
        // Add the following references to the project:
        // ESRI.ArcGIS.Display
        // ESRI.ArcGIS.Geometry
        // ESRI.ArcGIS.System 
        // 
        // Intended ArcGIS Products for this snippet:
        // ArcGIS Desktop
        // ArcGIS Engine
        // ArcGIS Server
        //
        // Required ArcGIS Extensions:
        // (NONE)
        //
        // Notes:
        // This snippet is intended to be inserted at the base level of a Class.
        // It is not intented to be nested withing an exisiting Method.
        //
        // Use the following XML documentation comments to use this snippet:
        /// <summary>Flash geometry on the display. The geometry type could be polygon, polyline, point, or multipoint.</summary>
        ///
        /// <param name="geometry"> An IGeometry interface</param>
        /// <param name="color">An IRgbColor interface</param>
        /// <param name="display">An IDisplay interface</param>
        /// <param name="delay">A System.Int32 that is the time im milliseconds to wait.</param>
        /// 
        /// <remarks></remarks>
        public void FlashGeometry(ESRI.ArcGIS.Geometry.IGeometry geometry, ESRI.ArcGIS.Display.IRgbColor color, ESRI.ArcGIS.Display.IDisplay display, System.Int32 delay)
        {
            if (geometry == null || color == null || display == null)
            {
                return;
            }

            display.StartDrawing(display.hDC, (System.Int16)ESRI.ArcGIS.Display.esriScreenCache.esriNoScreenCache); // Explicit Cast


            switch (geometry.GeometryType)
            {
                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolygon:
                    {
                        //Set the flash geometry's symbol.
                        ESRI.ArcGIS.Display.ISimpleFillSymbol simpleFillSymbol = new ESRI.ArcGIS.Display.SimpleFillSymbolClass();
                        simpleFillSymbol.Color = color;
                        ESRI.ArcGIS.Display.ISymbol symbol = simpleFillSymbol as ESRI.ArcGIS.Display.ISymbol; // Dynamic Cast
                        symbol.ROP2 = ESRI.ArcGIS.Display.esriRasterOpCode.esriROPNotXOrPen;

                        //Flash the input polygon geometry.
                        display.SetSymbol(symbol);
                        display.DrawPolygon(geometry);
                        System.Threading.Thread.Sleep(delay);
                        display.DrawPolygon(geometry);
                        break;
                    }

                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPolyline:
                    {
                        //Set the flash geometry's symbol.
                        ESRI.ArcGIS.Display.ISimpleLineSymbol simpleLineSymbol = new ESRI.ArcGIS.Display.SimpleLineSymbolClass();
                        simpleLineSymbol.Width = 4;
                        simpleLineSymbol.Color = color;
                        ESRI.ArcGIS.Display.ISymbol symbol = simpleLineSymbol as ESRI.ArcGIS.Display.ISymbol; // Dynamic Cast
                        symbol.ROP2 = ESRI.ArcGIS.Display.esriRasterOpCode.esriROPNotXOrPen;

                        //Flash the input polyline geometry.
                        display.SetSymbol(symbol);
                        display.DrawPolyline(geometry);
                        System.Threading.Thread.Sleep(delay);
                        display.DrawPolyline(geometry);
                        break;
                    }

                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryPoint:
                    {
                        //Set the flash geometry's symbol.
                        ESRI.ArcGIS.Display.ISimpleMarkerSymbol simpleMarkerSymbol = new ESRI.ArcGIS.Display.SimpleMarkerSymbolClass();
                        simpleMarkerSymbol.Style = ESRI.ArcGIS.Display.esriSimpleMarkerStyle.esriSMSCircle;
                        simpleMarkerSymbol.Size = 12;
                        simpleMarkerSymbol.Color = color;
                        ESRI.ArcGIS.Display.ISymbol symbol = simpleMarkerSymbol as ESRI.ArcGIS.Display.ISymbol; // Dynamic Cast
                        symbol.ROP2 = ESRI.ArcGIS.Display.esriRasterOpCode.esriROPNotXOrPen;

                        //Flash the input point geometry.
                        display.SetSymbol(symbol);
                        display.DrawPoint(geometry);
                        System.Threading.Thread.Sleep(delay);
                        display.DrawPoint(geometry);
                        break;
                    }

                case ESRI.ArcGIS.Geometry.esriGeometryType.esriGeometryMultipoint:
                    {
                        //Set the flash geometry's symbol.
                        ESRI.ArcGIS.Display.ISimpleMarkerSymbol simpleMarkerSymbol = new ESRI.ArcGIS.Display.SimpleMarkerSymbolClass();
                        simpleMarkerSymbol.Style = ESRI.ArcGIS.Display.esriSimpleMarkerStyle.esriSMSCircle;
                        simpleMarkerSymbol.Size = 12;
                        simpleMarkerSymbol.Color = color;
                        ESRI.ArcGIS.Display.ISymbol symbol = simpleMarkerSymbol as ESRI.ArcGIS.Display.ISymbol; // Dynamic Cast
                        symbol.ROP2 = ESRI.ArcGIS.Display.esriRasterOpCode.esriROPNotXOrPen;

                        //Flash the input multipoint geometry.
                        display.SetSymbol(symbol);
                        display.DrawMultipoint(geometry);
                        System.Threading.Thread.Sleep(delay);
                        display.DrawMultipoint(geometry);
                        break;
                    }
            }
            display.FinishDrawing();
        }
        #endregion        
            
        #region "Get Contents View from ArcMap"
        // ArcGIS Snippet Title: 
        // Get Contents View from ArcMap
        //
        // Add the following references to the project:
        // ESRI.ArcGIS.ArcMapUI
        // ESRI.ArcGIS.Framework
        // ESRI.ArcGIS.System
        // 
        // Intended ArcGIS Products for this snippet:
        // ArcGIS Desktop
        //
        // Required ArcGIS Extensions:
        // (NONE)
        //
        // Notes:
        // This snippet is intended to be inserted at the base level of a Class.
        // It is not intended to be nested within an existing Method.
        //
        // Use the following XML documentation comments to use this snippet:
        /// <summary>Get the Contents View (TOC) for ArcMap.</summary>
        ///
        /// <param name="application">An IApplication interface that is the ArcMap application.</param>
        /// <param name="index">A System.Int32 that is the tab number of the TOC. When specifying the index number: 0 = usually the Display tab, 1 = usually the Source tab.</param>
        /// 
        /// <returns>An IContentsView interface.</returns>
        /// 
        /// <remarks></remarks>
        public ESRI.ArcGIS.ArcMapUI.IContentsView GetContentsView(ESRI.ArcGIS.Framework.IApplication application, System.Int32 index)
        {
            if (application == null || index < 0 || index > 1)
            {
                return null;
            }
            ESRI.ArcGIS.ArcMapUI.IMxDocument mxDocument = (ESRI.ArcGIS.ArcMapUI.IMxDocument)(application.Document); // Explicit Cast
            ESRI.ArcGIS.ArcMapUI.IContentsView contentsView = mxDocument.get_ContentsView(index); // 0 = usually the Display tab, 1 = usually the Source tab

            return contentsView;
        }
        #endregion

        #region "Perform Spatial Query"
        // ArcGIS Snippet Title: 
        // Perform Spatial Query
        // 
        // Add the following references to the project:
        // ESRI.ArcGIS.Geodatabase
        // ESRI.ArcGIS.Geometry
        // ESRI ArcGIS.System
        //
        // Intended ArcGIS Products for this snippet:
        // ArcGIS Desktop
        // ArcGIS Engine
        // ArcGIS Server
        //
        // Required ArcGIS Extensions:
        // (NONE)
        //
        // Notes:
        // This snippet is intended to be inserted at the base level of a Class.
        // It is not intended to be nested within an existing Method.
        //
        // Use the following XML documentation comments to use this snippet:
        /// <summary>Creates a spatial query which preforms a spatial search for features in the supplied feature class and has the option to also apply an attribute query via a where clause.</summary>
        ///
        /// <param name="featureClass">An ESRI.ArcGIS.Geodatabase.IFeatureClass</param>
        /// <param name="searchGeometry">An ESRI.ArcGIS.Geometry.IGeometry (Only high-level geometries can be used)</param>
        /// <param name="spatialRelation">An ESRI.ArcGIS.Geodatabase.esriSpatialRelEnum (e.g., esriSpatialRelIntersects)</param>
        /// <param name="whereClause">A System.String, (e.g., "city_name = 'Redlands'").</param>
        /// 
        /// <returns>An IFeatureCursor holding the results of the query will be returned.</returns>
        /// 
        /// <remarks>Call the SpatialQuery method by passing in a reference to the Feature Class, a
        /// Geometry used for the search and the spatial operation to be preformed.
        /// An exmaple of a spatial opertaion would be intersects (e.g., esriSpatialRelEnum.esriSpatialRelContains)
        /// If you would like to return everything found by the spatial operation use "" for the 
        /// where clause. Optionally a whereclause (e.g. "income > 1000") maybe applied if desired.
        /// The SQL syntax used to specify the where clause is the same as that of the 
        /// underlying database holding the data. </remarks>
        public ESRI.ArcGIS.Geodatabase.IFeatureCursor SpatialQuery(ESRI.ArcGIS.Geodatabase.IFeatureClass featureClass,
                                                                   ESRI.ArcGIS.Geometry.IGeometry searchGeometry,
                                                                   ESRI.ArcGIS.Geodatabase.esriSpatialRelEnum spatialRelation,
                                                                   System.String whereClause)
        {
            // create a spatial query filter
            ESRI.ArcGIS.Geodatabase.ISpatialFilter spatialFilter = new ESRI.ArcGIS.Geodatabase.SpatialFilterClass();

            // specify the geometry to query with
            spatialFilter.Geometry = searchGeometry;

            // specify what the geometry field is called on the Feature Class that we will be querying against
            System.String nameOfShapeField = featureClass.ShapeFieldName;
            spatialFilter.GeometryField = nameOfShapeField;

            // specify the type of spatial operation to use
            spatialFilter.SpatialRel = spatialRelation;

            // create the where statement
            spatialFilter.WhereClause = whereClause;

            // perform the query and use a cursor to hold the results
            ESRI.ArcGIS.Geodatabase.IQueryFilter queryFilter = new ESRI.ArcGIS.Geodatabase.QueryFilterClass();
            queryFilter = (ESRI.ArcGIS.Geodatabase.IQueryFilter)spatialFilter;
            ESRI.ArcGIS.Geodatabase.IFeatureCursor featureCursor = featureClass.Search(queryFilter, false);

            return featureCursor;
        }
        #endregion


        #region "Get Selected Table in Contents View"
        // ArcGIS Snippet Title: 
        // Get Selected Table in Contents View
        //
        // Add the following references to the project:
        // ESRI.ArcGIS.ArcMapUI 
        // ESRI.ArcGIS.Carto
        // ESRI.ArcGIS.Geodatabase
        // 
        // Intended ArcGIS Products for this snippet:
        // ArcGIS Desktop
        //
        // Required ArcGIS Extensions:
        // (NONE)
        //
        // Notes:
        // This snippet is intended to be inserted at the base level of a Class.
        // It is not intended to be nested within an existing Method.
        //
        // Use the following XML documentation comments to use this snippet:
        /// <summary>Returns a reference to the currently selected table in the given contents view.</summary>
        ///
        /// <param name="currentContentsView">An IContentsView interface.</param>
        ///
        /// <returns>An ITable interface or null if not found.</returns>
        /// 
        /// <remarks></remarks>
        public ESRI.ArcGIS.Geodatabase.ITable GetSelectedTableFrom(ESRI.ArcGIS.ArcMapUI.IContentsView currentContentsView)
        {
            if (currentContentsView != null && currentContentsView.SelectedItem is ESRI.ArcGIS.Geodatabase.ITable)
            {
                ESRI.ArcGIS.Geodatabase.ITable table = (ESRI.ArcGIS.Geodatabase.ITable)currentContentsView.SelectedItem;
                return table;
            }
            return null;
        }
        #endregion    
                
    }
}

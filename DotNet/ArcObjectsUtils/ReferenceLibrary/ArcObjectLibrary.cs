using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace ReferenceLibrary
    {
    [Guid ( "1f65af39-c8b5-4c17-a4de-2a4122c2e3b4" )]
    [ClassInterface ( ClassInterfaceType.None )]
    [ProgId ( "ReferenceLibrary.ArcObjectLibrary" )]
    public class ArcObjectLibrary
        {
        #region "Get FeatureClass of Selected Feature Layer in Contents View"
        // ArcGIS Snippet Title: 
        // Get FeatureClass of Selected Feature Layer in Contents View
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
        /// <summary>Returns a reference to the currently selected featureclass from the given contents view.</summary>
        ///
        /// <param name="currentContentsView">An IContentsView interface.</param>
        ///
        /// <returns>An IFeatureClass interface or null if not found.</returns>
        /// 
        /// <remarks></remarks>
        public ESRI.ArcGIS.Geodatabase.IFeatureClass GetSelectedFeatureClassFrom ( ESRI.ArcGIS.ArcMapUI.IContentsView currentContentsView )
            {
            if ( currentContentsView == null )
                {
                return null;
                }
            if ( currentContentsView.SelectedItem is ESRI.ArcGIS.Carto.IFeatureLayer )
                {
                ESRI.ArcGIS.Carto.IFeatureLayer featureLayer = ( ESRI.ArcGIS.Carto.IFeatureLayer ) currentContentsView.SelectedItem; // Explicit Cast
                ESRI.ArcGIS.Geodatabase.IFeatureClass featureClass = featureLayer.FeatureClass;

                return featureClass;
                }
            return null;
            }
        #endregion

        #region "Get Index Number from Layer Name"
        /// <summary>Get the index number for the specified layer name.</summary>
        ///
        /// <param name="activeView">An IActiveView interface</param>
        /// <param name="layerName">A System.String that is the layer name in the active view. Example: "states"</param>
        /// 
        /// <returns>An System.Int32 representing a layer number</returns>
        /// 
        /// <remarks>Returns a System.Int32 where values of 0 and greater are valid layers. A return value of -1 means the layer name was not found.</remarks>
        public System.Int32 GetIndexNumberOfLayerName ( ESRI.ArcGIS.Carto.IActiveView activeView, System.String layerName )
            {
            if ( activeView == null || layerName == null )
                {
                return -1;
                }
            ESRI.ArcGIS.Carto.IMap map = activeView.FocusMap;

            // Get the number of layers
            int numberOfLayers = map.LayerCount;

            // Loop through the layers and get the correct layer index
            for ( System.Int32 i = 0; i < numberOfLayers; i++ )
                {
                if ( layerName == map.get_Layer ( i ).Name )
                    {

                    // Layer was found
                    return i;
                    }
                }

            // No layer was found
            return -1;
            }
        /// <summary>Get the index number for the specified layer name.</summary>
        ///
        /// <param name="activeView">An IActiveView interface</param>
        /// <param name="layerName">A System.String that is the layer name in the active view. Example: "states"</param>
        /// <param name="trimDot">A boolean for whether the layer name has the db.owner. data trimmed. 
        /// Set this to 'true' if the layerName passed has been trimmed</param>
        /// <returns></returns>
        public System.Int32 GetIndexNumberOfLayerName ( ESRI.ArcGIS.Carto.IActiveView activeView, System.String layerName, bool trimDot )
            {
            if ( activeView == null || layerName == null )
                {
                return -1;
                }
            ESRI.ArcGIS.Carto.IMap map = activeView.FocusMap;

            // Get the number of layers
            int numberOfLayers = map.LayerCount;

            // Loop through the layers and get the correct layer index
            for ( System.Int32 i = 0; i < numberOfLayers; i++ )
                {
                // Check to see if the passed layer name was trimmed
                if ( trimDot )
                    {
                    // Yes
                    string name = map.get_Layer ( i ).Name;
                    int dotLocation = name.LastIndexOf ( "." );
                    if ( dotLocation == -1 )
                        {
                        if ( layerName == map.get_Layer ( i ).Name )
                            {
                            return i;   // Layer was found
                            }
                        }
                    else
                        {
                        if(layerName==name.Substring(dotLocation+1))
                            {
                            return i;   // Layer was found
                            }
                        }
                    }
                else
                    {
                    // No
                    if ( layerName == map.get_Layer ( i ).Name )
                        {
                        return i;       // Layer was found
                        }
                    }
                }

            // No layer was found
            return -1;
            }
        #endregion

        /// <summary>Get the index number for the specified table name.</summary>
        ///
        /// <param name="activeView">An IActiveView interface</param>
        /// <param name="layerName">A System.String that is the table name in the active view. Example: "states"</param>
        /// 
        /// <returns>An System.Int32 representing a table number</returns>
        /// 
        /// <remarks>Returns a System.Int32 where values of 0 and greater are valid layers. A return value of -1 means the table name was not found.</remarks>
        public System.Int32 GetIndexNumberOfStandaloneTable(ESRI.ArcGIS.Carto.IActiveView activeView, System.String tableName)
            {
            if ( activeView == null || tableName == null )
                {
                return -1;
                }
            ESRI.ArcGIS.Carto.IMap map = activeView.FocusMap;

            // Get the number of tables
            ESRI.ArcGIS.Carto.IStandaloneTableCollection saTableCollection = ( ESRI.ArcGIS.Carto.IStandaloneTableCollection ) map;
            int numberOfTables = saTableCollection.StandaloneTableCount;

            // Loop through the tables and get the correct table index
            for ( System.Int32 i = 0; i < numberOfTables; i++ )
                {
                if ( tableName == saTableCollection.get_StandaloneTable(i).Name )
                    {
                    return i;   // Table was found
                    }
                }
            // No table was found
            return -1;
            }

        #region "Get Selected Table in Contents View"
        /// <summary>Returns a reference to the currently selected table in the given contents view.</summary>
        ///
        /// <param name="currentContentsView">An IContentsView interface.</param>
        ///
        /// <returns>An ITable interface or null if not found.</returns>
        /// 
        /// <remarks></remarks>
        public ESRI.ArcGIS.Geodatabase.ITable GetSelectedTableFrom ( ESRI.ArcGIS.ArcMapUI.IContentsView currentContentsView )
            {
            if ( currentContentsView != null && currentContentsView.SelectedItem is ESRI.ArcGIS.Geodatabase.ITable )
                {
                ESRI.ArcGIS.Geodatabase.ITable table = ( ESRI.ArcGIS.Geodatabase.ITable ) currentContentsView.SelectedItem;
                return table;
                }
            return null;
            }
        #endregion

        #region "Get Editor from ArcMap"
        /// <summary>Returns a reference to the ESRI editor object.</summary>
        ///
        /// <param name="mxApplication">An IMxApplication interface, ie. ArcMap.</param>
        /// 
        /// <returns>An IEditor2 interface, the ArcMap Editor.</returns>
        /// 
        /// <remarks>You could also use the: application.FindExtensionByName("ESRI Object Editor") to get the extension object.</remarks>
        public ESRI.ArcGIS.Editor.IEditor2 GetESRIEditorFrom ( ESRI.ArcGIS.ArcMapUI.IMxApplication mxApplication )
            {
            if ( mxApplication == null )
                {
                return null;
                }
            ESRI.ArcGIS.esriSystem.UID uid = new ESRI.ArcGIS.esriSystem.UIDClass ();
            uid.Value = "{F8842F20-BB23-11D0-802B-0000F8037368}";
            ESRI.ArcGIS.Framework.IApplication application = mxApplication as ESRI.ArcGIS.Framework.IApplication; // Dynamic Cast
            ESRI.ArcGIS.esriSystem.IExtension extension = application.FindExtensionByCLSID ( uid );
            ESRI.ArcGIS.Editor.IEditor2 editor2 = extension as ESRI.ArcGIS.Editor.IEditor2; // Dynamic Cast

            return editor2;
            }
        #endregion        
        
        /// <summary>
        /// Creates a new MemoryRelationshipClass from a MemoryRelationshipClassName
        /// </summary>
        /// <param name="originName">The name object for the origin table of the memory relationship.</param>
        /// <param name="destinationName">The name object of the destination class of the memory relationship.</param>
        /// <param name="originField">Origin primary key field name.</param>
        /// <param name="destinationField">Origin foreign key field name.</param>
        /// <returns>An IRelationshipClass that provides access to members that define a memory relationship class name</returns>
              
        public ESRI.ArcGIS.Geodatabase.IRelationshipClass CreateMemRelClass ( ESRI.ArcGIS.esriSystem.IName originName,
                                                                            ESRI.ArcGIS.esriSystem.IName destinationName,
                                                                            string originField,
                                                                            string destinationField )
            {
            // Set the needed memory relationship class properties
            ESRI.ArcGIS.Geodatabase.IMemoryRelationshipClassName memoryRelClassName = ( ESRI.ArcGIS.Geodatabase.IMemoryRelationshipClassName ) new ESRI.ArcGIS.Geodatabase.MemoryRelationshipClassNameClass ();
            memoryRelClassName.OriginName = originName;
            memoryRelClassName.DestinationName = destinationName;
            memoryRelClassName.OriginPrimaryKey = originField;
            memoryRelClassName.OriginForeignKey = destinationField;
            memoryRelClassName.ForwardPathLabel = "forward";
            memoryRelClassName.BackwardPathLabel = "backward";

            // Set the needed relationship class properties
            ESRI.ArcGIS.Geodatabase.IRelationshipClassName relationshipClassName = ( ESRI.ArcGIS.Geodatabase.IRelationshipClassName ) memoryRelClassName;
            // 1:1...THIS MAY NEED TO BE ADJUSTABLE...possibly with another parameter
            relationshipClassName.Cardinality = ESRI.ArcGIS.Geodatabase.esriRelCardinality.esriRelCardinalityOneToOne;
            ESRI.ArcGIS.Geodatabase.IDatasetName datasetName = ( ESRI.ArcGIS.Geodatabase.IDatasetName ) relationshipClassName;
            datasetName.Name = originName.NameString + "_to_" + destinationName.NameString;

            // Open the new MemoryRelationshipClass
            ESRI.ArcGIS.esriSystem.IName name = ( ESRI.ArcGIS.esriSystem.IName ) relationshipClassName;
            return ( ESRI.ArcGIS.Geodatabase.IRelationshipClass ) name.Open ();
            }

        #region "Get FeatureLayer from Layer Index Number"
        /// <summary>Get the FeatureLayer by specifying the Layer Index Number.</summary>
        ///
        /// <param name="activeView">An IActiveView interface</param>
        /// <param name="layerIndex">An System.Int32 that is the layer index number in the map (zero based). Example: 3</param>
        /// 
        /// <returns>An IFeatureLayer interface (if found) or null (if not found)</returns>
        /// 
        /// <remarks></remarks>
        public ESRI.ArcGIS.Carto.IFeatureLayer GetIFeatureLayerFromLayerIndexNumber ( ESRI.ArcGIS.Carto.IActiveView activeView, System.Int32 layerIndex )
            {

            if ( activeView == null || layerIndex < 0 )
                {
                return null;
                }
            ESRI.ArcGIS.Carto.IMap map = activeView.FocusMap;
            if ( layerIndex < map.LayerCount && map.get_Layer ( layerIndex ) is ESRI.ArcGIS.Carto.IFeatureLayer )
                {
                return ( ESRI.ArcGIS.Carto.IFeatureLayer ) activeView.FocusMap.get_Layer ( layerIndex ); // Explicit Cast
                }
            else
                {
                return null;
                }
            }
        #endregion

        #region "Get StandaloneTable from Table Index Number"
        /// <summary>
        /// 
        /// </summary>
        /// <param name="activeView"></param>
        /// <param name="tableIndex"></param>
        /// <returns></returns>
        public ESRI.ArcGIS.Geodatabase.ITable GetStandaloneTableFromTableIndexNumber ( ESRI.ArcGIS.Carto.IActiveView activeView, System.Int32 tableIndex )
            {
            if ( activeView == null || tableIndex < 0 )
            { return null; }

            ESRI.ArcGIS.Carto.IMap map = activeView.FocusMap;
            ESRI.ArcGIS.Carto.IStandaloneTableCollection saTableCollection = ( ESRI.ArcGIS.Carto.IStandaloneTableCollection ) map;
            if ( tableIndex < saTableCollection.StandaloneTableCount && saTableCollection.get_StandaloneTable ( tableIndex ) is ESRI.ArcGIS.Geodatabase.ITable )
                {
                return ( ESRI.ArcGIS.Geodatabase.ITable ) saTableCollection.get_StandaloneTable ( tableIndex );// Explicit Cast
                }
            else
                {
                return null;
                }
            }
        #endregion
        }
    }

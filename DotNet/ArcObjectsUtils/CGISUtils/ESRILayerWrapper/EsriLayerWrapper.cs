using System;

namespace CGIS.MapWorks.GeoData
{
	/// <summary>
	/// Summary description for EsriLayerWrapper.
	/// </summary>
	public class EsriLayerWrapper
	{
		private string _layerTableName = null;
		private ESRI.ArcGIS.Carto.IMap _sourceMap;
		private ESRI.ArcGIS.Carto.ILayer _containedEsriLayer;
		private ESRI.ArcGIS.Geodatabase.IFeature _currentFeature;
		private ESRI.ArcGIS.Geometry.IGeometry _currentGeometry;
		private ESRI.ArcGIS.Geometry.IGeometry _currentGeneralizedGeometry;
		private ESRI.ArcGIS.Geometry.IGeometry _selectedGeneralizedGeometry;
		
		public EsriLayerWrapper()
		{
		}
		
		public EsriLayerWrapper(string layerFilePath, ESRI.ArcGIS.MapControl.AxMapControl targetMapControl, bool addLayerToMap)
		{
			if (System.IO.File.Exists(layerFilePath) == true)
			{
				try
				{
					_sourceMap = targetMapControl.Map;
					targetMapControl.AddLayerFromFile(layerFilePath,0);
					_containedEsriLayer = targetMapControl.Map.get_Layer(0);
					if (addLayerToMap == false && _containedEsriLayer != null)
					{
						targetMapControl.Map.DeleteLayer(_containedEsriLayer);
					}

				}
				catch
				{
					_layerTableName = null;
					_sourceMap = null;
					_containedEsriLayer = null;
				}
			}
		}

		public EsriLayerWrapper(ESRI.ArcGIS.Carto.IMap sourceMap, string mapLayerTableName)
		{
			_layerTableName = mapLayerTableName;
			_sourceMap = sourceMap;
			_containedEsriLayer = CGIS.MapWorks.EsriUtils.GetOneLayerByTableName(_layerTableName, _sourceMap);
		}

		public EsriLayerWrapper(ESRI.ArcGIS.Carto.ILayer sourceLayer, ESRI.ArcGIS.Carto.IMap sourceMap)
		{
			_containedEsriLayer = sourceLayer;
			_sourceMap = sourceMap;
			try
			{
				_layerTableName = CGIS.MapWorks.EsriUtils.GetLayerTableName(_containedEsriLayer);
			}
			catch
			{
				_layerTableName = null;
			}
		}

		public string GeometryFieldName
		{
			get 
			{
				if (this.Valid == false)
					return null;

				if (_containedEsriLayer is ESRI.ArcGIS.Carto.IFeatureLayer)
				{
					ESRI.ArcGIS.Carto.IFeatureLayer featureLayer = (ESRI.ArcGIS.Carto.IFeatureLayer)_containedEsriLayer;
					ESRI.ArcGIS.Geodatabase.IFeatureClass featureClass = featureLayer.FeatureClass;
					return featureClass.ShapeFieldName;
				}
				else
				{
					return null;
				}
			}
		}

		public string TableName
		{
			get 
			{ 
				if (_layerTableName == null)
				{
					_layerTableName = CGIS.MapWorks.EsriUtils.GetLayerTableName(_containedEsriLayer);
				}
				return _layerTableName; 
			}
			set { _layerTableName = value; }
		}

		public ESRI.ArcGIS.Carto.IMap SourceMap
		{
			get { return _sourceMap; }
			set { _sourceMap = value; }
		}

		public ESRI.ArcGIS.Carto.ILayer EsriLayer
		{
			get 
			{ 
				if (_containedEsriLayer == null)
				{
					_containedEsriLayer = CGIS.MapWorks.EsriUtils.GetOneLayerByTableName(_layerTableName, _sourceMap);
				}
				return _containedEsriLayer;
			}
			set { _containedEsriLayer = value; }

		}

		public bool Valid
		{
			get	{ return CGIS.MapWorks.EsriUtils.LayerIsValid(_containedEsriLayer);	}
		}

		public void SelectOnMap(string whereClause)
		{
			if (whereClause == null || whereClause == "")
				return;
			ESRI.ArcGIS.Geodatabase.QueryFilter queryFilter = new ESRI.ArcGIS.Geodatabase.QueryFilterClass();
			queryFilter.WhereClause = whereClause;
			this.SelectOnMap(queryFilter);
			// release problem unmanaged objects
			System.Runtime.InteropServices.Marshal.ReleaseComObject(queryFilter);
		}

		public void SelectOnMap(ESRI.ArcGIS.Geodatabase.IQueryFilter queryFilter)
		{
			this.SelectOnMap(queryFilter,null,CGIS.MapWorks.EsriUtils.SelectionModifierEnum.NO_MODIFIER);
		}

		public void SelectOnMap(ESRI.ArcGIS.Geodatabase.IQueryFilter queryFilter, int[] modifySelectionArrayOfIDs, CGIS.MapWorks.EsriUtils.SelectionModifierEnum modifyMethod)
		{
			if (queryFilter == null || this.Valid == false || _containedEsriLayer is ESRI.ArcGIS.Carto.IFeatureLayer == false)
				return;
			ESRI.ArcGIS.Carto.FeatureLayer featureLayer = (ESRI.ArcGIS.Carto.FeatureLayer)_containedEsriLayer;
			ESRI.ArcGIS.Carto.IFeatureSelection featureSelection = (ESRI.ArcGIS.Carto.IFeatureSelection)featureLayer;
			try
			{
				featureSelection.SelectFeatures(queryFilter, ESRI.ArcGIS.Carto.esriSelectionResultEnum.esriSelectionResultNew, false);
			}
			catch (System.Exception ex)
			{
				//todo: handle error with special gui
				System.Windows.Forms.MessageBox.Show("Failed to query table '" + _layerTableName + "' using the following where clause: '" + queryFilter.WhereClause + "'. Underlying system returned to following error '" + ex.Message + "'","Error",System.Windows.Forms.MessageBoxButtons.OK,System.Windows.Forms.MessageBoxIcon.Error);
				return;
			}
			if (modifyMethod == CGIS.MapWorks.EsriUtils.SelectionModifierEnum.ADD_TO_SELECTION)
			{
				if (modifySelectionArrayOfIDs != null)
				{
					ESRI.ArcGIS.Geodatabase.ISelectionSet selectionSet = featureSelection.SelectionSet;
					selectionSet.AddList(modifySelectionArrayOfIDs.Length, ref modifySelectionArrayOfIDs[0]);
				}
			}
			else if (modifyMethod == CGIS.MapWorks.EsriUtils.SelectionModifierEnum.SUBTRACT_FROM_SELECTION)
			{
				if (modifySelectionArrayOfIDs != null)
				{
					ESRI.ArcGIS.Geodatabase.ISelectionSet selectionSet = featureSelection.SelectionSet;
					selectionSet.RemoveList(modifySelectionArrayOfIDs.Length, ref modifySelectionArrayOfIDs[0]);
				}
			}
			featureSelection.SelectionChanged();
			// release problem unmanaged objects
			System.Runtime.InteropServices.Marshal.ReleaseComObject(queryFilter);
		}

		public void SelectOnMap(ESRI.ArcGIS.Geodatabase.ISpatialFilter spatialFilter)
		{
			ESRI.ArcGIS.Geodatabase.QueryFilter queryFilter = (ESRI.ArcGIS.Geodatabase.QueryFilter)spatialFilter;
			this.SelectOnMap(queryFilter);
		}

		public ESRI.ArcGIS.Geodatabase.IFeature[] Query(string whereClause)
		{
			if (whereClause == null || whereClause == "")
				return null;
			ESRI.ArcGIS.Geodatabase.QueryFilter queryFilter = new ESRI.ArcGIS.Geodatabase.QueryFilterClass();
			queryFilter.WhereClause = whereClause;
			ESRI.ArcGIS.Geodatabase.IFeature[] featureArray = this.Query(queryFilter);
			// release problem unmanaged objects
			System.Runtime.InteropServices.Marshal.ReleaseComObject(queryFilter);
			return featureArray;		
		}

		public ESRI.ArcGIS.Geodatabase.IFeature[] Query(ESRI.ArcGIS.Geodatabase.IQueryFilter queryFilter)
		{
			if (this.Valid == false)
				return null;
			if (_containedEsriLayer is ESRI.ArcGIS.Carto.IFeatureLayer == false)
				return null;

			ESRI.ArcGIS.Carto.FeatureLayer featureLayer = (ESRI.ArcGIS.Carto.FeatureLayer)_containedEsriLayer;
			ESRI.ArcGIS.Geodatabase.IFeatureClass featureClass = featureLayer.FeatureClass;
			ESRI.ArcGIS.Geodatabase.IFeatureCursor featureCursor = featureClass.Search(queryFilter,false);
			ESRI.ArcGIS.Geodatabase.IFeature[] featureArray = CGIS.MapWorks.EsriUtils.FeatureCursorToArray(featureCursor);
			// release problem unmanaged objects
			System.Runtime.InteropServices.Marshal.ReleaseComObject(featureCursor);
			System.Runtime.InteropServices.Marshal.ReleaseComObject(queryFilter);
			return featureArray;
		}

		public ESRI.ArcGIS.Geodatabase.IFeature[] CurrentSelection()
		{
			ESRI.ArcGIS.Geodatabase.ISelectionSet selectionSet = this.CurrentSelectionSet();
			ESRI.ArcGIS.Geodatabase.ICursor cursor;
			try
			{
				selectionSet.Search(null, false, out cursor);
				ESRI.ArcGIS.Geodatabase.IFeature[] featureArray = CGIS.MapWorks.EsriUtils.FeatureCursorToArray((ESRI.ArcGIS.Geodatabase.IFeatureCursor)cursor);
				System.Runtime.InteropServices.Marshal.ReleaseComObject(cursor);
				return featureArray;
			}
			catch
			{
				return null;
			}
		}

		public ESRI.ArcGIS.Geodatabase.ISelectionSet CurrentSelectionSet()
		{
			ESRI.ArcGIS.Carto.IFeatureSelection featureSelection = (ESRI.ArcGIS.Carto.IFeatureSelection)_containedEsriLayer;
			return featureSelection.SelectionSet;
		}

		public ESRI.ArcGIS.Geodatabase.IFeature CurrentFeature
		{
			get { return _currentFeature; }
			set 
			{
				if (_currentFeature != null)
					System.Runtime.InteropServices.Marshal.ReleaseComObject(_currentFeature);

				_currentFeature = value;
				this.CurrentGeometry = null;
				this.CurrentGeneralizedGeometry = null;
			}
		}

		public ESRI.ArcGIS.Geometry.IGeometry CurrentGeometry
		{
			set { _currentGeometry = value; }
			get
			{
				if (_currentGeometry == null && _currentFeature != null)
				{
					_currentGeometry = _currentFeature.ShapeCopy;
				}
				return _currentGeometry;
			}
		}

		public ESRI.ArcGIS.Geometry.IGeometry CurrentGeneralizedGeometry
		{
			set { _currentGeneralizedGeometry = value; }
			get
			{
				if (_currentGeneralizedGeometry == null)
				{
					ESRI.ArcGIS.Geometry.IGeometry currentGeometry = this.CurrentGeometry;
					if (CGIS.MapWorks.EsriUtils.GeometryIsValid(currentGeometry) == true)
					{
						if (currentGeometry is ESRI.ArcGIS.Geometry.IArea)
						{
							ESRI.ArcGIS.Geometry.IArea shapeArea = (ESRI.ArcGIS.Geometry.IArea)currentGeometry;
							_currentGeneralizedGeometry = shapeArea.LabelPoint;
						}
						else
						{
							_currentGeneralizedGeometry = this.CurrentGeometry;
						}
					}
				}
				return _currentGeneralizedGeometry;
			}		
		}

		public ESRI.ArcGIS.Geometry.IGeometry SelectedGeneralizedGeometry
		{
			set { _selectedGeneralizedGeometry = value; }
			get 
			{ 
				if (_selectedGeneralizedGeometry == null)
				{
					if (this.Valid == true)
					{
						_selectedGeneralizedGeometry = new ESRI.ArcGIS.Geometry.MultipointClass();
						ESRI.ArcGIS.Geometry.IGeometryCollection geometryCollection = (ESRI.ArcGIS.Geometry.IGeometryCollection)_selectedGeneralizedGeometry;
						ESRI.ArcGIS.Geodatabase.IFeature[] featureArray = this.CurrentSelection();
						System.Object missing = System.Reflection.Missing.Value;
						foreach (ESRI.ArcGIS.Geodatabase.IFeature selectedFeature in featureArray)
						{
							ESRI.ArcGIS.Geometry.IArea featureArea = (ESRI.ArcGIS.Geometry.IArea)selectedFeature.Shape;
							ESRI.ArcGIS.Geometry.IGeometry centroid = featureArea.Centroid;
							geometryCollection.AddGeometry(centroid, ref missing, ref missing);
						}
					}
				}
				return _selectedGeneralizedGeometry;
			}

		}



	}
}

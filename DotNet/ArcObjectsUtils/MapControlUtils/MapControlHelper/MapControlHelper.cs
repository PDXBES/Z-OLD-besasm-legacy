using System;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Utility;
using SystemsAnalysis;

namespace SystemsAnalysis.Utils.ArcObjects.MapControl
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	public class MapControlHelper
	{
		private MapControlHelper()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		public static void ZoomToOIDArray(AxMapControl MapControl, ILayer pLayer, int[] SelectedOIDArray)
		{
			//ESRI.ArcGIS.Carto.IFeatureSelection pFeatSelection;
			//pFeatSelection = (IFeatureSelection)pFeatLayer;
			//pFeatSelection.CombinationMethod = 
			//	esriSelectionResultEnum.esriSelectionResultAdd;
			//int[] selectedEdgeIDArray = mstLinks.GetSelectedEdgeIDArray();			
			IFeatureLayer pFeatLayer;
			pFeatLayer = (IFeatureLayer)pLayer;
			IFeatureClass pFC;
			pFC = pFeatLayer.FeatureClass;

			IFeatureSelection pFeatSelection;
			pFeatSelection = (IFeatureSelection)pFeatLayer;	
			pFeatSelection.Clear();
			if (SelectedOIDArray.Length <= 0) 
			{
				return;
			}			
			try
			{
				pFeatSelection.SelectionSet.AddList(
					SelectedOIDArray.Length, ref SelectedOIDArray[0]);	
			}
			//Sometimes large arrays fail when using SelectionSet.AddList
			//I think this is because the array is not in a single large
			//block of memory, althought stupid ArcObjects expects it to be.
			//However, selection using SelectionSet.Add is much slower, so
			//I only do it this way if AddList has failed.
			catch (Exception ex)
			{
				foreach (int i in SelectedOIDArray)
				{                    
					pFeatSelection.SelectionSet.Add(i);
				}
			}       

			IGeometryFactory pGeomFactory;
			pGeomFactory = new GeometryEnvironmentClass();
			
			IEnumGeometry pEnumGeom;
			IEnumGeometryBind pEnumGeomBind;
			pEnumGeom = new EnumFeatureGeometry();
			pEnumGeomBind = (IEnumGeometryBind)pEnumGeom;
			pEnumGeomBind.BindGeometrySource(null, pFeatSelection.SelectionSet);	

			IGeometry pGeom;
			pGeom = pGeomFactory.CreateGeometryFromEnumerator(pEnumGeom);
            CGIS.MapWorks.EsriUtils.ZoomToGeometry(pGeom, MapControl.Map, 1.2);
			MapControl.ActiveView.Refresh();		
		}


		public static void ClearSelection(AxMapControl MapControl, ILayer pLayer)
		{
			IFeatureLayer pFeatLayer;
			pFeatLayer = (IFeatureLayer)pLayer;			

			IFeatureSelection pFeatSelection;
			pFeatSelection = (IFeatureSelection)pFeatLayer;	
			pFeatSelection.Clear();
			MapControl.ActiveView.Refresh();
		}
	}
}

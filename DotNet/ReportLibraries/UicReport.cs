using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Catalog;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.Utility;

using System;
using System.Collections.Generic;
using System.Text;
using SystemsAnalysis.Modeling;
using System.Collections;
using System.Collections.Specialized;
using SystemsAnalysis.Utils.Events;

namespace SystemsAnalysis.Reporting.ReportLibraries
{
  public class UicReport : ReportBase
  {
    private string mstSurfSC_FC;
    private string uicDB;

    private Links links;
    private Nodes nodes;

    /// <summary>
    /// Creates a UicReport from a collections of Links, Nodes and Dscs
    /// </summary>
    /// <param name="links">A Links collection to report on</param>
    /// <param name="nodes">A Nodes collection to report on</param>
    /// <param name="dscs">A Dscs collection to report on</param>
    public UicReport(Links links, Nodes nodes)
    //: base(links, nodes, dscs)
    {
      this.links = links;
      this.nodes = nodes;
    }

    /*public override string Function(string functionName, IDictionary<string, Parameter> parameters)
    {
    switch (functionName)
    {
    case "UICCount":
    return this.UICCount(parameters);
    case "UICForReviewCount":
    return this.UICForReviewCount(parameters);
    default:
    return "Unknown UIC Function";
    }
    }*/
    /*
    private string UICCount(IDictionary<string, Parameter> parameters)
    {
    //TODO: Move some of this AO code to a utils library...
    //Open a .lyr file as a GxFile, which can QI to GxLayer
    GxLayer sscGxLayer;
    sscGxLayer = new GxLayerClass();
    GxFile sscGxFile;
    sscGxFile = (GxFile)sscGxLayer;
    sscGxFile.Path = this.mstSurfSC_FC;
    //Which can QI to an IFeatureLayer
    IFeatureLayer sscFL;
    sscFL = (IFeatureLayer)sscGxLayer.Layer;
    //Which can QI to an IFeatureSelection, which will allow 
    //for selecting individual objects...
    IFeatureSelection sscFeatureSelection;
    sscFeatureSelection = (IFeatureSelection)sscFL;
    //And IFeatureLayer can also provide an IFeatureCursor for iterating...
    IFeatureCursor sscCursor;
    sscCursor = sscFL.Search(null, false);

    IFeature sscFeature;
    sscFeature = sscCursor.NextFeature();
    while (sscFeature != null)
    {
    string ngto;
    ngto = Convert.ToString(
    sscFeature.get_Value(sscFeature.Fields.FindField("NGTO_EX")));
    if (nodes.Contains(ngto))
    {
    sscFeatureSelection.SelectionSet.Add(sscFeature.OID);
    }
    sscFeature = sscCursor.NextFeature();
    }

    //Gnarly ArcObjects code... the intent is to generate an IEnumGeometry
    //containing all the geometries from the feature selection created above.
    //The IEnumGeometry contains a collection of geometries and can be used 
    //to output a single unified geometry via 
    //GeometryFactory.CreateGeometryFromEnumerator.
    IEnumGeometry enumGeom;
    IEnumGeometryBind enumGeomBind;
    enumGeom = new EnumFeatureGeometryClass();
    enumGeomBind = (IEnumGeometryBind)enumGeom;
    enumGeomBind.BindGeometrySource(null, sscFeatureSelection.SelectionSet);

    IGeometryFactory geomFactory;
    geomFactory = new GeometryEnvironmentClass();

    IGeometry geometryFilter;
    geometryFilter = geomFactory.CreateGeometryFromEnumerator(enumGeom);

    //Now that we have a Geometry representing the selected SSC objects
    //we can generate a spatial filter for use in spatial selection.
    ISpatialFilter sscFilter;
    sscFilter = new SpatialFilterClass();
    sscFilter.Geometry = geometryFilter;
    sscFilter.GeometryField = sscFL.FeatureClass.ShapeFieldName;
    sscFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;

    //Finally, create the UIC layer...
    GxLayer uicGxLayer;
    uicGxLayer = new GxLayerClass();
    GxFile uicGxFile;
    uicGxFile = (GxFile)uicGxLayer;
    uicGxFile.Path = this.uicDB;

    IFeatureLayer uicFL;
    uicFL = (IFeatureLayer)uicGxLayer.Layer;
    //.. and define an IFeatureSelection
    IFeatureSelection uicFeatureSelection;
    uicFeatureSelection = (IFeatureSelection)uicFL;
    //...and select from it using the spatial filter
    uicFeatureSelection.SelectFeatures(sscFilter,
    esriSelectionResultEnum.esriSelectionResultNew, false);
    //...and return the count of the UIC selection.
    return Convert.ToString(uicFeatureSelection.SelectionSet.Count);
    //TODO: Take some tylenol and put this in a library.

    }
    private string UICForReviewCount(IDictionary<string, Parameter> parameters)
    {

    //Open a .lyr file as a GxFile, which can QI to GxLayer
    GxLayer sscGxLayer;
    sscGxLayer = new GxLayerClass();
    GxFile sscGxFile;
    sscGxFile = (GxFile)sscGxLayer;
    sscGxFile.Path = this.mstSurfSC_FC;
    //Which can QI to an IFeatureLayer
    IFeatureLayer sscFL;
    sscFL = (IFeatureLayer)sscGxLayer.Layer;
    //Which can QI to an IFeatureSelection, which will allow 
    //for selecting individual objects...
    IFeatureSelection sscFeatureSelection;
    sscFeatureSelection = (IFeatureSelection)sscFL;
    //And IFeatureLayer can also provide an IFeatureCursor for iterating...
    IFeatureCursor sscCursor;
    sscCursor = sscFL.Search(null, false);

    IFeature sscFeature;
    sscFeature = sscCursor.NextFeature();
    while (sscFeature != null)
    {
    string ngto;
    ngto = Convert.ToString(
    sscFeature.get_Value(sscFeature.Fields.FindField("NGTO_EX")));
    if (nodes.Contains(ngto))
    {
    sscFeatureSelection.SelectionSet.Add(sscFeature.OID);
    }
    sscFeature = sscCursor.NextFeature();
    }

    //Gnarly ArcObjects code... the intent is to generate an IEnumGeometry
    //containing all the geometries from the feature selection created above.
    //The IEnumGeometry contains a collection of geometries and can be used 
    //to output a single unified geometry via 
    //GeometryFactory.CreateGeometryFromEnumerator.
    IEnumGeometry enumGeom;
    IEnumGeometryBind enumGeomBind;
    enumGeom = new EnumFeatureGeometryClass();
    enumGeomBind = (IEnumGeometryBind)enumGeom;
    enumGeomBind.BindGeometrySource(null, sscFeatureSelection.SelectionSet);

    IGeometryFactory geomFactory;
    geomFactory = new GeometryEnvironmentClass();

    IGeometry geometryFilter;
    geometryFilter = geomFactory.CreateGeometryFromEnumerator(enumGeom);

    //Now that we have a Geometry representing the selected SSC objects
    //we can generate a spatial filter for use in spatial selection.
    ISpatialFilter sscFilter;
    sscFilter = new SpatialFilterClass();
    sscFilter.Geometry = geometryFilter;
    sscFilter.GeometryField = sscFL.FeatureClass.ShapeFieldName;
    sscFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;

    //Finally, create the UIC layer...
    GxLayer uicGxLayer;
    uicGxLayer = new GxLayerClass();
    GxFile uicGxFile;
    uicGxFile = (GxFile)uicGxLayer;
    uicGxFile.Path = this.uicDB;

    IFeatureLayer uicFL;
    uicFL = (IFeatureLayer)uicGxLayer.Layer;
    //.. and define an IFeatureSelection
    IFeatureSelection uicFeatureSelection;
    uicFeatureSelection = (IFeatureSelection)uicFL;

    //...and select from it using the spatial filter
    uicFeatureSelection.SelectFeatures(sscFilter,
    esriSelectionResultEnum.esriSelectionResultNew, false);

    int count = 0;
    //Iterate through the UIC selection to find UICs matching the review criteria.
    ICursor uicCursor;
    uicFeatureSelection.SelectionSet.Search(sscFilter, false, out uicCursor);
    IRow uicRow;
    uicRow = uicCursor.NextRow();
    while (uicRow != null)
    {
    double gw_sepdist;
    double nr_wellmin;
    double in_tot;
    gw_sepdist = Convert.ToDouble(uicRow.get_Value(uicRow.Fields.FindField("GW_SEPDIST")));
    nr_wellmin = Convert.ToDouble(uicRow.get_Value(uicRow.Fields.FindField("nr_wellmin")));
    in_tot = Convert.ToDouble(uicRow.get_Value(uicRow.Fields.FindField("in_tot")));
    if (nr_wellmin <= 500 || in_tot == 1 || gw_sepdist <= 10)
    {
    count++;
    }
    uicRow = uicCursor.NextRow();
    }

    bool isFraction = parameters["IsFraction"].ValueAsBool;
    return isFraction ? Convert.ToString(
    Math.Round(count /
    (uicFeatureSelection.SelectionSet.Count + System.Double.Epsilon),
    2) * 100) + "%"
    : Convert.ToString(count);


    }*/
  }
}

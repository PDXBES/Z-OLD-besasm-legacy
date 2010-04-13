
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;

namespace BESGISUtils
{
    
    public static class MakeCircleAtPoint
    {
        public static IGeometry GetCircularGeometry(IPoint point, double radius)
        {
            IConstructCircularArc circularArc;
            circularArc = new CircularArcClass();
            circularArc.ConstructCircle(point, radius, true);

            IPolygon polygon;
            polygon = new PolygonClass();
            polygon.SpatialReference = point.SpatialReference;

            ISegmentCollection segmentCollection;
            segmentCollection = (ISegmentCollection)polygon;
            object missing = Type.Missing;
            segmentCollection.AddSegment((ISegment)circularArc, ref missing, ref missing);

            return (IGeometry)polygon;
        }
    }
}

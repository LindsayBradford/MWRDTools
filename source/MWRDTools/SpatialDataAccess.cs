using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

public class SpatialDataAccess
{
    public delegate void ProgressEventHandler(object sender, ProgressEventArgs e);

    public event ProgressEventHandler ProgressEvent;

    public IFeatureCursor GetSpeciesByWetland(IFeatureWorkspace pFeatureWorkspace, IFeatureLayer pFeatureLayer, string wetlandID, 
                                              double buffer, DateTime? afterDate, DateTime? beforeDate, string speciesFilter)
    {
        IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
        IQueryFilter pQueryFilter = new QueryFilterClass();
        pQueryFilter.WhereClause = string.Format("MBCMA_wetland = {0}", wetlandID);
        IFeatureCursor pFeatureCursor = pFeatureClass.Search(pQueryFilter, false);
        IFeature pFeature = pFeatureCursor.NextFeature();
        ITopologicalOperator pTopo = (ITopologicalOperator)pFeature.Shape;
        IGeometry pGeometry = pTopo.Buffer(buffer);
        if (pFeature != null)
        {
            IFeatureClass speciesFC = pFeatureWorkspace.OpenFeatureClass(Constants.LayerName.ThreatenedSpecies);
            ISpatialFilter pSpatialFilter = new SpatialFilterClass();
            pSpatialFilter.GeometryField = speciesFC.ShapeFieldName;
            pSpatialFilter.Geometry = pGeometry;
            pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelContains;
            StringBuilder sb = new StringBuilder();

            sb.Append(speciesFilter);
                
            if (afterDate.HasValue)
            {
                if (sb.Length > 0)
                {
                    sb.Append(" AND ");
                }
                sb.Append('"');
                sb.Append("DateFirst");
                sb.Append('"');
                sb.Append("> date '");
                sb.Append(Common.DateFormat(afterDate.Value));
                sb.Append("'");
            }
            if (beforeDate.HasValue)
            {
                if(sb.Length > 0)
                {
                    sb.Append(" AND ");
                }
                sb.Append('"');
                sb.Append("DateLast");
                sb.Append('"');
                sb.Append("< date '");
                sb.Append(Common.DateFormat(beforeDate.Value));
                sb.Append("'");
            }
            pSpatialFilter.WhereClause = sb.ToString();
            IFeatureCursor speciesCursor = speciesFC.Search(pSpatialFilter, false);
            return speciesCursor;
        }
        return null;
    }
}

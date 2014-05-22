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


    public ArrayList GetWetlandsBySpecies(IFeatureWorkspace pFeatureWorkspace, IFeatureLayer wetlandsFL, 
                                          string scientificName, double buffer, DateTime? afterDate, DateTime? beforeDate) {
        ProgressEventArgs pe = new ProgressEventArgs(ProgressEventEnums.eProgress.start, 0);
            
        ArrayList result = new ArrayList();
        ArrayList species = new ArrayList();

        ISpatialFilter pSpatialFilter = new SpatialFilterClass();
        IFeatureClass wetlandsFC = wetlandsFL.FeatureClass;
        pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;
        pSpatialFilter.GeometryField = wetlandsFC.ShapeFieldName;

        IFeatureCursor pWetlandsCursor;
        IFeature pWetlandsFeature;
        int steps = 0;
        int step = 0;
        IQueryFilter pQueryFilter = new QueryFilterClass();

        StringBuilder sb = new StringBuilder();
        sb.Append("ScientificName = '");
        sb.Append(scientificName);
        sb.Append("'");

        if (afterDate.HasValue)
        {
            sb.Append(" AND ");
            sb.Append('"');
            sb.Append("DateFirst");
            sb.Append('"');
            sb.Append("> date '");
            sb.Append(DateFormat(afterDate.Value));
            sb.Append("'");
        }

        if (beforeDate.HasValue)
        {
            sb.Append(" AND ");
            sb.Append('"');
            sb.Append("DateLast");
            sb.Append('"');
            sb.Append("< date '");
            sb.Append(DateFormat(beforeDate.Value));
            sb.Append("'");
        }

        pQueryFilter.WhereClause = sb.ToString();
        IFeatureClass speciesFC = pFeatureWorkspace.OpenFeatureClass(Constants.LayerName.ThreatenedSpecies);
        IFeatureCursor pFeatureCursor = speciesFC.Search(pQueryFilter, false);
        IFeature pFeature = pFeatureCursor.NextFeature();
        while (pFeature != null)
        {
            steps++;
            species.Add(pFeature);
            pFeature = pFeatureCursor.NextFeature();
        }
        pe.Step = steps;
        ProgressEvent(this, pe);
        pe.Activity = ProgressEventEnums.eProgress.update;
        ITopologicalOperator pTopo;
            
        for(int i = 0; i < species.Count; i++)
        {
            pFeature = (IFeature)species[i];
            pTopo = (ITopologicalOperator)pFeature.Shape;
            pTopo.Simplify();
            pSpatialFilter.Geometry = pTopo.Buffer(buffer);
            pWetlandsCursor = wetlandsFC.Search(pSpatialFilter, false);
            pWetlandsFeature = pWetlandsCursor.NextFeature();
            while (pWetlandsFeature != null)
            {
                AddFeature(pWetlandsFeature, result);
                pWetlandsFeature = pWetlandsCursor.NextFeature();
            }
            step++;
            pe.Step = step;
            ProgressEvent(this, pe);
        }
        pe.Activity = ProgressEventEnums.eProgress.finish;
        ProgressEvent(this, pe);
        return result;
    }

    private static void AddFeature(IFeature pNewFeature, ArrayList result)
    {
        IFeature pFeature;
        for (int i = 0; i < result.Count; i++)
        {
            pFeature = (IFeature)result[i];
            if (pFeature.OID == pNewFeature.OID)
            {
                return;
            }
        }
        result.Add(pNewFeature);
    }

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
                sb.Append(DateFormat(afterDate.Value));
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
                sb.Append(DateFormat(beforeDate.Value));
                sb.Append("'");
            }
            pSpatialFilter.WhereClause = sb.ToString();
            IFeatureCursor speciesCursor = speciesFC.Search(pSpatialFilter, false);
            return speciesCursor;
        }
        return null;
    }

    public static string DateFormat(DateTime val) {
      return string.Format(
        "{0} 00:00:00", val.ToString("yyyy-MM-dd")
      );
    }
}

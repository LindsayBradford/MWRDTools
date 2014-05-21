using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

public class DataAccess
{

    public static ICursor GetSpeciesNames(IFeatureWorkspace pFeatureWorkspace)
    {
        ITable pTable = pFeatureWorkspace.OpenTable(Constants.TableName.ThreatenedSpeciesUnique);
        ICursor pCursor = pTable.Search(null, false);
        return pCursor;
    }
    public static ICursor GetSpeciesNames(IFeatureWorkspace pFeatureWorkspace, string whereClause)
    {
        ITable pTable = pFeatureWorkspace.OpenTable(Constants.TableName.ThreatenedSpeciesUnique);
        IQueryFilter pQueryFilter = new QueryFilterClass();
        pQueryFilter.WhereClause = whereClause;
        ICursor pCursor = pTable.Search(pQueryFilter, false);
        return pCursor;
    }

    public static IEnumerator GetClassNames(IFeatureWorkspace pFeatureWorkspace)
    {
        //ITable pTable = pFeatureWorkspace.OpenTable("ThreatenedSpeciesUnique");
        //IDataset pDataset = (IDataset)pTable;
        //IQueryDef pQueryDef = pFeatureWorkspace.CreateQueryDef();
        //pQueryDef.Tables = pDataset.Name;
        //pQueryDef.SubFields = "distinct (First_CLASS_NAME)";
        //pQueryDef.WhereClause = "";
        //ICursor pCursor = pQueryDef.Evaluate();
        //return pCursor;

      ITable pTable = pFeatureWorkspace.OpenTable(Constants.TableName.ThreatenedSpeciesUnique);
        ICursor pCursor = pTable.Search(null, false); 
        IDataStatistics pDataStatistics = new DataStatisticsClass();
        pDataStatistics.Field = "ClassName";
        pDataStatistics.Cursor = pCursor;
        IEnumerator pEnumerator = pDataStatistics.UniqueValues;
        return pEnumerator;
    }

    public static IFeatureCursor GetWetlandsBySpecies(IFeatureWorkspace pFeatureWorkspace, IFeatureLayer wetlandsFL, string scientificName, double buffer)
    {
        IQueryFilter pQueryFilter = new QueryFilterClass();
        pQueryFilter.WhereClause = string.Format("ScientificName ='{0}'", scientificName);
        IFeatureClass speciesFC = pFeatureWorkspace.OpenFeatureClass(Constants.LayerName.ThreatenedSpecies);
        IFeatureCursor pFeatureCursor = speciesFC.Search(pQueryFilter, false);
        IFeature pFeature = pFeatureCursor.NextFeature();
            
        ITopologicalOperator pTopo;
        // IPolygon pSearchArea = null;
        IPolygon pBufferedPoint;
        IGeometryCollection pGeometryBag = new GeometryBagClass();
        object missing = Type.Missing;
        while (pFeature != null)
        {
            pTopo = (ITopologicalOperator)pFeature.Shape;
            pTopo.Simplify();
            pBufferedPoint = pTopo.Buffer(buffer) as IPolygon;
            pGeometryBag.AddGeometry(pBufferedPoint, ref missing, ref missing);
            pFeature = pFeatureCursor.NextFeature();
        }

        //pTopo = new PolygonClass();
        //pTopo.ConstructUnion(pGeometryBag as IEnumGeometry);
        //pSearchArea = pTopo as IPolygon;

        IFeatureClass wetlandsFC = wetlandsFL.FeatureClass;
        ISpatialFilter pSpatialFilter = new SpatialFilterClass();
        pSpatialFilter.GeometryField = wetlandsFC.ShapeFieldName;
        pSpatialFilter.Geometry = pGeometryBag as IPolygon;
        pSpatialFilter.SpatialRel = esriSpatialRelEnum.esriSpatialRelIntersects;

        pFeatureCursor = wetlandsFC.Search(pSpatialFilter, false);
        return pFeatureCursor;
    }

}


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

    public static ICursor GetSpeciesNames(IFeatureWorkspace pFeatureWorkspace, string whereClause)
    {
        ITable pTable = pFeatureWorkspace.OpenTable(Constants.TableName.ThreatenedSpeciesUnique);
        IQueryFilter pQueryFilter = new QueryFilterClass();
        pQueryFilter.WhereClause = whereClause;
        ICursor pCursor = pTable.Search(pQueryFilter, false);
        return pCursor;
    }
}


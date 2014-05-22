using System;
using System.Data;
using System.ComponentModel;
using System.Collections.Generic;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace MWRDTools.Model {
  public interface IGeodatabaseBridge {

    event EventHandler<ProgressChangedEventArgs> StatusChanged;

    void BeginTransaction();
    void EndTransaction();

    int GetIndexForTableColumnName(
      string tableName,
      string columnName
    );

    ICursor GetCursorForQuery(
      string tableNames,
      string whereClause,
      string fields
    );

    ICursor GetCursorForQuery(
      string tableNames,
      string whereClause
    );

    T GetFirstColValueForQuery<T>(
      string tableName,
      string whereClause,
      string columnName
    );

    List<T> GetColValuesForQuery<T>(
      string tableName,
      string whereClause,
      string columnName
    );

    List<T> GetUniqueColValuesForQuery<T>(
      string tableName,
      string whereClause,
      string columnName
    );

    T GetValueForRowColumnName<T>(
      IRow row, 
      string columnName
    );

    void DeleteTableContent(string tableName);
    void DeleteTableContent(string tableName, string whereClause);
    
    void WriteDataTable(
      string tableName, 
      DataTable content
    );

    IFeatureCursor GetCursorForFeatureClassQuery(
      string featureClassName, 
      string whereClause, 
      string subFields
    );

    IFeatureCursor GetIntersectionCursor(
      string featureClassName, 
      IFeature feature, 
      double buffer
    );

    void WriteDataTableAsPoints(
      string tableName, 
      DataTable content, 
      int latitudeColIndex, 
      int longitudeColIndex)
    ;
  }
}

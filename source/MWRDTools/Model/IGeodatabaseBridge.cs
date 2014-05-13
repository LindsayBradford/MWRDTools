using System;
using System.Data;
using System.ComponentModel;

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

    ICursor GetCursorForTableQuery(
      string tableName, 
      string whereClause
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

    void WriteDataTableAsPoints(
      string tableName, 
      DataTable content, 
      int latitudeColIndex, 
      int longitudeColIndex)
    ;
  }
}

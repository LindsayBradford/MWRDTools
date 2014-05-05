using System;
using System.Data;

using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace MWRDTools.Model {
  public interface IGeodatabaseBridge {

    event EventHandler<ModelStatusEventArgs> StatusChanged;

    void BeginTransaction();
    void EndTransaction();

    void DeleteTableContent(string tableName);
    void WriteDataTable(string tableName, DataTable content);

    void WriteDataTableAsPoints(
      string tableName, 
      DataTable content, 
      int latitudeColIndex, 
      int longitudeColIndex)
    ;
  }
}

using System;
using System.IO;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;

using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;

namespace MWRDTools.Model {
  class FileGeodatabaseBridge : IGeodatabaseBridge {

    public event EventHandler<ModelStatusEventArgs> StatusChanged;

    private const int STATUS_UPDATE_FREQUENCY = 500;

    // Efficiency matters per type of database.  As this a file-geodatabase bridge,
    // the methods for DB interaction have been optimised for file geodatabses. See:
    // http://help.arcgis.com/en/sdk/10.0/arcobjects_net/conceptualhelp/index.html#//0001000002rs000000

    private IWorkspace workspace;
    private string databasePath;

    private static FileGeodatabaseBridge instance;

    protected FileGeodatabaseBridge() {}
    
    public static FileGeodatabaseBridge getInstance() {
      if (instance == null) {
        instance = new FileGeodatabaseBridge();
      }
      return instance;
    }

    public string DatabasePath {
      get { return this.databasePath; }
      set { this.databasePath = value; deriveWorkspace(); }
    }

    private void deriveWorkspace() {
      Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory");
      IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);

      workspace = workspaceFactory.OpenFromFile(databasePath, 0);
    }

    private IWorkspace Workspace {
      get { return this.workspace; }
    }

    public void BeginTransaction() {
      IWorkspaceEdit edit = (Workspace as IWorkspaceEdit);
      edit.StartEditing(false);
      edit.StartEditOperation();
    }

    public void EndTransaction() {
      IWorkspaceEdit edit = (Workspace as IWorkspaceEdit);
      edit.StopEditOperation();
      edit.StopEditing(true);
    }

    public void DeleteTableContent(string tableName) {
      ITable table = (Workspace as IFeatureWorkspace).OpenTable(tableName);
      table.DeleteSearchedRows(null);
    }

    public void WriteDataTable(string tableName, DataTable dataTable) {

      ITable esriTable = (Workspace as IFeatureWorkspace).OpenTable(tableName);
      IFeatureClassLoad esriTableLoad = esriTable as IFeatureClassLoad;

      try {
        esriTableLoad.LoadOnlyMode = true;

        using (ComReleaser comReleaser = new ComReleaser()) {

          ICursor insertCursor = esriTable.Insert(true);
          comReleaser.ManageLifetime(insertCursor);

          IRowBuffer insertBuffer = esriTable.CreateRowBuffer();
          comReleaser.ManageLifetime(insertBuffer);

          for (int rowNum = 0; rowNum < dataTable.Rows.Count; rowNum++) {

            if (rowNum % STATUS_UPDATE_FREQUENCY == 0 || rowNum == dataTable.Rows.Count - 1) {
              raiseStatusEvent(
                String.Format(
                  "Buffering \"{0}\" write ({1}/{2}) .",
                  tableName, rowNum, dataTable.Rows.Count - 1
                )
              );
            }

            (insertBuffer as IRowSubtypes).InitDefaultValues();

            for (int colNum = 0; colNum < dataTable.Columns.Count; colNum++) {
              int fieldIndex = esriTable.FindField(dataTable.Columns[colNum].Caption);

              if (fieldIndex == -1) continue;

              string value = dataTable.Rows[rowNum][colNum] as string;
              if (value != null && !value.Trim().Equals("")) {

                IField field = insertBuffer.Fields.get_Field(fieldIndex);

                if (field.Type == esriFieldType.esriFieldTypeString) {

                  // string lengths that exceed the ggedatabase field length
                  // cause exceptions when we set the value. Need to trim 
                  // the over-long strings back. 

                  string rawValue = dataTable.Rows[rowNum][colNum] as string;
                  string trimmedValue;

                  if (rawValue.Length > field.Length) {
                    trimmedValue = rawValue.Substring(0, field.Length);
                  } else {
                    trimmedValue = rawValue;
                  }

                  insertBuffer.set_Value(
                    fieldIndex, trimmedValue
                  );

                } else { // other data-types are much easier to manage.
                  insertBuffer.set_Value(
                    fieldIndex, dataTable.Rows[rowNum][colNum]
                  );
                }
              }
            } // columns
            insertCursor.InsertRow(insertBuffer);
          }  // rows

          insertCursor.Flush();
        } // using ComReleaser()
      } catch (Exception e) {
        MessageBox.Show(e.Message);
      } finally {
        esriTableLoad.LoadOnlyMode = false;
      }
    }

    public void WriteDataTableAsPoints(string tableName, DataTable dataTable, int latitudeColIndex, int longitudeColIndex) {
      IFeatureClass esriTable = (Workspace as IFeatureWorkspace).OpenFeatureClass(tableName);
      IFeatureClassLoad esriTableLoad = esriTable as IFeatureClassLoad;
      
      ISpatialReference sp = (esriTable as IGeoDataset).SpatialReference;

      try {
        esriTableLoad.LoadOnlyMode = true;

        using (ComReleaser comReleaser = new ComReleaser()) {

          IFeatureCursor insertCursor = esriTable.Insert(true);
          comReleaser.ManageLifetime(insertCursor);

          IFeatureBuffer insertBuffer = esriTable.CreateFeatureBuffer();
          comReleaser.ManageLifetime(insertBuffer);

          for (int rowNum = 0; rowNum < dataTable.Rows.Count; rowNum++) {

            if (rowNum % STATUS_UPDATE_FREQUENCY == 0 || rowNum == dataTable.Rows.Count - 1) {
              raiseStatusEvent(
                String.Format(
                  "Buffering \"{0}\" write ({1}/{2}) .",
                  tableName, rowNum, dataTable.Rows.Count - 1
                )
              );
            }

            (insertBuffer as IRowSubtypes).InitDefaultValues();

            IPoint point = buildPointFromDataRow(
              sp,
              dataTable.Rows[rowNum],
              latitudeColIndex,
              longitudeColIndex
            );

            insertBuffer.Shape = point;

            for (int colNum = 0; colNum < dataTable.Columns.Count; colNum++) {

              int fieldIndex = esriTable.FindField(dataTable.Columns[colNum].Caption);

              if (fieldIndex == -1) continue;

              string value = dataTable.Rows[rowNum][colNum] as string;
              if (value != null && !value.Trim().Equals("")) {

                IField field = insertBuffer.Fields.get_Field(fieldIndex);

                if (field.Type == esriFieldType.esriFieldTypeString) {

                  // string lengths that exceed the ggedatabase field length
                  // cause exceptions when we set the value. Need to trim 
                  // the over-long strings back. 

                  string rawValue = dataTable.Rows[rowNum][colNum] as string;
                  string trimmedValue;

                  if (rawValue.Length > field.Length) {
                    trimmedValue = rawValue.Substring(0, field.Length);
                  } else {
                    trimmedValue = rawValue;
                  }

                  insertBuffer.set_Value(
                    fieldIndex, trimmedValue
                  );

                } else { // other data-types are much easier to manage.
                  insertBuffer.set_Value(
                    fieldIndex, dataTable.Rows[rowNum][colNum]
                  );
                }
              }
            } // columns
            insertCursor.InsertFeature(insertBuffer);
          } // rows 
          insertCursor.Flush();
        } // using ComReleaser()
      } catch (Exception e) {
        MessageBox.Show(e.Message);
      } finally {
        esriTableLoad.LoadOnlyMode = false;
      }
    }

    private IPoint buildPointFromDataRow(ISpatialReference spatialReference, DataRow row, 
                                         int latitudeColIndex, int longitudeColIndex) {
      IPoint point = new PointClass();
      point.SpatialReference = spatialReference;

      point.PutCoords(
        Double.Parse(
         (String)row[latitudeColIndex]
        ),
        Double.Parse(
          (String)row[longitudeColIndex]
        )
      );

      return point;
    }

    private void raiseStatusEvent(string status) {

      ModelStatusEventArgs statusArgs = new ModelStatusEventArgs();
      statusArgs.Status = status;

      if (StatusChanged != null) {
        StatusChanged(this, statusArgs);
      }
    }

  }
}

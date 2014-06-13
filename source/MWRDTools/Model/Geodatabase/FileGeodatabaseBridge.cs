using System;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Data;

using ESRI.ArcGIS.ADF;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using ESRI.ArcGIS.esriSystem;

namespace MWRDTools.Model {
  public class FileGeodatabaseBridge : AbstractGeodatabaseBridge, IGeodatabaseBridge {

    private string databasePath;

    public string DatabasePath {
      get { return this.databasePath; }
      set { this.databasePath = value; deriveWorkspace(); }
    }

    public override GeodatabaseBridgeType GetBridgeType() {
      return GeodatabaseBridgeType.FileGeodatabase;
    }

    public override ICursor GetCursorForSQLQuery(string query) {
      return null;  //  not something a file-geodatabase can do. 
    }

    private void deriveWorkspace() {
      Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.FileGDBWorkspaceFactory");
      IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);

      workspace = workspaceFactory.OpenFromFile(databasePath, 0);
    }

    public override void WriteDataTable(string tableName, DataTable dataTable) {
      ITable esriTable = getTable(tableName);
      IFeatureClassLoad esriTableLoad = esriTable as IFeatureClassLoad;

      try {
        base.WriteDataTable(tableName, dataTable);
      } catch (Exception e) {
        throw e;
      } finally {
        esriTableLoad.LoadOnlyMode = false;
      }

    }

    public override void WriteDataTableAsPoints(string tableName, DataTable dataTable, int latitudeColIndex, int longitudeColIndex) {
      IFeatureClass esriTable = (Workspace as IFeatureWorkspace).OpenFeatureClass(tableName);
      IFeatureClassLoad esriTableLoad = esriTable as IFeatureClassLoad;

      try {
        base.WriteDataTableAsPoints(tableName, dataTable, latitudeColIndex, longitudeColIndex);
      } catch (Exception e) {
        throw e;
      } finally {
        esriTableLoad.LoadOnlyMode = false;
      }

    }


  }
}

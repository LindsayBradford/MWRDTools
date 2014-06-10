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
  public class ArcSDEGeodatabaseBridge : AbstractGeodatabaseBridge, IGeodatabaseBridge {

    private const string SERVER = "PC024953_SQLEXPRESS";
    private const string SDE_INSTANCE = "sde:sqlserver:PC024953\\sqlexpress";
    private const string DB_NAME = "MWRD";
    private const string AUTH_MODE = "OSA";
    private const string VERSION = "dbo.DEFAULT";

    private const string DB_CLIENT="SQLServer";
    private const string SQL_SERVER_INSTANCE="PC024953\\SqlExpress";

    private ISqlWorkspace sqlWorkspace;

    // Server = "PC024953_SQLEXPRESS".
    //   <HostName>_SQLEXPRESS

    public void EstablishConnection() {
      this.workspace = EstablishSDEConnection();
      this.sqlWorkspace = EstablishSQLConnection();
    }
    
    private IWorkspace EstablishSDEConnection() {
      IPropertySet propertySet = new PropertySetClass();

      propertySet.SetProperty("SERVER", SERVER);
      propertySet.SetProperty("INSTANCE", SDE_INSTANCE);
      propertySet.SetProperty("DATABASE", DB_NAME);
      propertySet.SetProperty("AUTHENTICATION_MODE", AUTH_MODE);
      propertySet.SetProperty("VERSION", VERSION);

      Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.SdeWorkspaceFactory");
      IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);

      return workspaceFactory.Open(propertySet, 0);
    }

    private ISqlWorkspace EstablishSQLConnection() {
      IPropertySet propertySet = new PropertySetClass();

      propertySet.SetProperty("dbclient", DB_CLIENT);
      propertySet.SetProperty("serverinstance", SQL_SERVER_INSTANCE);
      propertySet.SetProperty("DATABASE", DB_NAME);
      propertySet.SetProperty("authentication_mode", AUTH_MODE);

      Type factoryType = Type.GetTypeFromProgID("esriDataSourcesGDB.SqlWorkspaceFactory");
      IWorkspaceFactory workspaceFactory = (IWorkspaceFactory)Activator.CreateInstance(factoryType);

      return workspaceFactory.Open(propertySet, 0) as ISqlWorkspace;
    }


    public override ICursor GetCursorForSQLQuery(string query) {
      return sqlWorkspace.OpenQueryCursor(query);
    }
  }
}

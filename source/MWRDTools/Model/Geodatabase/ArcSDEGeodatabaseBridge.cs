/*
  Copyright (c) 2014, Riverina Local Land Services.

  This program and the accompanying materials are made available under the terms of the
  BSD 3-Clause licence which accompanies this distribution, and is available at
  http://opensource.org/licenses/BSD-3-Clause
 */

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

    private const string SDE_INSTANCE_TEMPLATE = "sde:sqlserver:{0}\\{1}";

    private const string DB_NAME = "MWRD";
    private const string AUTH_MODE = "OSA";
    private const string VERSION = "dbo.DEFAULT";

    private const string DB_CLIENT="SQLServer";
    private const string SQL_SERVER_INSTANCE_TEMPLATE="{0}\\{1}";

    private ISqlWorkspace sqlWorkspace;

    private string databaseServer;
    private string databaseHost;
    private string databaseType;

    public override GeodatabaseBridgeType GetBridgeType() {
      return GeodatabaseBridgeType.ArcSDPPersonalServer;
    }

    public void EstablishConnection(string databaseServer) {
      setDatabaseServer(databaseServer);
      EstablishConnection();
    }

    public string GetDatabaseServer() {
      return this.databaseServer;
    }

    private void setDatabaseServer(string databaseServer) {
      this.databaseServer = databaseServer;

      string[] content = databaseServer.Split('_');

      if (content.Length != 2) {
        throw new ArgumentException(
          String.Format("Database Server ({0}) is invalid.", databaseServer)  
        );
      }

      this.databaseHost = content[0];
      this.databaseType = content[1];
    }

    private void EstablishConnection() {
      this.workspace = EstablishSDEConnection();
      this.sqlWorkspace = EstablishSQLConnection();
    }

    private IWorkspace EstablishSDEConnection() {
      IPropertySet propertySet = new PropertySetClass();

      propertySet.SetProperty("SERVER", databaseServer);
      propertySet.SetProperty(
        "INSTANCE", 
        String.Format(
          SDE_INSTANCE_TEMPLATE,
          this.databaseHost, 
          this.databaseType
        )
      );

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
      propertySet.SetProperty(
        "serverinstance", 
        String.Format(
          SQL_SERVER_INSTANCE_TEMPLATE,
          this.databaseHost,
          this.databaseType
        )
      );

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

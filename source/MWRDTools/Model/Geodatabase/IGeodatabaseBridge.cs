﻿/*
  Copyright (c) 2014, Riverina Local Land Services.

  This program and the accompanying materials are made available under the terms of the
  BSD 3-Clause licence which accompanies this distribution, and is available at
  http://opensource.org/licenses/BSD-3-Clause
 */

using System;
using System.Data;
using System.ComponentModel;
using System.Collections.Generic;

using ESRI.ArcGIS.Geodatabase;

namespace MWRDTools.Model {

  public enum GeodatabaseBridgeType {
    FileGeodatabase,
    ArcSDPPersonalServer
  }

  public interface IGeodatabaseBridge {

    event EventHandler<ProgressChangedEventArgs> StatusChanged;

    void BeginTransaction();
    void EndTransaction();

    GeodatabaseBridgeType GetBridgeType();

    ICursor GetCursorForSQLQuery(string query);

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
      string whereClause,
      IFeature feature, 
      double buffer
    );

    IFeatureCursor GetContainsCursor(
      string featureClassName,
      string whereClause,
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

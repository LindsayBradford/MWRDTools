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
  public abstract class AbstractGeodatabaseBridge : IGeodatabaseBridge {

    public event EventHandler<ProgressChangedEventArgs> StatusChanged;

    protected const int STATUS_UPDATE_FREQUENCY = 1000;

    // Efficiency matters per type of database.  As this a file-geodatabase bridge,
    // the methods for DB interaction have been optimised for file geodatabses. See:
    // http://help.arcgis.com/en/sdk/10.0/arcobjects_net/conceptualhelp/index.html#//0001000002rs000000

    protected IWorkspace workspace;

    protected IWorkspace Workspace {
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

    public abstract GeodatabaseBridgeType GetBridgeType();

    public abstract ICursor GetCursorForSQLQuery(string query);

    #region ESRI Table Processing

    protected ITable getTable(string tableName) {
      return (Workspace as IFeatureWorkspace).OpenTable(tableName);
    }

    public T GetFirstColValueForQuery<T>(string tableName, string whereClause, string columnName) {
      T value = default(T);
      using (ComReleaser comReleaser = new ComReleaser()) {
        ICursor cursor = GetCursorForQuery(tableName, whereClause);
        comReleaser.ManageLifetime(cursor);

        IRow firstRow = cursor.NextRow();
        if (firstRow != null) {
          value = GetValueForRowColumnName<T>(firstRow, columnName);
        }
      }
      return value;
    }

    public List<T> GetUniqueColValuesForQuery<T>(string tableName, string whereClause, string columnName) {
      List<T> valueList = new List<T>();

      using (ComReleaser comReleaser = new ComReleaser()) {
        ICursor cursor = GetCursorForQuery(tableName, whereClause);
        comReleaser.ManageLifetime(cursor);

        IDataStatistics stats = new DataStatisticsClass();

        stats.Field = columnName;
        stats.Cursor = cursor;
        IEnumerator enumerator = stats.UniqueValues;

        enumerator.Reset();
        T value;
        while (enumerator.MoveNext()) {
          value = (T)enumerator.Current;
          valueList.Add(
             value
          );
        }
      }

      return valueList;
    }

    public List<T> GetColValuesForQuery<T>(string tableName, string whereClause, string columnName) {
      List<T> valueList = new List<T>();

      using (ComReleaser comReleaser = new ComReleaser()) {
        ICursor cursor = GetCursorForQuery(tableName, whereClause);
        comReleaser.ManageLifetime(cursor);

        IRow currentRow;
        while ((currentRow = cursor.NextRow()) != null) {
          T rowValue = GetValueForRowColumnName<T>(currentRow, columnName);
          valueList.Add(rowValue);
        }
      }

      return valueList;
    }

    public T GetValueForRowColumnName<T>(IRow row, string columnName) {
      int index = row.Table.FindField(columnName);
      return (T)row.get_Value(index);
    }

    public int GetIndexForTableColumnName(string tableName, string columnName) {
      return getTable(tableName).FindField(columnName);
    }

    public ICursor GetCursorForQuery(string tableList, string whereClause) {
      return GetCursorForQuery(tableList, whereClause, "*");
    }

    public ICursor GetCursorForQuery(string tableList, string whereClause, string subFields) {
      if (tableList.Contains(",")) {
        return GetCursorForMultiTableQuery(
          tableList,
          whereClause,
          subFields
        );
      } else {
        return GetCursorForTableQuery(
          getTable(tableList),
          whereClause,
          subFields
        );
      }
    }

    public ICursor GetCursorForMultiTableQuery(string tableList, string whereClause, string subFields) {
      IQueryDef query = (Workspace as IFeatureWorkspace).CreateQueryDef();
      query.Tables = tableList;
      query.SubFields = subFields;
      query.WhereClause = whereClause;

      return query.Evaluate();
    }

    public ICursor GetCursorForTableQuery(string tableName, string whereClause, string subFields) {
      return GetCursorForTableQuery(
        getTable(tableName),
        whereClause,
        subFields
      );
    }

    protected ICursor GetCursorForTableQuery(ITable table, string whereClause, string subFields) {
      IQueryFilter filter = new QueryFilter();
      filter.WhereClause = whereClause;
      filter.SubFields = subFields;
      return table.Search(filter, false);
    }

    public void DeleteTableContent(string tableName) {
      //getTable(tableName).DeleteSearchedRows(null);
      // much faster, less "safe", consider context of each call.

      (getTable(tableName) as ITableWrite2).Truncate();
    }

    public void DeleteTableContent(string tableName, string whereClause) {
      IQueryFilter filter = new QueryFilter();
      filter.WhereClause = whereClause;
      getTable(tableName).DeleteSearchedRows(filter);
    }

    public virtual void WriteDataTable(string tableName, DataTable dataTable) {

      ITable esriTable = getTable(tableName);

      try {
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
        throw e;
      }
    }

    #endregion

    #region ESRI FeatureClass Support

    protected IFeatureClass getFeatureClass(string featureClassName) {
      return (Workspace as IFeatureWorkspace).OpenFeatureClass(featureClassName);
    }

    public IFeatureCursor GetCursorForFeatureClassQuery(string featureClassName, string whereClause, string subFields) {
      return GetCursorForFeatureClassQuery(
        getFeatureClass(featureClassName),
        whereClause,
        subFields
      );
    }

    protected IFeatureCursor GetCursorForFeatureClassQuery(IFeatureClass featureClass, string whereClause, string subFields) {
      IQueryFilter filter = new QueryFilter();
      filter.WhereClause = whereClause;
      filter.SubFields = subFields;
      return featureClass.Search(filter, false);
    }

    public IFeatureCursor GetIntersectionCursor(string featureClassName, string whereClause, IFeature feature, double buffer) {
      return GetSpatialCursor(
        getFeatureClass(featureClassName),
        whereClause,
        feature,
        buffer,
        esriSpatialRelEnum.esriSpatialRelIntersects
      );
    }

    public IFeatureCursor GetContainsCursor(string featureClassName, string whereClause, IFeature feature, double buffer) {
      return GetSpatialCursor(
        getFeatureClass(featureClassName),
        whereClause,
        feature,
        buffer,
        esriSpatialRelEnum.esriSpatialRelContains
      );
    }

    protected IFeatureCursor GetSpatialCursor(IFeatureClass featureClass, string whereClause, IFeature feature, 
                                            double buffer, esriSpatialRelEnum spatialRelationship) {
      ISpatialFilter filter = new SpatialFilterClass();
      filter.SpatialRel = spatialRelationship;
      filter.GeometryField = featureClass.ShapeFieldName;
      filter.WhereClause = whereClause;

      ITopologicalOperator shape = (ITopologicalOperator)feature.Shape;
      shape.Simplify();
      filter.Geometry = shape.Buffer(buffer);

      return featureClass.Search(filter, false);
    }


    public virtual void WriteDataTableAsPoints(string tableName, DataTable dataTable, int latitudeColIndex, int longitudeColIndex) {
      IFeatureClass esriTable = (Workspace as IFeatureWorkspace).OpenFeatureClass(tableName);

      ISpatialReference sp = (esriTable as IGeoDataset).SpatialReference;

      try {
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
        throw e;
      }
    }

    protected IPoint buildPointFromDataRow(ISpatialReference spatialReference, DataRow row,
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

    #endregion

    protected void raiseStatusEvent(string status) {
      ProgressChangedEventArgs statusArgs = new ProgressChangedEventArgs(0, status);
      if (StatusChanged != null) {
        StatusChanged(this, statusArgs);
      }
    }
  }
}

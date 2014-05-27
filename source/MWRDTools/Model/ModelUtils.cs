using System;
using System.Data;
using System.Collections.Generic;

using ESRI.ArcGIS.Geodatabase;

namespace MWRDTools.Model {
  class ModelUtils {
    public static DataTable CursorToDataTable(string[] columnNames, ICursor cursor) {

      IRow row = cursor.NextRow();
      if (row == null) {
        return null;
      }

      DataTable table = new DataTable();

      addColumnsToDataTable(columnNames, table);
      addIRowToDataTable(row, table);

      while ((row = cursor.NextRow()) != null) {
        addIRowToDataTable(row, table);
      }

      table.AcceptChanges();

      return table;
    }

    public static DataTable CursorToDataTable(ICursor cursor) {

      IRow row = cursor.NextRow();
      if (row == null) {
        return null;
      }

      DataTable table = new DataTable();

      addFieldsToDataTable(row.Fields, table);
      addIRowToDataTable(row, table);

      while ((row = cursor.NextRow()) != null) {
        addIRowToDataTable(row, table);
      }

      table.AcceptChanges();

      return table;
    }

    private static void addColumnsToDataTable(string[] columnNames, DataTable table) {
      for (int i = 0; i < columnNames.Length; i++) {

        if (columnNames[i].Equals("OBJECTID")) {
          table.Columns.Add(Constants.OID);
        } else {
          table.Columns.Add(
            columnNames[i]
          );
        }
      }
    }

    private static void addFieldsToDataTable(IFields fields, DataTable table) {

      for (int i = 0; i < fields.FieldCount; i++) {
        IField field = fields.get_Field(i);

        if (field.Type == esriFieldType.esriFieldTypeGeometry ||
            field.Type == esriFieldType.esriFieldTypeGlobalID) {
          continue;
        }

        if (field.AliasName.Equals("OBJECTID")) {
          table.Columns.Add(Constants.OID);
        } else {
          table.Columns.Add(
            field.AliasName
          );
        }
      }
    }

    private static void addIRowToDataTable(IRow esriRow, DataTable table) {
      DataRow newRow = table.NewRow();

      foreach (DataColumn column in table.Columns) {
        try {
          if (column.ColumnName.Equals(Constants.OID)) {
            int oidIndex = esriRow.Fields.FindFieldByAliasName("OBJECTID");
            newRow[column.ColumnName] = esriRow.get_Value(oidIndex).ToString();
            continue;
          } else {
            int fieldIndex = esriRow.Fields.FindFieldByAliasName(column.ColumnName);
            newRow[column.ColumnName] = esriRow.get_Value(fieldIndex).ToString();
          }
        } catch (Exception e) {
          throw e;
        }
      }
      table.Rows.Add(newRow);
    }


    public static DataTable FeatureListToDataTable(List<IFeature> features) {

      if (features == null || features.Count == 0) {
        return null;
      }

      DataTable table = new DataTable();

      addFieldsToDataTable(features[0].Fields, table);

      foreach (IFeature feature in features) {
        addFeatureToDataTable(feature, table);
      }

      table.AcceptChanges();

      return table;
    }

    private static void addFeatureToDataTable(IFeature feature, DataTable table) {
      DataRow newRow = table.NewRow();

      foreach (DataColumn column in table.Columns) {
        try {
          if (column.ColumnName.Equals(Constants.OID)) {
            int oidIndex = feature.Fields.FindFieldByAliasName("OBJECTID");
            newRow[column.ColumnName] = feature.get_Value(oidIndex).ToString();
            continue;
          } else {
            int fieldIndex = feature.Fields.FindFieldByAliasName(column.ColumnName);
            newRow[column.ColumnName] = feature.get_Value(fieldIndex).ToString();
          }
        } catch (Exception e) {
          throw e;
        }
      }
      table.Rows.Add(newRow);
    }
  }
}

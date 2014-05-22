using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;

using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

class Common
{

  public static IMap GetFocusMap(IApplication application) {
    return (application.Document as IMxDocument).FocusMap;
  }

  public static IActiveView GetActiveView(IApplication application) {
    return (GetFocusMap(application) as IActiveView);
  }
  

  public static IFeatureLayer GetFeatureLayer(IApplication application, string layerName) {
    return GetFeatureLayer(
      GetFocusMap(application), 
      layerName
    );
  }

  public static IFeatureLayer GetFeatureLayer(IMap pMap, string layerName)
  {
      IEnumLayer pEnumLayer = GetLayers(pMap);
      ILayer pLayer = pEnumLayer.Next();
      while (pLayer != null)
      {
          if (pLayer.Name.ToLower() == layerName.ToLower())
          {
              break;
          }
          pLayer = pEnumLayer.Next();
      }
      if (pLayer != null)
      {
          return pLayer as IFeatureLayer;
      }
      return null;
  }

  public static void RefreshFeatureLayer(IApplication application, ILayer layer) {
    GetActiveView(application).PartialRefresh(
      esriViewDrawPhase.esriViewGeography,
      layer,
      null
    );
  }

  public static IEnumLayer GetLayers(IMap pMap)
  {
      UID pId = new UIDClass();
      pId.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}";
      IEnumLayer layers = pMap.get_Layers(pId, false);
      return layers;
  }

  public static string GetValueAsString(IFeature pFeature, string fieldName)
  {
      int fieldIndex = pFeature.Fields.FindField(fieldName);
      if (fieldIndex == -1)
      {
          throw new Exception(string.Format("Unable to find field {0} in {1}.", fieldName, pFeature.Class.AliasName));
      }
      string result = "";
      result = pFeature.get_Value(fieldIndex).ToString();
      return result;
  }

  public static string GetValueAsString(IRow pRow, string fieldName)
  {
      int fieldIndex = pRow.Fields.FindField(fieldName);
      if (fieldIndex == -1)
      {
          throw new Exception(string.Format("Unable to find field {0} in {1}.", fieldName, "table"));
      }
      string result = "";
      result = pRow.get_Value(fieldIndex).ToString();
      return result;
  }

  public static int GetValueAsInt(IFeature pFeature, string fieldName)
  {
      int fieldIndex = pFeature.Fields.FindField(fieldName);
      if (fieldIndex == -1)
      {
          throw new Exception(string.Format("Unable to find field {0} in {1}.", fieldName, pFeature.Class.AliasName));
      }
      int result = -1;
      try
      {
          result = Convert.ToInt32(pFeature.get_Value(fieldIndex).ToString());
      }
      catch
      {
          throw new Exception(string.Format("Unable to convert value in {0} field in {1} to an integer.", fieldName, pFeature.Class.AliasName));
      }
      return result;
  }

  public static int GetValueAsInt(IRow pRow, string fieldName)
  {
      int fieldIndex = pRow.Fields.FindField(fieldName);
      if (fieldIndex == -1)
      {
          throw new Exception(string.Format("Unable to find field {0}.", fieldName));
      }
      int result = -1;
      try
      {
          result = Convert.ToInt32(pRow.get_Value(fieldIndex).ToString());
      }
      catch
      {
          throw new Exception(string.Format("Unable to convert value in {0} field to an integer.", fieldName));
      }
      return result;
  }

  public static void ZoomToFeature(int oid, IFeatureLayer pFeatureLayer, IMap pMap) {
      IFeature pFeature = pFeatureLayer.FeatureClass.GetFeature(oid);
      if (pFeature != null) {
          IEnvelope pEnvelope = pFeature.Shape.Envelope;
          IActiveView pActiveView = (IActiveView) pMap;
          pEnvelope.Expand(10, 10, true);
          pActiveView.Extent = pEnvelope;
          pActiveView.Refresh();
      }
  }

  public static void ZoomToFeatures(int[] oid, IFeatureLayer pFeatureLayer, IMap pMap) {
      IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
      IQueryFilter pQueryFilter = new QueryFilterClass();
      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < oid.Length; i++) {
        if (i > 0) {
            sb.Append(" OR ");
        }
        sb.Append(pFeatureClass.OIDFieldName);
        sb.Append(" = ");
        sb.Append(oid[i].ToString());
      }
      pQueryFilter.WhereClause = sb.ToString();
      IFeatureCursor pFeatureCursor = pFeatureClass.Search(pQueryFilter, false);
      IFeature pFeature = pFeatureCursor.NextFeature();
      IEnvelope pEnvelope = null;
      bool first = true;
      while (pFeature != null) {
        if (first) {
          first = false;
          pEnvelope = pFeature.Extent;
        }
        else {
          if (pFeature.Shape.Envelope.XMax > pEnvelope.XMax)
          {
              pEnvelope.XMax = pFeature.Shape.Envelope.XMax;
          }
          if (pFeature.Shape.Envelope.XMin < pEnvelope.XMin)
          {
              pEnvelope.XMin = pFeature.Shape.Envelope.XMin;
          }
          if (pFeature.Shape.Envelope.YMax > pEnvelope.YMax)
          {
              pEnvelope.YMax = pFeature.Shape.Envelope.YMax;
          }
          if (pFeature.Shape.Envelope.YMin < pEnvelope.YMin)
          {
              pEnvelope.YMin = pFeature.Shape.Envelope.YMin;
          }
        }
        pFeature = pFeatureCursor.NextFeature();
      }

      if (pEnvelope.Height < 500 || pEnvelope.Width < 500) {
          pEnvelope.Expand(500, 500, false);
      }

      if (pEnvelope != null) {
          IActiveView pActiveView = (IActiveView)pMap;
          pEnvelope.Expand(5, 5, true);
          pActiveView.Extent = pEnvelope;
          pActiveView.Refresh();
      }
  }

  public static void SelectFeature(int oid, IFeatureLayer pFeatureLayer, IMap pMap) {
      IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;

      IQueryFilter pQueryFilter = new QueryFilterClass();
      pQueryFilter.WhereClause = 
        String.Format(
        "{0} = {1}", 
        pFeatureClass.OIDFieldName, 
        oid.ToString()
      );

      ISelectionSet pSelectionSet = pFeatureClass.Select(
        pQueryFilter, 
        esriSelectionType.esriSelectionTypeIDSet, 
        esriSelectionOption.esriSelectionOptionNormal, 
        null
      );

      IFeatureSelection pFeatureSelection = (IFeatureSelection)pFeatureLayer;
      pFeatureSelection.CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew;
      pFeatureSelection.SelectionSet = pSelectionSet;
      pFeatureSelection.SelectionChanged();

      IActiveView pActiveView = (IActiveView)pMap;
      pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
  }

  public static void HighlightFeatures(int[] oid, IFeatureLayer pFeatureLayer, IMap pMap)
  {
      IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
      IQueryFilter pQueryFilter = new QueryFilterClass();
      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < oid.Length; i++)
      {
          if (i > 0)
          {
              sb.Append(" OR ");
          }
          sb.Append(pFeatureClass.OIDFieldName);
          sb.Append(" = ");
          sb.Append(oid[i].ToString());
      }
      pQueryFilter.WhereClause = sb.ToString();
      ISelectionSet pSelectionSet = pFeatureClass.Select(pQueryFilter, esriSelectionType.esriSelectionTypeIDSet, esriSelectionOption.esriSelectionOptionNormal, null);
      IFeatureSelection pFeatureSelection = (IFeatureSelection)pFeatureLayer;
      pFeatureSelection.CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew;
      pFeatureSelection.SelectionSet = pSelectionSet;
      pFeatureSelection.SelectionChanged();
      IActiveView pActiveView = (IActiveView)pMap;
      pActiveView.Refresh();
  }

  public static void FlashFeatures(int[] oid, IFeatureLayer pFeatureLayer, IMap pMap)
  {
      IActiveView pActiveView = (IActiveView)pMap;
      IScreenDisplay pScreenDisplay = pActiveView.ScreenDisplay;

      ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbolClass();
      pSimpleFillSymbol.Outline = null;
      IRgbColor pRGBColour = new RgbColorClass();
      pRGBColour.Red = 177;
      pRGBColour.Green = 8;
      pRGBColour.Blue = 54;
      pSimpleFillSymbol.Color = pRGBColour;
      ISymbol pSymbol = (ISymbol)pSimpleFillSymbol;
      pSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;

      object missing = Type.Missing;

      IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
      IQueryFilter pQueryFilter = new QueryFilterClass();
      StringBuilder sb = new StringBuilder();
      for (int i = 0; i < oid.Length; i++)
      {
          if (i > 0)
          {
              sb.Append(" OR ");
          }
          sb.Append(pFeatureClass.OIDFieldName);
          sb.Append(" = ");
          sb.Append(oid[i].ToString());
      }
      pQueryFilter.WhereClause = sb.ToString();
      IFeatureCursor pFeatureCursor = pFeatureClass.Search(pQueryFilter, false);
      IGeometry pGeometry = null;
      IFeature feature = pFeatureCursor.NextFeature();
      if (feature != null)
      {
          if (feature.Shape.GeometryType == esriGeometryType.esriGeometryMultipoint || 
              feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint)
          {
              IGeometryCollection pGeometryBag = new GeometryBagClass();
              IGeometry featureShape = (IGeometry)feature.Shape;
              ITopologicalOperator pTopo = (ITopologicalOperator)featureShape;
              pTopo.Simplify();
              IPolygon pPolygon = (IPolygon) pTopo.Buffer(100);
              pGeometryBag.AddGeometry(pPolygon, ref missing, ref missing);
              feature = pFeatureCursor.NextFeature();
              while (feature != null)
              {
                  featureShape = (IGeometry)feature.Shape;
                  pTopo = (ITopologicalOperator)featureShape;
                  pTopo.Simplify();
                  pPolygon = (IPolygon)pTopo.Buffer(100);
                  pGeometryBag.AddGeometry(pPolygon, ref missing, ref missing);
                  feature = pFeatureCursor.NextFeature();
              }
              pTopo = new PolygonClass();
              pTopo.ConstructUnion(pGeometryBag as IEnumGeometry);
              pGeometry = pTopo as IPolygon;
          }
      }
      if(feature != null)
      {
          if(feature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)   
          {
              IGeometryCollection pGeometryBag = new GeometryBagClass();
              pGeometryBag.AddGeometry(feature.Shape, ref missing, ref missing);
              feature = pFeatureCursor.NextFeature();
              while (feature != null)
              {
                  pGeometryBag.AddGeometry(feature.Shape, ref missing, ref missing);
                  feature = pFeatureCursor.NextFeature();
              }
              ITopologicalOperator pTopo = new PolygonClass();
              pTopo.ConstructUnion(pGeometryBag as IEnumGeometry);
              pGeometry = pTopo as IPolygon;
          }
      }

      pScreenDisplay.StartDrawing(pScreenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);
            
      pScreenDisplay.SetSymbol(pSymbol as ISymbol);
      pScreenDisplay.DrawPolygon(pGeometry);

      System.Threading.Thread.Sleep(300);
      pScreenDisplay.DrawPolygon(pGeometry);

      pScreenDisplay.FinishDrawing();

  }
  /// <summary>
  /// Obselete
  /// </summary>
  /// <param name="oid"></param>
  /// <param name="pFeatureLayer"></param>
  /// <param name="pMap"></param>
  public static void FlashFeature(int oid, IFeatureLayer pFeatureLayer, IMap pMap)
  {
      IActiveView pActiveView = (IActiveView)pMap;
      IScreenDisplay pScreenDisplay = pActiveView.ScreenDisplay;

      IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
      IQueryFilter pQueryFilter = new QueryFilterClass();
      pQueryFilter.WhereClause = String.Format("{0} = {1}", pFeatureClass.OIDFieldName, oid.ToString());
      IFeatureCursor pFeatureCursor = pFeatureClass.Search(pQueryFilter, false);
      IFeature pFeature = pFeatureCursor.NextFeature();

      pScreenDisplay.StartDrawing(pScreenDisplay.hDC, (short)esriScreenCache.esriNoScreenCache);

      ISimpleFillSymbol pSimpleFillSymbol = new SimpleFillSymbolClass();
      pSimpleFillSymbol.Outline = null;
      IRgbColor pRGBColour = new RgbColorClass();
      pRGBColour.Red = 177;
      pRGBColour.Green = 8;
      pRGBColour.Blue = 54;
      pSimpleFillSymbol.Color = pRGBColour;
      ISymbol pSymbol = (ISymbol)pSimpleFillSymbol;
      pSymbol.ROP2 = esriRasterOpCode.esriROPNotXOrPen;
            
      pScreenDisplay.SetSymbol(pSymbol as ISymbol);
      pScreenDisplay.DrawPolygon(pFeature.Shape);
      System.Threading.Thread.Sleep(300);
      pScreenDisplay.DrawPolygon(pFeature.Shape);

      pScreenDisplay.FinishDrawing();
  }

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
        MessageBox.Show("[" + column.ColumnName + "]" + e.Message);
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
        MessageBox.Show("[" + column.ColumnName + "]" + e.Message);
      }
    }
    table.Rows.Add(newRow);
  }
}

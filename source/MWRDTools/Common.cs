using System;
using System.Collections;
using System.Collections.Generic;
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
  
  public static IFeatureWorkspace GetFeatureWorkspace(string layerName, IMap pMap, ref IFeatureLayer pFeatureLayer)
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
          pFeatureLayer = pLayer as IFeatureLayer;
          if (pFeatureLayer != null)
          {
              IDataset pDataset = pFeatureLayer.FeatureClass as IDataset;
              if (pDataset != null)
              {
                  return pDataset.Workspace as IFeatureWorkspace;
              }
          }
      }
      return null;
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

  public static void ZoomToFeature(int oid, IFeatureLayer pFeatureLayer, IMap pMap)
  {
      IFeature pFeature = pFeatureLayer.FeatureClass.GetFeature(oid);
      if (pFeature != null)
      {
          IEnvelope pEnvelope = pFeature.Shape.Envelope;
          IActiveView pActiveView = (IActiveView) pMap;
          pEnvelope.Expand(10, 10, true);
          pActiveView.Extent = pEnvelope;
          pActiveView.Refresh();
      }
  }

  public static void ZoomToFeatures(int[] oid, IFeatureLayer pFeatureLayer, IMap pMap)
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
      IFeatureCursor pFeatureCursor = pFeatureClass.Search(pQueryFilter, false);
      IFeature pFeature = pFeatureCursor.NextFeature();
      IEnvelope pEnvelope = null;
      bool first = true;
      while (pFeature != null)
      {
          if (first)
          {
              first = false;
              pEnvelope = pFeature.Extent;
          }
          else
          {
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

      if (pEnvelope.Height < 500 || pEnvelope.Width < 500)
      {
          pEnvelope.Expand(500, 500, false);
      }

      if (pEnvelope != null)
      {
          IActiveView pActiveView = (IActiveView)pMap;
          pEnvelope.Expand(5, 5, true);
          pActiveView.Extent = pEnvelope;
          pActiveView.Refresh();
      }

            

  }

  public static void SelectFeature(int oid, IFeatureLayer pFeatureLayer, IMap pMap)
  {
      IFeatureClass pFeatureClass = pFeatureLayer.FeatureClass;
      IQueryFilter pQueryFilter = new QueryFilterClass();
      pQueryFilter.WhereClause = String.Format("{0} = {1}", pFeatureClass.OIDFieldName, oid.ToString());
      ISelectionSet pSelectionSet = pFeatureClass.Select(pQueryFilter, esriSelectionType.esriSelectionTypeIDSet, esriSelectionOption.esriSelectionOptionNormal, null);
      IFeatureSelection pFeatureSelection = (IFeatureSelection)pFeatureLayer;
      pFeatureSelection.CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew;
      pFeatureSelection.SelectionSet = pSelectionSet;
      pFeatureSelection.SelectionChanged();
      IActiveView pActiveView = (IActiveView)pMap;
      pActiveView.PartialRefresh(esriViewDrawPhase.esriViewGeoSelection, null, null);
  }

  public static void SelectFeatures(int[] oid, IFeatureLayer pFeatureLayer, IMap pMap)
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

  public static void ExportListView(string fileName, ListView lv)
  {
      StreamWriter sw = null;
      try
      {
          bool useSelected = false;
          if ((lv.SelectedItems.Count > 0) && (lv.SelectedItems.Count != lv.Items.Count))
          {
              useSelected = true;
          }
          sw = new StreamWriter(fileName);
          StringBuilder sb = new StringBuilder(lv.Columns[0].Text);
          for (int i = 1; i < lv.Columns.Count; i++)
          {
              sb.Append(",");
              sb.Append(lv.Columns[i].Text);
          }
          sb.Append(Environment.NewLine);
          foreach (ListViewItem li in lv.Items)
          {
              if ((li.Selected == true && useSelected == true) || (useSelected == false))
              {
                  for (int i = 0; i < li.SubItems.Count; i++)
                  {
                      sb.Append(li.SubItems[i].Text);
                      sb.Append(",");
                  }
                  sb.Append(Environment.NewLine);
              }
          }
          sw.Write(sb.ToString());
      }
      catch (Exception ex)
      {
          MessageBox.Show(ex.Message, Application.ProductName);
      }
      finally
      {
          try
          {
              sw.Close();
          }
          catch
          {

          }
      }

  }

  public static void ArrayListToListView(ArrayList features, ref ListView lv, string tagField)
  {
      lv.Items.Clear();
      IFeature pFeature;
      for (int i = 0; i < features.Count; i++)
      {
          pFeature = (IFeature)features[i];
          if (i == 0)
          {
              AddListViewColumns(pFeature.Fields, lv);
          }
          AddListItem(pFeature, lv, tagField);
      }
  }

  public static void CursorToListView(ICursor pCursor, ref ListView lv, string tagField)
  {
      lv.Items.Clear();
      IRow pRow = pCursor.NextRow();
      if (pRow != null)
      {
          AddListViewColumns(pRow.Fields, lv);
          AddListItem(pRow, lv, tagField);
          pRow = pCursor.NextRow();
          while (pRow != null)
          {
              AddListItem(pRow, lv, tagField);
              pRow = pCursor.NextRow();
          }
      }

  }

  private static void AddListItem(IRow pRow, ListView lv, string tagField)
  {
      ListViewItem li = new ListViewItem();
      IFields pFields = pRow.Fields;
      bool first = true;
      string val = "";
      for (int i = 0; i < pFields.FieldCount; i++)
      {
          IField pField = pFields.get_Field(i);
          if (pField.Type != esriFieldType.esriFieldTypeGeometry && pField.Type != esriFieldType.esriFieldTypeGlobalID)
          {
              if (pRow.get_Value(i) != null)
              {
                  val = pRow.get_Value(i).ToString();
              }
              else
              {
                  val = "";
              }
              if (first)
              {
                  li.Text = val;
                  first = false;
              }
              else
              {
                  li.SubItems.Add(val);
              }
          }
      }
      li.Tag = GetValueAsString(pRow, tagField); 
      lv.Items.Add(li);
  }

  public static void FeatureCursorToListView(IFeatureCursor pFeatureCursor, ref ListView lv, string tagField)
  {
      lv.Items.Clear();
      IFeature pFeature = pFeatureCursor.NextFeature();
      if (pFeature != null)
      {
          AddListViewColumns(pFeature.Fields, lv);
          AddListItem(pFeature, lv, tagField);
          pFeature = pFeatureCursor.NextFeature();
          while (pFeature != null)
          {
              AddListItem(pFeature, lv, tagField);
              pFeature = pFeatureCursor.NextFeature();
          }
      }
  }

  private static void AddListViewColumns(IFields pFields, ListView lv)
  {
      lv.Columns.Clear();
      for (int i = 0; i < pFields.FieldCount; i++)
      {
          IField pField = pFields.get_Field(i);
          if (pField.Type != esriFieldType.esriFieldTypeGeometry && pField.Type != esriFieldType.esriFieldTypeGlobalID)
          {
              lv.Columns.Add(pField.AliasName);
          }
      }
  }

  private static void AddListItem(IFeature pFeature, ListView lv, string tagField)
  {
      ListViewItem li = new ListViewItem();
      IFields pFields = pFeature.Fields;
      bool first = true;
      for (int i = 0; i < pFields.FieldCount; i++)
      {
          IField pField = pFields.get_Field(i);
          if (pField.Type != esriFieldType.esriFieldTypeGeometry && pField.Type != esriFieldType.esriFieldTypeGlobalID)
          {
              string val = pFeature.get_Value(i).ToString();
              if (first)
              {
                  li.Text = val;
                  first = false;
              }
              else
              {
                  li.SubItems.Add(val);
              }
          }
      }
      if (tagField == "")
      {
          li.Tag = pFeature.OID;
      }
      else
      {
          li.Tag = GetValueAsString(pFeature, tagField);
      }
      lv.Items.Add(li);
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
      IFeature pFeature = pFeatureCursor.NextFeature();
      if (pFeature != null)
      {
          if (pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPoint || pFeature.Shape.GeometryType == esriGeometryType.esriGeometryMultipoint)
          {
              IGeometryCollection pGeometryBag = new GeometryBagClass();
              IMultipoint pMultiPoint = (IMultipoint)pFeature.Shape;
              ITopologicalOperator pTopo = (ITopologicalOperator)pMultiPoint;
              pTopo.Simplify();
              IPolygon pPolygon = (IPolygon) pTopo.Buffer(100);
              pGeometryBag.AddGeometry(pPolygon, ref missing, ref missing);
              pFeature = pFeatureCursor.NextFeature();
              while (pFeature != null)
              {
                  pMultiPoint = (IMultipoint)pFeature.Shape;
                  pTopo = (ITopologicalOperator)pMultiPoint;
                  pTopo.Simplify();
                  pPolygon = (IPolygon)pTopo.Buffer(100);
                  pGeometryBag.AddGeometry(pPolygon, ref missing, ref missing);
                  pFeature = pFeatureCursor.NextFeature();
              }
              pTopo = new PolygonClass();
              pTopo.ConstructUnion(pGeometryBag as IEnumGeometry);
              pGeometry = pTopo as IPolygon;
          }
      }
      if(pFeature != null)
      {
          if(pFeature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon)   
          {
              IGeometryCollection pGeometryBag = new GeometryBagClass();
              pGeometryBag.AddGeometry(pFeature.Shape, ref missing, ref missing);
              pFeature = pFeatureCursor.NextFeature();
              while (pFeature != null)
              {
                  pGeometryBag.AddGeometry(pFeature.Shape, ref missing, ref missing);
                  pFeature = pFeatureCursor.NextFeature();
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
}

using System;
using System.Text;

using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace MWRDTools.Presenter {
  public class MapUtils : IMapUtils {

    private IApplication application;

    public void setApplication(IApplication application) {
      this.application = application;
    }

    private IMap getFocusMap() {
      return (application.Document as IMxDocument).FocusMap;
    }

    private IActiveView getActiveView() {
      return (getFocusMap() as IActiveView);
    }

    private IFeatureLayer getFeatureLayer(string layerName) {
      return GetFeatureLayer(
        getFocusMap(),
        layerName
      );
    }

    private IFeatureLayer GetFeatureLayer(IMap pMap, string layerName) {
      IEnumLayer pEnumLayer = GetLayers(pMap);
      ILayer pLayer = pEnumLayer.Next();
      while (pLayer != null) {
        if (pLayer.Name.ToLower() == layerName.ToLower()) {
          break;
        }
        pLayer = pEnumLayer.Next();
      }
      if (pLayer != null) {
        return pLayer as IFeatureLayer;
      }
      return null;
    }

    private IEnumLayer GetLayers(IMap pMap) {
      UID pId = new UIDClass();
      pId.Value = "{40A9E885-5533-11d0-98BE-00805F7CED21}";
      IEnumLayer layers = pMap.get_Layers(pId, false);
      return layers;
    }

    public string GetMapDatabase() {
      IFeatureLayer wetlandLayer = (IFeatureLayer)getFeatureLayer(Constants.LayerName.WetLands);
      if (wetlandLayer.DataSourceType.Equals("SDE Feature Class")) {
        IDataLayer2 dataLayer = wetlandLayer as IDataLayer2;
        IDatasetName dataSetName = dataLayer.DataSourceName as IDatasetName;
        IWorkspaceName workspaceName = dataSetName.WorkspaceName;
        IPropertySet properties = workspaceName.ConnectionProperties;
        return properties.GetProperty("SERVER") as string;
      }
      return null;
    }

    public void RefreshFeatureLayer(string layerName) {
      getActiveView().PartialRefresh(
        esriViewDrawPhase.esriViewGeography,
        getFeatureLayer(layerName),
        null
      );
    }

    public void HighlightFeatures(int[] oid, string layerName) {
      IFeatureLayer pFeatureLayer = getFeatureLayer(layerName);

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
      ISelectionSet pSelectionSet =
        pFeatureClass.Select(
          pQueryFilter,
          esriSelectionType.esriSelectionTypeIDSet,
          esriSelectionOption.esriSelectionOptionNormal,
          null
        );
      IFeatureSelection pFeatureSelection = (IFeatureSelection)pFeatureLayer;
      pFeatureSelection.CombinationMethod = esriSelectionResultEnum.esriSelectionResultNew;
      pFeatureSelection.SelectionSet = pSelectionSet;
      pFeatureSelection.SelectionChanged();
      getActiveView().Refresh();
    }

    public void ZoomToFeatures(int[] oid, string layerName) {
      IFeatureLayer pFeatureLayer = getFeatureLayer(layerName);

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
        } else {
          if (pFeature.Shape.Envelope.XMax > pEnvelope.XMax) {
            pEnvelope.XMax = pFeature.Shape.Envelope.XMax;
          }
          if (pFeature.Shape.Envelope.XMin < pEnvelope.XMin) {
            pEnvelope.XMin = pFeature.Shape.Envelope.XMin;
          }
          if (pFeature.Shape.Envelope.YMax > pEnvelope.YMax) {
            pEnvelope.YMax = pFeature.Shape.Envelope.YMax;
          }
          if (pFeature.Shape.Envelope.YMin < pEnvelope.YMin) {
            pEnvelope.YMin = pFeature.Shape.Envelope.YMin;
          }
        }
        pFeature = pFeatureCursor.NextFeature();
      }

      if (pEnvelope.Height < 500 || pEnvelope.Width < 500) {
        pEnvelope.Expand(500, 500, false);
      }

      if (pEnvelope != null) {
        IActiveView pActiveView = getActiveView();
        pEnvelope.Expand(5, 5, true);
        pActiveView.Extent = pEnvelope;
        pActiveView.Refresh();
      }
    }
 
    public void FlashFeatures(int[] oid, string layerName) {
      IFeatureLayer pFeatureLayer = getFeatureLayer(layerName);

      IActiveView pActiveView = getActiveView();
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
      IGeometry pGeometry = null;
      IFeature feature = pFeatureCursor.NextFeature();
      if (feature != null) {
        if (feature.Shape.GeometryType == esriGeometryType.esriGeometryMultipoint ||
            feature.Shape.GeometryType == esriGeometryType.esriGeometryPoint) {
          IGeometryCollection pGeometryBag = new GeometryBagClass();
          IGeometry featureShape = (IGeometry)feature.Shape;
          ITopologicalOperator pTopo = (ITopologicalOperator)featureShape;
          pTopo.Simplify();
          IPolygon pPolygon = (IPolygon)pTopo.Buffer(100);
          pGeometryBag.AddGeometry(pPolygon, ref missing, ref missing);
          feature = pFeatureCursor.NextFeature();
          while (feature != null) {
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
      if (feature != null) {
        if (feature.Shape.GeometryType == esriGeometryType.esriGeometryPolygon) {
          IGeometryCollection pGeometryBag = new GeometryBagClass();
          pGeometryBag.AddGeometry(feature.Shape, ref missing, ref missing);
          feature = pFeatureCursor.NextFeature();
          while (feature != null) {
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
  }
}


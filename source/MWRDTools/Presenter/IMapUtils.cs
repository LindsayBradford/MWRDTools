using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace MWRDTools.Presenter {
  public interface IMapUtils {
    void setApplication(IApplication application);

    string GetMapDatabase();

    void RefreshFeatureLayer(string layerName);
    void HighlightFeatures(int[] oid, string layerName);
    void ZoomToFeatures(int[] oid, string layerName);
    void FlashFeatures(int[] oid, string layerName);
  }
}

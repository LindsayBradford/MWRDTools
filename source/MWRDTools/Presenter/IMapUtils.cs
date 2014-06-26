/*
  Copyright (c) 2014, Riverina Local Land Services.

  This program and the accompanying materials are made available under the terms of the
  BSD 3-Clause licence which accompanies this distribution, and is available at
  http://opensource.org/licenses/BSD-3-Clause
 */

using System;

using ESRI.ArcGIS.Framework;

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

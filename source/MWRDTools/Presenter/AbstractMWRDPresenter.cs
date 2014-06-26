/*
  Copyright (c) 2014, Riverina Local Land Services.

  This program and the accompanying materials are made available under the terms of the
  BSD 3-Clause licence which accompanies this distribution, and is available at
  http://opensource.org/licenses/BSD-3-Clause
 */

using System;
using System.Data;
using System.Linq;

using MWRDTools.Model;

using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.ArcMapUI;
using ESRI.ArcGIS.Carto;

namespace MWRDTools.Presenter {
  public abstract class AbstractMWRDPresenter {

    protected IMapUtils mapUtils;
    protected IFileSystemBridge fileBridge;

    public void setFileBridge(IFileSystemBridge fileBridge) {
      this.fileBridge = fileBridge;
    }

    public void setMapUtils(IMapUtils mapUtils) {
      this.mapUtils = mapUtils;
    }

    public void HighlightFeatures(int[] featureIDs, string layerName) {
      if (featureIDs == null) return;

      mapUtils.HighlightFeatures(
        featureIDs,
        layerName
      );
    }

    public void ZoomToFeatures(int[] featuretIDs, string layerName) {
      if (featuretIDs == null) return;

      mapUtils.ZoomToFeatures(
        featuretIDs,
        layerName
      );
    }

    public void FlashFeatures(int[] featuretIDs, string layerName) {
      if (featuretIDs == null) return;

      mapUtils.FlashFeatures(
        featuretIDs,
        layerName
      );
    }

    public void HighlightWetlands(int[] wetlandIDs) {
      HighlightFeatures(
        wetlandIDs,
        Constants.LayerName.WetLands
      );
    }

    public void FlashWetlands(int[] wetlandIDs) {
      FlashFeatures(
        wetlandIDs,
        Constants.LayerName.WetLands
      );
    }

    public void ZoomToWetlands(int[] wetlandIDs) {
      ZoomToFeatures(
        wetlandIDs, 
        Constants.LayerName.WetLands
      );
    }

    public void ExportFeatures(string filename, DataTable featureTable, int[] featureIDs) {

      if (featureTable == null || featureTable.Rows.Count == 0) return;
      if (featureIDs == null || featureIDs.Count() == 0) return;

      DataTable exportableWetlands = featureTable.Clone();

      foreach (DataRow featureRow in featureTable.Rows) {
        int index = Array.IndexOf(featureIDs, Convert.ToInt32(featureRow["OID"]));
        if (index != -1) {
          exportableWetlands.ImportRow(featureRow);
        }
      }

      this.fileBridge.DataTableToCSV(
        exportableWetlands,
        filename
      );
    }
  }
}

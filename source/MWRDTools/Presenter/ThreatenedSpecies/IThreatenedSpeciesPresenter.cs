/*
  Copyright (c) 2014, Riverina Local Land Services.

  This program and the accompanying materials are made available under the terms of the
  BSD 3-Clause licence which accompanies this distribution, and is available at
  http://opensource.org/licenses/BSD-3-Clause
 */

using System;
using System.Data;
using System.ComponentModel;

using MWRDTools.Model;

using ESRI.ArcGIS.Framework;
using ESRI.ArcGIS.Carto;

namespace MWRDTools.Presenter {
  public interface IThreatenedSpeciesPresenter {

    event EventHandler<ThreatenedSpeciesEventArgs> ThreatenedSpeciesPresentationChanged;
    event EventHandler<ProgressChangedEventArgs> StatusChanged;

    string[] GetSpeciesClasses();
    string[] GetSpeciesStatuses();

    void SpeciesByWetlandsTabSelected();

    void SpeciesFilterApplied(
      string[] classesSelected, 
      string[] statusesSelected
    );

    void setFileBridge(IFileSystemBridge fileBridge);
    void setMapUtils(IMapUtils mapUtils);

    void HighlightWetlands(int[] wetlandIDs);
    void ZoomToWetlands(int[] wetlandIDs);
    void FlashWetlands(int[] wetlandIDs);

    void HighlightSpecies(int[] speciesIDs);
    void ZoomToSpecies(int[] speciesIDs);
    void FlashSpecies(int[] speciesIDs);

    void ExportFeatures(string filename, DataTable features, int[] featureIDs);

    void setWetlandsModel(IWetlandsModel model);

    void setThreatenedSpeciesModel(IThreatenedSpeciesModel model);

    void FindWetlandsBySpecies(
      string speciesScientificName, 
      double buffer, 
      DateTime? afterDate, 
      DateTime? beforeDate
    );

     void FindSpeciesByWetlands(
      string wetlandsID,
      string[] speciesClasses,
      string[] speciesStatuses,
      double buffer,
      DateTime? afterDate,
      DateTime? beforeDate
    );
  }
}

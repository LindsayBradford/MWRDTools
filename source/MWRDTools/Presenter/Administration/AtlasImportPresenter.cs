using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using MWRDTools.View;
using MWRDTools.Model;

namespace MWRDTools.Presenter
{
  class AtlasImportPresenter : INSWAtlasWildlifeImportPresenter
  {

    private const int FLORA_INDEX = 0;
    private const int FAUNA_INDEX = 1;

    private const char ATLAS_DELIMITER_CHAR = '\t';
    private const int ATLAS_HEADER_LINES = 4;

    private const string NSW_STATUS_COLUMN_NAME = "NSWStatus";

    private INSWAtlasWildlifeImportView view;
    private IFileSystemBridge bridge;
    private IThreatenedSpeciesModel model;

    public void setView(INSWAtlasWildlifeImportView view) {
      this.view = view;
    }

    public void setModel(IThreatenedSpeciesModel model) {
      this.model = model;

      model.StatusChanged += new EventHandler<ModelStatusEventArgs>(this.HandleModelStatusEvent);
    }

    public void setFileBridge(IFileSystemBridge bridge) {
      this.bridge = bridge;
    }

    public void ImportFiles(params string[] files) {
      WriteImportedData(
        importFloraFile(
          files[FLORA_INDEX]
        ),
        importFaunaFile(
          files[FAUNA_INDEX]
        )
      );
    }

    private DataTable importFloraFile(string floraFile) {
      view.ShowStatusString(" Loading NSW Atlas of Wildlife flora file...");

      DataTable floraSightings = bridge.CSVtoDataTable(
        floraFile, 
        ATLAS_DELIMITER_CHAR, 
        ATLAS_HEADER_LINES
      );

      return floraSightings;
    }

    private DataTable importFaunaFile(string faunaFile) {
      view.ShowStatusString(" Loading NSW Atlas of Wildlife fauna file...");

      DataTable faunaSightings = bridge.CSVtoDataTable(
        faunaFile, 
        ATLAS_DELIMITER_CHAR, 
        ATLAS_HEADER_LINES
      );

      return faunaSightings;
    }

    public void WriteImportedData(params DataTable[] sightingTables) {

      view.ShowStatusString(" Merging flora and fauna species sightings...");

      DataTable allSightings = mergeSightings(sightingTables);

      view.ShowStatusString(" Removing Exotic species sightings...");

      removeSightingsWithNoNSWStatus(allSightings);

      model.OverwriteSightingData(allSightings);

      view.ShowStatusString(" Threatened species sightings written to database.");
    }

    private DataTable mergeSightings(params DataTable[] sightingTables) {
      DataTable firstTable = sightingTables[0];

      foreach (DataTable tableToMerge in sightingTables) {
        if (tableToMerge != firstTable) {
          firstTable.Merge(tableToMerge);
        }
      }

      return firstTable;
    }

    private void removeSightingsWithNoNSWStatus(DataTable sightings) {
      HashSet<DataRow> rowsToDelete = new HashSet<DataRow>();

      for (int rowIndex = 0; rowIndex < sightings.Rows.Count; rowIndex++) {
        string value = sightings.Rows[rowIndex][NSW_STATUS_COLUMN_NAME] as string;
        if (value == null || value.Trim().Equals("")) {
          rowsToDelete.Add(sightings.Rows[rowIndex]);
        }
      }

      foreach (DataRow row in rowsToDelete) {
        sightings.Rows.Remove(row);
      }

      sightings.AcceptChanges();
    }

    public void HandleModelStatusEvent(object sender, ModelStatusEventArgs args) {
      view.ShowStatusString(args.Status, args.progressesStatus);
    }
  }
}

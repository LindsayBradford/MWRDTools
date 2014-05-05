using System;
using System.Data;
using System.IO;

using MWRDTools.View;
using MWRDTools.Model;

namespace MWRDTools.Presenter
{
  class CARMImportPresenter : ICARMScenarioImportPresenter
  {
    private ICARMScenarioImportView view;
    private IFileSystemBridge bridge;
    private ICARMScenarioModel model;

    struct CARMFileDetail {
      public string baseFileName;
      public string tableName;
      public string fullFilePath;
      public DataTable table;
    }

    private const int TIME_SERIES_GROUP_INDEX = 0;
    private const int TIME_SERIES_INDEX = 1;
    private const int TIME_SERIES_VALUE_INDEX = 2;

    private CARMFileDetail[] CARMFileDetails = new CARMFileDetail[]{
      new CARMFileDetail() { 
        baseFileName = "time_series_group.csv",
        tableName = "CARM_time_series_grpup"
      },
      new CARMFileDetail() { 
        baseFileName = "time_series.csv",
        tableName = "CARM_time_series"
      },
      new CARMFileDetail() { 
        baseFileName = "time_series_value.csv",
        tableName = "CARM_time_series_value"
      }
    };

    private const char CARM_DELIMITER_CHAR = '\t';

    public void setView(ICARMScenarioImportView view)
    {
      this.view = view;
    }

    public void setModel(ICARMScenarioModel model)
    {
      this.model = model;
    }

    public void setFileBridge(IFileSystemBridge bridge)
    {
      this.bridge = bridge;
    }

    public void ImportDirectory(string direcroryPath)
    {
      WriteImportedData(
        importScenarioDirectory(
          direcroryPath
        )
      );
    }

    private DataTable[] importScenarioDirectory(string directoryPath)
    {
      DataTable[] scenarioTables = new DataTable[CARMFileDetails.Length];

      buildScenarioFiles(directoryPath);
      
      view.ShowStatusString("Loading CARM TImeseries Group file...");

      buildDataTable(
        CARMFileDetails[TIME_SERIES_GROUP_INDEX],
        TIME_SERIES_GROUP_INDEX
      );

      view.ShowStatusString("Loading CARM TImeseries file...");

      buildDataTable(
        CARMFileDetails[TIME_SERIES_INDEX],
        TIME_SERIES_INDEX
      );

      view.ShowStatusString("Loading CARM TImeseries Values file...");

      buildDataTable(
        CARMFileDetails[TIME_SERIES_VALUE_INDEX],
        TIME_SERIES_VALUE_INDEX
      );

      for (int i = 0; i < CARMFileDetails.Length; i++) {
        scenarioTables[i] = CARMFileDetails[i].table;
      }

      return scenarioTables;
    }

    private void buildScenarioFiles(string directoryPath) {
      for (int i = 0; i < CARMFileDetails.Length; i++) {
        CARMFileDetails[i].fullFilePath = Path.Combine(directoryPath, CARMFileDetails[i].baseFileName);
      }
    }

    private void buildDataTable(CARMFileDetail detail, int index) {
      detail.table = 
        bridge.CSVtoDataTable(
          detail.fullFilePath,
          CARM_DELIMITER_CHAR
        );

      detail.table.TableName = detail.tableName;
    }

    public void WriteImportedData(params DataTable[] scenarioTables)
    {
      view.ShowStatusString("Writing CARM Scenario data to database...");

      model.WriteScenarioData(scenarioTables);

      view.ShowStatusString("CARM Scenario data written to database.");
    }
  }

}

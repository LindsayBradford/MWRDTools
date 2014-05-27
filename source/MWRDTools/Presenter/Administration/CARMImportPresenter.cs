using System;
using System.ComponentModel;
using System.Data;
using System.IO;

using MWRDTools.View;
using MWRDTools.Model;

namespace MWRDTools.Presenter
{
  class CARMImportPresenter : ICARMScenarioImportPresenter
  {
    public event EventHandler<ProgressChangedEventArgs> StatusChanged;

    private int percentComplete = 0;
    
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
        tableName = Constants.TableName.CARM_time_series_group
      },
      new CARMFileDetail() { 
        baseFileName = "time_series.csv",
        tableName = Constants.TableName.CARM_time_series
      },
      new CARMFileDetail() { 
        baseFileName = "time_series_value.csv",
        tableName = Constants.TableName.CARM_time_series_value
      }
    };

    private const char CARM_DELIMITER_CHAR = ',';

    public void setModel(ICARMScenarioModel model)
    {
      this.model = model;
      model.StatusChanged += new EventHandler<ProgressChangedEventArgs>(this.HandleModelStatusEvent);
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
      
     this.raiseStatusEvent(20," Loading CARM Timeseries Group file...");

      buildDataTable(
        ref CARMFileDetails[TIME_SERIES_GROUP_INDEX],
        TIME_SERIES_GROUP_INDEX
      );

      this.raiseStatusEvent(40," Loading CARM Timeseries file...");

      buildDataTable(
        ref CARMFileDetails[TIME_SERIES_INDEX],
        TIME_SERIES_INDEX
      );

      this.raiseStatusEvent(60, " Loading CARM Timeseries Values file...");

      buildDataTable(
        ref CARMFileDetails[TIME_SERIES_VALUE_INDEX],
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

    private void buildDataTable(ref CARMFileDetail detail, int index) {
      detail.table = 
        bridge.CSVtoDataTable(
          detail.fullFilePath,
          CARM_DELIMITER_CHAR
        );

      detail.table.TableName = detail.tableName;
    }

    public void WriteImportedData(params DataTable[] scenarioTables)
    {
      this.raiseStatusEvent(80, " Writing CARM Scenario data to database...");

      model.WriteScenarioData(scenarioTables);

      raiseStatusEvent(100, " CARM Scenario data written to database.");
    }

    protected void raiseStatusEvent(int percentComplete, string status) {
      this.percentComplete = percentComplete;
      ProgressChangedEventArgs statusArgs = new ProgressChangedEventArgs(percentComplete, status);

      if (StatusChanged != null) {
        StatusChanged(this, statusArgs);
      }
    }

    public void HandleModelStatusEvent(object sender, ProgressChangedEventArgs args) {
      if (StatusChanged != null) {
        ProgressChangedEventArgs updatedArgs = new ProgressChangedEventArgs(percentComplete, args.UserState);
        StatusChanged(this, updatedArgs);
      }
    }
  }
}

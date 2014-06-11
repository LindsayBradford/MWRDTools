using MWRDTools.Model;

namespace MWRDTools.Presenter {
  class MapDatabasePresenter : IMapDatabasePresenter {

    private IFileSystemBridge fileSystemBridge;
    private IGeodatabaseBridge geodatabaseBridge;
    private IMapUtils mapUtils;

    public void setGeodatabaseBridge(IGeodatabaseBridge bridge) {
      this.geodatabaseBridge = bridge;
    }

    public void setFileBridge(IFileSystemBridge bridge) {
      this.fileSystemBridge = bridge;
    }

    public void SetDatabaseServer(string databaseServer) {
      UpdateMapDataSources(databaseServer);
      (geodatabaseBridge as ArcSDEGeodatabaseBridge).EstablishConnection(databaseServer);
    }

    public void setMapUtils(IMapUtils mapUtils) {
      this.mapUtils = mapUtils;
    }

    public string GetDatabaseServer() {
      return (geodatabaseBridge as ArcSDEGeodatabaseBridge).GetDatabaseServer();
    }

    private void UpdateMapDataSources(string databaseServer) {
      // for all layers, remap to databaseServer
      // for all tables, remap to databaseServer
      // save map.
    }

  }
}

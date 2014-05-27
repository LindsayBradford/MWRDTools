using System.Data;

namespace MWRDTools.Model
{
  public interface IFileSystemBridge
  {
    DataTable CSVtoDataTable(string filePath, char delimiter, int linesToSkip);
    DataTable CSVtoDataTable(string filePath, char delimiter);

    void DataTableToCSV(DataTable table, string filePath);
  }
}

using System;
using System.Data;
using System.IO;

using System.Windows.Forms;

namespace MWRDTools.Model
{
  class FileSystemBridge : IFileSystemBridge
  {
    public DataTable CSVtoDataTable(string filePath, char delimiter)
    {
      return CSVtoDataTable(filePath, delimiter, 0);
    }

    public DataTable CSVtoDataTable(string filePath, char delimiter, int linesToSkip)
    {
      if (!File.Exists(filePath)) {
        return null;
      }

      DataTable table = new DataTable();

      string currentLine;
      long linesRead = 0;
      Boolean headerLoaded = false;

      StreamReader file = new StreamReader(filePath);
      while ((currentLine = file.ReadLine())!= null)
      {
        linesRead++;
        if (linesRead <= linesToSkip) continue;

        if (!headerLoaded)
        {
          setDataTableHeaders(
            table,
            currentLine.Split(delimiter)
          );
          headerLoaded = true;
        } // adding columns
        else
        {
          addDataTableRow(
            table,
            currentLine.Split(delimiter)
          );
        } // adding row
      }  // while there's a new line to read
      file.Close();

      return table;
    }

    private void setDataTableHeaders(DataTable table, params string[] headerNames)
    {
      foreach (string header in headerNames)
      {
        table.Columns.Add(header);
      }
    }

    private void addDataTableRow(DataTable table, params string[] rowValues)
    {
      DataRow row = table.NewRow();
      row.ItemArray = rowValues;
      table.Rows.Add(row);
    }
  }
}

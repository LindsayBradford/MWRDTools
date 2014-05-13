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
            smartSplit(currentLine, delimiter)
          );
          headerLoaded = true;
        } // adding columns
        else
        {
          addDataTableRow(
            table,
            smartSplit(currentLine,delimiter)
          );
        } // adding row
      }  // while there's a new line to read
      file.Close();

      return table;
    }

    private static void setDataTableHeaders(DataTable table, params string[] headerNames)
    {
      foreach (string header in headerNames)
      {
        table.Columns.Add(header);
      }
    }

    private static void addDataTableRow(DataTable table, params string[] rowValues)
    {
      DataRow row = table.NewRow();
      row.ItemArray = rowValues;
      table.Rows.Add(row);
    }

    private static string[] smartSplit(string textToSplit, char delimiter) {
      string[] splitText = textToSplit.Split(delimiter);

      for (int i = 0; i < splitText.Length; i++) {
        splitText[i] = splitText[i].TrimStart('\"');
        splitText[i] = splitText[i].TrimEnd('\"');
      }

      return splitText;
    }
  }
}

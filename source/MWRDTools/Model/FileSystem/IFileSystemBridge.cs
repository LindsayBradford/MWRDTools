/*
  Copyright (c) 2014, Riverina Local Land Services.

  This program and the accompanying materials are made available under the terms of the
  BSD 3-Clause licence which accompanies this distribution, and is available at
  http://opensource.org/licenses/BSD-3-Clause
 */

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

using System;
using System.Data;
using System.Windows.Forms;

namespace MWRDTools.View {
  public class ViewUtilities {

    public static void SelectAllFeatures(ListView view) {
      for (int i = 0; i < view.Items.Count; i++) {
        view.Items[i].Selected = true;
      }
    }

    public static int[] GetSelectedFeatures(ListView view) {
      if (view.SelectedItems == null || view.SelectedItems.Count == 0) {
        return null;
      }
      int[] featureTags = new int[view.SelectedItems.Count];
      for (int i = 0; i < view.SelectedItems.Count; i++) {
        featureTags[i] = Convert.ToInt32(view.SelectedItems[i].Tag.ToString());
      }
      return featureTags;
    }

    public static void ProcessColumnClickEvent(ListView view, ColumnClickEventArgs e) {
      ListViewColumnSorter sorter = view.ListViewItemSorter as ListViewColumnSorter;
      if (e.Column == sorter.ColunmToSort) {
        if (sorter.SortOrder == "ascending") {
          sorter.SortOrder = "descending";
        } else {
          sorter.SortOrder = "ascending";
        }
      } else {
        sorter.ColunmToSort = e.Column;
        sorter.SortOrder = "ascending";
      }
      view.Sort();
    }

    public static void DataTableToListView(DataTable table, ListView view, string tagColumn) {
      view.Columns.Clear();
      view.Items.Clear();

      if (table == null || table.Rows.Count == 0) {
        return;
      }

      foreach (DataColumn column in table.Columns) {
        view.Columns.Add(
          column.ColumnName
        );
      }

      foreach (DataRow row in table.Rows) {
        ListViewItem item = new ListViewItem(row[tagColumn].ToString());

        foreach (DataColumn column in table.Columns) {
          if (column.ColumnName.Equals(tagColumn)) {
            continue;
          }
          item.SubItems.Add(
            row[column.ColumnName].ToString()
          );
        }

        item.Tag = row[tagColumn];

        view.Items.Add(item);
      }

      view.AutoResizeColumns(
        ColumnHeaderAutoResizeStyle.HeaderSize
      );
    }
  }
}

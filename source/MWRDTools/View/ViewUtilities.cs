using System.Data;
using System.Windows.Forms;

namespace MWRDTools.View {
  public class ViewUtilities {
    public static void DataTableToListView(DataTable table, ListView view) {
      view.Columns.Clear();
      view.Items.Clear();

      foreach (DataColumn column in table.Columns) {
        view.Columns.Add(
          column.ColumnName
        );
      }

      foreach (DataRow row in table.Rows) {
        ListViewItem item = new ListViewItem(row[Constants.OID].ToString());

        foreach (DataColumn column in table.Columns) {
          if (column.ColumnName.Equals(Constants.OID)) {
            continue;
          }
          item.SubItems.Add(
            row[column.ColumnName].ToString()
          );
        }

        item.Tag = row[Constants.OID];

        view.Items.Add(item);
      }

      view.AutoResizeColumns(
        ColumnHeaderAutoResizeStyle.HeaderSize
      );
    }
  }
}

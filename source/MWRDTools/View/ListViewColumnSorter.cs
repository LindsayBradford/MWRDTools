using System.Collections;
using System.Windows.Forms;

public class ListViewColumnSorter : IComparer
{
    private int _columnToSort;
    private string _orderOfSort;
    private CaseInsensitiveComparer _objectCompare;

    public ListViewColumnSorter() {
        _columnToSort = 0;
        _orderOfSort = "none";
        _objectCompare = new CaseInsensitiveComparer();
    }

    #region IComparer Members

    public int Compare(object x, object y) {
        int compareResult;
        ListViewItem listViewItemX;
        ListViewItem listViewItemY;

        listViewItemX = (ListViewItem)x;
        listViewItemY = (ListViewItem)y;

        compareResult = _objectCompare.Compare(
          listViewItemX.SubItems[_columnToSort].Text, 
          listViewItemY.SubItems[_columnToSort].Text
        );

        if (_orderOfSort == "ascending") {
            return compareResult;
        }

        if (_orderOfSort == "descending") {
            return (-compareResult);
        }
        else {
            return 0;
        }
    }

    public int ColunmToSort {
      set {
        _columnToSort = value;
      }
      get {
        return _columnToSort;
      }
    }

    public string SortOrder {
      set {
        _orderOfSort = value;
      }
      get {
        return _orderOfSort;
      }
    }
    #endregion
}
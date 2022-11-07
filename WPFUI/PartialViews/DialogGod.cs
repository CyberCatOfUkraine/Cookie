using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;

namespace WPFUI.PartialViews
{
    internal class DialogGod<T> where T:class
    {
        private IComparer<T> _comparer;
        public DialogGod(IComparer<T> comparer)
        {
            _comparer = comparer;
        }
        internal void Kill(DialogHost dialog,DataGrid updatableDataGrid,List<T> dataList)
        {
            dialog.IsOpen = false;
            
            updatableDataGrid.ItemsSource = null;
            dataList.Sort(_comparer);
            updatableDataGrid.ItemsSource = dataList;
        }

        internal void Create(UserControl control)
        {
                DialogHost.Show(control);
        }
    }
}

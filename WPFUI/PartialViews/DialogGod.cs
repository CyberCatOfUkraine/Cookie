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
        
        internal void Kill(DialogHost dialog,DataGrid updatableDataGrid,List<T> dataList)
        {
            dialog.IsOpen = false;
            
            updatableDataGrid.ItemsSource = null;
            dataList.Sort();
            updatableDataGrid.ItemsSource = dataList;
        }

        internal void Create(DialogHost dialog,UserControl control)
        {
            DialogHost.Show(control);
        }
    }
}

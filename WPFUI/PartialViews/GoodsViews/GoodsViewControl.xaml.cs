using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataTransferWrapper;
using WPFUI.ExtentionMethods;
using WPFUI.Models;
using WPFUI.PartialViews.GoodsViews;

namespace WPFUI.PartialViews
{
    /// <summary>
    /// Interaction logic for GoodsViewControl.xaml
    /// </summary>
    public partial class GoodsViewControl : UserControl
    {
        GoodsWrapper wrapper = new ();
        private List<Goods> goodsList;

        public GoodsViewControl()
        {
            InitializeComponent();

            goodsList = wrapper.GetAll().Convert();
            GoodsDataGrid.ItemsSource = goodsList;
        }
        

        private void DelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            dynamic goods = GoodsDataGrid.SelectedItem;
            try
            {
                wrapper.Remove(goods.Name);
            }
            catch (InvalidOperationException exception)
            {
                MessageBox.Show(exception.Message);
            }
            RefreshDataGrid();
        }

        private void AddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            DialogHost.Show(new AddGoodsControl(GoodsDialogHost, wrapper,RefreshDataGrid));
        }
        private void RefreshDataGrid()
        {
            GoodsDataGrid.ItemsSource = null;
            wrapper.Sort();
            goodsList=wrapper.GetAll().Convert();
            GoodsDataGrid.ItemsSource = goodsList;
        }
        private void GoodsDataGrid_OnCellEditEnding(object? sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditingElement is TextBox textBox)
            {
               var text= textBox.Text;
               dynamic dataContext = e.Row.DataContext;
               string Name = dataContext.Name;
               var wrappedGoods = wrapper.GetBy(Name);
               wrappedGoods.Count = Convert.ToInt32(text);
               wrapper.Update(Name,wrappedGoods);
            }
        }

        private void GoodsDialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventargs)
        {
            RefreshDataGrid();
        }
    }
}

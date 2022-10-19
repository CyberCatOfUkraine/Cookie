using DataTransferWrapper;
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
using WPFUI.ExtentionMethods;
using WPFUI.Models;

namespace WPFUI.PartialViews.ProvidersViews
{
    /// <summary>
    /// Interaction logic for AddProviderViewControl.xaml
    /// </summary>
    public partial class AddProviderViewControl : UserControl
    {
        public AddProviderViewControl()
        {
            InitializeComponent();
        }


        private DialogHost _dialogHost;
        private Action _invokeAfterExecution;
        private ProviderWrapper _providerWrapper;
        private GoodsWrapper _goodsWrapper;

        private List<Goods> _goodsList;
        private List<Goods> _selectedGoodsList;
        public AddProviderViewControl(DialogHost dialogHost, ProviderWrapper providerWrapper, GoodsWrapper goodsWrapper, Action invokeAfterExecution) : this()
        {
            _dialogHost = dialogHost;
            _goodsWrapper = goodsWrapper;
            _invokeAfterExecution = invokeAfterExecution;
            _providerWrapper = providerWrapper;

            _goodsList = new List<Goods>();
            _selectedGoodsList = new List<Goods>();
            
            InitiateGoodsDataGrid();
        }
        private void InitiateGoodsDataGrid()
        {
            _goodsWrapper.Sort();
            _goodsList = _goodsWrapper.GetAll().Convert();

            GoodsDataGrid.ItemsSource = _goodsList;
        }

        private void AddToProviderGoodsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var goods = GoodsDataGrid.CurrentItem as Goods;
            _selectedGoodsList.Add(goods);
            _goodsList.Remove(goods);

            RefreshDataGridsItems();
        }
        private void DelSelectedGoodsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var goods = SelectedGoodsDataGrid.CurrentItem as Goods;

            _selectedGoodsList.Remove(goods);
            _goodsList.Add(goods);

            _goodsList = (from oneGoods in _goodsList orderby oneGoods.Name select oneGoods).ToList();

            RefreshDataGridsItems();
        }

        private void RefreshDataGridsItems()
        {
            SelectedGoodsDataGrid.ItemsSource = null;
            SelectedGoodsDataGrid.ItemsSource = _selectedGoodsList;

            GoodsDataGrid.ItemsSource = null;
            GoodsDataGrid.ItemsSource = _goodsList;
        }
        private void SubmitBtn_OnClick(object sender, RoutedEventArgs e)
        {
            _providerWrapper.Add(new(ProviderNameTextBox.Text,_selectedGoodsList.Convert(),Convert.ToInt64(TelegramIdTextBox.Text)));
            CloseDialogControl();
        }

        private void CancelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            CloseDialogControl();
        }
        private void CloseDialogControl()
        {
            _dialogHost.IsOpen = false;
            _invokeAfterExecution.Invoke();
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using DataTransferWrapper;
using MaterialDesignThemes.Wpf;
using WPFUI.ExtentionMethods;
using WPFUI.Models;

namespace WPFUI.PartialViews.ProvidersViews
{
    /// <summary>
    /// Interaction logic for ChangeProviderGoodsViewControl.xaml
    /// </summary>
    public partial class ChangeProviderGoodsViewControl : UserControl
    {
        public ChangeProviderGoodsViewControl()
        {
            InitializeComponent();
        }


        private DialogHost _dialogHost;
        private GoodsWrapper _goodsWrapper;
        private ProviderWrapper _providerWrapper;
        private Action _invokeAfterExecution;
        private Provider _provider;
        private List<Goods> _goodsList;
        private List<Goods> _selectedGoodsList;
        public ChangeProviderGoodsViewControl(DialogHost dialogHost, GoodsWrapper goodsWrapper,ProviderWrapper providerWrapper, Provider provider, Action invokeAfterExecution) : this()
        {
            _goodsList = new List<Goods>();

            _dialogHost = dialogHost;
            _goodsWrapper = goodsWrapper;
            _providerWrapper = providerWrapper;
            _provider = provider ?? throw new InvalidOperationException("Отримано пустого постачальника");
            _invokeAfterExecution = invokeAfterExecution;
            ProviderNameTextBlock.Text = provider.Name;

            InitiateGoodsDataGrid();
            InitiateSelectedGoodsDataGrid();
        }

        private void InitiateGoodsDataGrid()
        {
            _goodsWrapper.Sort();

            
            foreach (var goods in _goodsWrapper.GetAll().Convert())
            {
                var addGoodsToList = true;
                foreach (var providerGoods in _provider.GoodsList)
                {
                    if (providerGoods.Name==goods.Name)
                    {
                        addGoodsToList = false;
                    }
                }   
                if (addGoodsToList)
                    _goodsList.Add(goods);
            }

            GoodsDataGrid.ItemsSource = _goodsList;
        }
        private void InitiateSelectedGoodsDataGrid()
        {
            _goodsWrapper.Sort();
            _selectedGoodsList = _provider.GoodsList;
            
            SelectedGoodsDataGrid.ItemsSource = _selectedGoodsList;
        }


        private void AddToSelectedGoodsBtn_OnClick(object sender, RoutedEventArgs e)
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
            _providerWrapper.Update(_provider.Name,new DataTransferWrapper.Models.Provider(_provider.Name,_selectedGoodsList.Convert(),_provider.TelegramId));
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

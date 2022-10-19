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
using DataTransferWrapper.Models;
using MaterialDesignThemes.Wpf;
using WPFUI.ExtentionMethods;
using WPFUI.Models;
using WPFUI.PartialViews.GoodsViews;
using WPFUI.PartialViews.ProvidersViews;
using Provider = WPFUI.Models.Provider;

namespace WPFUI.PartialViews
{
    /// <summary>
    /// Interaction logic for ProvidersViewControl.xaml
    /// </summary>
    public partial class ProvidersViewControl : UserControl
    {
        GoodsWrapper _goodsWrapper = new();
        ProviderWrapper _providerWrapper = new();
        private List<ProviderUI> _providersList;
        public ProvidersViewControl()
        {
            InitializeComponent();
            if (_goodsWrapper.Length == 0)
            {
                throw new InvalidOperationException("Немає товарів, відповідно і немає постачальників!");
            }

            _providersList = _providerWrapper.GetAll().ConvertUI();
            ProviderDataGrid.ItemsSource = _providersList;
        }
        private void DelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            dynamic provider = ProviderDataGrid.SelectedItem;
            try
            {
                _providerWrapper.Remove(provider.Name);
            }
            catch (InvalidOperationException exception)
            {
                MessageBox.Show(exception.Message);
            }
            RefreshDataGrid();
        }

        private void AddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            DialogHost.Show(new AddProviderViewControl(ProviderDialogHost, _providerWrapper,_goodsWrapper, RefreshDataGrid));
        }
        private void RefreshDataGrid()
        {
            ProviderDataGrid.ItemsSource = null;
            _providerWrapper.Sort();
            _providersList = _providerWrapper.GetAll().ConvertUI();
            ProviderDataGrid.ItemsSource = _providersList;
        }

        private void GoodsDialogHost_OnDialogClosing(object sender, DialogClosingEventArgs eventargs)
        {
            RefreshDataGrid();
        }

        private void ChangeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var provider = (ProviderDataGrid.SelectedItem as ProviderUI)!;
            DialogHost.Show(new ChangeProviderGoodsViewControl(ProviderDialogHost, _goodsWrapper,_providerWrapper,provider.Convert(),RefreshDataGrid));
        }
    }
}

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

namespace WPFUI.PartialViews.GoodsViews
{
    /// <summary>
    /// Interaction logic for AddGoodsControl.xaml
    /// </summary>
    public partial class AddGoodsControl : UserControl
    {
        private DialogHost _dialogHost;
        private GoodsWrapper goodsWrapper;
        private Action _invokeAfterExecution;
        public AddGoodsControl(DialogHost dialogHost, GoodsWrapper goodsWrapper, Action invokeAfterExecution) :this()
        {
                this._dialogHost = dialogHost;
                this.goodsWrapper = goodsWrapper;
                this._invokeAfterExecution = invokeAfterExecution;
        }
        public AddGoodsControl()
        {
            InitializeComponent();
        }
        private void CancelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            CloseDialogControl();
        }

        private void CloseDialogControl()
        {
            _dialogHost.IsOpen=false;
            _invokeAfterExecution.Invoke();
        }

        private void AddGoodsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Goods goods=new("",0,0,0);

            try
            {
                goods.Name =     NameTextBox.Text;
                goods.Count =    Convert.ToInt32(CountTextBox.Text);
                goods.MinCount = Convert.ToInt32(MinCountTextBox.Text);
                goods.Price =    Convert.ToInt32(PriceTextBox.Text);
                goodsWrapper.Add(new(goods.Name, goods.Count, goods.MinCount, goods.Price));
            }
            catch (InvalidOperationException exception)
            {
                MessageBox.Show(exception.Message);
            }
            CloseDialogControl();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
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
using WPFUI.PartialViews;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            SimulateDbLoading();

            GoodsBtn.Focus();
            GoodsBtn_OnClick(null, null);

        }

        private void SimulateDbLoading()
        {
            GoodsWrapper goodsWrapper = new();
            ProviderWrapper providerWrapper = new();
            Goods goods1 = new("Товар1", 3, 2, 10);
            Goods goods2 = new("Товар2", 2, 2, 9);
            Goods goods3 = new("Товар3", 4, 3, 8);
            Goods goods4 = new("Товар4", 3, 4, 5);
            goodsWrapper.Add(goods1.Convert());;
            goodsWrapper.Add(goods2.Convert());;
            goodsWrapper.Add(goods3.Convert());;
            goodsWrapper.Add(goods4.Convert());;

            var goodsList1 = new List<Goods>
            {
                goods1,
                goods2,
                goods3,
                goods4
            };
            var tokenText1 = File.ReadAllText("C:\\SecureData\\tg1");
            providerWrapper.Add(new("Постачальник1",goodsList1.Convert(),Convert.ToInt32(tokenText1)));


            Goods goods5 = new("Товар5", 3, 2, 10);
            Goods goods6 = new("Товар6", 2, 2, 9);
            Goods goods7 = new("Товар7", 4, 3, 8);
            Goods goods8 = new("Товар8", 3, 4, 5);
            goodsWrapper.Add(goods5.Convert()); ;
            goodsWrapper.Add(goods6.Convert()); ;
            goodsWrapper.Add(goods7.Convert()); ;
            goodsWrapper.Add(goods8.Convert()); ;

            var goodsList2 = new List<Goods>
            {
                goods5,
                goods6,
                goods7,
                goods8
            };
            var tokenText2 = File.ReadAllText("C:\\SecureData\\tg2");
            providerWrapper.Add(new("Постачальник2", goodsList2.Convert(), Convert.ToInt32(tokenText2)));


            Goods goods9 = new("Товар9", 3, 2, 10);
            Goods goods10 = new("Товар10", 2, 2, 9);
            Goods goods11 = new("Товар11", 4, 3, 8);
            Goods goods12 = new("Товар12", 3, 4, 5);
            goodsWrapper.Add(goods9.Convert()); ;
            goodsWrapper.Add(goods10.Convert()); ;
            goodsWrapper.Add(goods11.Convert()); ;
            goodsWrapper.Add(goods12.Convert()); ;

            var goodsList3 = new List<Goods>
            {
                goods9,
                goods10,
                goods11,
                goods12
            };
            var tokenText3 = File.ReadAllText("C:\\SecureData\\tg3");
            providerWrapper.Add(new("Постачальник3", goodsList3.Convert(), Convert.ToInt32(tokenText3)));

            goodsWrapper.Sort();
            providerWrapper.Sort();
        }

        private void ForecastsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Container.Children.Clear();
            var viewControl = new ForecastViewControl();
            Container.Children.Add(viewControl);

            Title = "Прогнози";
        }

        private void StatisticBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Container.Children.Clear();
            var viewControl = new StatisticViewControl();
            Container.Children.Add(viewControl);
            Title = "Статистика";
        }

        private void ProvidersBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Container.Children.Clear();
            var viewControl = new ProvidersViewControl();
            Container.Children.Add(viewControl);
            Title = "Постачальники";
        }

        private void GoodsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Container.Children.Clear();
            var viewControl = new GoodsViewControl();
            Container.Children.Add(viewControl);
            Title = "Товари";
        }
    }
}

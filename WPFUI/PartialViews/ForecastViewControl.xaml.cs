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
using LiveCharts;
using LiveCharts.Wpf;
using MessageBroker.TelegramBroker;
using WPFUI.ExtentionMethods;

namespace WPFUI.PartialViews
{
    /// <summary>
    /// Interaction logic for ForecastViewControl.xaml
    /// </summary>
    public partial class ForecastViewControl : UserControl
    {
        class ProviderWihMessage
        {
            public long TelegramID { get; set; }
            public Tuple<string, int> OrderNameAndCount;
            public ProviderWihMessage(long telegramID, Tuple<string, int> orderNameAndCount)
            {
                TelegramID = telegramID;
                OrderNameAndCount = orderNameAndCount;
            }

            public string Message => "Hello! We need  " + OrderNameAndCount.Item1 + "  in  " + OrderNameAndCount.Item2+"  samples";
        }
        public ForecastViewControl()
        {
            InitializeComponent();

            PointLabel = chartPoint =>
                $"{chartPoint.Y} ({chartPoint.Participation:P})";

            DataContext = this;


            var goodsWrapper = new GoodsWrapper();
            var needsOrderGoodsCount = goodsWrapper.GetAll().Where(x => x.Count < x.MinCount).Count();
            var alreadyPlacedGoodsCount = goodsWrapper.GetAll().Count - needsOrderGoodsCount;

            NeedsOrderPieSeries.Values.Clear();
            NeedsOrderPieSeries.Values.Add((double)needsOrderGoodsCount);
            AlreadyPlacedPieSeries.Values.Clear();
            AlreadyPlacedPieSeries.Values.Add((double)alreadyPlacedGoodsCount);

            OrderMissingGoodsBtn.IsEnabled = needsOrderGoodsCount > 0;
        }

        private void OrderMissingBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var goodsWrapper = new GoodsWrapper();
            var providersWrapper = new ProviderWrapper();
            List<ProviderWihMessage> providersWihMessages = new();
            foreach (var goods in goodsWrapper.GetAll().Convert())
            {
                if (goods.MinCount>goods.Count)
                {
                    foreach (var provider in providersWrapper.GetAll().Convert())
                    {
                        if (provider.GoodsList.Convert().Exists(x=>x.Name==goods.Name))
                        {
                            providersWihMessages.Add(new ProviderWihMessage(provider.TelegramId,new Tuple<string,int>(goods.Name, goods.MinCount - goods.Count)));
                        }
                    }
                }
            }

            foreach (var providerWihMessage in providersWihMessages)
            {
                Broker broker = new Broker();
                broker.DeliverThisMessage(providerWihMessage.TelegramID,providerWihMessage.Message);
            }



            MessageBox.Show("Замовлення доставлено успішно!");
        }
        public Func<ChartPoint, string> PointLabel { get; set; }
        private void Chart_OnDataClick(object sender, ChartPoint chartpoint)
        {
            var chart = (LiveCharts.Wpf.PieChart)chartpoint.ChartView;

            //clear selected slice.
            foreach (PieSeries series in chart.Series)
                series.PushOut = 0;

            var selectedSeries = (PieSeries)chartpoint.SeriesView;
            selectedSeries.PushOut = 8;
        }
    }
}

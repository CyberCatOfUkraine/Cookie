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
        }

        private void ForecastsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Container.Children.Clear();
            var viewControl = new ForecastViewControl();
            Container.Children.Add(viewControl);
        }

        private void StatisticBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Container.Children.Clear();
            var viewControl = new StatisticViewControl();
            Container.Children.Add(viewControl);
        }

        private void ProvidersBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Container.Children.Clear();
            var viewControl = new ProvidersViewControl();
            Container.Children.Add(viewControl);
        }

        private void GoodsBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Container.Children.Clear();
            var viewControl = new GoodsViewControl();
            Container.Children.Add(viewControl);
        }
    }
}

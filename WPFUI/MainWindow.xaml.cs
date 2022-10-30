using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Mapsui;
using Mapsui.Geometries;
using Mapsui.Projection;
using Mapsui.UI.Wpf;
using Mapsui.Utilities;
using WPFUI.PartialViews;
using WPFUI.PartialViews.Employees;
using WPFUI.PartialViews.Map;
using WPFUI.PartialViews.Message;
using WPFUI.PartialViews.Statistic;

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

            TasksBtn.Focus();
            TasksBtn_OnClick(null, null);

        }
        private void TasksBtn_OnClick(object sender, RoutedEventArgs e)
        {
            
            AddViewControlToContainer(new TasksViewControl(), "Задачі");
        }
        private void EmployeesBtn_OnClick(object sender, RoutedEventArgs e)
        {
            AddViewControlToContainer(new EmployeesViewControl(),"Співробітники");
        }

        private void MapBtn_OnClick(object sender, RoutedEventArgs e)
        {

            AddViewControlToContainer(new MapViewControl(), "Карта");
        }

        private void StatisticBtn_OnClick(object sender, RoutedEventArgs e)
        {
            AddViewControlToContainer(new StatisticViewControl(),"Статистика");
        }

        private void MessageBtn_OnClick(object sender, RoutedEventArgs e)
        {
            AddViewControlToContainer(new MessageViewControl(),"Повідомлення");
        }
        private void AddViewControlToContainer(UIElement viewControl, string title)
        {
            Container.Children.Clear();
            Container.Children.Add(viewControl);
            Title = title;
        }
    }
}

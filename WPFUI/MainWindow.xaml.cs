using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using DatabaseBroker;
using WPFUI.LoadingWindow;
using WPFUI.PartialViews;
using WPFUI.PartialViews.Accesses;
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
        private UnitOfCookie _unitOfCookie;
        public MainWindow(UnitOfCookie unitOfCookie)
        {
            InitializeComponent();

            TasksBtn.Focus();
            TasksBtn_OnClick(null, null);
            _unitOfCookie = unitOfCookie;
        }

        private void TasksBtn_OnClick(object sender, RoutedEventArgs e)
        {
            
            AddViewControlToContainer(new TasksViewControl(), "Задачі");
        }
        private void EmployeesBtn_OnClick(object sender, RoutedEventArgs e)
        {
            AddViewControlToContainer(new EmployeesViewControl(_unitOfCookie),"Співробітники");
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
            AddViewControlToContainer(new MessageViewControl(_unitOfCookie),"Повідомлення");
        }
        private void AddViewControlToContainer(UIElement viewControl, string title)
        {
            Container.Children.Clear();
            Container.Children.Add(viewControl);
            Title = title;
        }
        private void AccessesBtn_OnClick(object sender, RoutedEventArgs e)
        {
            AddViewControlToContainer(new AccessesViewControl(_unitOfCookie),"Допуски співробітників");
        }

        public event Action FinalyDataLoadded;
    }
}

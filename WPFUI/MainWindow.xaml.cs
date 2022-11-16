using System;
using System.Windows;
using DatabaseBroker;
using MessageBroker.TelegramBroker;
using WPFUI.PartialViews;
using WPFUI.PartialViews.Accesses;
using WPFUI.PartialViews.Employees;
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
            _unitOfCookie = unitOfCookie;

            InitializeComponent();

            TasksBtn.Focus();
            TasksBtn_OnClick(null, null);

            broker = new Broker();
            broker.LoadTelegramBot();
        }

        private Broker broker;

        ~MainWindow()
        {
         GC.KeepAlive(broker);   
        }
        private void TasksBtn_OnClick(object sender, RoutedEventArgs e)
        {
            
            AddViewControlToContainer(new TasksViewControl(_unitOfCookie,broker), "Задачі");
        }
        private void EmployeesBtn_OnClick(object sender, RoutedEventArgs e)
        {
            AddViewControlToContainer(new EmployeesViewControl(_unitOfCookie),"Співробітники");
        }
        private void StatisticBtn_OnClick(object sender, RoutedEventArgs e)
        {
            AddViewControlToContainer(new StatisticViewControl(_unitOfCookie),"Статистика");
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
    }
}

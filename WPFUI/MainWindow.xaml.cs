using System;
using System.Configuration;
using System.Windows;
using Cookie;
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

            var settingsManager = new SettingsManager();
            if (settingsManager.EngineerTelegramID==-1)
            {
                MessageBox.Show("Задайте TelegramID інженера (ваш) в файлі: " + settingsManager.EngineerTelegramidFileName);
                Close();
            }
            broker = new Broker(_unitOfCookie);
            broker.OnExceptionAction += OnBrokerException;
            broker.SetEngineerTelegramID(settingsManager.EngineerTelegramID);
            broker.LoadTelegramBot();
            broker.SendMessage(settingsManager.EngineerTelegramID,"Бот запущено!");
        }

        /// <summary>
        /// Оброблює винятки що отримана в процесі використання чат-боту 
        /// </summary>
        /// <param name="e"></param>
        /// <param name="isEngineer">Чи є користувач інженером</param>
        /// <param name="isElectrician">Чи є користувач електриком</param>
        /// <param name="credentials">Прізвище Ім'я По-батькові</param>
        private void OnBrokerException(Exception e,bool isEngineer,bool isElectrician,string credentials)
        {
            var isBotBlockedByUser= e.Message == "Forbidden: bot was blocked by the user";

            if (!isBotBlockedByUser&&credentials == string.Empty&&!isEngineer&&!isElectrician)
            {
                MessageBox.Show("В процесі роботи чат-боту виникла помилка: "+e.Message);
                return;
            }
            if (isBotBlockedByUser)
            {
                if (isEngineer)
                {
                    MessageBox.Show(
                        "Взаємодія чат-бота з вашим обліковим записом була заблокована вашим обліковим записом");
                    return;
                }

                if (isElectrician)
                {
                    MessageBox.Show(
                        "Взаємодія чат-бота з обліковим записом електрика була заблокована його обліковим записом\n" +
                        $"ПІБ: {credentials}");
                    return;
                }
            }

            MessageBox.Show(e.Message+"\n\n"+e.StackTrace);
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

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
using Models;
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


            TasksBtn.Focus();
            TasksBtn_OnClick(null, null);

        }

        private void TasksBtn_OnClick(object sender, RoutedEventArgs e)
        {
            
            AddViewControlToContainer(new TasksViewControl(), "Задачі");
        }
        private void EmployeesBtn_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void MapBtn_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void StatisticBtn_OnClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void MessageBtn_OnClick(object sender, RoutedEventArgs e)
        {
            
        }
        private void AddViewControlToContainer(UIElement viewControl, string title)
        {
            Container.Children.Clear();
            Container.Children.Add(viewControl);
            Title = title;
        }
    }
}

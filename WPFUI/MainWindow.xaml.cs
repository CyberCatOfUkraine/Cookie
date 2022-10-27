using System.Windows;
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

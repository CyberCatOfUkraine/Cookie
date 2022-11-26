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
using DatabaseBroker;
using MessageBroker.TelegramBroker;
using WPFUI.ExtentionMethods;
using WPFUI.Models;

namespace WPFUI.PartialViews
{
    /// <summary>
    /// Interaction logic for TasksViewControl.xaml
    /// </summary>
    public partial class TasksViewControl : UserControl
    {
        private readonly UnitOfCookie _unitOfCookie;
        private readonly Broker _broker;
        public TasksViewControl(UnitOfCookie unitOfCookie, Broker broker)
        {

            _unitOfCookie = unitOfCookie;
            _broker = broker;
            InitializeComponent();
            
        }


        private void AssignEmployeeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(new Random().Next(-2, 2) > 0
                ? "Задачу призначено вільного співробітника"
                : "Задачу перепризначено  на вільного співробітника ");
        }
    }
}

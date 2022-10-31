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
using WPFUI.ExtentionMethods;
using WPFUI.Models;

namespace WPFUI.PartialViews.Accesses
{
    /// <summary>
    /// Interaction logic for AddAccessViewControl.xaml
    /// </summary>
    public partial class AddAccessViewControl : UserControl
    {
        public Action RemoveThisControl;
        private UnitOfCookie uof;

        public AddAccessViewControl(UnitOfCookie uof)
        {
            InitializeComponent();
            this.uof = uof;
        }
        private void AddAccessBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Access access = new Access(NameTextBox.Text);
            uof.AccessRepository.Create(access.Convert());
            RemoveThisControl.Invoke();
        }

        private void CancelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            RemoveThisControl.Invoke();
        }
        
    }
}

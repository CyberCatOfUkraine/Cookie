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
using WPFUI.Models;

namespace WPFUI.PartialViews.Message
{
    /// <summary>
    /// Interaction logic for ProcessMessageViewControl.xaml
    /// </summary>
    public partial class ProcessMessageViewControl : UserControl
    {
        private readonly UnitOfCookie _unitOfCookie;
        private readonly ClientMessage _clientMessage;
        public Action RemoveThisControl;
        public ProcessMessageViewControl(UnitOfCookie unitOfCookie,ClientMessage clientMessage)
        {
            _unitOfCookie = unitOfCookie;
            _clientMessage = clientMessage;
            InitializeComponent();
            MessageTextBlock.Text = clientMessage.Text;
            foreach (var generalAccess in _unitOfCookie.GeneralAccessRepository.GetAll())
            {
                AccessesComboBox.Items.Add(generalAccess);
            }
            
            
        }

        private void SubmitBtn_OnClick(object sender, RoutedEventArgs e)
        {

            CloseThisControl();
        }

        private void CancelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            CloseThisControl();
        }

        private void CloseThisControl()
        {
            RemoveThisControl.Invoke();
        }
        
        private void Access_Selected(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            MessageBox.Show(selectedItem.Content.ToString());
        }
    }
}

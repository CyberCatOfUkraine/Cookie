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
using ClientMessage = WPFUI.Models.ClientMessage;

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
        private string accessName;
        public ProcessMessageViewControl(UnitOfCookie unitOfCookie,ClientMessage clientMessage)
        {
            _unitOfCookie = unitOfCookie;
            _clientMessage = clientMessage;
            InitializeComponent();
            MessageTextBlock.Text = clientMessage.Text;
            
            foreach (var generalAccess in _unitOfCookie.GeneralAccessRepository.GetAll())
            {
                AccessesComboBox.Items.Add(generalAccess.Name);
            }

            if (AccessesComboBox.Items.Count>0)
            {
                AccessesComboBox.SelectedItem = AccessesComboBox.Items[0];
            }
            
        }

        private void SubmitBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (accessName==null)
            {
                MessageBox.Show("Неможливе подальше оброблення повідомлення без допусків.");
                return;
            }
            _unitOfCookie.ClientMessageRepository.Create(_clientMessage.Convert());
            WorkTask task = new(MessageTextBlock.Text);
            task.Started=DateTime.UnixEpoch;
            task.Finished=DateTime.UnixEpoch;
            task.Addresses = new List<Address>() { _clientMessage.Address };
            task.CurrentState = TaskState.Created;
            task.AssignedEmployeesAccesses = new () { new Access(accessName) };
            _unitOfCookie.WorkTaskRepository.Create(task.Convert());
            _unitOfCookie.WorkTaskRepository.SaveChanges();
            
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
            accessName= comboBox.SelectedItem.ToString();
        }
    }
}

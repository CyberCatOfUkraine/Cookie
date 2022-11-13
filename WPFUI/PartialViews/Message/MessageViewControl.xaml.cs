using DatabaseBroker;
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
using WPFUI.Comparator;
using WPFUI.ExtentionMethods;
using WPFUI.Models;
using WPFUI.PartialViews.Employees;
using Cookie;
using Newtonsoft.Json;

namespace WPFUI.PartialViews.Message
{
    /// <summary>
    /// Interaction logic for MessageViewControl.xaml
    /// </summary>
    public partial class MessageViewControl : UserControl
    {

        private List<ClientMessage> _clientMessages;
        private UnitOfCookie _unitOfCookie;
        private DialogGod<ClientMessage> dialog;
        public MessageViewControl(UnitOfCookie unitOfCookie)
        {
            InitializeComponent();
            _unitOfCookie = unitOfCookie;
            _clientMessages = /*_unitOfCookie.ClientMessageRepository.Count() > 0 ? _unitOfCookie.ClientMessageRepository.GetAll().Convert() :*/ new List<ClientMessage>();
            string[] files; 
            files=MessagesTextReader.ReadFromFiles().Clone() as string[];

            foreach (var file in files)
            {
                try
                {
                    _clientMessages.Add(JsonConvert.DeserializeObject<ClientMessage>(file));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            ClientMessagesDataGrid.ItemsSource = _clientMessages;
            dialog = new DialogGod<ClientMessage>(new ClientMessageComparator());
        }

        private void ProcessMessageBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var addControl = new ProcessMessageViewControl(_unitOfCookie, ClientMessagesDataGrid.CurrentItem as ClientMessage);
            addControl.RemoveThisControl += RemoveAddControl;

            dialog.Create(addControl);
        }

        private void RemoveAddControl()
        {
            _clientMessages = _unitOfCookie.ClientMessageRepository.GetAll().Convert();
            dialog.Kill(ClientMessagesDialogHost, ClientMessagesDataGrid, _clientMessages);
        }
    }
}

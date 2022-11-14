using DatabaseBroker;
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
        private Dictionary<ClientMessage, string> _deletableFilesDictionary;
        private UnitOfCookie _unitOfCookie;
        private DialogGod<ClientMessage> dialog;
        public MessageViewControl(UnitOfCookie unitOfCookie)
        {
            InitializeComponent();
            _unitOfCookie = unitOfCookie;
            _clientMessages = new List<ClientMessage>();
            _deletableFilesDictionary = new();
            string[] files; 
            files=MessagesTextReader.ReadFromFiles().Clone() as string[];

            for (var index = 0; index < files.Length; index++)
            {
                var file = files[index];
                try
                {
                    var message = JsonConvert.DeserializeObject<ClientMessage>(file);
                    _clientMessages.Add(message);
                    _deletableFilesDictionary.Add(message, MessagesTextReader.DeletableFilesList[index]);
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
            var message = ClientMessagesDataGrid.CurrentItem as ClientMessage;
            var addControl = new ProcessMessageViewControl(_unitOfCookie, message);

            messsage = message;
            addControl.RemoveThisControl += RemoveAddControl;

            dialog.Create(addControl);
        }

        private ClientMessage messsage;
        private void RemoveAddControl()
        {
            List<WorkTask> taskList= _unitOfCookie.WorkTaskRepository.GetAll().Convert();
            if (taskList.Exists(x=>x.Name==messsage.Text))
            {
               var pair= _deletableFilesDictionary.First(x=>x.Key.Text==messsage.Text);
               _clientMessages.Remove(pair.Key);
               _deletableFilesDictionary.Remove(pair.Key);
               File.Delete(pair.Value);
            }
            dialog.Kill(ClientMessagesDialogHost, ClientMessagesDataGrid, _clientMessages);
        }
    }
}

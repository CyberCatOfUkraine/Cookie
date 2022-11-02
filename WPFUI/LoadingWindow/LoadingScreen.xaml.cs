using DatabaseBroker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFUI.LoadingWindow
{
    /// <summary>
    /// Interaction logic for LoadingScreen.xaml
    /// </summary>
    public partial class LoadingScreen : Window
    {
        private MainWindow _mainWindow;
        private UnitOfCookie _unitOfCookie;

        public LoadingScreen()
        {
            InitializeComponent();

            _unitOfCookie = new UnitOfCookie();
            WindowStyle = WindowStyle.None;
            CheckForDbEmpty();

        }

        private async void CheckForDbEmpty()
        {

            int entitiesCount=0;

            entitiesCount += await Task.Run(() => _unitOfCookie.AccessRepository.Count());
            entitiesCount += await Task.Run(() => _unitOfCookie.AddressRepository.Count());
            entitiesCount += await Task.Run(() => _unitOfCookie.ClientMessageRepository.Count());
            entitiesCount += await Task.Run(() => _unitOfCookie.EmployeeRepository.Count());
            entitiesCount += await Task.Run(() => _unitOfCookie.PauseRepository.Count());
            entitiesCount += await Task.Run(() => _unitOfCookie.WorkTaskRepository.Count());

            if (entitiesCount == 0)
            {
                Console.WriteLine("Database is empty!");
            }

            ContinueProgramLoading();
        }


        private void ContinueProgramLoading()
        {
            this.Hide();
            _mainWindow = new MainWindow(_unitOfCookie);
            _mainWindow.Show();
            this.Close();
        }

        ~LoadingScreen()
        {
            GC.KeepAlive(_mainWindow);
        }
    }
}

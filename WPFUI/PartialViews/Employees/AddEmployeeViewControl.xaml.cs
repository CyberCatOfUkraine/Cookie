using System;
using System.Collections.Generic;
using System.Globalization;
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
using DatabaseBroker.Models;
using WPFUI.Comparator;
using WPFUI.ExtentionMethods;
using WPFUI.Models;
using Access = WPFUI.Models.Access;
using Employee = WPFUI.Models.Employee;

namespace WPFUI.PartialViews.Employees
{
    /// <summary>
    /// Interaction logic for AddEmployeeViewControl.xaml
    /// </summary>
    public partial class AddEmployeeViewControl : UserControl
    {
        public Action RemoveThisControl;
        private List<Access> _selectedAccesses;
        private List<Access> _allAccesses;
        private UnitOfCookie _unitOfCookie;
        public AddEmployeeViewControl(UnitOfCookie unitOfCookie)
        {
            _unitOfCookie = unitOfCookie;
            InitializeComponent();
            InputAcessesGrid.Visibility = Visibility.Hidden;
            InputNameGrid.Visibility = Visibility.Visible;
            _allAccesses = _unitOfCookie.GeneralAccessRepository.GetAll().Convert();
            _selectedAccesses = new();
            EmployeeAccessesDataGrid.ItemsSource =_allAccesses;
        }

        private bool IsProcessed = false;
        private void AddEmployeeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Неможливо створити співробітника з пустими даними!");
                return;
            }
            
            if (string.IsNullOrEmpty(TelegramIdTextBox.Text))
            {
                MessageBox.Show("Неможливо створити співробітника без Telegram ID!");
                return;
            }

            if (!long.TryParse(TelegramIdTextBox.Text,out var telegramId))
            {
                MessageBox.Show("Введіть Telegram ID цифрами!");
                return;
            }

            if (_unitOfCookie.EmployeeRepository.Get(x=>x.Credentials== NameTextBox.Text)!=null)
            {
                MessageBox.Show("Неможливо додати ще одного співробітника з ідентичними даними!");
                CloseThisControl();
                return;
            }
            var text = _selectedAccesses.Count == 0 ? "Ви точно збираєтесь створити нового співробітника з ВІДСУТНІМИ ДОПУСКАМИ ?" : "Ви точно збираєтесь створити нового співробітника ?";
            var messageBoxResult = MessageBox.Show(text, "Створення допуску", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                if (!IsProcessed)
                {
                    Employee employee = new Employee(NameTextBox.Text, _selectedAccesses, telegramId);
                    _unitOfCookie.EmployeeRepository.Create(employee.Convert());
                    _unitOfCookie.EmployeeRepository.SaveChanges();
                }

                IsProcessed = true;
            }
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
        
        private void AddAccessesToEmployeeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            InputNameGrid.Visibility = Visibility.Hidden;
            InputAcessesGrid.Visibility = Visibility.Visible;
        }
        
        
        private void SubmitAccessesBtn_OnClick(object sender, RoutedEventArgs e)
        {
            InputNameGrid.Visibility = Visibility.Visible;
            InputAcessesGrid.Visibility = Visibility.Hidden;
            
        }

        private void ChooseButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var access= EmployeeAccessesDataGrid.CurrentItem as Access;
            if (_selectedAccesses.Exists(x=>x.Name==access.Name))
            {
                if (sender is Button btn) btn.Content = "Вибрати";
                if (access != null) _selectedAccesses.Remove(access);
            }
            else
            {
                if (sender is Button btn) btn.Content = "Зняти вибір";
                if (access != null) _selectedAccesses.Add(access);
            }
        }
    }
}

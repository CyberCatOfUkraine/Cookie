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
using WPFUI.ExtentionMethods;
using WPFUI.Models;

namespace WPFUI.PartialViews.Employees
{
    /// <summary>
    /// Interaction logic for ChangeEmployeeAcessesViewControl.xaml
    /// </summary>
    public partial class ChangeEmployeeAcessesViewControl : UserControl
    {
        
                private List<Access> _selectedAccesses;
                private UnitOfCookie _unitOfCookie;

                private Employee _employee;
                /*public AddEmployeeViewControl(UnitOfCookie unitOfCookie)
                {
                    _unitOfCookie = unitOfCookie;
                    InitializeComponent();
                    InputAcessesGrid.Visibility = Visibility.Hidden;
                    InputNameGrid.Visibility = Visibility.Visible;
                    EmployeeAccessesDataGrid.ItemsSource =_allAccesses;
                }*/
         
        public Action RemoveThisControl;
        public ChangeEmployeeAcessesViewControl(UnitOfCookie unitOfCookie,Employee employee)
        {
            _unitOfCookie = unitOfCookie;
            _employee=employee;
            List<Access> allAccesses = _unitOfCookie.GeneralAccessRepository.GetAll().Convert();
            _selectedAccesses = new();

            InitializeComponent();
            EmployeeAccessesDataGrid.ItemsSource = allAccesses;
            
        }

        private void ChooseButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var access = EmployeeAccessesDataGrid.CurrentItem as Access;
            if (_selectedAccesses.Exists(x => x.Name == access.Name))
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

        private void SubmitEmployeeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var text = _selectedAccesses.Count == 0 ? "Ви точно збираєтесь ОБНУЛИТИ ДОПУСКИ ?" : "Ви точно збираєтесь змінити допуски співробітника ?";
            var messageBoxResult = MessageBox.Show(text, "Редагування допуску", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                
                var employeeDatabase = _unitOfCookie.EmployeeRepository.Get(x => x.Credentials == _employee.Credentials);
                if (employeeDatabase != null && employeeDatabase.Accesses.Count > 0)
                {
                    foreach (var access in employeeDatabase.Accesses)
                    {
                        _unitOfCookie.AccessRepository.RemoveBy(x => x.Id == access.Id);
                    }
                }


                _employee.Accesses = _selectedAccesses;
                _unitOfCookie.EmployeeRepository.Update(x=>x.Id==_employee.Id,_employee.Convert());
                _unitOfCookie.EmployeeRepository.SaveChanges();
            }
            CloseThisControl();
        }

        private void CloseThisControl()
        {
            RemoveThisControl.Invoke();
        }
        private void CancelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            CloseThisControl();
        }
    }
}

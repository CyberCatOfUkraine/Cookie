using DatabaseBroker;
using DatabaseBroker.Models;
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
using Employee = WPFUI.Models.Employee;

namespace WPFUI.PartialViews.Employees
{
    /// <summary>
    /// Interaction logic for EmployeesViewControl.xaml
    /// </summary>
    public partial class EmployeesViewControl : UserControl
    {
        private List<Employee> _employees;
        private UnitOfCookie _unitOfCookie;
        private readonly Action _updateEmployees;

        public EmployeesViewControl(UnitOfCookie unitOfCookie, Action updateEmployees)
        {
            InitializeComponent();
            _unitOfCookie = unitOfCookie;
            _updateEmployees = updateEmployees;
            _employees = _unitOfCookie.EmployeeRepository.Count() > 0 ? _unitOfCookie.EmployeeRepository.GetAll().Convert() : new List<Employee>();
            EmployeesDataGrid.ItemsSource = _employees;
            dialog = new DialogGod<Employee>(new EmployeeComparator());
        }

        private DialogGod<Employee> dialog;
        private void AddEmployeeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var addControl = new AddEmployeeViewControl(_unitOfCookie);
            addControl.RemoveThisControl += RemoveAddControl;

            dialog.Create(addControl);
            _updateEmployees.Invoke();
        }

        private void RemoveAddControl()
        {
            _employees=_unitOfCookie.EmployeeRepository.GetAll().Convert();
            dialog.Kill(EmployeeDialogHost,EmployeesDataGrid,_employees);
            _updateEmployees.Invoke();
        }

        private void DeleteEmployeeBtn_OnClick(object sender, RoutedEventArgs e)
        {


            _unitOfCookie.EmployeeRepository.SaveChanges();
            _unitOfCookie.AccessRepository.SaveChanges();
            var employee = EmployeesDataGrid.CurrentItem as Employee;
            var employeeDatabase = _unitOfCookie.EmployeeRepository.Get(x => x.Credentials == employee.Credentials);
            if (employeeDatabase != null && employeeDatabase.Accesses.Count>0)
            {
                var accesses = employeeDatabase.Accesses.Convert();
                foreach (var access in accesses)
                {
                    _unitOfCookie.AccessRepository.RemoveBy(x=>x.Id==access.Id);
                }
            }
            
            _unitOfCookie.EmployeeRepository.RemoveBy(x=>x.Id==employee.Id);
            _employees.Remove(employee);
            EmployeesDataGrid.ItemsSource = null;
            EmployeesDataGrid.ItemsSource = _employees;

            _unitOfCookie.EmployeeRepository.SaveChanges();
            _unitOfCookie.AccessRepository.SaveChanges();
            _updateEmployees.Invoke();
        }

        private void ChangeEmployeeAccessesBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var employee = EmployeesDataGrid.CurrentItem as Employee;
            var control = new ChangeEmployeeAcessesViewControl(_unitOfCookie, employee);
            control.RemoveThisControl += RemoveChangeControl;
            dialog.Create(control);
        }

        private void RemoveChangeControl()
        {
            _employees = _unitOfCookie.EmployeeRepository.GetAll().Convert();
            dialog.Kill(EmployeeDialogHost, EmployeesDataGrid, _employees);
            _updateEmployees.Invoke();
        }
    }
}

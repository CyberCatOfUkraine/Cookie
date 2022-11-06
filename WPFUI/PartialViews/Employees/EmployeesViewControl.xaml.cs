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
        public EmployeesViewControl(UnitOfCookie unitOfCookie)
        {
            InitializeComponent();
            _unitOfCookie = unitOfCookie;
            //accesses = uof.AccessRepository.Count() > 0 ? uof.AccessRepository.GetAll().Convert() : new List<Access>();
            
            _employees = _unitOfCookie.EmployeeRepository.Count() > 0 ? _unitOfCookie.EmployeeRepository.GetAll().Convert() : new List<Employee>();
            EmployeesDataGrid.ItemsSource = _employees;
        }

        private DialogGod<Employee> dialog;
        private void AddEmployeeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            dialog = new DialogGod<Employee>(new EmployeeComparator());
            var addControl = new AddEmployeeViewControl(_unitOfCookie);
            addControl.RemoveThisControl += RemoveAddControl;

            dialog.Create(AddEmployeeDialogHost,addControl);
        }

        private void RemoveAddControl()
        {
            _employees=_unitOfCookie.EmployeeRepository.GetAll().Convert();
            dialog.Kill(AddEmployeeDialogHost,EmployeesDataGrid,_employees);
        }
    }
}

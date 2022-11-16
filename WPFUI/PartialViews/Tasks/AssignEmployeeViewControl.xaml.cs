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
using WPFUI.Models;

namespace WPFUI.PartialViews.Tasks
{
    /// <summary>
    /// Interaction logic for AssignEmployeeViewControl.xaml
    /// </summary>
    public partial class AssignEmployeeViewControl : UserControl
    {
        private readonly List<Employee> _employeesList;
        public Action<Employee> RemoveThisControl;
        private Employee _selectedEmployee;

        public AssignEmployeeViewControl(List<Employee> employeesList)
        {
            _employeesList = employeesList;
            InitializeComponent();


            foreach (var employee in employeesList)
            {
                EmployeeComboBox.Items.Add(employee.Credentials);
            }

            if (EmployeeComboBox.Items.Count > 0)
            {
                EmployeeComboBox.SelectedItem = EmployeeComboBox.Items[0];
            }
        }

        private void EmployeeComboBox_OnSelectionChanged_Selected(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            _selectedEmployee = _employeesList.Find(x=>x.Credentials== comboBox.SelectedItem.ToString());
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
            RemoveThisControl.Invoke(_selectedEmployee);
        }

    }
}

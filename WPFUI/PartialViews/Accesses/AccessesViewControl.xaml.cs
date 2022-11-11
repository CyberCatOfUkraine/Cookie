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
using DatabaseBroker.Repository;
using MaterialDesignThemes.Wpf;
using WPFUI.Comparator;
using WPFUI.Models;
using WPFUI.ExtentionMethods;

namespace WPFUI.PartialViews.Accesses
{
    /// <summary>
    /// Interaction logic for AccessesViewControl.xaml
    /// </summary>
    public partial class AccessesViewControl : UserControl
    {
        private UnitOfCookie uof;
        
        public AccessesViewControl(UnitOfCookie unitOfCookie)
        {
            InitializeComponent();
            uof = unitOfCookie;

            accesses = uof.GeneralAccessRepository.Count() > 0 ? uof.GeneralAccessRepository.GetAll().Convert() : new List<Access>();
            AccessesDataGrid.ItemsSource = accesses;
        }

        private List<Access> accesses;
        private void AccessesDataGrid_OnPreparingCellForEdit(object? sender, DataGridPreparingCellForEditEventArgs e)
        {
            var messageBoxResult= MessageBox.Show("Увага! Ви збираєтесь редагувати допуск!\nВи точно бажаєте продовжити?","Редагування допуску",MessageBoxButton.YesNo);

            if (messageBoxResult is MessageBoxResult.No or MessageBoxResult.Cancel)
            {
                AccessesDataGrid.CancelEdit();
            }
        }

        private void AccessDataGrid_OnCellEditEnding(object? sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditingElement is TextBox textBox)
            {
                
                var dataContext = e.Row.DataContext as Access;
                var name = dataContext!.Name;
                var access = accesses.First(x=>x.Name==name);
                access.Name = textBox.Text;
                uof.GeneralAccessRepository.Update(x=>x.Name==name,access.ConvertToDatabaseGeneralAcess());
                uof.GeneralAccessRepository.SaveChanges();
            }
        }

        private void DelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Access  access=AccessesDataGrid.SelectedItem as Access;
            if (access!=null)
            {


                var employeesWithThisAccess = EmployeesWithThisAccess(access);
                switch (employeesWithThisAccess.Count)
                {
                    case 1:
                        MessageBox.Show("Даний допуск не може бути видалений!\n Спочатку видаліть співробітника:\n" + employeesWithThisAccess.First().Credentials);
                        break;
                    case > 1:
                    {
                        var sb = new StringBuilder();
                        for (var index = 0; index < employeesWithThisAccess.Count; index++)
                        {
                            var employee = employeesWithThisAccess[index];
                            var number = index + 1;
                            sb.AppendLine(number+")   "+employee.Credentials);
                        }

                        MessageBox.Show("Даний допуск не може бути видалений!\n Спочатку видаліть співробітників:\n" + sb);
                        break;
                    }
                    case 0:
                        uof.GeneralAccessRepository.RemoveBy(x => x.Name == access.Name);
                        AccessesDataGrid.ItemsSource = null;
                        accesses.Remove(access);
                        AccessesDataGrid.ItemsSource = accesses;
                        uof.GeneralAccessRepository.SaveChanges();
                        break;
                }
            }
        }
        DialogGod<Access> dialogGod = new(new AccessComparator());
        
        private void AddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var addControl = new AddAccessViewControl(uof);
            addControl.RemoveThisControl+=RemoveAddControl;
            dialogGod.Create(addControl);
        }

        private void RemoveAddControl()
        {
            accesses = uof.GeneralAccessRepository.GetAll().Convert();
            dialogGod.Kill(AddAccessesDialogHost, AccessesDataGrid, accesses);
        }

        private List<Employee> EmployeesWithThisAccess(Access access)
        {
            return (from employee in uof.EmployeeRepository.GetAll() where employee.Accesses.Exists(x => x.Name == access.Name) select employee.Convert()).ToList();
        }

        private void AccessesViewControl_OnUnloaded(object sender, RoutedEventArgs e)
        {
            uof.AccessRepository.SaveChanges();
        }
    }
}

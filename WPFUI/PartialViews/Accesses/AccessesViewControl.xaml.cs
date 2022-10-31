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
using MaterialDesignThemes.Wpf;
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

            accesses = uof.AccessRepository.Count() > 0 ? uof.AccessRepository.GetAll().Convert() : new List<Access>();
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
                MessageBox.Show($"id:{access.Id}\nname: {access.Name}\n");
            }
        }

        private void DelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            Access  access=AccessesDataGrid.SelectedItem as Access;
            if (access!=null)
            {
                
            }
        }
        DialogGod<Access> dialogGod = new();
        
        private void AddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var addControl = new AddAccessViewControl(uof);
            addControl.RemoveThisControl+=RemoveAddControl;
            dialogGod.Create(AddAccessesDialogHost,addControl);
            
        }

        private void RemoveAddControl()
        {
            dialogGod.Kill(AddAccessesDialogHost, AccessesDataGrid, accesses);
        }
    }
}

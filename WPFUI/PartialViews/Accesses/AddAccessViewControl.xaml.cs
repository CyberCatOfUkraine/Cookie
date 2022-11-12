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
using WPFUI.ExtentionMethods;
using WPFUI.Models;

namespace WPFUI.PartialViews.Accesses
{
    /// <summary>
    /// Interaction logic for AddAccessViewControl.xaml
    /// </summary>
    public partial class AddAccessViewControl : UserControl
    {
        public Action RemoveThisControl;
        private UnitOfCookie uof;

        public AddAccessViewControl(UnitOfCookie uof)
        {
            InitializeComponent();
            this.uof = uof;
        }

        private bool IsProcessed = false;
        private void AddAccessBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(NameTextBox.Text))
            {
                MessageBox.Show("Неможливо створити пустий допуск!");
                return;
            }

            Access access = new Access(NameTextBox.Text);
            if (IsDuplicated(access))
            {
                MessageBox.Show("Неможливо додати дублікат!");
                RemoveThisControl.Invoke();
                return;
            }

            var messageBoxResult = MessageBox.Show("Ви точно збираєтесь створити новий допуск", "Створення допуску",MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                if (!IsProcessed)
                {
                    uof.GeneralAccessRepository.Create(access.ConvertToDatabaseGeneralAcess());
                    uof.GeneralAccessRepository.SaveChanges();
                }

                IsProcessed = true;
            }
            RemoveThisControl.Invoke();
        }

        private bool IsDuplicated(Access access)
        {
            return uof.GeneralAccessRepository.GetAll().Exists(x => x.Name == access.Name);
        }

        private void CancelBtn_OnClick(object sender, RoutedEventArgs e)
        {
            RemoveThisControl.Invoke();
        }
        
    }
}

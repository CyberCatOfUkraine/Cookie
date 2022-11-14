using DatabaseBroker;
using System.Linq;
using System.Windows.Controls;
using TaskState = DatabaseBroker.Models.TaskState;

namespace WPFUI.PartialViews.Statistic
{
    /// <summary>
    /// Interaction logic for StatisticViewControl.xaml
    /// </summary>
    public partial class StatisticViewControl : UserControl
    {
        private readonly UnitOfCookie _unitOfCookie;
        public StatisticViewControl(UnitOfCookie unitOfCookie)
        {
            _unitOfCookie = unitOfCookie;
            InitializeComponent();
            string employeeCount=(_unitOfCookie.EmployeeRepository.Count()-_unitOfCookie.WorkTaskRepository.Count()).ToString();
            EmployeeCountLabel.Content = employeeCount;
            TaskCountLabel.Content =
                _unitOfCookie.WorkTaskRepository.GetAll().Count(x => x.CurrentState == TaskState.Started);
        }
    }
}

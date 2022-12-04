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
            int allEmployeeCount = _unitOfCookie.EmployeeRepository.GetAll().Count;
            int involvedEmployeeCount = _unitOfCookie.WorkTaskRepository.GetAll() //involved - задіяний
                .Count(x => x.CurrentState is TaskState.Started or TaskState.Recived);
            string employeeCount=(allEmployeeCount-involvedEmployeeCount).ToString();
            EmployeeCountLabel.Content = employeeCount;
            TaskCountLabel.Content =
                _unitOfCookie.WorkTaskRepository.GetAll().Count(x => x.CurrentState == TaskState.Started);
        }
    }
}

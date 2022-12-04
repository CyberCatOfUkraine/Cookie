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
using DatabaseBroker.Models;
using MessageBroker.TelegramBroker;
using WPFUI.ExtentionMethods;
using WPFUI.Models;
using Address = WPFUI.Models.Address;
using Employee = WPFUI.Models.Employee;
using TaskEmployee = WPFUI.Models.TaskEmployee;
using TaskState = WPFUI.Models.TaskState;
using WorkTask = WPFUI.Models.WorkTask;

namespace WPFUI.PartialViews
{
    /// <summary>
    /// Interaction logic for TasksViewControl.xaml
    /// </summary>
    public partial class TasksViewControl : UserControl
    {
        private readonly UnitOfCookie _unitOfCookie;
        private readonly Broker _broker;
        private List<WorkTask> _tasks;
        public TasksViewControl(UnitOfCookie unitOfCookie, Broker broker)
        {

            _unitOfCookie = unitOfCookie;
            _broker = broker;
            _tasks = new();
            InitializeComponent();
            IntitalizeDataGrid();
            //IntitalizeDataGridStub();
        }

        private void IntitalizeDataGridStub()
        {
            TasksDataGrid.ItemsSource = null;

            var task = new WorkTask("test");
            task.Started = DateTime.UnixEpoch;
            task.Finished = DateTime.UnixEpoch;
            task.Addresses = new List<Address>();
            task.CurrentState = TaskState.Finished;
            task.AssignedEmployeesAccesses = new() { };
            var task1 = new WorkTask("test1");
            task1.Id = 2;
            task1.Started = DateTime.UnixEpoch;
            task1.Finished = DateTime.UnixEpoch;
            task1.Addresses = new List<Address>();
            task1.CurrentState = TaskState.Assigned;
            task1.AssignedEmployeesAccesses = new() { };
            _tasks = new();
            _tasks.Add(task);
            _tasks.Add(task1);
            TasksDataGrid.ItemsSource = _tasks;
        }

        private void IntitalizeDataGrid()
        {
            _tasks = _unitOfCookie.WorkTaskRepository.GetAll().Convert();
            TasksDataGrid.ItemsSource = _tasks;
        }


        private List<int> AssignedUserIdList = new();
        private void AssignEmployeeBtn_OnClick(object sender, RoutedEventArgs e)
        {
            var task = TasksDataGrid.CurrentItem as WorkTask;

            if (task.CurrentState == TaskState.Created)
            {
                if (task.AssignedEmployeesAccesses.Count == 0)
                {
                    throw new InvalidOperationException("Assign employee denied, can't get access task");
                }

                var acess = task.AssignedEmployeesAccesses[0];
                if (_unitOfCookie.EmployeeRepository.GetAll().Exists(x => x.Accesses.Exists(x => x.Name == acess.Name)))
                {
                    var employeesDbId = from employeeDb in _unitOfCookie.EmployeeRepository.GetAll()
                    where
                        employeeDb.Accesses.Exists(x => x.Name == acess.Name)
                    select employeeDb.Id;
                    //Тут повинна бути перевірка перед назначенням співробітника чи є взагалі такий і якщо нема то кидаємо ексепшин
                    var employees = employeesDbId.Except(AssignedUserIdList);
                    Employee employee;
                    if (employees.Any())
                    {
                        employee= _unitOfCookie.EmployeeRepository.Get(x=>x.Id==employees.First()).Convert();
                    }
                    else
                    {
                        MessageBox.Show("Немає вільних співробітників, очікуйте");
                        return;
                    }

                    var addressString = new StringBuilder();
                    addressString.Append(task.Addresses.First().Region+", ");
                    addressString.Append(task.Addresses.First().District+", ");
                    addressString.Append(task.Addresses.First().Settlement+", ");
                    addressString.Append(task.Addresses.First().Street+", ");
                    addressString.Append(task.Addresses.First().House);
                    if (!string.IsNullOrEmpty(task.Addresses.First().Apartment))
                    {
                        addressString.Append(","+task.Addresses.First().Apartment);
                    }
                    _broker.SendTask(task.Id, task.Name, employee.TelegramId, addressString.ToString(), onTaskRecived,
                        onTaskStarted, onTaskFinished, onTaskCanceled);
                    AssignedUserIdList.Add(task.Id);

                    var taskForUpdate = _tasks.Find(x => x.Id == task.Id);
                    taskForUpdate.CurrentState = TaskState.Assigned;
                    var tEmployee = new TaskEmployee(employee.Credentials, employee.Accesses, employee.TelegramId);
                    taskForUpdate.AssignedEmployees = new List<TaskEmployee> {tEmployee  }; 
                    
                    _unitOfCookie.WorkTaskRepository.Update(x => x.Id == task.Id, taskForUpdate.Convert());
                    _unitOfCookie.WorkTaskRepository.SaveChanges();

                    TryUpdateDataGrid();
                    MessageBox.Show("Задачу призначено вільного співробітника");
                }
                else
                {
                    MessageBox.Show(
                        "Не знайдено співробітника якому можна було б призначити завдання з даним допуском");
                    return;
                }

            }

            if (task.CurrentState==TaskState.Canceled)
            {
                if (task.AssignedEmployeesAccesses.Count == 0)
                {
                    throw new InvalidOperationException("Assign employee denied, can't get access task");
                }
                var acess = task.AssignedEmployeesAccesses[0];
                if (_unitOfCookie.EmployeeRepository.GetAll().Exists(x => x.Accesses.Exists(x => x.Name == acess.Name)
                                                                          &&!AssignedUserIdList.Exists(k=>k.Equals(x.Id))))
                {
                    var employeesDbId = from employeeDb in _unitOfCookie.EmployeeRepository.GetAll()
                                        where
                                            employeeDb.Accesses.Exists(x => x.Name == acess.Name)
                                        select employeeDb.Id;
                    var assignedEmployees = employeesDbId.Except(AssignedUserIdList);
                        var employees =assignedEmployees.Where(x=>x!=task.AssignedEmployees.First().Id);
                    Employee employee;
                    if (employees.Any())
                    {
                        employee = _unitOfCookie.EmployeeRepository.Get(x => x.Id == employees.First()).Convert();
                    }
                    else
                    {
                        MessageBox.Show("Немає вільних співробітників, очікуйте");
                        return;
                    }

                    try
                    {
                        AssignedUserIdList.Remove(task.AssignedEmployees.First().Id);
                    }
                    catch (Exception exception)
                    {
                        Console.WriteLine(exception);
                    }
                    var addressString = new StringBuilder();
                    addressString.Append(task.Addresses.First().Region + ", ");
                    addressString.Append(task.Addresses.First().District + ", ");
                    addressString.Append(task.Addresses.First().Settlement + ", ");
                    addressString.Append(task.Addresses.First().Street + ", ");
                    addressString.Append(task.Addresses.First().House);
                    if (!string.IsNullOrEmpty(task.Addresses.First().Apartment))
                    {
                        addressString.Append("," + task.Addresses.First().Apartment);
                    }
                    _broker.SendTask(task.Id, task.Name, employee.TelegramId, addressString.ToString(), onTaskRecived,
                        onTaskReStarted, onTaskFinished, onTaskCanceled);
                    AssignedUserIdList.Add(task.Id);

                    var taskForUpdate = _tasks.Find(x => x.Id == task.Id);
                    taskForUpdate.CurrentState = TaskState.Assigned;
                    var tEmployee = new TaskEmployee(employee.Credentials, employee.Accesses, employee.TelegramId);
                    taskForUpdate.AssignedEmployees = new List<TaskEmployee> { tEmployee };

                    _unitOfCookie.WorkTaskRepository.Update(x => x.Id == task.Id, taskForUpdate.Convert());
                    _unitOfCookie.WorkTaskRepository.SaveChanges();

                    TryUpdateDataGrid();
                    MessageBox.Show("Задачу перепризначено  на вільного співробітника ");
                }
                else
                {
                    MessageBox.Show(
                        "Не знайдено співробітника якому можна було б призначити завдання з даним допуском");
                    return;
                }
            }

            
        }

        private void onTaskRecived(int taskId)
        {
            _tasks.Find(x => x.Id == taskId).CurrentState = TaskState.Recived;

            TryUpdateDataGrid();

            var task = _unitOfCookie.WorkTaskRepository.Get(x => x.Id == taskId);
            task.CurrentState = (DatabaseBroker.Models.TaskState)TaskState.Recived;
            _unitOfCookie.WorkTaskRepository.Update(x => x.Id == taskId, task);
            _unitOfCookie.WorkTaskRepository.SaveChanges();
        }
        private void onTaskStarted(int taskId)
        {
            _tasks.Find(x => x.Id == taskId).CurrentState = TaskState.Started;
            _tasks.Find(x => x.Id == taskId).Started = DateTime.Now;

            TryUpdateDataGrid();
            var task = _unitOfCookie.WorkTaskRepository.Get(x => x.Id == taskId);
            task.CurrentState = (DatabaseBroker.Models.TaskState)TaskState.Started;
            task.Started = DateTime.Now;
            _unitOfCookie.WorkTaskRepository.Update(x => x.Id == taskId, task);
            _unitOfCookie.WorkTaskRepository.SaveChanges();

        }
        private void onTaskReStarted(int taskId)
        {
            _tasks.Find(x => x.Id == taskId).CurrentState = TaskState.Started;
            _tasks.Find(x => x.Id == taskId).Started = DateTime.Now;

            TryUpdateDataGrid();
            var task = _unitOfCookie.WorkTaskRepository.Get(x => x.Id == taskId);
            task.CurrentState = (DatabaseBroker.Models.TaskState)TaskState.Started;
            _unitOfCookie.WorkTaskRepository.Update(x => x.Id == taskId, task);
            _unitOfCookie.WorkTaskRepository.SaveChanges();

        }

        private void onTaskFinished(int taskId)
        {

            _tasks.Find(x => x.Id == taskId).CurrentState = TaskState.Finished;
            _tasks.Find(x => x.Id == taskId).Finished = DateTime.Now;

            TryUpdateDataGrid();
            var task = _unitOfCookie.WorkTaskRepository.Get(x => x.Id == taskId);
            task.CurrentState = (DatabaseBroker.Models.TaskState)TaskState.Finished;
            task.Finished = DateTime.Now;
            _unitOfCookie.WorkTaskRepository.Update(x => x.Id == taskId, task);
            _unitOfCookie.WorkTaskRepository.SaveChanges();

            var taskForRemove = _tasks.Find(x => x.Id == taskId);
            AssignedUserIdList.Remove(taskForRemove.AssignedEmployees.First().Id);
               // тут видаляємо користувача з списку призначених користувачів
        }

        private void onTaskCanceled(int taskId)
        {


            _tasks.Find(x => x.Id == taskId).CurrentState = TaskState.Canceled;
            _tasks.Find(x => x.Id == taskId).Finished = DateTime.UnixEpoch;

            TryUpdateDataGrid();
            var task = _unitOfCookie.WorkTaskRepository.Get(x => x.Id == taskId);
            task.CurrentState = (DatabaseBroker.Models.TaskState)TaskState.Canceled;
            task.Finished = DateTime.Now;
            _unitOfCookie.WorkTaskRepository.Update(x => x.Id == taskId, task);
            _unitOfCookie.WorkTaskRepository.SaveChanges();
        }

        private void TryUpdateDataGrid()
        {
            try
            {
                TasksDataGrid.ItemsSource = null;
                TasksDataGrid.ItemsSource = _tasks;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}

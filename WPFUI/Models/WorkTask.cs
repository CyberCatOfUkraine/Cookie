using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFUI.Models
{
    public class WorkTask
    {

        public WorkTask(string name)
        {
            Name = name;
        }
        public WorkTask(DateTime started, DateTime finished, List<Pause> pausesList, List<Employee> assignedEmployeeses, TaskState currentState, List<Address> addresses, string name, List<Access> assignedEmployeesAccesses)
        {
            Started = started;
            Finished = finished;
            PausesList = pausesList;
            AssignedEmployees = assignedEmployeeses;
            CurrentState = currentState;
            Addresses = addresses;
            Name = name;
            AssignedEmployeesAccesses = assignedEmployeesAccesses;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public List<Pause> PausesList { get; set; }
        public List<Employee> AssignedEmployees { get; set; }
        public List<Access> AssignedEmployeesAccesses { get; set; }
        public List<Address> Addresses { get; set; }
        public TaskState CurrentState { get; set; }

        public string StartedWPF
        {
            get
            {
                if (Started==DateTime.UnixEpoch)
                {
                    return "Ще не розпочато";
                }
                return Started.Year + "." +
                       Started.Month + "." +
                       Started.Day + "_" +
                       Started.Hour + ":" +
                       Started.Minute + ":" +
                       Started.Second;
            }
        }

        public string FinishedWPF
        {
            get
            {
                if (Finished==DateTime.UnixEpoch)
                {
                    return "Ще не завершено";
                }
                return Finished.Year + "." +
                       Finished.Month + "." +
                       Finished.Day + "_" +
                       Finished.Hour + ":" +
                       Finished.Minute + ":" +
                       Finished.Second;
            }
        }

        public string EmployeeWPF => AssignedEmployees == null||AssignedEmployees.Count==0 ? "" : AssignedEmployees.First().Credentials;

        public string AccessWPF => AssignedEmployeesAccesses==null || AssignedEmployeesAccesses.Count == 0 ? "" : AssignedEmployeesAccesses.First().Name;

        public string AddressWPF
        {
            get
            {
                if (Addresses == null|| Addresses.Count==0)
                {
                    return "";
                }

                var address = Addresses.First();
               return address.Apartment == null
                    ?
                    "Область:         " + address.Region + Environment.NewLine +
                    "Район:           " + address.District + Environment.NewLine +
                    "Населений пункт: " + address.Settlement + Environment.NewLine +
                    "Вулиця:          " + address.Street + Environment.NewLine +
                    "Будинок:         " + address.House + Environment.NewLine
                    :
                    "Область:         " + address.Region + Environment.NewLine +
                    "Район:           " + address.District + Environment.NewLine +
                    "Населений пункт: " + address.Settlement + Environment.NewLine +
                    "Вулиця:          " + address.Street + Environment.NewLine +
                    "Будинок:         " + address.House + Environment.NewLine +
                    "Квартира:        " + address.Apartment;
            }
        }

        public string CurrentStateWPF
        {
            get
            {
                return CurrentState switch
                {
                    TaskState.Created => "Створено",
                    TaskState.Assigned => "Призначено",
                    TaskState.Recived => "Отримано для виконання",
                    TaskState.Started => "Розпочато",
                    TaskState.OnPause => "На паузі",
                    TaskState.Resume => "Продовжено",
                    TaskState.Finished => "Завершено",
                    TaskState.Canceled => "Скасовано",
                    _ => throw new ArgumentOutOfRangeException(message: "Неможливо визначити поточний стан завдання",
                        new InvalidOperationException("CurrentStateWPF, впихнуте значення виходить за межі діапазону"))
                };
            }
        }

        public Visibility AssignFinishState
        {
            get
            {
                if (CurrentState==TaskState.Finished)
                {
                    return Visibility.Hidden;
                }
                return Visibility.Visible;
            }
        }
    }
}

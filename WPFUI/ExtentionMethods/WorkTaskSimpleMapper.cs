using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI.Models;

namespace WPFUI.ExtentionMethods
{
    internal static class WorkTaskSimpleMapper
    {
        public static List<DatabaseBroker.Models.WorkTask> ConvertDatabaseAcesses(this List<WPFUI.Models.WorkTask> tasks)
        {
            return (from task in tasks select new DatabaseBroker.Models.WorkTask() { }).ToList();
        }
        public static List<WPFUI.Models.WorkTask> Convert(this List<DatabaseBroker.Models.WorkTask> tasks)
        {
            return (from task in tasks select new WPFUI.Models.WorkTask(task.Started, task.Finished, task.PausesList.Convert(), task.AssignedEmployees.Convert(), (TaskState)(int)task.CurrentState, task.Addresses.ConvertToWPFAddress(), task.Name, task.AssignedEmployeesAccesses.Convert()) { Id = task.Id }).ToList();
        }
        public static DatabaseBroker.Models.WorkTask Convert(this WPFUI.Models.WorkTask task)
        {
            return new DatabaseBroker.Models.WorkTask() {Started = task.Started,
               Finished = task.Finished,
               PausesList = task.PausesList.Convert(),
               AssignedEmployees = task.AssignedEmployees.Convert(), 
               CurrentState = (DatabaseBroker.Models.TaskState)(int)task.CurrentState, 
               Addresses = task.Addresses.ConvertToDatabaseAddress(), 
               Name = task.Name, 
               AssignedEmployeesAccesses = task.AssignedEmployeesAccesses.ConvertToDatabaseAcesses()};
        }
        public static WPFUI.Models.WorkTask Convert(this DatabaseBroker.Models.WorkTask task)
        {
            TaskState currentState = (TaskState)((int)task.CurrentState);
            return new WPFUI.Models.WorkTask(task.Started,task.Finished,task.PausesList.Convert(),task.AssignedEmployees.Convert(), currentState, task.Addresses.ConvertToWPFAddress(),task.Name,task.AssignedEmployeesAccesses.Convert()) { Id = task.Id };
        }
    }
}

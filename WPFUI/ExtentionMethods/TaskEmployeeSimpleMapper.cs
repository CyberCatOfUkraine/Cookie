using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI.ExtentionMethods
{
    internal static class TaskEmployeeSimpleMapper
    {
        internal static List<WPFUI.Models.TaskEmployee> Convert(this List<DatabaseBroker.Models.TaskEmployee> employees)
        {
            if (employees == null)
            {
                return new List<WPFUI.Models.TaskEmployee>();
            }
            return (from employee in employees select new WPFUI.Models.TaskEmployee(employee.Credentials, employee.Accesses.Convert(), employee.TelegramID) { Id = employee.Id }).ToList();
        }

        internal static List<DatabaseBroker.Models.TaskEmployee> ConvertTask(this List<WPFUI.Models.TaskEmployee> employees)
        {
            if (employees == null)
            {
                return new List<DatabaseBroker.Models.TaskEmployee>();
            }
            return (from employee in employees select new DatabaseBroker.Models.TaskEmployee { Id = employee.Id, Accesses = employee.Accesses.ConvertToDatabaseAcesses(), Credentials = employee.Credentials, TelegramID = employee.TelegramId }).ToList();
        }
        internal static WPFUI.Models.TaskEmployee ConvertTask(this DatabaseBroker.Models.TaskEmployee employee)
        {
            return new WPFUI.Models.TaskEmployee(employee.Credentials, employee.Accesses.Convert(), employee.TelegramID) { Id = employee.Id };
        }

        internal static DatabaseBroker.Models.TaskEmployee ConvertTask(this WPFUI.Models.TaskEmployee employee)
        {
            if (employee == null)
                return new DatabaseBroker.Models.TaskEmployee();


            return new DatabaseBroker.Models.TaskEmployee { Accesses = employee.Accesses.ConvertToDatabaseAcesses(), Credentials = employee.Credentials, Id = employee.Id, TelegramID = employee.TelegramId };
        }
    }
}

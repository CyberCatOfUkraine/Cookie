using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI.ExtentionMethods
{
    internal static class EmployeeSimpleMapper
    {
        internal static List<WPFUI.Models.Employee> Convert(this List<DatabaseBroker.Models.Employee> employees)
        {
            if (employees == null)
            {
                return new List<WPFUI.Models.Employee>();
            }
            return (from employee in employees select  new WPFUI.Models.Employee(employee.Credentials,employee.Accesses.Convert(),employee.TelegramID){Id = employee.Id}).ToList();
        }

        internal static List<DatabaseBroker.Models.Employee> Convert(this List<WPFUI.Models.Employee> employees)
        {
            if (employees==null)
            {
                return new List<DatabaseBroker.Models.Employee>();
            }
            return (from employee in employees select  new DatabaseBroker.Models.Employee{Id = employee.Id,Accesses = employee.Accesses.ConvertToDatabaseAcesses(),Credentials = employee.Credentials,TelegramID = employee.TelegramId}).ToList();
        }

        internal static WPFUI.Models.Employee Convert(this DatabaseBroker.Models.Employee employee)
        {
            return new WPFUI.Models.Employee(employee.Credentials, employee.Accesses.Convert(), employee.TelegramID) { Id = employee.Id };
        }

        internal static DatabaseBroker.Models.Employee Convert(this WPFUI.Models.Employee employee)
        {
            return new DatabaseBroker.Models.Employee{Accesses = employee.Accesses.ConvertToDatabaseAcesses(),Credentials = employee.Credentials,Id = employee.Id,TelegramID = employee.TelegramId};
        }

    }
}

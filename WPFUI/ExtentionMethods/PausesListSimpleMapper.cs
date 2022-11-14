using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI.ExtentionMethods
{
    internal static class PausesListSimpleMapper
    {
        public static List<DatabaseBroker.Models.Pause> Convert(this List<WPFUI.Models.Pause> tasks)
        {
            if (tasks==null)
            {
                return new List<DatabaseBroker.Models.Pause>();
            }
            return (from task in tasks select new DatabaseBroker.Models.Pause() {FinishedTime = task.FinishedTime,StartedTime = task.StartedTime}).ToList();
        }
        public static List<WPFUI.Models.Pause> Convert(this List<DatabaseBroker.Models.Pause> tasks)
        {
            if (tasks == null)
            {
                return new List<WPFUI.Models.Pause>();
            }
            return (from task in tasks select new WPFUI.Models.Pause(task.StartedTime,task.FinishedTime) { Id = task.Id }).ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI.Models;

namespace WPFUI.Comparator
{
    internal class WorkTaskComparator:IComparer<WorkTask>
    {
        public int Compare(WorkTask x, WorkTask y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            /*var idComparison = x.Id.CompareTo(y.Id);
            if (idComparison != 0) return idComparison;
            var nameComparison = string.Compare(x.Name, y.Name, StringComparison.Ordinal);
            if (nameComparison != 0) return nameComparison;
            var startedComparison = x.Started.CompareTo(y.Started);
            if (startedComparison != 0) return startedComparison;
            var finishedComparison = x.Finished.CompareTo(y.Finished);
            if (finishedComparison != 0) return finishedComparison;
            return x.CurrentState.CompareTo(y.CurrentState);*/
            var xText = x.Name + x.Started;
            var yText = y.Name + y.Started;
            return xText.CompareTo(yText);
        }
    }
}

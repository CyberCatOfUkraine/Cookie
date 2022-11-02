using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI.Models;

namespace WPFUI.Comparator
{
    internal class PauseComparator:IComparer<Pause>
    {
        public int Compare(Pause x, Pause y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            /*var idComparison = x.Id.CompareTo(y.Id);
            if (idComparison != 0) return idComparison;*/
            var startedTimeComparison = x.StartedTime.CompareTo(y.StartedTime);
            if (startedTimeComparison != 0) return startedTimeComparison;
            return x.FinishedTime.CompareTo(y.FinishedTime);
        }
    }
}

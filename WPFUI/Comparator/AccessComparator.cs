using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI.Models;

namespace WPFUI.Comparator
{
    internal class AccessComparator:IComparer<Access>
    {
        public int Compare(Access x, Access y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            /*var idComparison = x.Id.CompareTo(y.Id);
            if (idComparison != 0) return idComparison;*/
            return string.Compare(x.Name, y.Name, StringComparison.Ordinal);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI.Models;

namespace WPFUI.Comparator
{
    internal class AddressComparator:IComparer<Address>
    {
        public int Compare(Address x, Address y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;

            var xString = x.Oblast + x.Rajon + x.NaseleniyPunkt + x.Vulicya + x.Budinok + x.Kvartira;
            var yString = y.Oblast + y.Rajon + y.NaseleniyPunkt + y.Vulicya + y.Budinok + y.Kvartira;
            /*var oblastComparison = string.Compare(x.Oblast, y.Oblast, StringComparison.Ordinal);
            if (oblastComparison != 0) return oblastComparison;
            var rajonComparison = string.Compare(x.Rajon, y.Rajon, StringComparison.Ordinal);
            if (rajonComparison != 0) return rajonComparison;
            var naseleniyPunktComparison = string.Compare(x.NaseleniyPunkt, y.NaseleniyPunkt, StringComparison.Ordinal);
            if (naseleniyPunktComparison != 0) return naseleniyPunktComparison;
            var vulicyaComparison = string.Compare(x.Vulicya, y.Vulicya, StringComparison.Ordinal);
            if (vulicyaComparison != 0) return vulicyaComparison;
            var budinokComparison = string.Compare(x.Budinok, y.Budinok, StringComparison.Ordinal);
            if (budinokComparison != 0) return budinokComparison;
            return string.Compare(x.Kvartira, y.Kvartira, StringComparison.Ordinal);*/
            return string.Compare(xString,yString,StringComparison.OrdinalIgnoreCase);
        }
    }
}

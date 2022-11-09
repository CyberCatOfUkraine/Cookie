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

            var xString = x.Region + x.District + x.Settlement + x.Street + x.House + x.Apartment;
            var yString = y.Region + y.District + y.Settlement + y.Street + y.House + y.Apartment;
            /*var oblastComparison = string.Compare(x.Region, y.Region, StringComparison.Ordinal);
            if (oblastComparison != 0) return oblastComparison;
            var rajonComparison = string.Compare(x.District, y.District, StringComparison.Ordinal);
            if (rajonComparison != 0) return rajonComparison;
            var naseleniyPunktComparison = string.Compare(x.Settlement, y.Settlement, StringComparison.Ordinal);
            if (naseleniyPunktComparison != 0) return naseleniyPunktComparison;
            var vulicyaComparison = string.Compare(x.Street, y.Street, StringComparison.Ordinal);
            if (vulicyaComparison != 0) return vulicyaComparison;
            var budinokComparison = string.Compare(x.House, y.House, StringComparison.Ordinal);
            if (budinokComparison != 0) return budinokComparison;
            return string.Compare(x.Apartment, y.Apartment, StringComparison.Ordinal);*/
            return string.Compare(xString,yString,StringComparison.OrdinalIgnoreCase);
        }
    }
}

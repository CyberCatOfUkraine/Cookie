using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI.ExtentionMethods
{
    internal static class AccessAndGeneralAccessSimpleMapper
    {
        public static List<DatabaseBroker.Models.GeneralAccess> Convert(this List<WPFUI.Models.Access> accesses)
        {
            return (from access in accesses select new DatabaseBroker.Models.GeneralAccess() { Name = access.Name }).ToList();
        }
        public static List<WPFUI.Models.Access> Convert(this List<DatabaseBroker.Models.GeneralAccess> accesses)
        {
            return (from access in accesses select new WPFUI.Models.Access(access.Name)).ToList();
        }
        public static DatabaseBroker.Models.GeneralAccess Convert(this WPFUI.Models.Access access)
        {
            return new DatabaseBroker.Models.GeneralAccess() { Name = access.Name };
        }
        public static WPFUI.Models.Access Convert(this DatabaseBroker.Models.GeneralAccess access)
        {
            return new WPFUI.Models.Access(access.Name);
        }
    }
}

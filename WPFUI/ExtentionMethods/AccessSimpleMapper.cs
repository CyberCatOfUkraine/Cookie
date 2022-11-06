using System.Collections.Generic;
using System.Linq;

namespace WPFUI.ExtentionMethods
{
    internal static class AccessSimpleMapper
    {
        public static List<DatabaseBroker.Models.Access> Convert(this List<WPFUI.Models.Access> accesses)
        {
            return (from access in accesses select new DatabaseBroker.Models.Access() { Name = access.Name}).ToList();
        }
        public static List<WPFUI.Models.Access> Convert(this List<DatabaseBroker.Models.Access> accesses)
        {
            return (from access in accesses select new WPFUI.Models.Access(access.Name)).ToList();
        }
        public static DatabaseBroker.Models.Access Convert(this WPFUI.Models.Access access)
        {
            return new DatabaseBroker.Models.Access(){Name = access.Name};
        }
        public static WPFUI.Models.Access Convert(this DatabaseBroker.Models.Access access)
        {
            return new WPFUI.Models.Access(access.Name);
        }

    }
}

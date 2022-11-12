using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookie
{
    public static class GeneralStrings
    {
        public static readonly string ClientMessagesDirectoryPath=Path.Combine(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),"Cookie"),"Messages");
    }
}

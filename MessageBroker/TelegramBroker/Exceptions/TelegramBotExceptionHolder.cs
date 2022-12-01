using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.TelegramBroker.Exceptions
{
    internal class TelegramBotExceptionHolder
    {

        public TelegramBotExceptionHolder(Exception exception, bool isEngineer, bool isElectrician, string credentials) 
        {
            Exception = exception;
            IsEngineer = isEngineer;
            IsElectrician = isElectrician;
            Credentials = credentials;
        }

        public Exception Exception { get; set; }
        public bool IsEngineer { get; set; }
        public bool IsElectrician { get; set; }
        public string Credentials { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.TelegramBroker.User.Tasks
{
    public enum TaskState
    {
        Created, Assigned, Recived, Started, OnPause, Resume, Finished, Canceled,Unknown
    }
}

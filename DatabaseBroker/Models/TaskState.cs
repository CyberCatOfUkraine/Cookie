using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBroker.Models
{
    public enum TaskState
    {
        Created,Assigned,Recived,Started,OnPause,Resume,Finished,Canceled
    }
}

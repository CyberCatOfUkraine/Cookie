using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.TelegramBroker.Models
{
    public struct ActionHolder
    {
        public Action<int> OnTaskRecived { get; set; }
        public Action<int> OnTaskStarted { get; set; }
        public Action<int> OnTaskFinished { get; set; }
        public Action<int> OnTaskCanceled { get; set; }

        public ActionHolder(Action<int> onTaskRecived, Action<int> onTaskStarted, Action<int> onTaskFinished, Action<int> onTaskCanceled)
        {
            OnTaskRecived  = onTaskRecived;
            OnTaskStarted  = onTaskStarted;
            OnTaskFinished = onTaskFinished;
            OnTaskCanceled = onTaskCanceled;
        }
    }
}

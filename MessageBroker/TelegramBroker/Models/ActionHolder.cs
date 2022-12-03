using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.TelegramBroker.Models
{
    public struct ActionHolder
    {
        public Action OnTaskRecived { get; set; }
        public Action OnTaskStarted { get; set; }
        public Action OnTaskFinished { get; set; }
        public Action OnTaskCanceled { get; set; }

        public ActionHolder(Action onTaskRecived, Action onTaskStarted, Action onTaskFinished, Action onTaskCanceled)
        {
            OnTaskRecived = onTaskRecived;
            OnTaskStarted = onTaskStarted;
            OnTaskFinished = onTaskFinished;
            OnTaskCanceled = onTaskCanceled;
        }
    }
}

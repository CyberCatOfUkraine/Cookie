using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageBroker.TelegramBroker.Models;

namespace MessageBroker.TelegramBroker.User.Tasks
{
    public class UserTask
    {
        public UserTask(int id, OrdinaryUser user, string address, TaskState taskState, ActionHolder actionHolder, string name)
        {
            Id = id;
            User = user;
            Address = address;
            TaskState = taskState;
            ActionHolder = actionHolder;
            Name = name;
        }
        public OrdinaryUser User { get; set; }
        public string Address { get; set; }
        public TaskState TaskState { get; set; }

        public ActionHolder ActionHolder { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

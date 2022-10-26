using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI.Models
{
    public class Pause
    {
        public Pause(DateTime startedTime, DateTime finishedTime)
        {
            StartedTime = startedTime;
            FinishedTime = finishedTime;
        }

        public int Id { get; set; }
        public DateTime StartedTime { get; set; }
        public DateTime FinishedTime { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBroker.Models
{
    public class Pause
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartedTime { get; set; }
        public DateTime FinishedTime { get; set; }
    }
}

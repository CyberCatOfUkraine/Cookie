using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBroker.Models
{
    public class WorkTask
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Started { get; set; }
        public DateTime Finished { get; set; }
        public List<Pause> PausesList { get; set; }
        public List<Employee> AssignedEmployees { get; set; }
        public List<Address> Addresses { get; set; }
        public TaskState CurrentState { get; set; }
    }
}

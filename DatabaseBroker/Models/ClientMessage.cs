using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBroker.Models
{
    public class ClientMessage
    {
        [Key]
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime RecivedTime { get; set; }
        public Address Address { get; set; }
    }
}

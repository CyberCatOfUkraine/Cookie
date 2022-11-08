using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class ClientMessage
    {
        public ClientMessage(string text, DateTime recivedTime, Address address)
        {
            Text = text;
            RecivedTime = recivedTime;
            Address = address;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime RecivedTime { get; set; }
        public Address Address { get; set; }
    }
}

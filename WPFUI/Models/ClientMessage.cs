using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI.Models
{
    public class ClientMessage
    {
        public ClientMessage(string text, DateTime recivedTime, Address address, bool isProcessed)
        {
            Text = text;
            RecivedTime = recivedTime;
            Address = address;
            IsProcessed = isProcessed;
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime RecivedTime { get; set; }
        
        public string RecivedTimeWPF =>RecivedTime.Year+"."+RecivedTime.Month+"."+RecivedTime.Day+"_"+RecivedTime.Hour+":"+RecivedTime.Minute+":"+RecivedTime.Second+":"+RecivedTime.Millisecond;

        public Address Address { get; set; }

        public string AddressWPF => Address.Region + Environment.NewLine +
                                    Address.District + Environment.NewLine +
                                    Address.Settlement + Environment.NewLine +
                                    Address.Street + Environment.NewLine +
                                    Address.House + Environment.NewLine +
                                    Address.Apartment;
        public bool IsProcessed { get; set; }

    }
}

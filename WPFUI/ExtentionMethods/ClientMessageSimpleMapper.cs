using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI.Models;

namespace WPFUI.ExtentionMethods
{
    internal static class ClientMessageSimpleMapper
    {

        internal static List<WPFUI.Models.ClientMessage> Convert(this List<DatabaseBroker.Models.ClientMessage> clientMessages)
        {
            return (from message in clientMessages select new WPFUI.Models.ClientMessage(message.Text,message.RecivedTime,message.Address.Convert(),message.IsProcessed) { Id = message.Id }).ToList();
        }

        internal static List<DatabaseBroker.Models.ClientMessage> Convert(this List<WPFUI.Models.ClientMessage> employees)
        {
            return (from message in employees select new DatabaseBroker.Models.ClientMessage() { Id = message.Id,Address = message.Address.Convert(),IsProcessed = message.IsProcessed,RecivedTime = message.RecivedTime,Text = message.Text}).ToList();
        }

        internal static WPFUI.Models.ClientMessage Convert(this DatabaseBroker.Models.ClientMessage message)
        {
            return new WPFUI.Models.ClientMessage(message.Text,message.RecivedTime,message.Address.Convert(),message.IsProcessed) { Id = message.Id };
        }

        internal static DatabaseBroker.Models.ClientMessage Convert(this WPFUI.Models.ClientMessage message)
        {
            return new DatabaseBroker.Models.ClientMessage() { Address =message.Address.Convert(),Id = message.Id,IsProcessed = message.IsProcessed,RecivedTime = message.RecivedTime,Text = message.Text};
        }
    }
}

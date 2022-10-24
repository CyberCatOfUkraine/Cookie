using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageBroker.TelegramBroker.User.User;
using Telegram.Bot.Types.ReplyMarkups;

namespace MessageBroker.TelegramBroker.User
{
    internal class OrdinaryUser
    {
        public OrdinaryUser(long chatId)
        {
            ChatId=chatId;
            Role = RoleEnum.Client;
            KeyboardMarkup = KeyboardMarkups.ClientKeyboardMarkup;
        }
        public long ChatId { get; set; }
        public RoleEnum Role { get; set; }
        
        public InlineKeyboardMarkup KeyboardMarkup { get; set; }
    }
}

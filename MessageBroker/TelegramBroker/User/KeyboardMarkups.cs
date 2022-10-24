using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
using GlobalStrings;
namespace MessageBroker.TelegramBroker.User
{
    internal static class KeyboardMarkups
    {
        public static InlineKeyboardMarkup ClientKeyboardMarkup = new[]
        {
            new[]
            {
                InlineKeyboardButton.WithUrl("Сайт програми",Strings.SiteURL),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData("press me"),
            }
        };
    }
}

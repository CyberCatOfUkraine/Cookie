using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;
namespace MessageBroker.TelegramBroker.User
{
    internal static class KeyboardMarkups
    {
        //public static InlineKeyboardButton DefaultKeyboardButton => InlineKeyboardButton.WithCallbackData(BotStrings.RemindForAviableOperations);

        public static InlineKeyboardMarkup ClientKeyboardMarkup
        {
            get
            {
                return new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithUrl(BotStrings.SiteText, BotStrings.SiteURL),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData(BotStrings.CheckForProblems)
                    }
                };
            }
            
        }

        public static InlineKeyboardMarkup EngineerKeyboardMarkup
        {
            get
            {
                return new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData(BotStrings.StatOfFreeEmployees)
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData(BotStrings.StatOfPowerGridProblems)
                    }
                };
            }
            
        }

        public static InlineKeyboardMarkup ElectricianKeyboardMarkup
        {
           get
           {
               return new[]
               {
                   new[]
                   {
                       InlineKeyboardButton.WithCallbackData(BotStrings.NewTaskAvailability)
                   },
                   new[]
                   {
                       InlineKeyboardButton.WithCallbackData(BotStrings.UpdateTaskState)
                   }
               };
            }
        }
        public static InlineKeyboardMarkup ChangeRoleKeyboardMarkup
        {
           get
           {
               return new[]
               {
                   new[]
                   {
                       InlineKeyboardButton.WithCallbackData(BotStrings.UpdateRoleClient)
                   },
                   new[]
                   {
                       InlineKeyboardButton.WithCallbackData(BotStrings.UpdateRoleElectrician)
                   },
                   new[]
                   {
                       InlineKeyboardButton.WithCallbackData(BotStrings.UpdateRoleEngineer)
                   }
               };
            }
        }

        public static InlineKeyboardMarkup InvalidInput
        {
            get
            {
                return new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData(BotStrings.AvailableOperationsList)
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData(BotStrings.UpdateRoleRequest)
                    }
                };
            }
        }
    }
}

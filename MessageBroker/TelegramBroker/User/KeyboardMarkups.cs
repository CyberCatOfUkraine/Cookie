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
                        InlineKeyboardButton.WithUrl(BotStrings.SiteText, BotStrings.SiteUrl),
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
                    }
                };
            }
        }
        public static InlineKeyboardMarkup GetTaskRecievedMarkup
        {
            get
            {
                return new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData(BotStrings.TaskRecieved)
                    }
                };
            }
        }
        public static InlineKeyboardMarkup StartTaskMarkup
        {
            get
            {
                return new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData(BotStrings.TaskStarted)
                    }
                };
            }
        }
        public static InlineKeyboardMarkup FinishedTaskMarkup
        {
            get
            {
                return new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData(BotStrings.TaskFinished)
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData(BotStrings.TaskCanceled)
                    }
                };
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageBroker.TelegramBroker.User;
using MessageBroker.TelegramBroker.User.User;
using Microsoft.VisualBasic;
using Telegram.Bot.Types.ReplyMarkups;

namespace MessageBroker.TelegramBroker
{
    internal class UserOperationMikroWrapper
    {

        private static UserOperationMikroWrapper wrapper;
        private static bool _isInitialized = false;
        private List<OrdinaryUser> users = new();

        /// <summary>
        /// Don't use this
        /// </summary>
        /// <param name="users">Collection for copying</param>
        public UserOperationMikroWrapper(List<OrdinaryUser> users)
        {
            this.users = users;
        }

        public static UserOperationMikroWrapper Instance
        {
            get
            {
                if (_isInitialized)
                {
                    return wrapper;
                }

                _isInitialized = true;
                wrapper = new UserOperationMikroWrapper(new List<OrdinaryUser>());
                return wrapper;
            }
        }
        public RoleEnum GetRoleChatById(long chatId)
        {
            if (users.Exists(x => x.ChatId == chatId))
            {
                return users.Find(x => x.ChatId == chatId)!.Role;
            }

            Console.WriteLine("Current user not exist! On GetRoleChatById");
            return RoleEnum.Client;
        }

        public void TryAddUserToList(long chatId)
        {
            if (!IsExistInDb(chatId))
            {
                users.Add(new OrdinaryUser(chatId));
            }
        }

        private bool IsExistInDb(long chatId)
        {
            return users.Exists(x => x.ChatId == chatId);//TODO: Додати перевірку щодо реальної бази даних
        }

        public void ChangeStatus(long chatId, RoleEnum role)
        {
            if (IsExistInDb(chatId))
            {
                users.Find(x=>x.ChatId==chatId)!.Role=role;
                users.Find(x=>x.ChatId==chatId)!.KeyboardMarkup=ChangeMarkupByRole(role);
                return;    
            }
            Console.WriteLine("Current user not exist! On ChangeStatus");

        }

        private InlineKeyboardMarkup ChangeMarkupByRole(RoleEnum role)
        {
            return role switch
            {
                RoleEnum.Engineer => KeyboardMarkups.EngineerKeyboardMarkup,
                RoleEnum.Client => KeyboardMarkups.ClientKeyboardMarkup,
                RoleEnum.Electrician => KeyboardMarkups.ElectricianKeyboardMarkup,
                _ => KeyboardMarkups.ClientKeyboardMarkup
            };
        }

        public InlineKeyboardMarkup GetKeyboardMarkupByChatId(long chatId)
        {
            if (IsExistInDb(chatId))
            {
                return users.Find(x => x.ChatId == chatId)!.KeyboardMarkup;
            }
            Console.WriteLine("Current user not exist! On GetKeyboardMarkupByChatId");
            return new InlineKeyboardMarkup(new InlineKeyboardButton("GetKeyboardMarkupByChatId in MessageBroker.TelegramBroker.UserOperationMikroWrapper return shit"));
        }

        public string GetDefaultIntroByChatId(long chatId)
        {
            if (IsExistInDb(chatId))
            {
               return  GetIntroByRole(users.Find(x => x.ChatId == chatId)!.Role);
            }

            return GetIntroByRole(RoleEnum.Client);
        }

        private string GetIntroByRole(RoleEnum role)
        {
            return role switch
            {
                RoleEnum.Engineer => BotStrings.EngineerIntro,
                RoleEnum.Client => BotStrings.ClientIntro,
                RoleEnum.Electrician => BotStrings.ElectricianIntro,
                _ => BotStrings.ClientIntro
            };
        }
    }
}

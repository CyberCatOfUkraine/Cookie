using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseBroker;
using DatabaseBroker.Models;
using MessageBroker.TelegramBroker.User;
using MessageBroker.TelegramBroker.User.User;
using Microsoft.VisualBasic;
using Telegram.Bot.Types.ReplyMarkups;

namespace MessageBroker.TelegramBroker
{
    internal class MagicBox
    {
        private UnitOfCookie _unitOfCookie;
        private static MagicBox wrapper;
        private static bool _isInitialized = false;
        private List<OrdinaryUser> users = new();
        private OrdinaryUser Engineer;

        public MagicBox()
        {
            if (!_isInitialized)
            {
                throw new InvalidOperationException(typeof(MagicBox) + "must be initialized!");
            }
        }

        public static MagicBox Instance
        {
            get
            {
                if (_isInitialized)
                {
                    return wrapper;
                }

                _isInitialized = true;
                wrapper = new MagicBox();
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
            return users.Exists(x => x.ChatId == chatId); //TODO: Додати перевірку щодо реальної бази даних
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
            return null!;
        }

        public string GetDefaultIntroByChatId(long chatId)
        {
            if (IsExistInDb(chatId))
            {
                return GetIntroByRole(users.Find(x => x.ChatId == chatId)!.Role);
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

        public void SetUnitOfCookie(UnitOfCookie unitOfCookie)
        {
            _unitOfCookie = unitOfCookie;
        }

        public string GetStatOfFreeEmployees()
        {
            return (_unitOfCookie.EmployeeRepository.Count() - _unitOfCookie.WorkTaskRepository.GetAll()
                .Count(x => x.CurrentState == TaskState.Started)).ToString();

        }

        public string GetStatOfPowerGridProblems()
        {
            return _unitOfCookie.WorkTaskRepository.GetAll().Count(x => x.CurrentState == TaskState.Started).ToString();

        }

        public void SetEngineerTelegramID(long telegramId)
        {
            Engineer = new OrdinaryUser(telegramId)
            {
                Role = RoleEnum.Engineer,
                KeyboardMarkup = GetKeyboardMarkupByRole(RoleEnum.Engineer)
            };
            users.Add(Engineer);
        }

        private InlineKeyboardMarkup GetKeyboardMarkupByRole(RoleEnum role)
        {
            return role switch
            {
                RoleEnum.Engineer => KeyboardMarkups.EngineerKeyboardMarkup,
                RoleEnum.Client => KeyboardMarkups.ClientKeyboardMarkup,
                RoleEnum.Electrician => KeyboardMarkups.ElectricianKeyboardMarkup,
                _ => KeyboardMarkups.ClientKeyboardMarkup
            };
        }

        public string TryGetCredentialsById(long chatId)
        {
            throw new NotImplementedException();
        }
    }
}

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
        private static UnitOfCookie _unitOfCookie;
        private static MagicBox wrapper;
        private static bool _isInitialized = false;
        private List<OrdinaryUser> users = new();
        private static OrdinaryUser Engineer;

        public MagicBox()
        {
            if (!_isInitialized)
            {
                throw new InvalidOperationException(typeof(MagicBox) + "must be initialized!");
            }
        }

        public void SetUsers(List<OrdinaryUser> users)
        {
            this.users = users;
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

        public static void UpdateLocals()
        {
            var list =
                (from user in _unitOfCookie.EmployeeRepository.GetAll() select new OrdinaryUser(user.TelegramID){Role = RoleEnum.Electrician,KeyboardMarkup = KeyboardMarkups.ElectricianKeyboardMarkup}).ToList();
            if (Engineer!=null)
            {
                list.Add(Engineer);
            }
            wrapper.SetUsers(list);
            
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
        
        private bool IsExistInDb(long chatId)
        {
            if (Engineer.ChatId==chatId)
            {
                return true;
            }

            return
                users.Exists(x =>
                    x.ChatId == chatId); //_unitOfCookie.EmployeeRepository.GetAll().Exists(x => x.TelegramID== chatId);
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
            return IsExistInDb(chatId) ? users.Find(x => x.ChatId == chatId)!.KeyboardMarkup : MagicBox.Instance.GetKeyboardMarkupByRole(RoleEnum.Client);
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
            UpdateLocals();
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
            if (_unitOfCookie.EmployeeRepository==null||_unitOfCookie.EmployeeRepository.Get(x => x.TelegramID == chatId)==null)
            {
                return string.Empty;
            }
            return _unitOfCookie.EmployeeRepository.Get(x => x.TelegramID == chatId).Credentials;
        }
        

        /// <summary>
        /// Повертає доступну для виконання операцію або повідомлення про відсутність завдань
        /// </summary>
        /// <param name="chatId">Telegram id</param>
        /// <returns></returns>
        public string GetOperationById(long chatId)
        {
            var noOperation = "На цей момент операцій не знайдено, перевірте через деякий час.";
            return noOperation;
        }
    }
}

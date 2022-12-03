using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseBroker;
using MessageBroker.TelegramBroker.User;
using MessageBroker.TelegramBroker.User.Tasks;
using MessageBroker.TelegramBroker.User.User;
using Microsoft.VisualBasic;
using Telegram.Bot.Types.ReplyMarkups;
using TaskState = DatabaseBroker.Models.TaskState;

namespace MessageBroker.TelegramBroker
{
    internal class MagicBox
    {
        private static UnitOfCookie _unitOfCookie;
        private static MagicBox wrapper;
        private static bool _isInitialized = false;
        private List<OrdinaryUser> users = new();
        private static OrdinaryUser Engineer;
        private List<UserTask> _tasks = new();
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

        /// <summary>
        /// If user not exist return unknown state
        /// </summary>
        /// <param name="chatId">Telegram chat id</param>
        /// <returns>task state by id</returns>
        public User.Tasks.TaskState GetCurrentTaskStateById(long chatId)
        {
            if (_tasks.Count == 0)
            {
                return User.Tasks.TaskState.Unknown;
            }
            if (_tasks.Exists(x => x.User.ChatId == chatId))
            {
                return _tasks.Find(x => x.User.ChatId == chatId).TaskState;
            }

            return User.Tasks.TaskState.Unknown;
        }

        /// <summary>
        /// return -1 if task not assigned
        /// </summary>
        /// <param name="chatId">Telegram chat id</param>
        /// <returns>Id of current task</returns>
        public int GetCurrentTaskId(long chatId)
        {
            if (_tasks.Count==0)
            {
                return -1;
            }
            if (_tasks.Exists(x=>x.User.ChatId==chatId))
            {
                return _tasks.Find(x => x.User.ChatId == chatId).Id;
            }

            return -1;
        }

        public OrdinaryUser GetUserByChatId(long chatId)
        {
            if (users.Exists(x=>x.ChatId==chatId))
            {
                return users.Find(x => x.ChatId == chatId);
            }

            return null;
        }

        private Bot bot;
        public void SetBotInstance(Bot bot)
        {
            this.bot = bot;
        }
        public void ProcessTask(UserTask task)
        {
            if (task.TaskState == User.Tasks.TaskState.Assigned)
            {
                //We need deliver it, after onRecive action
                //after we need trace all user interaction and make corresponding(відповідні) actions
                _tasks.Add(task);
                bot.SendTaskToElectrician(task.Id, "Виникла наступна проблема:\n" +
                                                   task.Name +
                                                   "За адресою: "+task.Address);
            }
            else
            {
                throw new InvalidOperationException("Cannot process not assigned task\nTask ID:"+task.Id+"\nTask state: "+task.TaskState);
            }
        }
    }
}

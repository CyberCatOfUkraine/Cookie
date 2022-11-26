using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.TelegramBroker
{
    internal struct BotStrings
    {
        #region For KeyboardMarkups

        #region Client KeyboardMarkups
        public static readonly string SiteText = "Сайт програми";
        public static readonly string SiteURL = "https://github.com/CyberCatOfUkraine/Cookie";
        public static readonly string CheckForProblems = "Проблеми електропостачання";
        #endregion

        #region Engineer KeyboardMarkups
        public static readonly string StatOfFreeEmployees = "Зайнятість працівників";
        public static readonly string StatOfPowerGridProblems = "Проблеми в електромережі";
        #endregion

        #region Electrician KeyboardMarkups
        public static readonly string NewTaskAvailability = "Доступні завдання";
        public static readonly string UpdateTaskState = "Оновлення стану завдання";
        #endregion

        #region Change Role

        public static readonly string Electrician = "Електрик";
        public static readonly string Engineer = "Інженер";
        public static readonly string Client = "Клієнт";
        public static readonly string RoleChangedOn = "Поточний рівень доступу змінено на:  ";

        #endregion

        #region Invalid input
        public static readonly string AvailableOperationsList = "Перелік доступних операцій";
        public static readonly string UpdateRoleRequest = "Запит зміни прав доступу до даних";
        #endregion
        #endregion

        #region Bot answers
        public static readonly string ActionToCheckForProblems = "Щоб переглянути чи є проблема на вашій вулиці введіть адресу:\n(Розділення комами обов'язкове)";
        public static readonly string InvalidInput = "Не вдалося виконати ваш запит :(\n" +
                                                     "На жаль,розпізнавання тексту в даній версії боту не доступно.\n" +
                                                     "Для взаємодії з ботом необхідно використовувати кнопки подані нижче";
        public static readonly string ChooseRole = "Виберіть необхідний рівень доступу до даних";
        public static readonly string UpdateRoleRequestOk = "Запит щодо зміни прав доступу до даних погоджено";
        public static readonly string UpdateRoleRequestCanceled = "Запит щодо зміни прав доступу до даних відхилено";
        public static readonly string AvailableOperations = "Доступні операції";
        #endregion

        #region Intro 
        public static readonly string ClientIntro = "Доступні наступні функції:\n" +
                                                    "1) Перегляд проблем роботи електромережі за адресою\n" +
                                                    "2) Перегляд офіційного сайту боту\n";
        public static readonly string EngineerIntro = "Доступні наступні функції:\n" +
                                                      "1)Перегляд статистики щодо зайнятості працівників\n" +
                                                      "2)Перегляд статистики щодо проблем в електромережі\n";
        public static readonly string ElectricianIntro = "Доступні наступні функції:\n" +
                                                         "1)Перегляд доступних завдань\n" +
                                                         "2)Звітування щодо стану отриманого завдання\n";
        public static readonly string CheckRoleAndTryChangeItIntro = "Також Ви можете перевірити доступні вашому рівню доступу функції або відправити запит щодо зміни рівня доступу до даних:\n";
        #endregion
    }
}

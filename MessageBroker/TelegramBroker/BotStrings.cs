using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageBroker.TelegramBroker
{
    /// <summary>
    /// Аналог strings.xml в Android
    /// </summary>
    internal struct BotStrings
    {
        #region For KeyboardMarkups

        #region Client KeyboardMarkups
        public static readonly string SiteText = "Сайт програми";
        public static readonly string SiteUrl = "https://github.com/CyberCatOfUkraine/Cookie";
        public static readonly string CheckForProblems = "Проблеми електропостачання";
        #endregion

        #region Engineer KeyboardMarkups
        public static readonly string StatOfFreeEmployees = "Зайнятість працівників";
        public static readonly string StatOfPowerGridProblems = "Проблеми в електромережі";
        #endregion

        #region Electrician KeyboardMarkups
        /// <summary>
        /// Доступні завдання
        /// </summary>
        public static readonly string NewTaskAvailability = "Доступні завдання";
        #endregion

        #region Invalid input
        /// <summary>
        /// Перелік доступних операцій
        /// </summary>
        public static readonly string AvailableOperationsList = "Перелік доступних операцій";
        #endregion
        #endregion

        #region Bot answers
        public static readonly string ActionToCheckForProblems = "Щоб переглянути чи є проблема на вашій вулиці введіть адресу:\n(Розділення комами обов'язкове)";
        public static readonly string InvalidInput = "Не вдалося виконати ваш запит :(\n" +
                                                     "На жаль,розпізнавання тексту в даній версії боту не доступно.\n" +
                                                     "Для взаємодії з ботом необхідно використовувати кнопки подані нижче";
        #endregion

        #region Intro 
        public static readonly string ClientIntro = "Доступні наступні функції:\n" +
                                                    "1) Перегляд офіційного сайту боту\n" +
                                                    "2) Перегляд проблем роботи електромережі за адресою\n";
        public static readonly string EngineerIntro = "Доступні наступні функції:\n" +
                                                      "1)Перегляд статистики щодо зайнятості працівників\n" +
                                                      "2)Перегляд статистики щодо проблем в електромережі\n";
        public static readonly string ElectricianIntro = "Доступні наступні функції:\n" +
                                                         "1)Перегляд доступних завдань\n" +
                                                         "2)Звітування щодо стану отриманого завдання\n";
        #endregion
    }
}

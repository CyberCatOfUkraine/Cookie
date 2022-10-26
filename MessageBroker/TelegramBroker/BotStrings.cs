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
        public static readonly string RemindForAviableOperations = "Доступні операції";

        #region Client KeyboardMarkups
        public static readonly string SiteText = "Сайт програми";
        public static readonly string SiteURL = "https://github.com/CyberCatOfUkraine/Cookie";
        public static readonly string CheckForProblems = "Переглянути чи є проблема на вашій вулиці";
        #endregion

        #region Engineer KeyboardMarkups
        public static readonly string StatOfFreeEmployees = "Статистика зайнятості працівників";
        public static readonly string StatOfPowerGridProblems = "Статистика проблем в електромережі";
        #endregion

        #region Electrician KeyboardMarkups
        public static readonly string NewTaskAvailability = "Доступні завдання";
        public static readonly string UpdateTaskState = "Звіт щодо стану завдання";
        #endregion
        
        #endregion

        #region Bot answers
        public static readonly string ActionToCheckForProblems = "Щоб переглянути чи є проблема на вашій вулиці введіть адресу:\n(Розділення комами обов'язкове)";
        public static readonly string InvalidInput = "Не вдалося виконати ваш запит :(";
        #endregion

        #region Intro 
        public static readonly string ClientIntro = "Доступні наступні функції:\n" +
                                                    "1) Перегляд проблем роботи електромережі за адресою\n" +
                                                    "2) Перегляд офіційного сайту боту\n";
        public static readonly string EngineerIntro = "Доступні наступні функції:\n" +
                                                      "1)Перегляд статистики щодо зайнятості працівників\n" +
                                                      "2)Перегляд статистики щодо проблем в електромережі\n";
        public static readonly string ElectricianIntro = "Доступні наступні функції:\n" +
                                                         "1)Перегляд доступних завдань" +
                                                         "2)Звітування щодо стану отриманого завдання";
        #endregion

    }
}

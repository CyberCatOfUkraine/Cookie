using System.Collections.Generic;
using System.Text;

namespace WPFUI.Models
{
    public class Employee
    {
        public Employee(string credentials, List<Access> accesses, long telegramId)
        {
            Credentials = credentials;
            Accesses = accesses;
            TelegramId = telegramId;
        }

        public int Id { get; set; }
        /// <summary>
        /// ПІБ
        /// </summary>
        public string Credentials { get; set; }

        /// <summary>
        /// Допуски до роботи до вказаного діапазону напруг
        /// </summary>
        public List<Access> Accesses { get; set; }

        /// <summary>
        /// Допуски до роботи до вказаного діапазону напруг в одному рядку
        /// </summary>
        public string AccessesWPF
        {
            get
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var access in Accesses)
                {
                    stringBuilder.AppendLine(access.Name);
                }
                return stringBuilder.ToString();
            }
        }

        /// <summary>
        /// ID особистого облікового запису в Телеграм, куди будуть доставлятися повідомлення про задачі 
        /// </summary>
        public long TelegramId { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DatabaseBroker.Models
{
    public class Employee
    {
        [Key]
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
        /// ID особистого облікового запису в Телеграм, куди будуть доставлятися повідомлення про задачі 
        /// </summary>
        public long TelegramID { get; set; }
    }

    
}
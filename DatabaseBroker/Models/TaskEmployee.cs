using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBroker.Models
{
    public class TaskEmployee
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

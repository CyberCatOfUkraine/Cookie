using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBroker.Models
{

    /// <summary>
    /// Об'єкт допуску до роботи до вказаного діапазону напруг
    /// </summary>
    public class Access
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

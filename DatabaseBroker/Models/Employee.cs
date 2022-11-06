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
    }

    /// <summary>
    /// Об'єкт допуску до роботи до вказаного діапазону напруг
    /// </summary>
    public class Access
    {
        [Key]
        public string Name { get; set; }
    }
}
using System.Collections.Generic;

namespace WPFUI.Models
{
    public class Employee
    {
        public Employee(string credentials, List<Access> accesses)
        {
            Credentials = credentials;
            Accesses = accesses;
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
    }

    /// <summary>
    /// Об'єкт допуску до роботи до вказаного діапазону напруг
    /// </summary>
    public class Access
    {
        public Access(string name)
        {
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
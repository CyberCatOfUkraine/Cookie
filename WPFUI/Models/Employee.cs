using System.Collections.Generic;
using System.Text;

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
    }
}
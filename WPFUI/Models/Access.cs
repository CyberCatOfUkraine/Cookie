using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI.Models
{

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

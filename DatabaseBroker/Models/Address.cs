using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseBroker.Models
{
    /// <summary>
    /// TODO: Перекласти адресу НОРМАЛЬНО а не як зараз
    /// </summary>
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string Oblast { get; set; }
        public string Rajon { get; set; }
        public string NaseleniyPunkt { get; set; }
        public string Vulicya { get; set; }
        public string Budinok { get; set; }
        public string Kvartira { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebUI.Models
{
    /// <summary>
    /// TODO: Перекласти адресу НОРМАЛЬНО а не як зараз
    /// </summary>
    public class Address
    {
        public Address(string oblast, string rajon, string naseleniyPunkt, string vulicya, string budinok, string? kvartira)
        {
            Oblast = oblast;
            Rajon = rajon;
            NaseleniyPunkt = naseleniyPunkt;
            Vulicya = vulicya;
            Budinok = budinok;
            Kvartira = kvartira;
        }

        public string Oblast { get; set; }
        public string Rajon { get; set; }
        public string NaseleniyPunkt { get; set; }
        public string Vulicya { get; set; }
        public string Budinok { get; set; }
        public string? Kvartira { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFUI.Models;

namespace WPFUI.Comparator
{
    internal class ClientMessageComparator:IComparer<ClientMessage>
    {

        /// <summary>
        /// Метод що мав би порівнювати два повідомлення від клієнта.
        /// Оскільки невідомо як порівняти "в нас світла нема" від трьох персон в один і той самий час то прийнято рішення
        /// порівнювати по геш-коду
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(ClientMessage x, ClientMessage y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (ReferenceEquals(null, y)) return 1;
            if (ReferenceEquals(null, x)) return -1;
            /*var idComparison = x.Id.CompareTo(y.Id);
            if (idComparison != 0) return idComparison;
            var textComparison = string.Compare(x.Text, y.Text, StringComparison.Ordinal);
            if (textComparison != 0) return textComparison;
            return x.RecivedTime.CompareTo(y.RecivedTime);*/
            return x.GetHashCode().CompareTo(y.GetHashCode());
        }
    }
}

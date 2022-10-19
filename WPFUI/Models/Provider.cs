using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI.Models
{
    public class Provider
    {
        public Provider(string name, List<Goods> goodsList, long telegramId)
        {
            Name = name;
            GoodsList = goodsList;
            TelegramId = telegramId;
        }
        public string Name { get; set; }
        public List<Goods> GoodsList { get; set; }
        public long TelegramId { get; set; }
    }
}

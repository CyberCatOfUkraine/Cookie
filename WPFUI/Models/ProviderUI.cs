using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFUI.Models
{
    internal class ProviderUI
    {

        public ProviderUI(string name, string goodsListText, long telegramId)
        {
            Name = name;
            GoodsListText = goodsListText;
            TelegramId = telegramId;
        }
        public string Name { get; set; }
        public string GoodsListText { get; set; }
        public long TelegramId { get; set; }
    }
}

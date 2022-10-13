using System.Reflection;
using System.Runtime.Loader;

namespace MessageBroker.TelegramBroker
{
    public class Broker
    {
        public void LoadTelegramBot()
        {
            //TODO:Insert security code
            Bot bot = new Bot();
            bot.Start();
        }
    }

}
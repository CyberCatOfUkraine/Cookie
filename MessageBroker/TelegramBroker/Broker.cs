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

        public void DeliverThisMessage(long TelegramID, string Message)
        {
            Bot bot = new Bot();
            bot.Start();
            bot.SendMessageIntoClient(TelegramID, Message);
        }
    }

}
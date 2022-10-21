namespace MessageBroker.TelegramBroker
{
    public class Broker
    {
        private Bot bot;
        public Broker()
        {
            bot = new Bot();

        }
        public void LoadTelegramBot()
        {
            //TODO:Insert security code
            if (!_isBotStarted)
            {
                bot.Start();
            }
            _isBotStarted = true;
        }
        private bool _isBotStarted;
        public void DeliverThisMessage(long telegramId, string message)
        {
            if (!_isBotStarted)
            {
                bot.Start();
            }
            _isBotStarted = true;
            bot.SendMessageIntoClient(telegramId, message);
        }
    }

}
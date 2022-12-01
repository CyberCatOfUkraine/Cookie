using DatabaseBroker;

namespace MessageBroker.TelegramBroker
{
    public class Broker
    {
        private readonly UnitOfCookie _unitOfCookie;
        private Bot bot;
        public Broker(UnitOfCookie unitOfCookie)
        {
            _unitOfCookie = unitOfCookie;
            MagicBox.Instance.SetUnitOfCookie(_unitOfCookie);
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
        public void SendMessage(long telegramId, string message)
        {
            if (!_isBotStarted)
            {
                bot.Start();
            }
            _isBotStarted = true;
            bot.SendMessageIntoClient(telegramId, message);
        }

        public void SetEngineerTelegramID(long telegramId)
        {
            MagicBox.Instance.SetEngineerTelegramID(telegramId);
        }

        /// <summary>
        /// Exception,engineer (true) or electrician (false), credentials
        /// </summary>
        public Action<Exception,bool,string> OnExceptionAction
        {
            get => bot.OnExceptionAction;
            set => bot.OnExceptionAction = value;
        }
    }

}
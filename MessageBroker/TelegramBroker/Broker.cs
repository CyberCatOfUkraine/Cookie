using DatabaseBroker;
using MessageBroker.TelegramBroker.Models;
using MessageBroker.TelegramBroker.User;
using MessageBroker.TelegramBroker.User.Tasks;

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
            TryStartBot();
        }
        private bool _isBotStarted;
        public void SendMessage(long telegramId, string message)
        {
            TryStartBot();
            bot.SendMessageIntoClient(telegramId, message);
        }

        public void SetEngineerTelegramID(long telegramId)
        {
            MagicBox.Instance.SetEngineerTelegramID(telegramId);
        }

        /// <summary>
        /// Exception,engineer (true) or electrician (false), credentials
        /// </summary>
        public Action<Exception,bool,bool,string> OnExceptionAction
        {
            get => bot.OnExceptionAction;
            set => bot.OnExceptionAction = value;
        }

        public void SendTask(int taskId, long chatId, string address, 
            Action onTaskRecived,Action onTaskStarted,Action onTaskFinished,Action onTaskCanceled)
        {
            TryStartBot();
            var actionHolder = new ActionHolder(onTaskRecived,onTaskStarted,onTaskFinished,onTaskCanceled);
            var task = new UserTask(taskId,MagicBox.Instance.GetUserByChatId(chatId),address,TaskState.Assigned,actionHolder);

            MagicBox.Instance.SetBotInstance(bot);
            MagicBox.Instance.ProcessTask(task);
            onTaskRecived.Invoke();
        }

        private void TryStartBot()
        {
            if (!_isBotStarted)
            {
                bot.Start();
            }
            _isBotStarted = true;
        }

        /// <summary>
        /// return taskId -1 if task not assigned
        /// </summary>
        /// <param name="chatId">Telegram chat id</param>
        /// <returns></returns>
        public (TaskState taskState, int TaskId) OperationTuple(int chatId)
        {
            return (MagicBox.Instance.GetCurrentTaskStateById(chatId), MagicBox.Instance.GetCurrentTaskId(chatId));
        }
        public void UpdateEmployees()
        {
            MagicBox.UpdateLocals();
        }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using MessageBroker.TelegramBroker.User;
using MessageBroker.TelegramBroker.User.User;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.ReplyMarkups;

namespace MessageBroker.TelegramBroker
{
    internal class Bot
    {
        private readonly ITelegramBotClient _botClient;

        public Bot() //TODO: EngineerUser, OrdinaryUser, Electrician
        {
            _botClient = new TelegramBotClient(TokenLoader.Token);
        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken)
        {
            MagicBox.UpdateLocals();
            var magicBox = MagicBox.Instance;

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;

                var chatId = message.From.Id;
                try
                {
                    if (message!.Text?.ToLower() == "/start")
                    {
                        //TODO:Check for acceptable id and give the message
                        await botClient.SendTextMessageAsync(message.Chat,
                            "Привіт!\n(На даний момент вся взаємодія з ботом відбувається за допомогою кнопок)",
                            cancellationToken: cancellationToken);
                        await botClient.SendTextMessageAsync(message.Chat,
                            MagicBox.Instance.GetDefaultIntroByChatId(chatId),
                            cancellationToken: cancellationToken,
                            replyMarkup: magicBox.GetKeyboardMarkupByChatId(chatId));
                    }
                    else
                    {
                        #region Invalid input

                        await botClient.SendTextMessageAsync(chatId, BotStrings.InvalidInput,
                            cancellationToken: cancellationToken, replyMarkup: KeyboardMarkups.InvalidInput);

                        #endregion
                    }
                }
                catch (Exception e)
                {
                    OnExceptionAction.Invoke(e,
                        magicBox.GetRoleChatById(chatId)==RoleEnum.Engineer,
                        magicBox.GetRoleChatById(chatId)==RoleEnum.Electrician,magicBox.TryGetCredentialsById(chatId));
                }
            }

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                var chatId = update.CallbackQuery!.From.Id;
                if (update.CallbackQuery.Data == BotStrings.AvailableOperationsList)
                {
                    await botClient.SendTextMessageAsync(chatId, magicBox.GetDefaultIntroByChatId(chatId),
                        cancellationToken: cancellationToken,
                        replyMarkup: magicBox.GetKeyboardMarkupByChatId(chatId));
                }
                if (update.CallbackQuery.Data == BotStrings.StatOfFreeEmployees)
                {
                    await botClient.SendTextMessageAsync(chatId,"Співробітників вільно:"+
                        MagicBox.Instance.GetStatOfFreeEmployees(),
                        cancellationToken: cancellationToken, replyMarkup: magicBox.GetKeyboardMarkupByChatId(chatId));
                }
                if (update.CallbackQuery.Data == BotStrings.StatOfPowerGridProblems)
                {
                    await botClient.SendTextMessageAsync(chatId, "Кількість проблем що вирішується: " +
                                                                 magicBox.GetStatOfPowerGridProblems(),
                        cancellationToken: cancellationToken,
                        replyMarkup: magicBox.GetKeyboardMarkupByChatId(chatId));
                }
                /*if (update.CallbackQuery.Data == BotStrings.StatOfPowerGridProblems)
                {
                    await botClient.SendTextMessageAsync(chatId, "Кількість проблем що вирішується: " +
                                                                 magicBox.GetStatOfPowerGridProblems(),
                        cancellationToken: cancellationToken,
                        replyMarkup: magicBox.GetKeyboardMarkupByChatId(chatId));
                }*/
                /*if (update.CallbackQuery.Data == BotStrings.AvailableOperationsList)
                {
                    await botClient.SendTextMessageAsync(chatId, magicBox.GetOperationById(chatId),
                        cancellationToken: cancellationToken,
                        replyMarkup: magicBox.GetKeyboardMarkupByChatId(chatId));
                }*/
                /*
                 *
                 *
                 *Опрацювати кожен пункт роботи електрика, взаємодія через MagickBox
                 *
                 *
                 *
                 *
                 */

            }
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,
            CancellationToken cancellationToken)
        {
            OnExceptionAction.Invoke(exception,false,false,string.Empty);
        }

        public void Start()
        {
            Console.WriteLine("Started already " + _botClient.GetMeAsync().Result.FirstName);
            
            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            _botClient.StartReceiving(
                HandleUpdateAsync,
                HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            MagicBox.UpdateLocals();
        }

        public async void SendMessageIntoClient(long telegramId, string message)
        {
            try
            {
                await _botClient.SendTextMessageAsync(telegramId, message);
            }
            catch (Exception e)
            {
                OnExceptionAction.Invoke(e,
                    MagicBox.Instance.GetRoleChatById(telegramId) == RoleEnum.Engineer,
                    MagicBox.Instance.GetRoleChatById(telegramId) == RoleEnum.Electrician, 
                    MagicBox.Instance.TryGetCredentialsById(telegramId));
            }
        }
        public async void SendTaskToElectrician(long telegramId, string message)
        {
            try
            {
                if (MagicBox.Instance.GetCurrentTaskId(telegramId)!=-1)
                {
                    await _botClient.SendTextMessageAsync(telegramId, message,replyMarkup:KeyboardMarkups.GetTaskRecievedMarkup);
                }
                else
                {
                    throw new InvalidOperationException("Can't send message to user");
                }
            }
            catch (Exception e)
            {
                OnExceptionAction.Invoke(e,
                    MagicBox.Instance.GetRoleChatById(telegramId) == RoleEnum.Engineer,
                    MagicBox.Instance.GetRoleChatById(telegramId) == RoleEnum.Electrician, 
                    MagicBox.Instance.TryGetCredentialsById(telegramId));
            }
        }
        public Action<Exception,bool,bool,string> OnExceptionAction;
    }
}

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
            var wrapper = UserOperationMikroWrapper.Instance;


            //TODO: Прийшли повідомлення? Узнать від кого, далі опрацювати і видати результат нагора
            //TODO: Принцип який, якщо користувач пише повідомлення то ми такі нажаль не можемо опрацювати ваш запит і дефолтне повідомлення з кучою кнопок типу
            //перейти на сайт, (так блет це фіча)дізнатися про найближче заплановане відключення, дізнатися про проблеми в вашому регіоні, змінити регіон

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                wrapper.TryAddUserToList(update.Message!.From!.Id);

                try
                {
                    var message = update.Message;

                    var chatId = message.From.Id;
                    if (message!.Text?.ToLower() == "/start")
                    {
                        //TODO:Check for acceptable id and give the message
                        await botClient.SendTextMessageAsync(message.Chat,
                            "Привіт!\n(На даний момент вся взаємодія з ботом відбувається за допомогою кнопок)",
                            cancellationToken: cancellationToken);
                        await botClient.SendTextMessageAsync(message.Chat,
                            UserOperationMikroWrapper.Instance.GetDefaultIntroByChatId(chatId),
                            cancellationToken: cancellationToken,
                            replyMarkup: wrapper.GetKeyboardMarkupByChatId(chatId));

                        await botClient.SendTextMessageAsync(chatId, BotStrings.CheckRoleAndTryChangeItIntro,
                            cancellationToken: cancellationToken, replyMarkup: KeyboardMarkups.InvalidInput);
                    }
                    /*else if (message.Text.Equals("i"))
                    {
                        UserOperationMikroWrapper.Instance.ChangeStatus(chatId, RoleEnum.Engineer);

                        await botClient.SendTextMessageAsync(message.Chat, "You engineer now",
                            cancellationToken: cancellationToken);
                    }
                    else if (message.Text.Equals("e"))
                    {
                        UserOperationMikroWrapper.Instance.ChangeStatus(chatId, RoleEnum.Electrician);

                        await botClient.SendTextMessageAsync(message.Chat, "You electrician now",
                            cancellationToken: cancellationToken);
                    }
                    else if (message.Text.Equals("c"))
                    {
                        UserOperationMikroWrapper.Instance.ChangeStatus(chatId, RoleEnum.Client);
                        await botClient.SendTextMessageAsync(message.Chat, "You client now",
                            cancellationToken: cancellationToken);
                    }
                    else if (message.Text.Equals("Check"))
                    {
                        var role = UserOperationMikroWrapper.Instance.GetRoleChatById(chatId);
                        var markup = UserOperationMikroWrapper.Instance.GetKeyboardMarkupByChatId(chatId);
                        await botClient.SendTextMessageAsync(message.Chat,
                            "Status:" + role,
                            cancellationToken: cancellationToken, replyMarkup: markup);
                    }*/

                    else
                    {
                        #region Invalid input

                        var keyboardMarkups = wrapper.GetKeyboardMarkupByChatId(chatId);
                        await botClient.SendTextMessageAsync(chatId, BotStrings.InvalidInput,
                            cancellationToken: cancellationToken, replyMarkup: KeyboardMarkups.InvalidInput);

                        #endregion
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                var chatId = update.CallbackQuery!.From.Id;
                wrapper.TryAddUserToList(chatId);

                try
                {
                    await botClient.DeleteMessageAsync(chatId, update.CallbackQuery.Message!.MessageId,
                        cancellationToken);

                }
                catch (Exception e)
                {
                    Console.WriteLine("Не вдалося стерти повідомлення: " + e.Message);

                }

                if (update.CallbackQuery.Data == BotStrings.AvailableOperationsList)
                {
                    await botClient.SendTextMessageAsync(chatId, BotStrings.AvailableOperations,
                        cancellationToken: cancellationToken, replyMarkup: wrapper.GetKeyboardMarkupByChatId(chatId));
                }
                if (update.CallbackQuery.Data == BotStrings.UpdateRoleRequest)
                {
                    await botClient.SendTextMessageAsync(chatId, BotStrings.ChooseRole,
                        cancellationToken: cancellationToken, replyMarkup: KeyboardMarkups.ChangeRoleKeyboardMarkup);
                }
                if (update.CallbackQuery.Data == BotStrings.Engineer)
                {
                    TryChangeRole(chatId,RoleEnum.Engineer);
                    await botClient.SendTextMessageAsync(chatId, BotStrings.RoleChangedOn+BotStrings.Engineer,
                        cancellationToken: cancellationToken, replyMarkup: wrapper.GetKeyboardMarkupByChatId(chatId));
                }
                if (update.CallbackQuery.Data == BotStrings.Electrician)
                {
                    TryChangeRole(chatId,RoleEnum.Electrician);
                    await botClient.SendTextMessageAsync(chatId, BotStrings.RoleChangedOn+BotStrings.Electrician,
                        cancellationToken: cancellationToken, replyMarkup: wrapper.GetKeyboardMarkupByChatId(chatId));
                }
                if (update.CallbackQuery.Data == BotStrings.Client)
                {
                    TryChangeRole(chatId,RoleEnum.Client);
                    await botClient.SendTextMessageAsync(chatId, BotStrings.RoleChangedOn+BotStrings.Client,
                        cancellationToken: cancellationToken, replyMarkup: wrapper.GetKeyboardMarkupByChatId(chatId));
                }
            }

        }

        private void TryChangeRole(long chatId, RoleEnum roleEnum)
        {
            UserOperationMikroWrapper.Instance.ChangeStatus(chatId, roleEnum);
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,
            CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
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
        }

        public async void SendMessageIntoClient(long telegramId, string message)
        {
            await _botClient.SendTextMessageAsync(telegramId, message);
        }
    }
}

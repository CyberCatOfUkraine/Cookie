using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Requests;
using Telegram.Bot.Types.ReplyMarkups;

namespace MessageBroker.TelegramBroker
{
    internal class Bot
    {
        private readonly ITelegramBotClient _botClient = new TelegramBotClient(TokenLoader.Token);
        private string text1 = "посилання1";
        private string text2 = "посилання2";

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
            CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var inlineKeyboard = new InlineKeyboardMarkup(new[]
                {
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData(text1),
                    },
                    new[]
                    {
                        InlineKeyboardButton.WithCallbackData(text2),
                    }
                });
                try
                {
                    var message = update.Message;

                   var chatId = message.From.Id;
                    if (message!.Text?.ToLower() == "/start")
                    {
                        //TODO:Check for acceptable id and give the message
                        await botClient.SendTextMessageAsync(message.Chat, "Your chat id: "+chatId, cancellationToken: cancellationToken);
                        return;
                    }

                    if (message.Text.ToLower() == "del")
                    {
                        for (var i = 0; i <= update.Message.MessageId; i++)
                        {
                            try
                            {
                                await botClient.DeleteMessageAsync(chatId, i);

                            }
                            catch (Exception e)
                            {
                                Console.WriteLine(e);
                            }
                        }
                    }

                    await botClient.SendTextMessageAsync(message.Chat, "TestMessage!!",
                        cancellationToken: cancellationToken);
                    await botClient.SendTextMessageAsync(chatId, "доступные кнопки",
                        cancellationToken: cancellationToken, replyMarkup: inlineKeyboard);
                    await botClient.SendTextMessageAsync(417208492, "Message delivered!",
                        cancellationToken: cancellationToken);
                    MenuButton menuButton = new MenuButtonDefault();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
               var chatId = update.CallbackQuery.From.Id;

                if (update.CallbackQuery.Data == text1)
                {
                    await botClient.SendTextMessageAsync(chatId, "text1",
                        cancellationToken: cancellationToken);

                }
                else if (update.CallbackQuery.Data == text2)
                {
                    await botClient.SendTextMessageAsync(chatId, "text2",
                        cancellationToken: cancellationToken);

                }
                else
                {
                    await botClient.SendTextMessageAsync(chatId, "text not found",
                        cancellationToken: cancellationToken);
                }
            }


        }

        public  async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Некоторые действия
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
        }


        void Main()
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
            Console.ReadLine();
        }
        public void Start()
        {
            Main();
        }

        public async void SendMessageIntoClient(long telegramId, string message)
        {
            await _botClient.SendTextMessageAsync(telegramId, message);
        }
    }
}

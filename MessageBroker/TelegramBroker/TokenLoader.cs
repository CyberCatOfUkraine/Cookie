using static System.String;

namespace MessageBroker.TelegramBroker
{
    public static class TokenLoader
    {
        public static string Token => File.ReadAllTextAsync("C:\\SecureData\\TOKEN").Result;
    }
}
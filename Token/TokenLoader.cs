using static System.String;

namespace Token
{
    public static class TokenLoader
    {
        public static string Get => File.ReadAllTextAsync("C:\\SecureData\\TOKEN").Result;
    }
}
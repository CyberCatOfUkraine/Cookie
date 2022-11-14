using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookie
{
    public class MessagesTextWriter
    {
        public static void WriteToFile(string text)
        {
            WriteToFile(text, GeneralStrings.ClientMessagesDirectoryPath);
        }

        private static async void WriteToFile(string text, string directoryPath)
        {
            string dateTime =
                DateTime.Now.Year + "_" +
                DateTime.Now.Month + "_" +
                DateTime.Now.Day + "_" +
                DateTime.Now.Hour + "_" +
                DateTime.Now.Minute + "_" +
                DateTime.Now.Second + "_";
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            var fileStream = File.Create(Path.Combine(directoryPath, dateTime));
            var streamWriter = new StreamWriter(fileStream);
            await streamWriter.WriteAsync(text);
            await streamWriter.FlushAsync();
            streamWriter.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookie
{
    internal static class MessagesTextReader
    {
        public static string[] ReadFromFiles()
        {
           return ReadFromFiles(GeneralStrings.ClientMessagesDirectoryPath).Result;
        }

        private static async Task<string[]> ReadFromFiles(string directoryPath)
        {
            var files = Directory.EnumerateFiles(directoryPath);

            var filesTextList = new List<string>();
            foreach (var file in files)
            {
                var fileReader = new StreamReader(file);
                filesTextList.Add(await fileReader.ReadToEndAsync());
                fileReader.Close();
            }

            foreach (var file in files)
            {
                File.Delete(file);
            }

            return filesTextList.ToArray();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookie
{
    public static class MessagesTextReader
    {
        public static List<string> DeletableFilesList = new();
        public static string[] ReadFromFiles()
        {
            var files = Directory.EnumerateFiles(GeneralStrings.ClientMessagesDirectoryPath);

            var filesTextList = new List<string>();
            foreach (var file in files)
            {
                filesTextList.Add(File.ReadAllText(file));
            }

            foreach (var file in files)
            {
                DeletableFilesList.Add(file);
            }

            return filesTextList.ToArray();
        }
    }
}

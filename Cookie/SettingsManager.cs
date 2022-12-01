using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cookie
{
    public class SettingsManager
    {
        public long EngineerTelegramID 
        {
            get => Convert.ToInt64(ReadFromTgIdFile(GeneralStrings.SettingsDirectoryPath));
            set => WriteToTgIdFile(value.ToString(), GeneralStrings.SettingsDirectoryPath);
        }

        public string EngineerTelegramidFileName =>
            Path.Combine(GeneralStrings.SettingsDirectoryPath, EngineerTelegramIdFile);

        private readonly string EngineerTelegramIdFile = "Engineer.jabml";
        private async void WriteToTgIdFile(string text, string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var filePath = Path.Combine(directory, EngineerTelegramIdFile);
            var fileStream = File.Open(filePath, FileMode.OpenOrCreate);
            var streamWriter = new StreamWriter(fileStream);
            await streamWriter.WriteAsync(directory.ToCharArray());
            await streamWriter.FlushAsync();
            streamWriter.Close();
        }

        private string ReadFromTgIdFile(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            if (!File.Exists(Path.Combine(directory, EngineerTelegramIdFile)))
            {
                File.Create(Path.Combine(directory, EngineerTelegramIdFile));
                return (-1).ToString();
            }
            else
            {
                var text= File.ReadAllText(Path.Combine(directory, EngineerTelegramIdFile));
                if (string.IsNullOrEmpty(text)||string.IsNullOrWhiteSpace(text))
                {
                    return (-1).ToString();
                }
                return text;
            }
        }

    }
}

// See https://aka.ms/new-console-template for more information

using System.Xml.Serialization;
using MessageBroker.TelegramBroker;

Console.WriteLine("Hello, World!");
new Broker().LoadTelegramBot();
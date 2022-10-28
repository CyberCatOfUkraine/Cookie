// See https://aka.ms/new-console-template for more information
/*Console.WriteLine("Bot started");
MessageBroker.TelegramBroker.Broker broker=new();
broker.LoadTelegramBot();
Console.ReadKey();*/

using Autofac;

var s=Newtonsoft.Json.JsonConvert.SerializeObject(new Jaba(){Name = "Test frog"});
Console.WriteLine(Newtonsoft.Json.JsonConvert.DeserializeObject<Jaba>(s).Name);

Console.ReadKey();
var cb = new ContainerBuilder();
class Jaba
{
    public string Name { get; set; }
}
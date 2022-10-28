// See https://aka.ms/new-console-template for more information
/*Console.WriteLine("Bot started");
MessageBroker.TelegramBroker.Broker broker=new();
broker.LoadTelegramBot();
Console.ReadKey();*/

using DatabaseBroker;
using DatabaseBroker.Models;
using DatabaseBroker.Repository;


var repository = new UnitOfCookie().AccessRepository;

Console.WriteLine("Enter name for a new Access:");
var name = Console.ReadLine();
var access = new Access {Name = name };
repository.Create(access);
repository.SaveChanges();

//var query = from acess in db.Accesses orderby access.Name select access;

Jaba.Print(repository);


Console.WriteLine("add one and remove first");
repository.Create(new Access(){Name = name+"1"});
repository.RemoveBy(x=>x.Name==name);
repository.SaveChanges();
Jaba.Print(repository);



Console.WriteLine("add two and update second");
repository.Create(new Access(){Name = name+"2"});
repository.Create(new Access(){Name = name+"3"});
repository.SaveChanges();
Jaba.Print(repository);



repository.GetAll().Last().Name = "abracadabra";
repository.Update(x=>x.Id==repository.GetAll().Last().Id, repository.GetAll().Last());
repository.SaveChanges();
Jaba.Print(repository);



Console.WriteLine("clear all");
repository.Clear();
repository.SaveChanges();
Jaba.Print(repository);



Console.WriteLine("Press any key to exit...");
Console.ReadKey();

static class Jaba
{
    public static void Print(IRepository<Access> uof)
    {
        Console.WriteLine("All acesses in database");
        foreach (var acess in uof.GetAll())
        {
            Console.WriteLine("id: " + acess.Id+"\t\tname:" + acess.Name+"\n\n");
        }
    }
}

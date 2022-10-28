using DataBroker;
using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class MessagesController : Controller
    {
        private SQLDatabaseBroker<ClientMessage> database;
        public IActionResult Index()
        {
            PrintDatabaseCount();

            return View();
        }

        private void PrintDatabaseCount()
        {
           var foregroundColor= Console.ForegroundColor;
           var backgroundColor=Console.BackgroundColor;

           Console.BackgroundColor = ConsoleColor.DarkGreen;
           Console.ForegroundColor = ConsoleColor.Black;
           foreach (var message in database.GetAll())
           {
               Console.WriteLine(message.Id);
               Console.WriteLine(message.RecivedTime);
               Console.WriteLine(message.Text);
               Console.WriteLine(message.Address.ToString());
           }
        }


        public IActionResult Create(ClientMessage message)
        {
            database.Create(message);
            return RedirectToAction(nameof(Index));
        }
    }
}

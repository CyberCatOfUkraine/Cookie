using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class MessagesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create(ClientMessage message)
        {
            //database.Create(message);
            return RedirectToAction(nameof(Index));
        }
    }
}

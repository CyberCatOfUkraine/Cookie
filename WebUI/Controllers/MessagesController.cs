using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class MessagesController : Controller
    {
        public IActionResult Index()
        {
            var message = TempData["Message"];
            if (message!=null)
            {
                ViewBag.Message = message;
            }
            return View();
        }
        public IActionResult Create(ClientMessage message)
        {
            message.RecivedTime=DateTime.Now;
            TempData["Message"] = message.Text;
            return RedirectToAction(nameof(Index));
        }
    }
}

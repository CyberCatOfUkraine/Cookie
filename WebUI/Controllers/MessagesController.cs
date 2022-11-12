using Cookie;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

            MessagesTextWriter.WriteToFile(JsonConvert.SerializeObject(message));

            TempData["Message"] = message.Text;
            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class MapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

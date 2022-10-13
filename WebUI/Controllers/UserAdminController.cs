using Microsoft.AspNetCore.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class UserAdminController : Controller
    {
        private static List<UserViewModel> users = new List<UserViewModel>();
        public IActionResult Index()
        {
            return View(users);
        }

        public IActionResult CreateUser()
        {
            return View();
        }

        public IActionResult Create(UserViewModel user)
        {
            user.Index = new Random().Next(10, 50);
            users.Add(user);

            return RedirectToAction(nameof(Index));
        }
    }
}

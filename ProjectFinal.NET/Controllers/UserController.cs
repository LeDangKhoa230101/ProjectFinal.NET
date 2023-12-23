using Microsoft.AspNetCore.Mvc;

namespace ProjectFinal.NET.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Login()
        { 
            return View();
        }

        public IActionResult Register()
        {
            return View(); 
        }
    }
}

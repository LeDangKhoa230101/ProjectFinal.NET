using Microsoft.AspNetCore.Mvc;

namespace ProjectFinal.NET.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

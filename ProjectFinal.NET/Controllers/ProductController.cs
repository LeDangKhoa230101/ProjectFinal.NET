using Microsoft.AspNetCore.Mvc;

namespace ProjectFinal.NET.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Detail()
        {
            return View(); 
        }

        public IActionResult ProductManager()
        {
            return View();
        }
    }
}

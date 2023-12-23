using Microsoft.AspNetCore.Mvc;

namespace ProjectFinal.NET.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Cart()
        {
            return View(); 
        }
    }
}

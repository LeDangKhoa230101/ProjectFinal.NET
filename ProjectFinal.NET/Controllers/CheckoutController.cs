using Microsoft.AspNetCore.Mvc;

namespace ProjectFinal.NET.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Checkout()
        {
            return View();
        } 
    }
}

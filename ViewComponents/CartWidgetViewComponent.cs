using Microsoft.AspNetCore.Mvc;
using ProjectFinal.NET.Helper;
using ProjectFinal.NET.Models;

namespace ProjectFinal.NET.Components
{
    public class CartWidgetViewComponent:ViewComponent
    {
         
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();
            return View(new CartModel { 
                Widget = cart.Sum(p => p.Quantity)
            });
        }
    }
}

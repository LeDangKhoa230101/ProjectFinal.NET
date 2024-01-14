using Microsoft.AspNetCore.Mvc;
using ProjectFinal.NET.Data;
using ProjectFinal.NET.Helper;
using ProjectFinal.NET.Models;

namespace ProjectFinal.NET.Controllers
{
    public class CartController : Controller
    {
        private readonly DotnetContext db;

        public CartController(DotnetContext context)
        {
            db = context;
        }

        public List<CartItem> ListCart => HttpContext.Session.Get<List<CartItem>>(MySetting.CART_KEY) ?? new List<CartItem>();

        public IActionResult Cart()
        {
            return View(ListCart); 
        }

        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var cart = ListCart;
            var item = cart.SingleOrDefault(p => p.IdProduct == id);
            if (item == null)
            {
                var product = db.Products.SingleOrDefault(p => p.IdProduct == id);
                item = new CartItem
                {
                    IdProduct = product.IdProduct,
                    ProductName = product.NameProduct,
                    Image = product.Image,
                    Price = (int)product.PriceNew,
                    Quantity = quantity
                };
                cart.Add(item);
            }
            else
            {
                item.Quantity += quantity;
            }

            HttpContext.Session.Set(MySetting.CART_KEY, cart);

            return RedirectToAction("Cart");
        }

        public IActionResult RemoveCart(int id)
        {
            var cart = ListCart;
            var item = cart.SingleOrDefault(p => p.IdProduct == id);
            if (item != null)
            {
                cart.Remove(item);
                HttpContext.Session.Set(MySetting.CART_KEY, cart);
            }

            return RedirectToAction("Cart");
        }

        public IActionResult UpdateCart(int id)
        {
            var cart = ListCart;
            var item = cart.SingleOrDefault(p => p.IdProduct == id);
            if (item?.Quantity == 1)
            {
                cart.Remove(item);
            } else
            {
                item.Quantity -= 1;
            }

            HttpContext.Session.Set(MySetting.CART_KEY, cart);

            return RedirectToAction("Cart");
        }

    }
}

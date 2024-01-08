using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectFinal.NET.Data;
using ProjectFinal.NET.Models;
namespace ProjectFinal.NET.Controllers
{
    public class ProductController : Controller
    {
        public readonly DotnetContext db;
        public ProductController(DotnetContext context)
        {
            this.db = context;
        }
        public IActionResult Detail(int id)
        {
            var data = db.Products.Include(p => p.IdCateNavigation).SingleOrDefault(p => p.IdProduct == id);
            if (data == null)
            {
                TempData["Message"] = $"không thấ mã sản phẩm có mã {id}";
                return Redirect("/404");
            }
            var result = new ProductsDetailVM
            {
                IdProduct = data.IdProduct,
                ProductName = data.NameProduct,
                Image = data.Image,
                PriceNew = data.PriceNew ?? 0,
                PriceOld = data.PriceOld ?? 0,
                Quantity = data.Quantity ?? 0,
                Description = data.Description,
                Brand = data.IdBrandNavigation.BrandName,
                Cate = data.IdCateNavigation.CateName,
                Review = "đfd",
                Sales = 2
            };


            return View(result);
        }

        public IActionResult ProductManager()
        {
            return View();
        }
    }
}

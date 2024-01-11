using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProjectFinal.NET.Data;
using ProjectFinal.NET.Models;
using System.Diagnostics;
namespace ProjectFinal.NET.Controllers
{
    public class ProductController : Controller
    {
        public readonly DotnetContext db;
        public ProductController(DotnetContext context)
        {
            db = context;
        }
        [Route("Product/Detail/{productId}")]
        public IActionResult Detail(int productId)
        {
            var data = db.Products
        .Include(p => p.IdBrandNavigation)
        .Include(p => p.IdCateNavigation)
        .FirstOrDefault(p => p.IdProduct == productId);


            if (data == null)
            {
                TempData["Message"] = $"không thấ mã sản phẩm có mã {productId}";
                return NotFound();
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
                Brand = data.IdBrandNavigation.BrandName ?? "DefaultBrand",
                Cate = data.IdCateNavigation.CateName ?? "DefaultCategory",
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

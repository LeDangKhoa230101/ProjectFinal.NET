using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Detail(int? cate)
        {
            var product = db.Products.AsQueryable();
            if (cate.HasValue) {
                product = product.Where(p => p.IdCate == cate.Value);
            }
            var result = product.Select(p => new ProductVM
            {
                IdProduct =p.IdProduct,
                ProductName = p.NameProduct,
                Image = p.Image,
                PriceNew = p.PriceNew ?? 0,
                PriceOld = p.PriceOld ?? 0,
                Quantity = p.Quantity ?? 0,
                Description = p.Description,
                Brand = p.IdBrandNavigation.BrandName,
                Cate = p.IdCateNavigation.CateName

               
            });

            return View(result); 
        }

        public IActionResult ProductManager()
        {
            return View();
        }
    }
}

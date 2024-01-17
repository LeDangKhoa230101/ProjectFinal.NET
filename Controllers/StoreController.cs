using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectFinal.NET.Data;

namespace ProjectFinal.NET.Controllers
{
    public class StoreController : Controller
    {
        public readonly DotnetContext db;
        public StoreController(DotnetContext context)
        {
            db = context;
        }

        public IActionResult Store(int? brandId, int? categoryId, string searchTerm)
        {
            ViewBag.Categories = GetCategories();
            ViewBag.Brands = GetBrands();

            IQueryable<Product> products = db.Products.AsQueryable();

            if (brandId.HasValue)
            {
                products = products.Where(p => p.IdBrand == brandId.Value);
            }

            if (categoryId.HasValue)
            {
                products = products.Where(p => p.IdCate == categoryId.Value);
            }
            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(p => p.NameProduct.Contains(searchTerm));
            }

            List<Product> filteredProducts = products.ToList();

            // Pass the searchTerm to the view to display it
            ViewBag.SearchTerm = searchTerm;

            return View(filteredProducts);
        }
        private List<Brand> GetBrands()
        {
            // Replace this with your actual logic to fetch brands from the database
            return db.Brands.ToList();
        }
        private List<Category> GetCategories()
        {
            // Replace this with your actual logic to fetch brands from the database
            return db.Categories.ToList();
        }
    }
}

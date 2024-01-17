using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectFinal.NET.Data;
using System.Diagnostics;

namespace ProjectFinal.NET.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly DotnetContext db;

        public HomeController(ILogger<HomeController> logger, DotnetContext context)
		{
			_logger = logger;
			db = context;
		}

		public IActionResult Index()
		{
            var products = db.Products
    .Include(p => p.IdCateNavigation) // Kèm theo thông tin về danh mục
    .Select(p => new
    {
        IdProduct = p.IdProduct,
        NameProduct = p.NameProduct,
        Image = p.Image,
        PriceNew = p.PriceNew ?? 0,
        PriceOld = p.PriceOld ?? 0,
        Quantity = p.Quantity ?? 0,
        Description = p.Description,
        IdBrand = p.IdBrand ?? 0,
        IdCate = p.IdCate ?? 0,
        Cate = p.IdCateNavigation.CateName ?? "DefaultCategory", // Sử dụng thông tin từ navigation property
    })
    .OrderByDescending(x => x.IdProduct)
    .Take(4)
    .ToList();
            return View(products);
        }

    }
}

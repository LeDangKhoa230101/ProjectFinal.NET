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

        public async Task<IActionResult> ProductManager()
        {
            return db.Products != null ? View(await db.Products.ToListAsync()) :
                        Problem("Entity set 'Project Net'  is null.");
        }


        public async Task<IActionResult> DetailManager(int? id)
        { 
            if (id == null || db.Products == null)
            {
                return NotFound();
            }

            var products = await db.Products 
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("IdProduct,NameProduct,Image,PriceNew,PriceOld,Quantity,Description,IdBrand,IdCate")] 
                Product product)
        {
            if (ModelState.IsValid)
            {
                db.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(ProductManager));
            }
            return View(product); 
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Products == null)
            {
                return NotFound();
            }

            var product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("IdProduct,NameProduct,Image,PriceNew,PriceOld,Quantity,Description,IdBrand,IdCate")]
            Product product)
        {
            if (id != product.IdProduct)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(product);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.IdProduct))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ProductManager));
            }
            return View(product);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Products == null)
            {
                return NotFound();
            }

            var product = await db.Products
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Products == null)
            {
                return Problem("Entity set 'Project Net'  is null.");
            }
            var product = await db.Products.FindAsync(id);
            if (product != null)
            {
                db.Products.Remove(product);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(ProductManager));
        }
        

        private bool ProductExists(int id)
        {
            return (db.Products?.Any(e => e.IdProduct == id)).GetValueOrDefault();
        }

    }
}

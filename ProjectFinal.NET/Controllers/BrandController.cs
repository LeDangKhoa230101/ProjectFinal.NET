using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectFinal.NET.Data;

namespace ProjectFinal.NET.Controllers
{
    public class BrandController : Controller
    {
        public readonly DotnetContext db;
        public BrandController(DotnetContext context)
        {
            db = context; 
        }

        public async Task<IActionResult> BrandManager()
        {
            return db.Brands != null ? View(await db.Brands.ToListAsync()) :
                        Problem("Entity set 'Project Net'  is null.");
        }
         
        public async Task<IActionResult> DetailManager(int? id)
        {
            if (id == null || db.Brands == null)
            {
                return NotFound();
            }

            var brand = await db.Brands
                .FirstOrDefaultAsync(m => m.IdBrand == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("IdBrand,BrandName")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                db.Add(brand);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(BrandManager));
            }
            return View(brand);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Brands == null)
            {
                return NotFound();
            }

            var brand = await db.Brands.FindAsync(id);
            if (brand == null)
            {
                return NotFound();
            }
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("IdBrand,BrandName")] Brand brand)
        {
            if (id != brand.IdBrand)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(brand);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.IdBrand))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(BrandManager));
            }
            return View(brand);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Brands == null)
            {
                return NotFound();
            }

            var brand = await db.Brands
                .FirstOrDefaultAsync(m => m.IdBrand == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Brands == null)
            {
                return Problem("Entity set 'Project Net'  is null.");
            }
            var brand = await db.Brands.FindAsync(id);
            if (brand != null)
            {
                db.Brands.Remove(brand);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(BrandManager));
        }


        private bool BrandExists(int id)
        {
            return (db.Brands?.Any(e => e.IdBrand == id)).GetValueOrDefault();
        }
    }
}

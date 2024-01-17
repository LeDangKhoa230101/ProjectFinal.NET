using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectFinal.NET.Data;

namespace ProjectFinal.NET.Controllers
{
    public class CategoryController : Controller
    {
        public readonly DotnetContext db;
        public CategoryController(DotnetContext context)
        {
            db = context;
        }

        public async Task<IActionResult> CategoryManager()
        {
            return db.Categories != null ? View(await db.Categories.ToListAsync()) :
                        Problem("Entity set 'Project Net'  is null.");
        }
         
        public async Task<IActionResult> DetailManager(int? id)
        {
            if (id == null || db.Categories == null)
            {
                return NotFound();
            }

            var category = await db.Categories
                .FirstOrDefaultAsync(m => m.IdCate == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("IdCate,CateName")]
                Category category)
        {
            if (ModelState.IsValid)
            {
                db.Add(category);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(CategoryManager));
            }
            return View(category);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || db.Categories == null)
            {
                return NotFound();
            }

            var category = await db.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("IdCate,CateName")]
            Category category)
        {
            if (id != category.IdCate)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(category);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.IdCate))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(CategoryManager));
            }
            return View(category);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || db.Categories == null)
            {
                return NotFound();
            }

            var category = await db.Categories
                .FirstOrDefaultAsync(m => m.IdCate == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (db.Categories == null)
            {
                return Problem("Entity set 'Project Net'  is null.");
            }
            var category = await db.Categories.FindAsync(id);
            if (category != null)
            {
                db.Categories.Remove(category);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(CategoryManager));
        }


        private bool CategoryExists(int id)
        {
            return (db.Categories?.Any(e => e.IdCate == id)).GetValueOrDefault();
        }
    }
}

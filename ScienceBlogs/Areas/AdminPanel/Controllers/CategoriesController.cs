using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScienceBlogs.Models;
using Microsoft.AspNetCore.Hosting;

namespace ScienceBlogs.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [Authorize(Roles = "Admin")]
    public class CategoriesController : Controller
    {
        private readonly BlogsContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

		public CategoriesController(BlogsContext context,IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
		}

        // GET: AdminPanel/Categories
        public async Task<IActionResult> Index()
        {
              return View(await _context.Categories.ToListAsync());
        }

        // GET: AdminPanel/Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: AdminPanel/Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminPanel/Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Description,File")] Category category)
        {
            if (ModelState.IsValid)
            {
				//save image to wwwroot/Images/Categories
				string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(category.File.FileName);
                string extension = Path.GetExtension(category.File.FileName);
				category.Image =fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Images/Categories/", fileName);
                using(var fileStream = new FileStream(path,FileMode.Create))
                {
                    await category.File.CopyToAsync(fileStream);

				}

				_context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        // GET: AdminPanel/Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: AdminPanel/Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Description,File")] Category category)
        {
            if (id != category.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					//save image to wwwroot/Images/Categories
					string wwwRootPath = _hostEnvironment.WebRootPath;
					string fileName = Path.GetFileNameWithoutExtension(category.File.FileName);
					string extension = Path.GetExtension(category.File.FileName);
					category.Image = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
					string path = Path.Combine(wwwRootPath + "/Images/Categories/", fileName);
					using (var fileStream = new FileStream(path, FileMode.Create))
					{
						await category.File.CopyToAsync(fileStream);

					}

					_context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: AdminPanel/Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Categories == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: AdminPanel/Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Categories == null)
            {
                return Problem("Entity set 'BlogsContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                //delete image from wwwroot/Images/Categories
                var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Images/Categories", category.Image);
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);

				_context.Categories.Remove(category);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
          return _context.Categories.Any(e => e.ID == id);
        }
    }
}

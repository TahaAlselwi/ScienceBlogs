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
    public class BlogsController : Controller
    {
        private readonly BlogsContext _context;
		private readonly IWebHostEnvironment _hostEnvironment;
		public BlogsController(BlogsContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
			this._hostEnvironment = hostEnvironment;

		}

        // GET: AdminPanel/Blogs
        public async Task<IActionResult> Index()
        {
            var blogsContext = _context.Blogs.Include(b => b.Category);
            return View(await blogsContext.ToListAsync());
        }

        // GET: AdminPanel/Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Blogs == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: AdminPanel/Blogs/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name");
            return View();
        }

        // POST: AdminPanel/Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Description,Date,Topic,CategoryID,Author,file")] Blog blog)
        {
            if (ModelState.IsValid)
            {

				//save image to wwwroot/Images/Blogs
				string wwwRootPath = _hostEnvironment.WebRootPath;
				string fileName = Path.GetFileNameWithoutExtension(blog.file.FileName);
				string extension = Path.GetExtension(blog.file.FileName);
				blog.Image = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
				string path = Path.Combine(wwwRootPath + "/Images/Blogs/", fileName);
				using (var fileStream = new FileStream(path, FileMode.Create))
				{
					await blog.file.CopyToAsync(fileStream);

				}

				_context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name", blog.CategoryID);
            return View(blog);
        }
   
        // GET: AdminPanel/Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Blogs == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name", blog.CategoryID);
            return View(blog);
        }

        // POST: AdminPanel/Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Title,Description,Date,Topic,CategoryID,Author,file")] Blog blog)
        {
            if (id != blog.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					//save image to wwwroot/Images/Blogs
					string wwwRootPath = _hostEnvironment.WebRootPath;
					string fileName = Path.GetFileNameWithoutExtension(blog.file.FileName);
					string extension = Path.GetExtension(blog.file.FileName);
					blog.Image = fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
					string path = Path.Combine(wwwRootPath + "/Images/Blogs/", fileName);
					using (var fileStream = new FileStream(path, FileMode.Create))
					{
						await blog.file.CopyToAsync(fileStream);

					}

					_context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.ID))
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
            ViewData["CategoryID"] = new SelectList(_context.Categories, "ID", "Name", blog.CategoryID);
            return View(blog);
        }

        // GET: AdminPanel/Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Blogs == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .Include(b => b.Category)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: AdminPanel/Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Blogs == null)
            {
                return Problem("Entity set 'BlogsContext.Blogs'  is null.");
            }
            var blog = await _context.Blogs.FindAsync(id);
            if (blog != null)
            {
				//delete image from wwwroot/Images/Blogs
				var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Images/Blogs", blog.Image);
				if (System.IO.File.Exists(imagePath))
					System.IO.File.Delete(imagePath);

				_context.Blogs.Remove(blog);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
          return _context.Blogs.Any(e => e.ID == id);
        }
    }
}

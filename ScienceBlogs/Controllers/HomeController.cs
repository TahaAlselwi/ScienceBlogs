using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using ScienceBlogs.Models;
using System.Diagnostics;

namespace ScienceBlogs.Controllers
{
    public class HomeController : Controller
    {
        BlogsContext blog;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,BlogsContext context)
        {
            _logger = logger;
            blog = context;
        }

        public IActionResult Index()
        {
            var c = blog.Categories.ToList();
            return View(c);
        }

        public IActionResult Blogs(int id)
        {
            var B = blog.Blogs.Where(x => x.CategoryID == id).ToList();
            return View(B);
        }
		public IActionResult Blog(int id)
        {
			var B2 = blog.Blogs.FirstOrDefault(x => x.ID == id);

			return View(B2);
		}
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
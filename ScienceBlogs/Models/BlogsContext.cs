using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace ScienceBlogs.Models
{
	public class BlogsContext : DbContext
	{
		public BlogsContext(DbContextOptions<BlogsContext> options)
			: base(options)
		{
		}
		public DbSet<Category> Categories { get; set; }
		public DbSet<Blog> Blogs { get; set; }
	}
}
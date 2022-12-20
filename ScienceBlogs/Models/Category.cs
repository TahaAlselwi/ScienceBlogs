using Microsoft.Extensions.Hosting;

namespace ScienceBlogs.Models
{
	public class Category
	{
		public int ID { get; set; }
		public string Image { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public ICollection<Blog> Blogs { get; set; }
	}
}

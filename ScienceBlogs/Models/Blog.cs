using System.ComponentModel.DataAnnotations;

namespace ScienceBlogs.Models
{
	public class Blog
	{
		[Key]
		public int ID { get; set; }
		public string Image { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime Date { get; set; }
		public string Topic { get; set; }
		public int CategoryID { get; set; }
		public Category Category { get; set; }
		public string Author { get; set; }
	}
}

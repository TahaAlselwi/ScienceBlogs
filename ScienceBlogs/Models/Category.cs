using Microsoft.Extensions.Hosting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace ScienceBlogs.Models
{
	public class Category
	{
		public int ID { get; set; }

        [Required(ErrorMessage = "You have to fill the Name ")]
        [Display(Name = "Name")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "You have to fill the Description ")]
        [Display(Name = "Description")]
        public string? Description { get; set; }

        public string? Image { get; set; }

        [NotMapped]
        public IFormFile? File { get; set; }
        public ICollection<Blog>? Blogs { get; set; }
	}
}

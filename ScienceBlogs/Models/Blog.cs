using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScienceBlogs.Models
{
	public class Blog
	{
		[Key]
		public int ID { get; set; }

        [Required(ErrorMessage = "You have to fill the Title ")]
        [Display(Name = "Title")]
        public string? Title { get; set; }
        [Required(ErrorMessage = "You have to fill the Description ")]
        [Display(Name = "Description")]
        public string? Description { get; set; }
		public DateTime Date { get; set; }
        [Required(ErrorMessage = "You have to fill the Topic ")]
        [Display(Name = "Topic")]
        public string? Topic { get; set; }
		public int CategoryID { get; set; }
		public Category? Category { get; set; }
		public string? Author { get; set; }
        [Display(Name = "Image")]

        [NotMapped]
        public IFormFile? file { get; set; }
        public string? Image { get; set; }
    }
}

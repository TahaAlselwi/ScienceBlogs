using Microsoft.AspNetCore.Identity;
using Microsoft.Build.Framework;

namespace ScienceBlogs.Models
{
    public class UserDetails: IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}

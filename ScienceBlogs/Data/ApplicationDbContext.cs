using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScienceBlogs.Models;

namespace ScienceBlogs.Data
{
    public class ApplicationDbContext : IdentityDbContext<UserDetails>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
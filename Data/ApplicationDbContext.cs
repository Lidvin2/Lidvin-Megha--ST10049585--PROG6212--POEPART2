using Microsoft.EntityFrameworkCore;
using PROG6212_POEPART2.Models;

namespace PROG6212_POEPART2.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Lecturer> Lecturers { get; set; }
        public DbSet<Claim> Claims { get; set; }
    }
}

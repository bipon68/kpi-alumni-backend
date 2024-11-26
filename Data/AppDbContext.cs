
using KpiAlumni.Tables;
using Microsoft.EntityFrameworkCore;

namespace KpiAlumni.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<UserProfile> UserProfile { get; set; }
        
        public DbSet<VisitorInit> VisitorInit { get; set; }
    }
}

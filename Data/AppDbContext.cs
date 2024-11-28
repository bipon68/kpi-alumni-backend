
using KpiAlumni.Tables;
using Microsoft.EntityFrameworkCore;

namespace KpiAlumni.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<UserProfile> UserProfile { get; set; }
        
        public DbSet<UserProvider> UserProvider { get; set; }
        
        public DbSet<UserLoginLog> UserLoginLog { get; set; }
        
        public DbSet<InstituteInfo> InstituteInfo { get; set; }
        
        public DbSet<VisitorInit> VisitorInit { get; set; }
    }
}

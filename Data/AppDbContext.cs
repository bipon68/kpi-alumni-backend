
using KpiAlumni.Tables;
using Microsoft.EntityFrameworkCore;

namespace KpiAlumni.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public required DbSet<User> Users { get; set; }
    }
}

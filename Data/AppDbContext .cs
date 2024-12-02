using Microsoft.EntityFrameworkCore;
using train.Models;

namespace train.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<OtpRecord> OtpRecords { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}


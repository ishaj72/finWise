using Microsoft.EntityFrameworkCore;

namespace finWise.Model
{
    public class finWiseDbContext : DbContext
    {
        public finWiseDbContext(DbContextOptions<finWiseDbContext> options) : base(options) { }

        public DbSet<UserDetails> Users { get; set; }
    }
}

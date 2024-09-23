using Microsoft.EntityFrameworkCore;

namespace finWise.Model
{
    public class finWiseDbContext : DbContext
    {
        public finWiseDbContext(DbContextOptions<finWiseDbContext> options) : base(options) { }

        public DbSet<UserDetails> Users { get; set; }
        public DbSet<TransactionDetails> Transactions { get; set; }
        public DbSet<BudgetDetails> Budgets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionDetails>()
            .HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId);
        }
    }
}

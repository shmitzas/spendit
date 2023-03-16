using Microsoft.EntityFrameworkCore;
using REST_API.Models.Budgets;
using REST_API.Models.Categories;
using REST_API.Models.Transactions;
using REST_API.Models.Users;

namespace REST_API.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<RTransaction> RTransactions { get; set; }
        public virtual DbSet<Budget> Budgets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .Property(u => u.Id)
                .HasConversion(
                    id => id.ToByteArray(),
                    bytes => new Guid(bytes));

            modelBuilder.Entity<Transaction>()
                .Property(t => t.Id)
                .HasConversion(
                    id => id.ToByteArray(),
                    bytes => new Guid(bytes));

            modelBuilder.Entity<Transaction>()
                .Property(t => t.UserId)
                .HasConversion(
                    id => id.ToByteArray(),
                    bytes => new Guid(bytes));

            modelBuilder.Entity<Transaction>()
                .Property(t => t.CategoryId)
                .HasConversion(
                    id => id.ToByteArray(),
                    bytes => new Guid(bytes));

            modelBuilder.Entity<Category>()
                .Property(c => c.Id)
                .HasConversion(
                    id => id.ToByteArray(),
                    bytes => new Guid(bytes));

            modelBuilder.Entity<Category>()
                .Property(c => c.UserId)
                .HasConversion(
                    id => id.ToByteArray(),
                    bytes => new Guid(bytes));
        }
    }
}

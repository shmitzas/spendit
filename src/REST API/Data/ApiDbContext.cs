using Microsoft.EntityFrameworkCore;
using REST_API.Models.Budgets;
using REST_API.Models.Categories;
using REST_API.Models.RecurringTransactions;
using REST_API.Models.Transactions;
using REST_API.Models.Users;
using REST_API.Models.Goals;
using REST_API.Models.Bills;
using System.Text.Json;

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
        public virtual DbSet<RecurringTransaction> RecurringTransactions { get; set; }
        public virtual DbSet<Goal> Goals { get; set; }
        public virtual DbSet<Budget> Budgets { get; set; }
        public virtual DbSet<BillToDb> Bills { get; set; }
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
                .Property(t => t.BudgetId)
                .HasConversion(
                    id => id.ToByteArray(),
                    bytes => new Guid(bytes));

            modelBuilder.Entity<RecurringTransaction>()
                .Property(t => t.Id)
                .HasConversion(
                    id => id.ToByteArray(),
                    bytes => new Guid(bytes));

            modelBuilder.Entity<RecurringTransaction>()
                .Property(t => t.UserId)
                .HasConversion(
                    id => id.ToByteArray(),
                    bytes => new Guid(bytes));

            modelBuilder.Entity<Goal>()
                .Property(g => g.Id)
                .HasConversion(
                    id => id.ToByteArray(),
                    bytes => new Guid(bytes));

            modelBuilder.Entity<Goal>()
                .Property(g => g.UserId)
                .HasConversion(
                    id => id.ToByteArray(),
                    bytes => new Guid(bytes));

            modelBuilder.Entity<Budget>()
                .Property(g => g.Id)
                .HasConversion(
                    id => id.ToByteArray(),
                    bytes => new Guid(bytes));

            modelBuilder.Entity<Budget>()
                .Property(g => g.UserId)
                .HasConversion(
                    id => id.ToByteArray(),
                    bytes => new Guid(bytes));
            modelBuilder.Entity<BillToDb>()
                .Property(b => b.Id)
                .HasConversion(
                    id => id.ToByteArray(),
                    bytes => new Guid(bytes));

            modelBuilder.Entity<BillToDb>()
                .Property(b => b.UserId)
                .HasConversion(
                    id => id.ToByteArray(),
                    bytes => new Guid(bytes));
        }
    }
}

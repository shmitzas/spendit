using Microsoft.EntityFrameworkCore;
using Spendit.Server.Models.Budgets;
using Spendit.Server.Models.Categories;
using Spendit.Server.Models.Transactions;
using Spendit.Server.Models.Users;

namespace Spendit.Server.Data
{
    public class API_DbContext : DbContext
    {
        public API_DbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<RTransaction> RTransactions { get; set; }
        public DbSet<Budget> Budgets { get; set; }
    }
}

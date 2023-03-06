using Microsoft.EntityFrameworkCore;
using REST_API.Models.Budgets;
using REST_API.Models.Categories;
using REST_API.Models.Transactions;
using REST_API.Models.Users;

namespace REST_API.Data
{
    public class API_DbContext : DbContext
    {
        public API_DbContext(DbContextOptions options) : base(options)
        {

        }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<RTransaction> RTransactions { get; set; }
        public virtual DbSet<Budget> Budgets { get; set; }
    }
}

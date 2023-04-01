using Microsoft.EntityFrameworkCore;
using REST_API.Data;
using REST_API.Models.Categories;
using REST_API.Models.Users;
using REST_API.Models.Transactions;
using REST_API.Models.Goals;
using REST_API.Models.RecurringTransactions;

namespace REST_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //////////////////////////////////////////////
            //-     Enable/Disable Developer mode     -//
            ////////////////////////////////////////////
            bool DevMode = true;

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            if (DevMode) builder.Services.AddDbContext<ApiDbContext>(options => options.UseInMemoryDatabase("InMemoryDB"));
            else builder.Services.AddDbContext<ApiDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("SpenditDB")));

            var app = builder.Build();

            if (DevMode)
            {
                var scope = app.Services.CreateScope();
                ApiDbContext context = scope.ServiceProvider.GetService<ApiDbContext>();
                SetupInMemoryDatabase(context);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }

        private static void SetupInMemoryDatabase(ApiDbContext context)
        {
            List<User> Users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Username = "User1", Password = "User1", Email = "User1@gmail.com", Settings = "{\"currency\": \"EUR\"}"},
                new User { Id = Guid.NewGuid(), Username = "User2", Password = "User2", Email = "User2@gmail.com", Settings = "{\"currency\": \"EUR\"}"},
                new User { Id = Guid.NewGuid(), Username = "User3", Password = "User3", Email = "User3@gmail.com", Settings = "{\"currency\": \"EUR\"}"},
                new User { Id = Guid.NewGuid(), Username = "User4", Password = "User4", Email = "User4@gmail.com", Settings = "{\"currency\": \"EUR\"}"},
                new User { Id = Guid.NewGuid(), Username = "User5", Password = "User5", Email = "User5@gmail.com", Settings = "{\"currency\": \"EUR\"}"},
            };
            foreach (User user in Users)
            {
                context.Users.Add(user);
            }

            List<Category> Categories = new List<Category> {
                new Category { Id = 1, Name = "Other" },
                new Category { Id = 2, Name = "Housing" },
                new Category { Id = 3, Name = "Groceries" },
                new Category { Id = 4, Name = "Transportation" },
                new Category { Id = 5, Name = "Health" },
                new Category { Id = 6, Name = "Entertainment" },
                new Category { Id = 7, Name = "Savings" },
                new Category { Id = 8, Name = "Investments" },
                new Category { Id = 9, Name = "Debt Repayment" },
                new Category { Id = 10, Name = "Clothing" },
                new Category { Id = 11, Name = "Gifts" },
                new Category { Id = 12, Name = "Donations" },
                new Category { Id = 13, Name = "Travel" },
                new Category { Id = 14, Name = "Education" },
                new Category { Id = 15, Name = "Subscriptions" },
                new Category { Id = 16, Name = "Childcare" },
                new Category { Id = 17, Name = "Pets" },
            };
            foreach (Category category in Categories)
            {
                context.Categories.Add(category);
            }

            List<Transaction> Transactions = new List<Transaction>
            {
                new Transaction { Id = Guid.NewGuid(), UserId= (Guid)Users[0].Id, CategoryId = 7, Type="Income", Amount=1000M, Currency = "EUR",  Description = "Salary", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new Transaction { Id = Guid.NewGuid(), UserId= (Guid)Users[0].Id, CategoryId = 15, Type="Expense", Amount=11.19M, Currency = "EUR",  Description = "Netflix subscription", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new Transaction { Id = Guid.NewGuid(), UserId= (Guid)Users[0].Id, CategoryId = 2, Type="Expense", Amount=469.59M, Currency = "EUR",  Description = "Rent", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new Transaction { Id = Guid.NewGuid(), UserId= (Guid) Users[0].Id, CategoryId = 8, Type="Income", Amount=100.79M, Currency = "EUR",  Description = "Investment", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new Transaction { Id = Guid.NewGuid(), UserId= (Guid) Users[0].Id, CategoryId = 11, Type="Income", Amount=100M, Currency = "EUR",  Description = "Birthday present", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new Transaction { Id = Guid.NewGuid(), UserId= (Guid) Users[0].Id, CategoryId = 4, Type="Expense", Amount=1500M, Currency = "EUR",  Description = "Car loan", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
            };
            foreach (Transaction transaction in Transactions)
            {
                context.Transactions.Add(transaction);
            }
            List<RecurringTransaction> RecurringTransactions = new List<RecurringTransaction>
            {
                new RecurringTransaction { Id = Guid.NewGuid(), UserId= (Guid)Users[0].Id, CategoryId = 7, Type="Income", Amount=1000M, Currency = "EUR",  Description = "Salary", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(5), Frequency="Daily"},
                new RecurringTransaction { Id = Guid.NewGuid(), UserId= (Guid)Users[0].Id, CategoryId = 15, Type="Expense", Amount=11.19M, Currency = "EUR",  Description = "Netflix subscription", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(6), Frequency="Daily"},
                new RecurringTransaction { Id = Guid.NewGuid(), UserId= (Guid)Users[0].Id, CategoryId = 2, Type="Expense", Amount=469.59M, Currency = "EUR",  Description = "Rent", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(4), Frequency="Weekly"},
                new RecurringTransaction { Id = Guid.NewGuid(), UserId= (Guid) Users[0].Id, CategoryId = 8, Type="Income", Amount=100.79M, Currency = "EUR",  Description = "Investment", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(8), Frequency = "Weekly"},
                new RecurringTransaction { Id = Guid.NewGuid(), UserId= (Guid) Users[0].Id, CategoryId = 11, Type="Income", Amount=100M, Currency = "EUR",  Description = "Birthday present", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(1), Frequency = "Daily"},
                };
            foreach (RecurringTransaction recurringTransaction in RecurringTransactions)
            {
                context.RecurringTransactions.Add(recurringTransaction);
            }
            List<Goal> Goals = new List<Goal>
            {
                new Goal{ Id = Guid.NewGuid(), UserId= (Guid) Users[0].Id, CategoryId = 2, Description = "New apartment", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(4), Amount = 100000m, Currency = "EUR", CurrentAmount = 74000},
                new Goal{ Id = Guid.NewGuid(), UserId= (Guid) Users[0].Id, CategoryId = 13, Description = "Vacation to Hawaii", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(5), Amount = 5000m, Currency = "EUR", CurrentAmount = 2500},
                new Goal{ Id = Guid.NewGuid(), UserId= (Guid) Users[0].Id, CategoryId = 4, Description = "Buy a new car", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(6), Amount = 25000m, Currency = "EUR", CurrentAmount = 24000},
                new Goal{ Id = Guid.NewGuid(), UserId= (Guid) Users[0].Id, CategoryId = 7, Description = "Emergency fund", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(7), Amount = 10000m, Currency = "EUR", CurrentAmount = 0},
                new Goal{ Id = Guid.NewGuid(), UserId= (Guid) Users[0].Id, CategoryId = 1, Description = "Wedding expenses", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(8), Amount = 15000m, Currency = "EUR", CurrentAmount = 1670},
                new Goal{ Id = Guid.NewGuid(), UserId= (Guid) Users[0].Id, CategoryId = 2, Description = "Home renovation", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(9), Amount = 20000m, Currency = "EUR", CurrentAmount = 5000},
                new Goal{ Id = Guid.NewGuid(), UserId= (Guid) Users[0].Id, CategoryId = 8, Description = "Start a business", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(10), Amount = 50000m, Currency = "EUR", CurrentAmount = 19700},
                new Goal{ Id = Guid.NewGuid(), UserId= (Guid) Users[0].Id, CategoryId = 16, Description = "College fund for kids", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(11), Amount = 40000m, Currency = "EUR", CurrentAmount = 4563},
                new Goal{ Id = Guid.NewGuid(), UserId= (Guid) Users[0].Id, CategoryId = 9, Description = "Pay off student loans", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(12), Amount = 30000m, Currency = "EUR", CurrentAmount = 4240},
                new Goal{ Id = Guid.NewGuid(), UserId= (Guid) Users[0].Id, CategoryId = 7, Description = "Retirement savings", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(13), Amount = 100000m, Currency = "EUR", CurrentAmount = 76778},
                new Goal{ Id = Guid.NewGuid(), UserId= (Guid) Users[0].Id, CategoryId = 12, Description = "Charitable donations", StartDate = DateTime.Now, EndDate = DateTime.Now.AddMonths(14), Amount = 5000m, Currency = "EUR", CurrentAmount = 1240},
            };
            foreach (Goal goal in Goals)
            {
                context.Goals.Add(goal);
            }

            context.SaveChanges();
        }
    }
}
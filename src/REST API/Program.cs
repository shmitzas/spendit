using Microsoft.EntityFrameworkCore;
using REST_API.Data;
using REST_API.Models.Categories;
using REST_API.Models.Users;
using REST_API.Models.Transactions;

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

            if (DevMode) builder.Services.AddDbContext<API_DbContext>(options => options.UseInMemoryDatabase("InMemoryDB"));
            else builder.Services.AddDbContext<API_DbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("SpenditDB")));

            var app = builder.Build();

            if (DevMode)
            {
                var scope = app.Services.CreateScope();
                API_DbContext context = scope.ServiceProvider.GetService<API_DbContext>();
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

        private static void SetupInMemoryDatabase(API_DbContext context)
        {
            List<User> Users = new List<User>
            {
                new User { Id = 1, Username = "User1", Password = "User1", Email = "User1@gmail.com", Settings = "a"},
                new User { Id = 2, Username = "User2", Password = "User2", Email = "User2@gmail.com", Settings = "b"},
                new User { Id = 3, Username = "User3", Password = "User3", Email = "User3@gmail.com", Settings = "c"},
                new User { Id = 4, Username = "User4", Password = "User4", Email = "User4@gmail.com", Settings = "d"},
                new User { Id = 5, Username = "User5", Password = "User5", Email = "User5@gmail.com", Settings = "e"},
            };
            List<Transaction> Transactions = new List<Transaction>
            {
                new Transaction { Id = 1, UserId= 1, CategoryId = 0, Type="Income", Amount=1000M, Currency = "EUR",  Description = "Salary"},
                new Transaction { Id = 2, UserId= 1, CategoryId = 0, Type="Expense", Amount=11.19M, Currency = "EUR",  Description = "Netflix subscription"},
                new Transaction { Id = 3, UserId= 1, CategoryId = 0, Type="Expense", Amount=469.59M, Currency = "EUR",  Description = "Rent + comodities"},
                new Transaction { Id = 4, UserId= 1, CategoryId = 0, Type="Income", Amount=100.79M, Currency = "EUR",  Description = "Investment"},
                new Transaction { Id = 5, UserId= 2, CategoryId = 0, Type="Income", Amount=3598.6M, Currency = "EUR",  Description = "Salary"},
                new Transaction { Id = 6, UserId= 2, CategoryId = 0, Type="Income", Amount=100M, Currency = "EUR",  Description = "Birthday present"},
                new Transaction { Id = 7, UserId= 2, CategoryId = 0, Type="Expense", Amount=1500M, Currency = "EUR",  Description = "Car loan"},
                new Transaction { Id = 8, UserId= 3, CategoryId = 0, Type="Income", Amount=5000M, Currency = "EUR",  Description = "Salary"},
                new Transaction { Id = 9, UserId= 3, CategoryId = 0, Type="Expense", Amount=1000M, Currency = "EUR",  Description = "Bought shares"},
                new Transaction { Id = 10, UserId= 3, CategoryId = 0, Type="Income", Amount=545.89M, Currency = "EUR",  Description = "Fixed car issues and passed mandatory car check for 2 years"},
                new Transaction { Id = 11, UserId= 3, CategoryId = 0, Type="Expense", Amount=100M, Currency = "EUR",  Description = "Bought new sneakers and clothes at SportsDirect"},
                new Transaction { Id = 12, UserId= 4, CategoryId = 0, Type="Income", Amount=2000M, Currency = "EUR",  Description = "Salary"},
                new Transaction { Id = 13, UserId= 4, CategoryId = 0, Type="Expense", Amount=1000M, Currency = "EUR",  Description = "Bought new TV and PS5"},
                new Transaction { Id = 14, UserId= 5, CategoryId = 0, Type="Income", Amount=900M, Currency = "EUR",  Description = "Salary"},
                new Transaction { Id = 15, UserId= 5, CategoryId = 0, Type="Expense", Amount=100M, Currency = "EUR",  Description = "Lunch with clients"},
                new Transaction { Id = 16, UserId= 5, CategoryId = 0, Type="Expense", Amount=1000M, Currency = "EUR",  Description = "Present for parent's anniversary."},
            };
            List<Category> Categories = new List<Category>
            {
                new Category { Id = 1, UserId = 0, Name = "Default"}
            };

            foreach (User user in Users)
            {
                context.Users.Add(user);
            }
            /*
            foreach (Category category in Categories)
            {
                context.Categories.Add(category);
            }
            */
            foreach (Transaction transaction in Transactions)
            {
                context.Transactions.Add(transaction);
            }

            context.SaveChanges();
        }
    }
}
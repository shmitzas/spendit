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
            bool DevMode = false;

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
            List<Category> Categories = new List<Category>
            {
                new Category { Id = Guid.NewGuid(), UserId = (Guid)Users[0].Id, Name = "Default"}
            };
            List<Transaction> Transactions = new List<Transaction>
            {
                new Transaction { Id = Guid.NewGuid(), UserId= (Guid)Users[0].Id, CategoryId = Categories[0].Id, Type="Income", Amount=1000M, Currency = "EUR",  Description = "Salary", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new Transaction { Id = Guid.NewGuid(), UserId= (Guid)Users[0].Id, CategoryId = Categories[0].Id, Type="Expense", Amount=11.19M, Currency = "EUR",  Description = "Netflix subscription", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new Transaction { Id = Guid.NewGuid(), UserId= (Guid)Users[0].Id, CategoryId = Categories[0].Id, Type="Expense", Amount=469.59M, Currency = "EUR",  Description = "Rent + comodities", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new Transaction { Id = Guid.NewGuid(), UserId= (Guid) Users[0].Id, CategoryId = Categories[0].Id, Type="Income", Amount=100.79M, Currency = "EUR",  Description = "Investment", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new Transaction { Id = Guid.NewGuid(), UserId= (Guid) Users[1].Id, CategoryId = Categories[0].Id, Type="Income", Amount=3598.6M, Currency = "EUR",  Description = "Salary", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new Transaction { Id = Guid.NewGuid(), UserId= (Guid) Users[1].Id, CategoryId = Categories[0].Id, Type="Income", Amount=100M, Currency = "EUR",  Description = "Birthday present", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new Transaction { Id = Guid.NewGuid(), UserId= (Guid) Users[1].Id, CategoryId = Categories[0].Id, Type="Expense", Amount=1500M, Currency = "EUR",  Description = "Car loan", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new Transaction { Id = Guid.NewGuid(), UserId= (Guid) Users[1].Id, CategoryId = Categories[0].Id, Type="Income", Amount=5000M, Currency = "EUR",  Description = "Salary", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new Transaction { Id = Guid.NewGuid(), UserId= (Guid) Users[2].Id, CategoryId = Categories[0].Id, Type="Income", Amount=3598.6M, Currency = "EUR",  Description = "Salary", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new Transaction { Id = Guid.NewGuid(), UserId= (Guid) Users[2].Id, CategoryId = Categories[0].Id, Type="Income", Amount=100M, Currency = "EUR",  Description = "Birthday present", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new Transaction { Id = Guid.NewGuid(), UserId= (Guid) Users[2].Id, CategoryId = Categories[0].Id, Type="Expense", Amount=1500M, Currency = "EUR",  Description = "Car loan", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new Transaction { Id = Guid.NewGuid(), UserId= (Guid) Users[2].Id, CategoryId = Categories[0].Id, Type="Income", Amount=3598.6M, Currency = "EUR",  Description = "Salary", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new Transaction { Id = Guid.NewGuid(), UserId= (Guid) Users[2].Id, CategoryId = Categories[0].Id, Type="Income", Amount=100M, Currency = "EUR",  Description = "Birthday present", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
                new Transaction { Id = Guid.NewGuid(), UserId= (Guid) Users[2].Id, CategoryId = Categories[0].Id, Type="Expense", Amount=1500M, Currency = "EUR",  Description = "Car loan", CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now},
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
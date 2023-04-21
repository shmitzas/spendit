using Microsoft.AspNetCore.Components.Authorization;
using WebApp.Services;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using CurrieTechnologies.Razor.SweetAlert2;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var apiUrl = new Uri(builder.Configuration.GetSection("RestApiUrl").Value);

            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<ProtectedSessionStorage>();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthProvider>();
            builder.Services.AddHttpClient<TransactionsService>(client => client.BaseAddress = apiUrl);
            builder.Services.AddHttpClient<RecurringTransactionsService>(client => client.BaseAddress = apiUrl);
            builder.Services.AddHttpClient<DueRecurringTransactions>(client => client.BaseAddress = apiUrl);
            builder.Services.AddHttpClient<UsersService>(client => client.BaseAddress = apiUrl);
            builder.Services.AddHttpClient<CategoriesService>(client => client.BaseAddress = apiUrl);
            builder.Services.AddHttpClient<GoalsService>(client => client.BaseAddress = apiUrl);
            builder.Services.AddHttpClient<BudgetsService>(client => client.BaseAddress = apiUrl);
            builder.Services.AddScoped<InputValidationService>(); 
            builder.Services.AddScoped<CategoryIconsService>();
            builder.Services.AddScoped<AlertsService>();
            builder.Services.AddSweetAlert2();
            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.MapBlazorHub();
            app.MapFallbackToPage("/_Host");

            app.Run();
        }
    }
}
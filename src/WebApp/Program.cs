using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Authorization;
using WebApp.Services;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace WebApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<ProtectedSessionStorage>();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthProvider>();
            builder.Services.AddHttpClient<TransactionsService>(client => client.BaseAddress = new Uri(builder.Configuration.GetSection("RestApiUrl").Value));
            builder.Services.AddHttpClient<UsersService>(client => client.BaseAddress = new Uri(builder.Configuration.GetSection("RestApiUrl").Value));
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
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
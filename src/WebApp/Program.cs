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

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();
            builder.Services.AddScoped<ProtectedSessionStorage>();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthProvider>();
            builder.Services.AddHttpClient<TransactionsService>(client => client.BaseAddress = new Uri(builder.Configuration.GetSection("RestApiUrl").Value));
            builder.Services.AddHttpClient<UsersService>(client => client.BaseAddress = new Uri(builder.Configuration.GetSection("RestApiUrl").Value));
            builder.Services.AddScoped<InputValidationService>();
            builder.Services.AddSweetAlert2();
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
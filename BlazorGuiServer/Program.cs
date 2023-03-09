using BlazorGuiServer.Data.Repository.Model;
using BlazorGuiServer.Data.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorGuiServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddRazorPages();
            builder.Services.AddServerSideBlazor();

            builder.Services.AddLogging(builder => builder
                .SetMinimumLevel(LogLevel.Debug)
                .AddFilter("Microsoft", LogLevel.Debug)
                .AddFilter("System", LogLevel.Debug)
            );

            builder.Services.AddSingleton<CryptographicSecurityService>();
            builder.Services.AddSingleton<LoginManagerService>();
            builder.Services.AddSingleton<HashManagerService>();
            builder.Services.AddSingleton<SymmetricAlgorithmsManagerService>();
            builder.Services.AddSingleton<SecurePasswordDbContext>();
            builder.Services.AddSingleton<RsaReceiverService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
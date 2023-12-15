using Microsoft.EntityFrameworkCore;
using MyMovieApp.Models;

namespace MyMovieApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

            builder.Services.AddDbContext<FilmContext>(options => options.UseSqlServer(connection));
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();
           
            app.UseStaticFiles();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Films}/{action=Index}/{id?}");

            app.Run();
        }
    }
}

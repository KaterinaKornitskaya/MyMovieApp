using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace MyMovieApp.Models
{
    public class FilmContext : DbContext
    {
        public DbSet<Film> Films { get; set; }

        public FilmContext(DbContextOptions<FilmContext> options)
            : base(options) 
        {
            Database.EnsureCreated();
        }       
    }
}

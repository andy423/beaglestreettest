using BeagleStreetTest.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BeagleStreetTest.Data
{
    public class WeatherDbContext : DbContext
    {
        public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options)
        {
        }

        public DbSet<FavouriteModel> Favourites { get; set; }
    }
}

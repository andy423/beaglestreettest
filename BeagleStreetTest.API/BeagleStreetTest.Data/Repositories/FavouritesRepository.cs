using BeagleStreetTest.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace BeagleStreetTest.Data.Repositories
{
    public class FavouritesRepository : IFavouritesRepository
    {
        private readonly WeatherDbContext _weatherDbContext;

        public FavouritesRepository(WeatherDbContext weatherDbContext)
        {
            _weatherDbContext = weatherDbContext;
        }

        public async Task<FavouriteModel?> Create(FavouriteModel favourite)
        {
            if (favourite == null)
            {
                throw new ArgumentNullException(nameof(favourite));
            }

            favourite.Id = Guid.NewGuid();

            _weatherDbContext.Favourites.Add(favourite);

            if (await _weatherDbContext.SaveChangesAsync() > 0)
            {
                return favourite;
            }
            else
            {
                return null;
            }
        }

        public async Task<FavouriteModel?> Read(Guid favouriteId)
        {
            var found = await _weatherDbContext.Favourites.FirstOrDefaultAsync(s => s.Id == favouriteId);

            return found;
        }

        public async Task<FavouriteModel?> Update(FavouriteModel favourite)
        {
            if (favourite == null)
            {
                throw new ArgumentNullException(nameof(favourite));
            }

            var found = await _weatherDbContext.Favourites.FirstOrDefaultAsync(s => s.Id == favourite.Id);

            if (found != null)
            {
                found.Favourite = favourite.Favourite;
                found.Latitude = favourite.Latitude;
                found.Longitude = favourite.Longitude;

                if (await _weatherDbContext.SaveChangesAsync() > 0)
                {
                    return found;
                }
            }

            return null;
        }

        public async Task<bool> Delete(Guid favouriteId)
        {
            var found = await _weatherDbContext.Favourites.FirstOrDefaultAsync(s => s.Id == favouriteId);

            if (found != null)
            {
                _weatherDbContext.Favourites.Remove(found);

                if (await _weatherDbContext.SaveChangesAsync() > 0)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

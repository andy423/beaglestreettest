using BeagleStreetTest.Data.Models;
using BeagleStreetTest.Domain.OpenWeather;

namespace BeagleStreetTest.Domain.Handlers
{
    public interface IWeatherHandler
    {
        Task<Guid?> CreateFavourite(FavouriteModel favourite);
        Task<OpenWeatherResponse?> GetFavourite(Guid favouriteId);
        Task<OpenWeatherResponse?> UpdateFavourite(FavouriteModel favourite);
        Task<bool> DeleteFavourite(Guid favouriteId);
    }
}
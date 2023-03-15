using BeagleStreetTest.Data.Models;
using BeagleStreetTest.Data.Repositories;
using BeagleStreetTest.Domain.OpenWeather;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeagleStreetTest.Domain.Handlers
{
    public class WeatherHandler : IWeatherHandler
    {
        private readonly IFavouritesRepository _favouritesRepository;
        private readonly IWeatherService _weatherService;

        public WeatherHandler(
            IFavouritesRepository favouritesRepository,
            IWeatherService weatherService)
        {
            _favouritesRepository = favouritesRepository;
            _weatherService = weatherService;
        }

        public async Task<Guid?> CreateFavourite(FavouriteModel favourite)
        {
            if (favourite == null)
            {
                throw new ArgumentNullException(nameof(favourite));
            }

            var result = await _favouritesRepository.Create(favourite);

            return result?.Id ?? Guid.Empty;
        }

        public async Task<OpenWeatherResponse?> GetFavourite(Guid favouriteId)
        {
            if (favouriteId == Guid.Empty)
            {
                return new OpenWeatherResponse
                {
                    IsSuccessful = false,
                    Message = $"Id of favourite {favouriteId} is invalid",
                };
            }

            OpenWeatherResponse? result = null;

            var found = await _favouritesRepository.Read(favouriteId);

            if (found != null)
            {
                result = await _weatherService.GetWeather(found.Latitude, found.Longitude);
            }

            return result;
        }

        public async Task<OpenWeatherResponse?> UpdateFavourite(FavouriteModel favourite)
        {
            if (favourite == null)
            {
                throw new ArgumentNullException(nameof(favourite));
            }

            if (favourite.Id == Guid.Empty)
            {
                return new OpenWeatherResponse
                {
                    IsSuccessful = false,
                    Message = $"Id of favourite {favourite.Id} is invalid",
                };
            }

            OpenWeatherResponse? result = null;

            var found = await _favouritesRepository.Read(favourite.Id);

            if (found != null)
            {
                if (_favouritesRepository.Update(favourite) != null)
                {
                    result = await _weatherService.GetWeather(favourite.Latitude, favourite.Longitude);
                }
            }

            return result;
        }

        public async Task<bool> DeleteFavourite(Guid favouriteId)
        {
            return await _favouritesRepository.Delete(favouriteId);
        }
    }
}

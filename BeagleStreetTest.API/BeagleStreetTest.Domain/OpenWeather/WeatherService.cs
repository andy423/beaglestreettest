using OpenWeatherMap.Cache;
using OpenWeatherMap.Cache.Models;

namespace BeagleStreetTest.Domain.OpenWeather
{
    public class WeatherService : IWeatherService
    {
        private readonly IOpenWeatherMapCache _openWeatherMapCache;
        private readonly IWeatherMapper _weatherMapper;

        public WeatherService(
            IOpenWeatherMapCache openWeatherMapCache,
            IWeatherMapper weatherMapper)
        {
            _openWeatherMapCache = openWeatherMapCache;
            _weatherMapper = weatherMapper;
        }

        public async Task<OpenWeatherResponse> GetWeather(double latitude, double longitude)
        {
            if (latitude < -90 || latitude > 90)
            {
                return new OpenWeatherResponse
                {
                    IsSuccessful = false,
                    Message = $"Latitude {latitude} is not within valid range of -90 to 90"
                };
            }

            if (longitude < -180 || longitude > 180)
            {
                return new OpenWeatherResponse
                {
                    IsSuccessful = false,
                    Message = $"Longitude {longitude} is not within valid range of -180 to 180"
                };
            }

            //            var locationQuery = new Location(47.6371, -122.1237);
            var locationQuery = new Location(latitude, longitude);
            var readings = await _openWeatherMapCache.GetReadingsAsync(locationQuery);

            OpenWeatherResponse? result = null;

            if (readings?.IsSuccessful ?? false)
            {
                result = _weatherMapper.Map(readings);

                result.IsSuccessful = true;
            }
            else
            {
                result = new OpenWeatherResponse
                {
                    IsSuccessful = false,
                    Message = $"Failed to get reading for Latitude {latitude} and Longitude {longitude}"
                };
            }

            return result;
        }
    }
}

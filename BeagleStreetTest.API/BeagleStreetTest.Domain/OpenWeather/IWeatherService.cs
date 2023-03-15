using OpenWeatherMap.Cache.Models;

namespace BeagleStreetTest.Domain.OpenWeather
{
    public interface IWeatherService
    {
        Task<OpenWeatherResponse> GetWeather(double latitude, double longitude);
    }
}
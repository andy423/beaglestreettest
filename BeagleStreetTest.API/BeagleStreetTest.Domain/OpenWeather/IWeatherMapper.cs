using OpenWeatherMap.Cache.Models;

namespace BeagleStreetTest.Domain.OpenWeather
{
    public interface IWeatherMapper
    {
        OpenWeatherResponse Map(Readings readings);
    }
}
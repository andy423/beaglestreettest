using OpenWeatherMap.Cache.Models;

namespace BeagleStreetTest.Domain.OpenWeather
{
    public class WeatherMapper : IWeatherMapper
    {
        public OpenWeatherResponse Map(Readings readings)
        {
            var result = new OpenWeatherResponse();

            result.LocationName = readings.CityName;
            result.TemperatureCurrent = readings.Temperature.DegreesCelsius;
            result.TemperatureMaximum = readings.MaximumTemperature.DegreesCelsius;
            result.TemperatureMinimum = readings.MinimumTemperature.DegreesCelsius;
            result.Pressure = readings.Pressure.Value;
            result.Humidity = readings.Humidity.Percent;
            result.Sunrise = readings.Sunrise.ToString();
            result.Sunset = readings.Sunset.ToString();

            return result;
        }
    }
}

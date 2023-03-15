using BeagleStreetTest.Domain.OpenWeather;
using Moq;
using OpenWeatherMap.Cache;
using OpenWeatherMap.Cache.Models;
using Xunit;

namespace BeagleSteetTest.Domain.UnitTests
{
    public class WeatherServiceTests : TestBase
    {
        private Mock<IOpenWeatherMapCache> _openWeatherMapCache;
        private Mock<IWeatherMapper> _weatherMapper;

        public WeatherServiceTests()
        {
            _openWeatherMapCache = new Mock<IOpenWeatherMapCache>();
            _weatherMapper = new Mock<IWeatherMapper>();
        }


        /*
         * I would like to unit test GetWeather however left for now as unable tyo mock readings object
         * for successful result
        *
        [Fact]
        public async Task Calling_GetWeather_with_valid_location_returns_sucessfully_mapped_weather()
        {
            var cityName = Create<string>();
            var currentTemperature = Create<double>();

            var readings = new Mock<Readings>();
            readings.SetupGet(x => x.CityName).Returns(cityName);
            readings.SetupGet(x => x.IsSuccessful).Returns(true);

            var weather = Create<OpenWeatherResponse>();

            _openWeatherMapCache.Setup(s => s.GetReadingsAsync(It.IsAny<Location>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(readings.Object);

            _weatherMapper.Setup(s => s.Map(It.IsAny<Readings>())).Returns(weather);

            var sut = CreateSut();

            var result = await sut.GetWeather(10, 20);

            Assert.NotNull(result);
            Assert.Equal(cityName, result.LocationName);
            Assert.True(result.IsSuccessful);
        }
        */

        [Theory]
        [InlineData(-401)]
        [InlineData(1000)]
        public async Task Calling_GetWeather_with_invalid_latitude_return_unsuccessful_and_message(double latitude)
        {
            var longitude = 0.0;

            var sut = CreateSut();

            var result = await sut.GetWeather(latitude, longitude);

            Assert.NotNull(result);
            Assert.False(result.IsSuccessful);
            Assert.Contains("Latitude", result.Message);
        }

        [Theory]
        [InlineData(-401)]
        [InlineData(1000)]
        public async Task Calling_GetWeather_with_invalid_longitude_return_unsuccessful_and_message(double longitude)
        {
            var latitude = 0.0;

            var sut = CreateSut();

            var result = await sut.GetWeather(latitude, longitude);

            Assert.NotNull(result);
            Assert.False(result.IsSuccessful);
            Assert.Contains("Longitude", result.Message);
        }

        private WeatherService CreateSut()
        {
            return new WeatherService(_openWeatherMapCache.Object, _weatherMapper.Object);
        }
    }
}
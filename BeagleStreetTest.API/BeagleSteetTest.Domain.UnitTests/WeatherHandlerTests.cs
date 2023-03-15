using BeagleStreetTest.Data.Models;
using BeagleStreetTest.Data.Repositories;
using BeagleStreetTest.Domain.Handlers;
using BeagleStreetTest.Domain.OpenWeather;
using Moq;

namespace BeagleSteetTest.Domain.UnitTests
{
    public class WeatherHandlerTests : TestBase
    {
        private readonly Mock<IFavouritesRepository> _favouritesRepository;
        private readonly Mock<IWeatherService> _weatherService;

        public WeatherHandlerTests()
        {
            _favouritesRepository = new Mock<IFavouritesRepository>();
            _weatherService= new Mock<IWeatherService>();
        }

        [Fact]
        public async Task Calling_CreateFavourite_with_value_request_calls_respository_to_create_favourite()
        {
            var favourite = Create<FavouriteModel>();

            _favouritesRepository.Setup(s => s.Create(favourite)).ReturnsAsync(favourite).Verifiable();

            var sut = CreateSut();

            var result = await sut.CreateFavourite(favourite);

            Assert.NotNull(result);
            Assert.Equal(favourite.Id, result);

            _favouritesRepository.Verify(v => v.Create(favourite), Times.Once);
        }

        [Fact]
        public void Calling_CreateFavourite_with_invalid_favourite_throw_exception()
        {
            var sut = CreateSut();

            Assert.ThrowsAsync<ArgumentNullException>(async () => await sut.CreateFavourite(null));
        }

        [Fact]
        public async Task Calling_GetFavourite_with_valid_id_returns_weather()
        {
            var id = Create<Guid>();

            var weather = Create<OpenWeatherResponse>();

            var found = Create<FavouriteModel>();

            _favouritesRepository.Setup(s => s.Read(id)).ReturnsAsync(found);

            _weatherService.Setup(s => s.GetWeather(found.Latitude, found.Longitude)).ReturnsAsync(weather);

            var sut = CreateSut();

            var result = await sut.GetFavourite(id);

            Assert.NotNull(result);
            Assert.Equal(weather, result);
            Assert.True(result.IsSuccessful);
        }

        [Fact]
        public async Task Calling_GetFavourite_with_blank_guid_for_id_return_unsuccessful_response_with_message()
        {
            var id = Guid.Empty;

            var sut = CreateSut();

            var result = await sut.GetFavourite(id);

            Assert.NotNull(result);
            Assert.False(result.IsSuccessful);
            Assert.Equal($"Id of favourite {id} is invalid", result.Message);
        }

        [Fact]
        public async Task Calling_UpdateFavourite_with_valid_request_calls_to_update_favourite_and_returns_weather()
        {
            var weather = Create<OpenWeatherResponse>();

            var request = Create<FavouriteModel>();

            var found = Create<FavouriteModel>();

            _favouritesRepository.Setup(s => s.Read(request.Id)).ReturnsAsync(found);

            _favouritesRepository.Setup(s => s.Update(request)).ReturnsAsync(request).Verifiable();

            _weatherService.Setup(s => s.GetWeather(request.Latitude, request.Longitude)).ReturnsAsync(weather);

            var sut = CreateSut();

            var result = await sut.UpdateFavourite(request);

            Assert.NotNull(result);
            Assert.Equal(weather, result);

            _favouritesRepository.Verify(v => v.Read(request.Id), Times.Once);
            _favouritesRepository.Verify(v => v.Update(request), Times.Once);
        }

        [Fact]
        public async Task Calling_DeleteFavourite_calls_respository_to_delete_favourite()
        {
            var id = Create<Guid>();

            _favouritesRepository.Setup(s => s.Delete(id)).ReturnsAsync(true).Verifiable();

            var sut = CreateSut();

            var result = await sut.DeleteFavourite(id);

            Assert.True(result);

            _favouritesRepository.Verify(v => v.Delete(id), Times.Once);
        }

        private WeatherHandler CreateSut()
        {
            return new WeatherHandler(_favouritesRepository.Object, _weatherService.Object);
        }
    }
}

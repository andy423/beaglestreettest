using BeagleStreetTest.Data.Models;
using BeagleStreetTest.Domain.Handlers;
using BeagleStreetTest.Domain.OpenWeather;
using Microsoft.AspNetCore.Mvc;

namespace BeagleStreetTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly IWeatherService _weatherService;
        private readonly IWeatherHandler _weatherHandler;
        private readonly ILogger<WeatherController> _logger;

        public WeatherController(
            IWeatherService weatherService,
            IWeatherHandler weatherHandler,
            ILogger<WeatherController> logger)
        {
            _weatherService = weatherService;
            _weatherHandler = weatherHandler;
            _logger = logger;
        }

        [HttpGet("location/{latitude}/{longitude}")]
        public async Task<IActionResult> GetForLocation(double latitude, double longitude)
        {
            try
            {
                _logger.LogInformation($"Request for location latitdue: {latitude} longitude {longitude}");

                var result = await _weatherService.GetWeather(latitude, longitude);

                if (result?.IsSuccessful ?? false)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("favourite")]
        public async Task<IActionResult> PostFavourite([FromBody] FavouriteModel favourite)
        {
            try
            {
                var result = await _weatherHandler.CreateFavourite(favourite);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("favourite/{favouriteId}")]
        public async Task<IActionResult> GetFavourite(Guid favouriteId)
        {
            try
            {
                var result = await _weatherHandler.GetFavourite(favouriteId);

                if (result?.IsSuccessful ?? false)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("favourite")]
        public async Task<IActionResult> UpdateFavourite([FromBody] FavouriteModel favourite)
        {
            try
            {
                var result = await _weatherHandler.UpdateFavourite(favourite);

                if (result?.IsSuccessful ?? false)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound(result);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("favourite/{favouriteId}")]
        public async Task<IActionResult> DeleteFavourite(Guid favouriteId)
        {
            try
            {
                var result = await _weatherHandler.DeleteFavourite(favouriteId);

                if (result)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());

                return BadRequest(ex.Message);
            }
        }
    }
}

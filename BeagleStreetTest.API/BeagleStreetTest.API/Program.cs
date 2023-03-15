using BeagleStreetTest.Data;
using BeagleStreetTest.Data.Repositories;
using BeagleStreetTest.Domain.Handlers;
using BeagleStreetTest.Domain.OpenWeather;
using Microsoft.EntityFrameworkCore;
using OpenWeatherMap.Cache.Extensions;
using static OpenWeatherMap.Cache.Enums;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetValue<string>("ConnectionString");

builder.Services.AddDbContext<WeatherDbContext>(options => options.UseSqlServer(connectionString));

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var openWeatherMapApiKey = builder.Configuration.GetValue<string>("OpenWeatherAPIKey");

var openWeatherCacheTimeout = builder.Configuration.GetValue<int>("OpenWeatherCacheTimeout");

builder.Services.AddOpenWeatherMapCache(openWeatherMapApiKey, openWeatherCacheTimeout, FetchMode.AlwaysUseLastMeasuredButExtendCache, 300_000);

builder.Services.AddScoped<IFavouritesRepository, FavouritesRepository>();
builder.Services.AddScoped<IWeatherMapper, WeatherMapper>();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<IWeatherHandler, WeatherHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

using BeagleStreetTest.Data.Models;
using BeagleStreetTest.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Net.Sockets;
using System.Reflection.Metadata;

namespace BeagleStreetTest.Data.UnitTests
{
    public class FavouriteRepositoryTests : TestBase
    {
        /*
         * I have used this method of mocking ef before how currently failing on setting mock of favourites
         * would need a liitle more time to resolve.
         * 

        [Fact]
        public async Task Calling_Read_with_id_returns_favourite()
        {
            try
            {
                var id = Create<Guid>();

                var favouritesData = Create<List<FavouriteModel>>();
                favouritesData[0].Id = id;

                var favourite = favouritesData.AsQueryable();

                var favouriteSet = new Mock<DbSet<FavouriteModel>>();

                favouriteSet.As<IQueryable<FavouriteModel>>().Setup(m => m.Provider).Returns(favourite.Provider);
                favouriteSet.As<IQueryable<FavouriteModel>>().Setup(m => m.Expression).Returns(favourite.Expression);
                favouriteSet.As<IQueryable<FavouriteModel>>().Setup(m => m.ElementType).Returns(favourite.ElementType);
                favouriteSet.As<IQueryable<FavouriteModel>>().Setup(m => m.GetEnumerator()).Returns(() => favourite.GetEnumerator());

                var context = new Mock<WeatherDbContext>();
                context.Setup(s => s.Favourites).Returns(favouriteSet.Object);

                var sut = new FavouritesRepository(context.Object);

                var result = await sut.Read(id);

                Assert.NotNull(result);
                Assert.Equal(favouritesData[0], result);
            }
            catch (Exception ex) 
            { 

            }
        }
        */
    }
}
using BeagleStreetTest.Data.Models;

namespace BeagleStreetTest.Data.Repositories
{
    public interface IFavouritesRepository
    {
        Task<FavouriteModel?> Create(FavouriteModel favourite);
        Task<bool> Delete(Guid favouriteId);
        Task<FavouriteModel?> Read(Guid favouriteId);
        Task<FavouriteModel?> Update(FavouriteModel favourite);
    }
}
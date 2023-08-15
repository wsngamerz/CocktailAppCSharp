using CocktailApp.Repositories.Abstractions;
using CocktailApp.Services.Abstractions;
using ErrorOr;

namespace CocktailApp.Services;

public class FavouriteService<T>: IFavouriteService<T>
{
    private readonly IFavouriteRepository<T> _favouriteRepository;

    public FavouriteService(IFavouriteRepository<T> favouriteRepository)
    {
        _favouriteRepository = favouriteRepository;
    }
    
    public async Task<ErrorOr<Created>> AddFavourite(T value)
    {
        return await _favouriteRepository.Create(value);
    }

    public async Task<ErrorOr<IEnumerable<T>>> AllFavourites()
    {
        return await _favouriteRepository.All();
    }

    public async Task<ErrorOr<IEnumerable<T>>> UserFavourites(Guid userId)
    {
        return await _favouriteRepository.GetByUserId(userId);
    }

    public async Task<ErrorOr<Deleted>> RemoveFavourite(Guid userId, Guid entityId)
    {
        return await _favouriteRepository.Delete(userId, entityId);
    }
}
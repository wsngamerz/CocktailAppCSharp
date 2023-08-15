using ErrorOr;

namespace CocktailApp.Services.Abstractions;

public interface IFavouriteService<T>
{
    Task<ErrorOr<Created>> AddFavourite(T value);
    Task<ErrorOr<IEnumerable<T>>> AllFavourites();
    Task<ErrorOr<IEnumerable<T>>> UserFavourites(Guid userId);
    Task<ErrorOr<Deleted>> RemoveFavourite(Guid userId, Guid entityId);
}
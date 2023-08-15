using ErrorOr;

namespace CocktailApp.Repositories.Abstractions;

public interface IFavouriteRepository<T> : IRepository<T>
{
    public Task<ErrorOr<IEnumerable<T>>> GetByUserId(Guid id);
}

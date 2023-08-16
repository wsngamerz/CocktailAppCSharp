using ErrorOr;

namespace CocktailApp.Repositories.Abstractions;

public interface IRepository<T>
{
    public Task<ErrorOr<Created>> Create(T value);
    public Task<ErrorOr<T>> Get(params Guid[] ids);
    public Task<ErrorOr<IEnumerable<T>>> All();
    public Task<ErrorOr<T>> Update(T value);
    public Task<ErrorOr<Deleted>> Delete(params Guid[] ids);
    public Task<ErrorOr<int>> Count();
}
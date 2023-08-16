using ErrorOr;

namespace CocktailApp.Repositories.Abstractions;

public interface IRepository<T>
{
    public Task<ErrorOr<Created>> Create(T value);
    public Task<ErrorOr<IEnumerable<T>>> GetAll();
    public Task<ErrorOr<T>> GetById(params Guid[] keys);
    public Task<ErrorOr<T>> Update(T value);
    public Task<ErrorOr<Deleted>> Delete(params Guid[] keys);
    public Task<ErrorOr<int>> Count();
}
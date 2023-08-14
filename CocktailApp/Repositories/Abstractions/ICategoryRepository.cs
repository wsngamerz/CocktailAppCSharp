using CocktailApp.Models;
using ErrorOr;

namespace CocktailApp.Repositories.Abstractions;

public interface ICategoryRepository
{
    public Task<ErrorOr<Created>> Create(Category category);
    public Task<ErrorOr<Category>> GetById(Guid id);
    public Task<ErrorOr<IEnumerable<Category>>> GetMany();
    public Task<ErrorOr<Updated>> Update(Category category);
    public Task<ErrorOr<Deleted>> Delete(Guid id);
    public Task<ErrorOr<int>> Count();
}
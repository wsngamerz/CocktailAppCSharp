using CocktailApp.Models;
using ErrorOr;

namespace CocktailApp.Services.Abstractions;

public interface ICategoryService
{
    Task<ErrorOr<Created>> CreateCategory(Category category);
    Task<ErrorOr<Category>> GetCategory(Guid id);
    Task<ErrorOr<IEnumerable<Category>>> GetCategories();
    Task<ErrorOr<Updated>> UpdateCategory(Category category);
    Task<ErrorOr<Deleted>> DeleteCategory(Guid id);
}
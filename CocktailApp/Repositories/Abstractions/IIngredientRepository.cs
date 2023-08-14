using CocktailApp.Models;
using ErrorOr;

namespace CocktailApp.Repositories.Abstractions;

public interface IIngredientRepository
{
    public Task<ErrorOr<Created>> Create(Ingredient ingredient);
    public Task<ErrorOr<Ingredient>> GetById(Guid id);
    public Task<ErrorOr<IEnumerable<Ingredient>>> GetMany();
    public Task<ErrorOr<Updated>> Update(Ingredient ingredient);
    public Task<ErrorOr<Deleted>> Delete(Guid id);
    public Task<ErrorOr<int>> Count();
}
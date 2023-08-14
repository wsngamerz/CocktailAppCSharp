using CocktailApp.Models;
using ErrorOr;

namespace CocktailApp.Services.Abstractions;

public interface IIngredientService
{
    Task<ErrorOr<Created>> CreateIngredient(Ingredient ingredient);
    Task<ErrorOr<Ingredient>> GetIngredient(Guid id);
    Task<ErrorOr<IEnumerable<Ingredient>>> GetIngredients();
    Task<ErrorOr<Updated>> UpdateIngredient(Ingredient ingredient);
    Task<ErrorOr<Deleted>> DeleteIngredient(Guid id);
}
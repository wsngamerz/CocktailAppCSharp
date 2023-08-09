using CocktailApp.Models;
using ErrorOr;

namespace CocktailApp.Services.Abstractions;

public interface ICocktailService
{
    Task<ErrorOr<Created>> CreateCocktail(Cocktail cocktail);
    Task<ErrorOr<Cocktail>> GetCocktail(Guid id);
    Task<ErrorOr<IEnumerable<Cocktail>>> GetCocktails();
    Task<ErrorOr<Updated>> UpdateCocktail(Cocktail cocktail);
    Task<ErrorOr<Deleted>> DeleteCocktail(Guid id);
}
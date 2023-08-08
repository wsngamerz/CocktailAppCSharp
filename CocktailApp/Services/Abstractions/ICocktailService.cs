using CocktailApp.Models;
using ErrorOr;

namespace CocktailApp.Services.Abstractions;

public interface ICocktailService
{
    ErrorOr<Created> CreateCocktail(Cocktail cocktail);
    ErrorOr<Cocktail> GetCocktail(Guid id);
    ErrorOr<IEnumerable<Cocktail>> GetCocktails();
    ErrorOr<Updated> UpdateCocktail(Cocktail cocktail);
    ErrorOr<Deleted> DeleteCocktail(Guid id);
}
using CocktailApp.Models;
using ErrorOr;

namespace CocktailApp.Services.Cocktails;

public interface ICocktailService
{
    ErrorOr<Created> CreateCocktail(Cocktail cocktail);
    ErrorOr<Cocktail> GetCocktail(Guid id);
    ErrorOr<Cocktail[]> GetCocktails();
    ErrorOr<Updated> UpdateCocktail(Cocktail cocktail);
    ErrorOr<Deleted> DeleteCocktail(Guid id);
}
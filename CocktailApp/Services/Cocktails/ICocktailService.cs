using CocktailApp.Models;

namespace CocktailApp.Services.Cocktails;

public interface ICocktailService
{
    void CreateCocktail(Cocktail cocktail);
    Cocktail GetCocktail(Guid id);
    Cocktail[] GetCocktails();
    void UpdateCocktail(Cocktail cocktail);
    void DeleteCocktail(Guid id);
}
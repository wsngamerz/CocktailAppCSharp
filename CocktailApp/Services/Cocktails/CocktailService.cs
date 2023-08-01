using CocktailApp.Models;

namespace CocktailApp.Services.Cocktails;

public class CocktailService: ICocktailService
{
    private static readonly Dictionary<Guid, Cocktail> Cocktails = new();
    public void CreateCocktail(Cocktail cocktail)
    {
        Cocktails.Add(cocktail.Id, cocktail);
    }

    public Cocktail GetCocktail(Guid id)
    {
        return Cocktails[id];
    }

    public Cocktail[] GetCocktails()
    {
        return Cocktails.Values.ToArray();
    }

    public void UpdateCocktail(Cocktail cocktail)
    {
        Cocktails[cocktail.Id] = cocktail;
    }

    public void DeleteCocktail(Guid id)
    {
        Cocktails.Remove(id);
    }
}
using CocktailApp.Models;
using CocktailApp.ServiceErrors;
using ErrorOr;

namespace CocktailApp.Services.Cocktails;

public class CocktailService: ICocktailService
{
    private static readonly Dictionary<Guid, Cocktail> Cocktails = new();
    public ErrorOr<Created> CreateCocktail(Cocktail cocktail)
    {
        Cocktails.Add(cocktail.Id, cocktail);

        return Result.Created;
    }

    public ErrorOr<Cocktail> GetCocktail(Guid id)
    {
        if (Cocktails.TryGetValue(id, out var cocktail))
        {
            return cocktail;
        }

        return Errors.Cocktail.NotFound;
    }

    public ErrorOr<Cocktail[]> GetCocktails()
    {
        return Cocktails.Values.ToArray();
    }

    public ErrorOr<Updated> UpdateCocktail(Cocktail cocktail)
    {
        Cocktails[cocktail.Id] = cocktail;

        return Result.Updated;
    }

    public ErrorOr<Deleted> DeleteCocktail(Guid id)
    {
        Cocktails.Remove(id);

        return Result.Deleted;
    }
}
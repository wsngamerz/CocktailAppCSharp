using CocktailApp.Abstractions.Repositories;
using CocktailApp.Models;
using CocktailApp.ServiceErrors;
using ErrorOr;

namespace CocktailApp.Repositories;

public class CocktailDictRepository: ICocktailRepository
{
    private static readonly Dictionary<Guid, Cocktail> Cocktails = new();
    
    public ErrorOr<Created> Create(Cocktail cocktail)
    {
        Cocktails.Add(cocktail.Id, cocktail);

        return Result.Created;
    }

    public ErrorOr<Cocktail> GetById(Guid id)
    {
        if (Cocktails.TryGetValue(id, out var cocktail))
        {
            return cocktail;
        }

        return Errors.Cocktail.NotFound;
    }

    public ErrorOr<Cocktail> GetBySlug(string slug)
    {
        var cocktail = Cocktails.Values.FirstOrDefault(c => c.Slug == slug);

        if (cocktail is not null)
        {
            return cocktail;
        }

        return Errors.Cocktail.NotFound;
    }

    public ErrorOr<IEnumerable<Cocktail>> GetMany()
    {
        return Cocktails.Values;
    }

    public ErrorOr<Updated> Update(Cocktail cocktail)
    {
        Cocktails[cocktail.Id] = cocktail;

        return Result.Updated;
    }

    public ErrorOr<Deleted> Delete(Guid id)
    {
        Cocktails.Remove(id);

        return Result.Deleted;
    }

    public ErrorOr<int> Count()
    {
        return Cocktails.Count;
    }
}
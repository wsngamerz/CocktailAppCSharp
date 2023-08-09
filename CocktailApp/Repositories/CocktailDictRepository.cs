using CocktailApp.Models;
using CocktailApp.Repositories.Abstractions;
using CocktailApp.ServiceErrors;
using ErrorOr;

namespace CocktailApp.Repositories;

public class CocktailDictRepository : ICocktailRepository
{
    private static readonly Dictionary<Guid, Cocktail> Cocktails = new();

    public Task<ErrorOr<Created>> Create(Cocktail cocktail)
    {
        Cocktails.Add(cocktail.Id, cocktail);

        return Task.FromResult<ErrorOr<Created>>(Result.Created);
    }

    public Task<ErrorOr<Cocktail>> GetById(Guid id)
    {
        return Cocktails.TryGetValue(id, out var cocktail)
            ? Task.FromResult<ErrorOr<Cocktail>>(cocktail)
            : Task.FromResult<ErrorOr<Cocktail>>(Errors.Cocktail.NotFound);
    }

    public Task<ErrorOr<Cocktail>> GetBySlug(string slug)
    {
        var cocktail = Cocktails.Values.FirstOrDefault(c => c.Slug == slug);

        return cocktail is not null
            ? Task.FromResult<ErrorOr<Cocktail>>(cocktail)
            : Task.FromResult<ErrorOr<Cocktail>>(Errors.Cocktail.NotFound);
    }

    public Task<ErrorOr<IEnumerable<Cocktail>>> GetMany()
    {
        return Task.FromResult<ErrorOr<IEnumerable<Cocktail>>>(Cocktails.Values);
    }

    public Task<ErrorOr<Updated>> Update(Cocktail cocktail)
    {
        Cocktails[cocktail.Id] = cocktail;

        return Task.FromResult<ErrorOr<Updated>>(Result.Updated);
    }

    public Task<ErrorOr<Deleted>> Delete(Guid id)
    {
        Cocktails.Remove(id);

        return Task.FromResult<ErrorOr<Deleted>>(Result.Deleted);
    }

    public Task<ErrorOr<int>> Count()
    {
        return Task.FromResult<ErrorOr<int>>(Cocktails.Count);
    }
}
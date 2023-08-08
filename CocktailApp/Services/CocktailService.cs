using CocktailApp.Models;
using CocktailApp.Repositories.Abstractions;
using CocktailApp.Services.Abstractions;
using ErrorOr;

namespace CocktailApp.Services;

public class CocktailService: ICocktailService
{
    private readonly ICocktailRepository _cocktailRepository;

    public CocktailService(ICocktailRepository cocktailRepository)
    {
        _cocktailRepository = cocktailRepository;
    }
    
    public ErrorOr<Created> CreateCocktail(Cocktail cocktail)
    {
        return _cocktailRepository.Create(cocktail);
    }

    public ErrorOr<Cocktail> GetCocktail(Guid id)
    {
        return _cocktailRepository.GetById(id);
    }

    public ErrorOr<IEnumerable<Cocktail>> GetCocktails()
    {   
        return _cocktailRepository.GetMany();
    }

    public ErrorOr<Updated> UpdateCocktail(Cocktail cocktail)
    {
        return _cocktailRepository.Update(cocktail);
    }

    public ErrorOr<Deleted> DeleteCocktail(Guid id)
    {
        return _cocktailRepository.Delete(id);
    }
}
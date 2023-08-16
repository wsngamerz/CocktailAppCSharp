using System.Collections;
using CocktailApp.Contracts.Cocktail;
using CocktailApp.Models;
using CocktailApp.Repositories.Abstractions;
using CocktailApp.Services.Abstractions;
using ErrorOr;

namespace CocktailApp.Services;

public class CocktailService : ICocktailService
{
    private readonly ICocktailRepository _cocktailRepository;

    public CocktailService(ICocktailRepository cocktailRepository)
    {
        _cocktailRepository = cocktailRepository;
    }

    public async Task<ErrorOr<Created>> CreateCocktail(Cocktail cocktail,
        IEnumerable<CocktailIngredient> ingredients,
        IEnumerable<CocktailInstruction> instructions)
    {
        await _cocktailRepository.Create(cocktail);
        await _cocktailRepository.CreateIngredients(ingredients);
        await _cocktailRepository.CreateInstructions(instructions);
        return Result.Created;
    }

    public async Task<ErrorOr<Cocktail>> GetCocktail(Guid id)
    {
        return await _cocktailRepository.Get(id);
    }

    public async Task<ErrorOr<DetailedCocktail>> GetDetailedCocktail(Guid id)
    {
        return await _cocktailRepository.GetDetailed(id);
    }

    public async Task<ErrorOr<IEnumerable<Cocktail>>> GetCocktails()
    {
        return await _cocktailRepository.All();
    }

    public async Task<ErrorOr<Cocktail>> UpdateCocktail(Cocktail cocktail)
    {
        return await _cocktailRepository.Update(cocktail);
    }

    public async Task<ErrorOr<Deleted>> DeleteCocktail(Guid id)
    {
        return await _cocktailRepository.Delete(id);
    }
}
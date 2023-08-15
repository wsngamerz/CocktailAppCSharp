using CocktailApp.Contracts.Cocktail;
using CocktailApp.Models;
using ErrorOr;

namespace CocktailApp.Repositories.Abstractions;

public interface ICocktailRepository: IRepository<Cocktail>
{
    Task<ErrorOr<Created>> CreateIngredients(IEnumerable<CocktailIngredient> ingredients);
    Task<ErrorOr<Created>> CreateInstructions(IEnumerable<CocktailInstruction> instructions);
    Task<ErrorOr<Cocktail>> GetBySlug(string slug);
    Task<ErrorOr<DetailedCocktail>> GetDetailed(Guid id);
}
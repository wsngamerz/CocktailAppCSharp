using CocktailApp.Models;
using CocktailApp.Repositories.Abstractions;
using CocktailApp.Services.Abstractions;
using ErrorOr;

namespace CocktailApp.Services;

public class IngredientService: IIngredientService
{
    private readonly IIngredientRepository _ingredientRepository;

    public IngredientService(IIngredientRepository ingredientRepository)
    {
        _ingredientRepository = ingredientRepository;
    }
    
    public async Task<ErrorOr<Created>> CreateIngredient(Ingredient ingredient)
    {
        return await _ingredientRepository.Create(ingredient);
    }

    public async Task<ErrorOr<Ingredient>> GetIngredient(Guid id)
    {
        return await _ingredientRepository.Get(id);
    }

    public async Task<ErrorOr<IEnumerable<Ingredient>>> GetIngredients()
    {   
        return await _ingredientRepository.All();
    }

    public async Task<ErrorOr<Ingredient>> UpdateIngredient(Ingredient ingredient)
    {
        return await _ingredientRepository.Update(ingredient);
    }

    public async Task<ErrorOr<Deleted>> DeleteIngredient(Guid id)
    {
        return await _ingredientRepository.Delete(id);
    }
}
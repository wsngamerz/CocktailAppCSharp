using CocktailApp.Contracts.Enums;

namespace CocktailApp.Contracts.Cocktail;

public record CreateCocktailIngredient(Guid IngredientId, decimal Amount, CocktailUnit Unit, int Position);
using CocktailApp.Contracts.Enums;
using CocktailApp.Contracts.Ingredient;

namespace CocktailApp.Contracts.Cocktail;

public record CocktailIngredientResponse(
    decimal Amount,
    CocktailUnit Unit,
    int Position,
    IngredientResponse Ingredient
);
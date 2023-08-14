namespace CocktailApp.Contracts.Ingredient;

public record UpdateIngredientRequest(
    string Name,
    string Description,
    Guid CategoryId,
    decimal Abv
);
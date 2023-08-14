namespace CocktailApp.Contracts.Ingredient;

public record CreateIngredientRequest(
    string Name,
    string Description,
    Guid CategoryId,
    decimal Abv
);
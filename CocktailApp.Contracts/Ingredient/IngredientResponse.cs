namespace CocktailApp.Contracts.Ingredient;

public record IngredientResponse(
    Guid Id,
    string Name,
    string Description,
    Guid CategoryId,
    decimal Abv
);
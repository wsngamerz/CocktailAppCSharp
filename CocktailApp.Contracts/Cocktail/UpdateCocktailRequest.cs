using CocktailApp.Contracts.Enums;

namespace CocktailApp.Contracts.Cocktail;

public record UpdateCocktailRequest(
    string Name,
    string Description,
    string Slug,
    GlassType GlassType,
    string LiquidColor,
    float LiquidOpacity,
    CocktailPrivacy Privacy,
    int UserId,
    decimal Abv,
    DateTime CreatedAt
);
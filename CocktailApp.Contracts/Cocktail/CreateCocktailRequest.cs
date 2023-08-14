using CocktailApp.Contracts.Enums;

namespace CocktailApp.Contracts.Cocktail;

public record CreateCocktailRequest(
    string Name,
    string Description,
    GlassType GlassType,
    string LiquidColor,
    float LiquidOpacity,
    CocktailPrivacy Privacy,
    Guid UserId,
    decimal Abv
);
namespace CocktailApp.Contracts.Cocktail;

public record CocktailResponse(
    Guid Id,
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
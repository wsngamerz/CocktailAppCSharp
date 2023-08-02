using CocktailApp.Contracts.Cocktail;

namespace CocktailApp.Models;

public class Cocktail
{
    public Guid Id { get; }
    public string Name { get; }
    public string Description { get; }
    public string Slug { get; }
    public GlassType GlassType { get; }
    public string LiquidColor { get; }
    public float LiquidOpacity { get; }
    public CocktailPrivacy Privacy { get; }
    public int UserId { get; }
    public decimal Abv { get; }
    public DateTime CreatedAt { get; }

    public Cocktail(Guid id, string name, string description, string slug, GlassType glassType, string liquidColor,
        float liquidOpacity, CocktailPrivacy privacy, int userId, decimal abv, DateTime createdAt)
    {
        Id = id;
        Name = name;
        Description = description;
        Slug = slug;
        GlassType = glassType;
        LiquidColor = liquidColor;
        LiquidOpacity = liquidOpacity;
        Privacy = privacy;
        UserId = userId;
        Abv = abv;
        CreatedAt = createdAt;
    }
}
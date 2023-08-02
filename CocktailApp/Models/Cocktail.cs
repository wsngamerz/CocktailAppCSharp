using CocktailApp.Contracts.Cocktail;
using CocktailApp.ServiceErrors;
using ErrorOr;
using UuidExtensions;

namespace CocktailApp.Models;

public class Cocktail
{
    public const int MinNameLength = 3;
    public const int MaxNameLength = 64;
    public const int MinDescriptionLength = 3;
    public const int MaxDescriptionLength = 1024;

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

    private Cocktail(Guid id, string name, string description, string slug, GlassType glassType, string liquidColor,
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

    public static ErrorOr<Cocktail> Create(
        string name,
        string description,
        GlassType glassType,
        string liquidColor,
        float liquidOpacity,
        CocktailPrivacy privacy,
        int userId,
        decimal abv,
        Guid? id = null
    )
    {
        List<Error> errors = new();

        if (name.Length is < MinNameLength or > MaxNameLength)
            errors.Add(Errors.Cocktail.InvalidName);

        if (description.Length is < MinDescriptionLength or > MaxDescriptionLength)
            errors.Add(Errors.Cocktail.InvalidDescription);

        if (errors.Any()) return errors;

        return new Cocktail(
            id ?? Uuid7.Guid(),
            name,
            description,
            name.ToLower().Replace(" ", "-"),
            glassType,
            liquidColor,
            liquidOpacity,
            privacy,
            userId,
            abv,
            DateTime.UtcNow
        );
    }

    public static ErrorOr<Cocktail> From(CreateCocktailRequest request)
    {
        return Create(
            request.Name,
            request.Description,
            request.GlassType,
            request.LiquidColor,
            request.LiquidOpacity,
            request.Privacy,
            request.UserId,
            request.Abv
        );
    }

    public static ErrorOr<Cocktail> From(Guid id, UpdateCocktailRequest request)
    {
        return Create(
            request.Name,
            request.Description,
            request.GlassType,
            request.LiquidColor,
            request.LiquidOpacity,
            request.Privacy,
            request.UserId,
            request.Abv,
            id
        );
    }
}
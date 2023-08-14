using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CocktailApp.Contracts.Cocktail;
using CocktailApp.Contracts.Enums;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using UuidExtensions;

namespace CocktailApp.Models;

[Table("Cocktails"), PrimaryKey(nameof(Id)), Index(nameof(Slug), IsUnique = true)]
public class Cocktail
{
    [Key] public Guid Id { get; set; }

    [Required, MinLength(1), MaxLength(256)]
    public string Name { get; set; } = string.Empty;

    [Required, MinLength(1), MaxLength(1024)]
    public string Description { get; set; } = string.Empty;

    [Required] public string Slug { get; set; } = string.Empty;
    [Required] public GlassType GlassType { get; set; }
    [Required] public string LiquidColor { get; set; } = string.Empty;
    [Required] public float LiquidOpacity { get; set; }
    [Required] public CocktailPrivacy Privacy { get; set; }
    public int UserId { get; set; }
    [Required] public decimal Abv { get; set; }
    [Required] public DateTime CreatedAt { get; set; }

    private Cocktail()
    {
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
        var cocktail = new Cocktail
        {
            Id = id ?? Uuid7.Guid(),
            Name = name,
            Description = description,
            Slug = GenerateSlug(name),
            GlassType = glassType,
            LiquidColor = liquidColor,
            LiquidOpacity = liquidOpacity,
            Privacy = privacy,
            UserId = userId,
            Abv = abv,
            CreatedAt = DateTime.UtcNow
        };

        // check that the cocktail is valid using the annotations
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(cocktail, new ValidationContext(cocktail), results, true);

        if (isValid) return cocktail;
        
        var errors = new List<Error>();

        results.ForEach(r =>
        {
            if (r.ErrorMessage != null)
                errors.Add(Error.Validation("validation", r.ErrorMessage));
        });

        return errors;
    }

    public static Cocktail CreateId(Guid id)
    {
        return new Cocktail
        {
            Id = id
        };
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

    private static string GenerateSlug(string name)
    {
        return name.ToLower().Replace(" ", "-");
    }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CocktailApp.Contracts.Ingredient;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using UuidExtensions;

namespace CocktailApp.Models;

[Table("Ingredients"), PrimaryKey(nameof(Id))]
public class Ingredient
{
    [Key] public Guid Id { get; set; }

    [Required, MinLength(1), MaxLength(256)]
    public string Name { get; set; } = string.Empty;

    [Required, MinLength(1), MaxLength(1024)]
    public string Description { get; set; } = string.Empty;

    [Required] public Guid CategoryId { get; set; }
    [Required] public decimal Abv { get; set; }

    [ForeignKey(nameof(CategoryId))] public Category Category { get; set; }

    private Ingredient()
    {
    }

    public static ErrorOr<Ingredient> Create(
        string name,
        string description,
        Guid categoryId,
        decimal abv,
        Guid? id = null
    )
    {
        var ingredient = new Ingredient
        {
            Id = id ?? Uuid7.Guid(),
            Name = name,
            Description = description,
            CategoryId = categoryId,
            Abv = abv,
        };

        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(ingredient, new ValidationContext(ingredient), results, true);

        if (isValid) return ingredient;

        var errors = new List<Error>();

        results.ForEach(r =>
        {
            if (r.ErrorMessage != null)
                errors.Add(Error.Validation("validation", r.ErrorMessage));
        });

        return errors;
    }

    public static Ingredient CreateId(Guid id)
    {
        return new Ingredient
        {
            Id = id
        };
    }

    public static ErrorOr<Ingredient> From(CreateIngredientRequest request)
    {
        return Create(
            request.Name,
            request.Description,
            request.CategoryId,
            request.Abv
        );
    }


    public static ErrorOr<Ingredient> From(Guid id, UpdateIngredientRequest request)
    {
        return Create(
            request.Name,
            request.Description,
            request.CategoryId,
            request.Abv,
            id
        );
    }

    public IngredientResponse ToResponse()
    {
        var response = new IngredientResponse(
            Id,
            Name,
            Description,
            CategoryId,
            Abv
        );
        return response;
    }

    public static IngredientResponse ToResponse(Ingredient ingredient)
    {
        var response = new IngredientResponse(
            ingredient.Id,
            ingredient.Name,
            ingredient.Description,
            ingredient.CategoryId,
            ingredient.Abv
        );
        return response;
    }
}
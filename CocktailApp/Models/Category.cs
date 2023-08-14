using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using CocktailApp.Contracts.Category;
using CocktailApp.Contracts.Ingredient;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using UuidExtensions;

namespace CocktailApp.Models;

[Table("Categories"), PrimaryKey(nameof(Id))]
public class Category
{
    [Key] public Guid Id { get; set; }

    [Required, MinLength(1), MaxLength(256)]
    public string Name { get; set; } = string.Empty;

    [Required, MinLength(1), MaxLength(1024)]
    public string Description { get; set; } = string.Empty;

    public Guid? ParentId { get; set; }

    [ForeignKey(nameof(ParentId))] public Category Parent { get; set; }


    private Category()
    {
    }

    public static ErrorOr<Category> Create(
        string name,
        string description,
        Guid? parentId,
        Guid? id = null
    )
    {
        var category = new Category
        {
            Id = id ?? Uuid7.Guid(),
            Name = name,
            Description = description,
            ParentId = parentId
        };

        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(category, new ValidationContext(category), results, true);

        if (isValid) return category;

        var errors = new List<Error>();

        results.ForEach(r =>
        {
            if (r.ErrorMessage != null)
                errors.Add(Error.Validation("validation", r.ErrorMessage));
        });

        return errors;
    }

    public static Category CreateId(Guid id)
    {
        return new Category
        {
            Id = id
        };
    }

    public static ErrorOr<Category> From(CreateCategoryRequest request)
    {
        return Create(
            request.Name,
            request.Description,
            request.ParentId
        );
    }


    public static ErrorOr<Category> From(Guid id, UpdateCategoryRequest request)
    {
        return Create(
            request.Name,
            request.Description,
            request.ParentId,
            id
        );
    }
}
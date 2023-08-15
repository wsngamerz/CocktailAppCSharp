using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CocktailApp.Contracts.Cocktail;
using CocktailApp.Contracts.Enums;
using CocktailApp.Contracts.User;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using UuidExtensions;

namespace CocktailApp.Models;

[Table("Users"), PrimaryKey(nameof(Id)), Index(nameof(ClerkId), IsUnique = true)]
public class User
{
    [Key] public Guid Id { get; set; }
    public string? ClerkId { get; set; } = string.Empty;

    [Required, MinLength(1), MaxLength(256)]
    public string FirstName { get; set; } = string.Empty;

    [Required, MinLength(1), MaxLength(256)]
    public string LastName { get; set; } = string.Empty;

    [Required, MinLength(1), MaxLength(1024)]
    public string Bio { get; set; } = string.Empty;

    [Required] public string ImageUrl { get; set; } = string.Empty;

    [Required] public string AccentColor { get; set; } = string.Empty;

    [Required] public UserPrivacy Privacy { get; set; }

    [Required] public UserRole Role { get; set; }

    [Required] public DateTime CreatedAt { get; set; }

    public ICollection<Cocktail> Cocktails { get; set; }
    public ICollection<BarItem> BarItems { get; set; }
    public ICollection<CocktailFavourite> Favourites { get; set; }

    private User()
    {
    }

    public static ErrorOr<User> Create(
        string? clerkId,
        string firstName,
        string lastName,
        string bio,
        string imageUrl,
        string accentColor,
        UserPrivacy userPrivacy,
        UserRole userRole,
        Guid? id = null
    )
    {
        var user = new User
        {
            Id = id ?? Uuid7.Guid(),
            ClerkId = clerkId,
            FirstName = firstName,
            LastName = lastName,
            Bio = bio,
            ImageUrl = imageUrl,
            AccentColor = accentColor,
            Privacy = userPrivacy,
            Role = userRole,
            CreatedAt = DateTime.UtcNow
        };

        // check that the cocktail is valid using the annotations
        var results = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(user, new ValidationContext(user), results, true);

        if (isValid) return user;

        var errors = new List<Error>();

        results.ForEach(r =>
        {
            if (r.ErrorMessage != null)
                errors.Add(Error.Validation("validation", r.ErrorMessage));
        });

        return errors;
    }

    public static User CreateId(Guid id)
    {
        return new User
        {
            Id = id
        };
    }

    public static ErrorOr<User> From(Guid id, UpdateUserRequest request)
    {
        return Create(
            null,
            request.FirstName,
            request.LastName,
            request.Bio,
            request.ImageUrl,
            request.AccentColor,
            request.Privacy,
            request.Role,
            id
        );
    }
}
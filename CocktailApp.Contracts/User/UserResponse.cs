using CocktailApp.Contracts.Enums;

namespace CocktailApp.Contracts.User;

public record UserResponse(
    Guid Id,
    string FirstName,
    string LastName,
    string Bio,
    string AccentColor,
    string ImageUrl,
    UserPrivacy Privacy,
    UserRole Role,
    DateTime CreatedAt
);
using CocktailApp.Contracts.Enums;

namespace CocktailApp.Contracts.User;

public record UpdateUserRequest(
    string FirstName,
    string LastName,
    string Bio,
    string AccentColor,
    string ImageUrl,
    UserPrivacy Privacy,
    UserRole Role
);
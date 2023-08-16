namespace CocktailApp.Contracts.User;

public record CurrentUserResponse(
    Guid Id,
    Dictionary<string, string> Claims
);
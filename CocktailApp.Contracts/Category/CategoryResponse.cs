namespace CocktailApp.Contracts.Category;

public record CategoryResponse(
    Guid Id,
    string Name,
    string Description,
    Guid? ParentId
);
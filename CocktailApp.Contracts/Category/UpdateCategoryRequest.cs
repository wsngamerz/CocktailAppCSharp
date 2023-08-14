namespace CocktailApp.Contracts.Category;

public record UpdateCategoryRequest(
    string Name,
    string Description,
    Guid? ParentId
);
namespace CocktailApp.Contracts.Category;

public record CreateCategoryRequest(
    string Name,
    string Description,
    Guid? ParentId
);
using ErrorOr;

namespace CocktailApp.ServiceErrors;

public class Errors
{
    public static class Cocktail
    {
        public static Error InvalidName => Error.Validation(
            code: "Cocktail.InvalidName",
            description:
            $"Name must be between {Models.Cocktail.MinNameLength} and {Models.Cocktail.MaxNameLength} characters"
        );

        public static Error InvalidDescription => Error.Validation(
            code: "Cocktail.InvalidDescription",
            description:
            $"Description must be between {Models.Cocktail.MinDescriptionLength} and {Models.Cocktail.MaxDescriptionLength} characters"
        );

        public static Error NotFound => Error.NotFound(
            code: "Cocktail.NotFound",
            description: "Cocktail not found"
        );
    }
}
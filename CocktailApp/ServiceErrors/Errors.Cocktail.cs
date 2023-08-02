using ErrorOr;

namespace CocktailApp.ServiceErrors;

public class Errors
{
    public static class Cocktail
    {
        public static Error NotFound => Error.NotFound(
            code: "Cocktail.NotFound",
            description: "Cocktail not found"
        );
    }
}
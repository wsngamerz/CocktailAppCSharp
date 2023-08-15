namespace CocktailApp.Contracts.Favourite;

public record FavouriteRequest(Guid EntityId, bool Favourite);
namespace CocktailApp.Services.Abstractions;

public interface ISupabaseService
{
    public Task<string> LoginUser(string email, string password);
}
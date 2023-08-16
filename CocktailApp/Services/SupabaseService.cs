using CocktailApp.Services.Abstractions;
using Newtonsoft.Json;
using Supabase.Gotrue;
using Supabase.Gotrue.Interfaces;

namespace CocktailApp.Services;

public class SupabaseService : ISupabaseService
{
    private readonly Supabase.Client _supabase;
    private readonly IGotrueClient<User, Session> _auth;

    public SupabaseService(string url, string key)
    {
        _supabase = new Supabase.Client(url, key);
        Task.Run(async () => await _supabase.InitializeAsync());

        _auth = _supabase.Auth;
    }

    public async Task<string> LoginUser(string email, string password)
    {
        var session = await _auth.SignIn(email, password);

        Console.Out.WriteLine(JsonConvert.SerializeObject(session, Formatting.Indented));
        
        return session.AccessToken;
    }
}
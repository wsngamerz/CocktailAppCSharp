using CocktailApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CocktailApp.Data;

public class CocktailAppContext: DbContext
{
    public CocktailAppContext(DbContextOptions<CocktailAppContext> options) : base(options)
    {
    }
    
    public DbSet<Cocktail> Cocktails { get; set; }
}
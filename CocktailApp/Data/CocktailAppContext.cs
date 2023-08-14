using CocktailApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CocktailApp.Data;

public class CocktailAppContext : DbContext
{
    public CocktailAppContext(DbContextOptions<CocktailAppContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Cocktail> Cocktails { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<User> Users { get; set; }
}
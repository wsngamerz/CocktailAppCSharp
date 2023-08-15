using CocktailApp.Models;
using Microsoft.EntityFrameworkCore;

namespace CocktailApp.Data;

public class CocktailAppContext : DbContext
{
    public CocktailAppContext(DbContextOptions<CocktailAppContext> options) : base(options)
    {
    }

    public DbSet<BarItem> BarItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Cocktail> Cocktails { get; set; }
    public DbSet<CocktailIngredient> CocktailIngredients { get; set; }
    public DbSet<CocktailInstruction> CocktailInstructions { get; set; }
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<List> Lists { get; set; }
    public DbSet<ListItem> ListItems { get; set; }
    public DbSet<User> Users { get; set; }

    public DbSet<CocktailFavourite> CocktailFavourites { get; set; }
    public DbSet<ListFavourite> ListFavourites { get; set; }
}
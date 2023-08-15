using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CocktailApp.Models;

[Table("CocktailFavourites"), PrimaryKey(nameof(UserId), nameof(CocktailId))]
public class CocktailFavourite
{
    public Guid UserId { get; set; }
    public Guid CocktailId { get; set; }
    public DateTime DateAdded { get; set; }

    [ForeignKey(nameof(UserId))] public User User { get; set; }
    [ForeignKey(nameof(CocktailId))] public Cocktail Cocktail { get; set; }

    private CocktailFavourite()
    {
    }

    public static CocktailFavourite CreateId(Guid userId, Guid cocktailId)
    {
        return new CocktailFavourite
        {
            UserId = userId,
            CocktailId = cocktailId
        };
    }
}
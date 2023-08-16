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

    public static CocktailFavourite CreateId(Guid[] keys)
    {
        return new CocktailFavourite
        {
            UserId = keys[0],
            CocktailId = keys[1]
        };
    }
}
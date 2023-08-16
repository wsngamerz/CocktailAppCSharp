using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CocktailApp.Models;

[Table("ListFavourites"), PrimaryKey(nameof(UserId), nameof(CocktailListId))]
public class CocktailListFavourite
{
    public Guid UserId { get; set; }
    public Guid CocktailListId { get; set; }
    public DateTime DateAdded { get; set; }

    [ForeignKey(nameof(UserId))] public User User { get; set; }
    [ForeignKey(nameof(CocktailListId))] public CocktailList CocktailList { get; set; }

    private CocktailListFavourite()
    {
    }

    public static CocktailListFavourite CreateId(Guid[] keys)
    {
        return new CocktailListFavourite
        {
            UserId = keys[0],
            CocktailListId = keys[1]
        };
    }
}
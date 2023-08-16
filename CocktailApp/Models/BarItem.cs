using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CocktailApp.Models;

[Table("BarItems"), PrimaryKey(nameof(UserId), nameof(IngredientId))]
public class BarItem
{
    public Guid UserId { get; set; }
    public Guid IngredientId { get; set; }
    public DateTime DateAdded { get; set; }

    [ForeignKey(nameof(UserId))] public User User { get; set; }
    [ForeignKey(nameof(IngredientId))] public Ingredient Ingredient { get; set; }

    private BarItem()
    {
    }

    public static BarItem CreateId(Guid[] keys)
    {
        return new BarItem
        {
            UserId = keys[0],
            IngredientId = keys[1]
        };
    }
}
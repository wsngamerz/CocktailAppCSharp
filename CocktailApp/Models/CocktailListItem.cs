using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CocktailApp.Models;

[Table("CocktailListItems"), PrimaryKey(nameof(CocktailListId), nameof(CocktailId))]
public class CocktailListItem
{
    public Guid CocktailListId { get; set; }
    public Guid CocktailId { get; set; }
    public int Position { get; set; }
    public DateTime DateAdded { get; set; }

    [ForeignKey(nameof(CocktailListId))] public CocktailList CocktailList { get; set; }
    [ForeignKey(nameof(CocktailId))] public Cocktail Cocktail { get; set; }
}
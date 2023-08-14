using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CocktailApp.Models;

[Table("ListItems"), PrimaryKey(nameof(ListId), nameof(CocktailId))]
public class ListItem
{
    public Guid ListId { get; set; }
    public Guid CocktailId { get; set; }
    public int Position { get; set; }
    public DateTime DateAdded { get; set; }

    [ForeignKey(nameof(ListId))] public List List { get; set; }
    [ForeignKey(nameof(CocktailId))] public Cocktail Cocktail { get; set; }
}
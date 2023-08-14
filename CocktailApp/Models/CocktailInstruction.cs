using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CocktailApp.Models;

[Table("CocktailInstructions"), PrimaryKey(nameof(CocktailId), nameof(Position))]
public class CocktailInstruction
{
    public Guid CocktailId { get; set; }
    public int Position { get; set; }
    public string Content { get; set; } = string.Empty;

    [ForeignKey(nameof(CocktailId))] public Cocktail Cocktail { get; set; }
}
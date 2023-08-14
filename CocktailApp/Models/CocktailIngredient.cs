using System.ComponentModel.DataAnnotations.Schema;
using CocktailApp.Contracts.Enums;
using Microsoft.EntityFrameworkCore;

namespace CocktailApp.Models;

[Table("CocktailIngredients"), PrimaryKey(nameof(CocktailId), nameof(IngredientId))]
public class CocktailIngredient
{
    public Guid CocktailId { get; set; }
    public Guid IngredientId { get; set; }
    public decimal Amount { get; set; }
    public CocktailUnit Unit { get; set; }
    public int Position { get; set; }

    [ForeignKey(nameof(CocktailId))] public Cocktail Cocktail { get; set; }
    [ForeignKey(nameof(IngredientId))] public Ingredient Ingredient { get; set; }
}
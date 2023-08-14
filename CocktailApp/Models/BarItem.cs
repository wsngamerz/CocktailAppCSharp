using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CocktailApp.Models;

[Table("BarItems"), PrimaryKey(nameof(Id))]
public class BarItem
{
    [Key] public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid IngredientId { get; set; }
    public DateTime DateAdded { get; set; }

    [ForeignKey(nameof(UserId))] public User User { get; set; }
    [ForeignKey(nameof(IngredientId))] public Ingredient Ingredient { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CocktailApp.Models;

[Table("Lists"), PrimaryKey(nameof(Id))]
public class CocktailList
{
    [Key] public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public DateTime DateCreated { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
    
    public ICollection<CocktailListItem> Items { get; set; }
}
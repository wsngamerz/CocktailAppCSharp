using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CocktailApp.Models;

[Table("ListFavourites"), PrimaryKey(nameof(UserId), nameof(ListId))]
public class ListFavourite
{
    public Guid UserId { get; set; }
    public Guid ListId { get; set; }
    public DateTime DateAdded { get; set; }

    [ForeignKey(nameof(UserId))] public User User { get; set; }
    [ForeignKey(nameof(ListId))] public List List { get; set; }
}
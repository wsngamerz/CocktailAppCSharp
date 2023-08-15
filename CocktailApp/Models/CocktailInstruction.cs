using System.ComponentModel.DataAnnotations.Schema;
using CocktailApp.Contracts.Cocktail;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CocktailApp.Models;

[Table("CocktailInstructions"), PrimaryKey(nameof(CocktailId), nameof(Position))]
public class CocktailInstruction
{
    public Guid CocktailId { get; set; }
    public int Position { get; set; }
    public string Content { get; set; } = string.Empty;

    [ForeignKey(nameof(CocktailId))] public Cocktail Cocktail { get; set; }
    
    public static ErrorOr<IEnumerable<CocktailInstruction>> From(Guid cocktailId, CreateCocktailRequest request)
    {
        IEnumerable<CocktailInstruction> cocktailInstructions;
        try
        {
            cocktailInstructions = request.Instructions.Select(instruction => new CocktailInstruction()
            {
                CocktailId = cocktailId,
                Content = instruction.Content,
                Position = instruction.Position
            }).ToList();
        }
        catch
        {
            return Error.Failure();
        }

        return ErrorOrFactory.From(cocktailInstructions);
    }
}
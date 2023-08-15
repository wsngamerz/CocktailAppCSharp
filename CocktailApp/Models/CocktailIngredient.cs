using System.ComponentModel.DataAnnotations.Schema;
using CocktailApp.Contracts.Cocktail;
using CocktailApp.Contracts.Enums;
using ErrorOr;
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

    public static ErrorOr<IEnumerable<CocktailIngredient>> From(Guid cocktailId, CreateCocktailRequest request)
    {
        IEnumerable<CocktailIngredient> cocktailIngredients;
        try
        {
            cocktailIngredients = request.Ingredients.Select(ingredient => new CocktailIngredient
            {
                CocktailId = cocktailId,
                IngredientId = ingredient.IngredientId,
                Amount = ingredient.Amount,
                Unit = ingredient.Unit,
                Position = ingredient.Position
            }).ToList();
        }
        catch
        {
            return Error.Failure();
        }

        return ErrorOrFactory.From(cocktailIngredients);
    }
}
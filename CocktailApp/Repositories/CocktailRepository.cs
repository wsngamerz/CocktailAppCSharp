using CocktailApp.Contracts.Cocktail;
using CocktailApp.Data;
using CocktailApp.Models;
using CocktailApp.Repositories.Abstractions;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CocktailApp.Repositories;

public class CocktailRepository : ICocktailRepository
{
    private readonly CocktailAppContext _context;

    public CocktailRepository(CocktailAppContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Created>> Create(Cocktail cocktail)
    {
        await _context.Cocktails.AddAsync(cocktail);
        await _context.SaveChangesAsync();
        return Result.Created;
    }

    public async Task<ErrorOr<Created>> CreateIngredients(IEnumerable<CocktailIngredient> ingredients)
    {
        await _context.CocktailIngredients.AddRangeAsync(ingredients);
        await _context.SaveChangesAsync();
        return Result.Created;
    }

    public async Task<ErrorOr<Created>> CreateInstructions(IEnumerable<CocktailInstruction> instructions)
    {
        await _context.CocktailInstructions.AddRangeAsync(instructions);
        await _context.SaveChangesAsync();
        return Result.Created;
    }

    public async Task<ErrorOr<Cocktail>> Get(params Guid[] ids)
    {
        var cocktail = await _context.Cocktails.FindAsync(ids[0]);
        if (cocktail is null)
            return Error.NotFound();

        return cocktail;
    }

    public async Task<ErrorOr<IEnumerable<Cocktail>>> All()
    {
        return await _context.Cocktails.ToListAsync();
    }

    public async Task<ErrorOr<Cocktail>> GetBySlug(string slug)
    {
        var cocktail = await _context.Cocktails.SingleOrDefaultAsync(c => c.Slug == slug);
        if (cocktail is null)
            return Error.NotFound();

        return cocktail;
    }

    public async Task<ErrorOr<DetailedCocktail>> GetDetailed(Guid id)
    {
        var detailedCocktail = await _context.Cocktails
            .Include(cocktail => cocktail.Ingredients)
            .ThenInclude(cocktailIngredient => cocktailIngredient.Ingredient)
            .Include(cocktail => cocktail.Instructions)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (detailedCocktail is null)
            return Error.NotFound();

        return new DetailedCocktail(
            detailedCocktail.Id,
            detailedCocktail.Name,
            detailedCocktail.Description,
            detailedCocktail.Slug,
            detailedCocktail.GlassType,
            detailedCocktail.LiquidColor,
            detailedCocktail.LiquidOpacity,
            detailedCocktail.Privacy,
            detailedCocktail.UserId,
            detailedCocktail.Abv,
            detailedCocktail.CreatedAt,
            detailedCocktail.Ingredients.Select(ing => new CocktailIngredientResponse(
                ing.Amount,
                ing.Unit,
                ing.Position,
                ing.Ingredient.ToResponse()
            )),
            detailedCocktail.Instructions.Select(ins => new CocktailInstructionResponse(
                ins.Content,
                ins.Position
            ))
        );
    }

    public async Task<ErrorOr<Cocktail>> Update(Cocktail cocktail)
    {
        _context.Cocktails.Update(cocktail);
        await _context.SaveChangesAsync();

        return cocktail;
    }

    public async Task<ErrorOr<Deleted>> Delete(params Guid[] ids)
    {
        _context.Cocktails.Remove(Cocktail.CreateId(ids[0]));
        await _context.SaveChangesAsync();
        return Result.Deleted;
    }

    public async Task<ErrorOr<int>> Count()
    {
        return await _context.Cocktails.CountAsync();
    }
}
using CocktailApp.Data;
using CocktailApp.Models;
using CocktailApp.Repositories.Abstractions;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CocktailApp.Repositories;

public class IngredientRepository : IIngredientRepository
{
    private readonly CocktailAppContext _context;

    public IngredientRepository(CocktailAppContext context)
    {
        _context = context;
    }

    public async Task<ErrorOr<Created>> Create(Ingredient ingredient)
    {
        await _context.Ingredients.AddAsync(ingredient);
        await _context.SaveChangesAsync();
        return Result.Created;
    }

    public async Task<ErrorOr<Ingredient>> GetById(Guid id)
    {
        var ingredient = await _context.Ingredients.FindAsync(id);
        if (ingredient is null)
            return Error.NotFound();

        return ingredient;
    }

    public async Task<ErrorOr<IEnumerable<Ingredient>>> GetMany()
    {
        return await _context.Ingredients.ToListAsync();
    }

    public async Task<ErrorOr<Updated>> Update(Ingredient ingredient)
    {
        _context.Ingredients.Update(ingredient);
        await _context.SaveChangesAsync();

        return Result.Updated;
    }


    public async Task<ErrorOr<Deleted>> Delete(Guid id)
    {
        _context.Ingredients.Remove(Ingredient.CreateId(id));
        await _context.SaveChangesAsync();
        return Result.Deleted;
    }

    public async Task<ErrorOr<int>> Count()
    {
        return await _context.Ingredients.CountAsync();
    }
}
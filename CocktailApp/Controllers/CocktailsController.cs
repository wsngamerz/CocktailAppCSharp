using CocktailApp.Contracts.Cocktail;
using CocktailApp.Models;
using CocktailApp.Services.Abstractions;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace CocktailApp.Controllers;

public class CocktailsController : ApiController
{
    private readonly ICocktailService _cocktailService;

    public CocktailsController(ICocktailService cocktailService)
    {
        _cocktailService = cocktailService;
    }

    /// <summary>
    /// Creates a cocktail
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <response code="201">Returns the newly created cocktail</response>
    /// <response code="400">If the request is invalid</response>
    [HttpPost]
    public async Task<IActionResult> CreateCocktail(CreateCocktailRequest request)
    {
        var requestToCocktailResult = Cocktail.From(request);
        
        if (requestToCocktailResult.IsError)
           return Problem(requestToCocktailResult.Errors);
        var cocktail = requestToCocktailResult.Value;
        
        var requestToCocktailIngredientsResult = CocktailIngredient.From(cocktail.Id, request);
        var requestToCocktailInstructionsResult = CocktailInstruction.From(cocktail.Id, request);

        if (requestToCocktailInstructionsResult.IsError ||
            requestToCocktailIngredientsResult.IsError)
        {
            List<Error> errors = new();
            errors.AddRange(requestToCocktailIngredientsResult.Errors);
            errors.AddRange(requestToCocktailInstructionsResult.Errors);
            return Problem(errors);
        }
        
        var cocktailIngredients = requestToCocktailIngredientsResult.Value;
        var cocktailInstructions = requestToCocktailInstructionsResult.Value;

        var createCocktailResult =
            await _cocktailService.CreateCocktail(cocktail, cocktailIngredients, cocktailInstructions);
        return createCocktailResult.Match(
            _ => CreatedAtAction(
                nameof(GetCocktail),
                new { id = cocktail.Id },
                cocktail.ToResponse()
            ), Problem);
    }

    /// <summary>
    /// Gets all cocktails
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetCocktails()
    {
        var getCocktailsResult = await _cocktailService.GetCocktails();
        return getCocktailsResult.Match(
            cocktails => Ok(cocktails.Select(Cocktail.ToResponse)),
            Problem
        );
    }

    /// <summary>
    /// Gets a cocktail by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="200">Returns the cocktail</response>
    /// <response code="404">If the cocktail is not found</response>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetCocktail(Guid id)
    {
        var getCocktailResult = await _cocktailService.GetDetailedCocktail(id);

        return getCocktailResult.Match(
            Ok,
            Problem
        );
    }

    /// <summary>
    /// Updates a cocktail by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <response code="400">If the request is invalid</response>
    /// <response code="404">If the cocktail is not found</response>
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCocktail(Guid id, UpdateCocktailRequest request)
    {
        var requestToCocktailResult = Cocktail.From(id, request);

        if (requestToCocktailResult.IsError)
            return Problem(requestToCocktailResult.Errors);
        var cocktail = requestToCocktailResult.Value;

        var updateCocktailResult = await _cocktailService.UpdateCocktail(cocktail);
        return updateCocktailResult.Match(result => Ok(result.ToResponse()), Problem);
    }

    /// <summary>
    /// Deletes a cocktail by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCocktail(Guid id)
    {
        var deleteCocktailResult = await _cocktailService.DeleteCocktail(id);
        return deleteCocktailResult.Match(_ => NoContent(), Problem);
    }
}
using CocktailApp.Contracts.Cocktail;
using CocktailApp.Models;
using CocktailApp.Services.Abstractions;
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
    public IActionResult CreateCocktail(CreateCocktailRequest request)
    {
        var requestToCocktailResult = Cocktail.From(request);

        if (requestToCocktailResult.IsError)
            return Problem(requestToCocktailResult.Errors);
        var cocktail = requestToCocktailResult.Value;

        var createCocktailResult = _cocktailService.CreateCocktail(cocktail);
        return createCocktailResult.Match(
            _ => CreatedAtAction(
                nameof(GetCocktail),
                new { id = cocktail.Id },
                MapCocktailResponse(cocktail)
            ), Problem);
    }

    /// <summary>
    /// Gets all cocktails
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult GetCocktails()
    {
        var getCocktailsResult = _cocktailService.GetCocktails();
        return getCocktailsResult.Match(
            cocktails => Ok(cocktails.Select(MapCocktailResponse)),
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
    public IActionResult GetCocktail(Guid id)
    {
        var getCocktailResult = _cocktailService.GetCocktail(id);

        return getCocktailResult.Match(
            cocktail => Ok(MapCocktailResponse(cocktail)),
            Problem
        );
    }

    /// <summary>
    /// Updates a cocktail by id
    /// </summary>
    /// <param name="id"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <response code="204">If the cocktail is updated</response>
    /// <response code="400">If the request is invalid</response>
    /// <response code="404">If the cocktail is not found</response>
    [HttpPut("{id:guid}")]
    public IActionResult UpdateCocktail(Guid id, UpdateCocktailRequest request)
    {
        var requestToCocktailResult = Cocktail.From(id, request);

        if (requestToCocktailResult.IsError)
            return Problem(requestToCocktailResult.Errors);
        var cocktail = requestToCocktailResult.Value;

        var updateCocktailResult = _cocktailService.UpdateCocktail(cocktail);
        return updateCocktailResult.Match(_ => NoContent(), Problem);
    }

    /// <summary>
    /// Deletes a cocktail by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteCocktail(Guid id)
    {
        var deleteCocktailResult = _cocktailService.DeleteCocktail(id);
        return deleteCocktailResult.Match(_ => NoContent(), Problem);
    }

    private static CocktailResponse MapCocktailResponse(Cocktail cocktail)
    {
        var response = new CocktailResponse(
            cocktail.Id,
            cocktail.Name,
            cocktail.Description,
            cocktail.Slug,
            cocktail.GlassType,
            cocktail.LiquidColor,
            cocktail.LiquidOpacity,
            cocktail.Privacy,
            cocktail.UserId,
            cocktail.Abv,
            cocktail.CreatedAt
        );
        return response;
    }
}
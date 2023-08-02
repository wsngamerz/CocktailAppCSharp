using CocktailApp.Contracts.Cocktail;
using CocktailApp.Models;
using CocktailApp.Services.Cocktails;
using Microsoft.AspNetCore.Mvc;
using UuidExtensions;

namespace CocktailApp.Controllers;

[ApiController]
[Route("[controller]")]
public class CocktailsController : ApiController
{
    private readonly ICocktailService _cocktailService;

    public CocktailsController(ICocktailService cocktailService)
    {
        _cocktailService = cocktailService;
    }

    [HttpPost]
    public IActionResult CreateCocktail(CreateCocktailRequest request)
    {
        var cocktail = new Cocktail(
            id: Uuid7.Guid(),
            name: request.Name,
            description: request.Description,
            slug: request.Name.ToLower().Replace(" ", "-"),
            glassType: request.GlassType,
            liquidColor: request.LiquidColor,
            liquidOpacity: request.LiquidOpacity,
            privacy: request.Privacy,
            userId: request.UserId,
            abv: request.Abv,
            createdAt: DateTime.UtcNow
        );

        var createCocktailResult = _cocktailService.CreateCocktail(cocktail);
        return createCocktailResult.Match(
            _ => CreatedAtAction(
                nameof(GetCocktail),
                new { id = cocktail.Id },
                MapCocktailResponse(cocktail)
            ), Problem);
    }

    [HttpGet]
    public IActionResult GetCocktails()
    {
        var getCocktailsResult = _cocktailService.GetCocktails();
        return getCocktailsResult.Match(
            cocktails => Ok(cocktails.Select(MapCocktailResponse)),
            Problem
        );
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetCocktail(Guid id)
    {
        var getCocktailResult = _cocktailService.GetCocktail(id);

        return getCocktailResult.Match(
            cocktail => Ok(MapCocktailResponse(cocktail)),
            Problem
        );
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpdateCocktail(Guid id, UpdateCocktailRequest request)
    {
        var cocktail = new Cocktail(
            id,
            request.Name,
            request.Description,
            request.Slug,
            request.GlassType,
            request.LiquidColor,
            request.LiquidOpacity,
            request.Privacy,
            request.UserId,
            request.Abv,
            request.CreatedAt
        );

        var updateCocktailResult = _cocktailService.UpdateCocktail(cocktail);
        return updateCocktailResult.Match(_ => NoContent(), Problem);
    }

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
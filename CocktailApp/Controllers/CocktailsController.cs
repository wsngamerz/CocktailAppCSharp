using CocktailApp.Contracts.Cocktail;
using CocktailApp.Models;
using CocktailApp.Services.Cocktails;
using Microsoft.AspNetCore.Mvc;
using UuidExtensions;

namespace CocktailApp.Controllers;

[ApiController]
[Route("[controller]")]
public class CocktailsController: ControllerBase
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
        
        _cocktailService.CreateCocktail(cocktail);

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

        return CreatedAtAction(
            nameof(GetCocktail),
            new { id = response.Id },
            response
        );
    }
    
    [HttpGet]
    public IActionResult GetCocktails()
    {
        var cocktails = _cocktailService.GetCocktails();
        
        return Ok(cocktails);
    }
    
    [HttpGet("{id:guid}")]
    public IActionResult GetCocktail(Guid id)
    {
        var cocktail = _cocktailService.GetCocktail(id);
        
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
        
        return Ok(response);
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
        
        _cocktailService.UpdateCocktail(cocktail);
        
        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public IActionResult DeleteCocktail(Guid id)
    {
        _cocktailService.DeleteCocktail(id);
        return NoContent();
    }
}
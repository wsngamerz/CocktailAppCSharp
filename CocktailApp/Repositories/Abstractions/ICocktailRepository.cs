using CocktailApp.Models;
using ErrorOr;

namespace CocktailApp.Repositories.Abstractions;

public interface ICocktailRepository
{
    Task<ErrorOr<Created>> Create(Cocktail cocktail);
    Task<ErrorOr<Cocktail>> GetById(Guid id);
    Task<ErrorOr<Cocktail>> GetBySlug(string slug);
    Task<ErrorOr<IEnumerable<Cocktail>>> GetMany();
    Task<ErrorOr<Updated>> Update(Cocktail cocktail);
    Task<ErrorOr<Deleted>> Delete(Guid id);
    Task<ErrorOr<int>> Count();
}
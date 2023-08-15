using CocktailApp.Models;
using ErrorOr;

namespace CocktailApp.Repositories.Abstractions;

public interface ICocktailRepository: IRepository<Cocktail>
{
    Task<ErrorOr<Cocktail>> GetBySlug(string slug);
}
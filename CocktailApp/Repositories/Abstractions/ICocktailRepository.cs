using CocktailApp.Models;
using ErrorOr;

namespace CocktailApp.Repositories.Abstractions;

public interface ICocktailRepository
{
    ErrorOr<Created> Create(Cocktail cocktail);
    ErrorOr<Cocktail> GetById(Guid id);
    ErrorOr<Cocktail> GetBySlug(string slug);
    ErrorOr<IEnumerable<Cocktail>> GetMany();
    ErrorOr<Updated> Update(Cocktail cocktail);
    ErrorOr<Deleted> Delete(Guid id);
    ErrorOr<int> Count();
}
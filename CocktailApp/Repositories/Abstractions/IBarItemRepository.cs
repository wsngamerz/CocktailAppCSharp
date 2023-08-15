using CocktailApp.Models;
using ErrorOr;

namespace CocktailApp.Repositories.Abstractions;

public interface IBarItemRepository: IRepository<BarItem>
{
    Task<ErrorOr<IEnumerable<BarItem>>> GetByUserId(Guid userId);
}
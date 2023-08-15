using CocktailApp.Models;
using CocktailApp.Repositories.Abstractions;
using CocktailApp.Services.Abstractions;
using ErrorOr;

namespace CocktailApp.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IBarItemRepository _barItemRepository;

    public UserService(IUserRepository userRepository, IBarItemRepository barItemRepository)
    {
        _userRepository = userRepository;
        _barItemRepository = barItemRepository;
    }

    public Task<ErrorOr<Created>> CreateUser(User user)
    {
        return _userRepository.Create(user);
    }

    public Task<ErrorOr<User>> GetUser(Guid userId)
    {
        return _userRepository.Get(userId);
    }

    public Task<ErrorOr<IEnumerable<User>>> GetUsers()
    {
        return _userRepository.All();
    }

    public Task<ErrorOr<IEnumerable<BarItem>>> GetUserBar(Guid userId)
    {
        return _barItemRepository.GetByUserId(userId);
    }

    public Task<ErrorOr<Updated>> UpdateUser(User user)
    {
        return _userRepository.Update(user);
    }

    public Task<ErrorOr<Deleted>> DeleteUser(Guid id)
    {
        return _userRepository.Delete(id);
    }
}
using CocktailApp.Models;
using CocktailApp.Repositories.Abstractions;
using CocktailApp.Services.Abstractions;
using ErrorOr;

namespace CocktailApp.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository =  userRepository;
    }

    public Task<ErrorOr<Created>> CreateUser(User user)
    {
        return _userRepository.Create(user);
    }

    public Task<ErrorOr<User>> GetUser(Guid id)
    {
        return _userRepository.GetById(id);
    }

    public Task<ErrorOr<IEnumerable<User>>> GetUsers()
    {
        return _userRepository.GetMany();
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
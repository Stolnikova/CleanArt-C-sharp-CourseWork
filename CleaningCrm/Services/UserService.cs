using CleaningCrm.Entities;
using CleaningCrm.Repositories.Interfaces;
using CleaningCrm.Services.Interfaces;

namespace CleaningCrm.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public User GetByLogin(string login)
    {
        User user = _repository.GetByLogin(login);
        if (user is null)
        {
            throw new KeyNotFoundException($"User with login '{login}' not found");
        }
            
        return user;
    }
}
using CleaningCrm.Entities;
using CleaningCrm.Enums;
using CleaningCrm.Repositories.Interfaces;

namespace CleaningCrm.Repositories;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users;

    public UserRepository()
    {
        _users = new List<User>
        {
            new User { Id = 1, Login = "admin1", PasswordHash = "hash1", Role = UserRole.Admin },
            new User { Id = 2, Login = "admin2", PasswordHash = "hash2", Role = UserRole.Admin }
        };
    }

    public User? GetByLogin(string login)
    {
        return _users.FirstOrDefault(u => u.Login == login);
    }
}
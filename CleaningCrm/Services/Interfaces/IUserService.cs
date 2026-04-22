using CleaningCrm.Entities;

namespace CleaningCrm.Services.Interfaces;

public interface IUserService
{
    Task<User> GetByLoginAsync(string login);
}
using CleaningCrm.Entities;

namespace CleaningCrm.Repositories.Interfaces;

public interface IUserRepository
{
   Task<User?> FindByLoginAsync(string login);
}

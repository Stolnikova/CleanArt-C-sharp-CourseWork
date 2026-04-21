using CleaningCrm.Entities;

namespace CleaningCrm.Repositories.Interfaces;

public interface IUserRepository
{
   User GetByLogin(string login);  
}
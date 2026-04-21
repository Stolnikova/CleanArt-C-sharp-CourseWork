using CleaningCrm.Entities;

namespace CleaningCrm.Services.Interfaces;

public interface IUserService
{
    User GetByLogin(string login);
}
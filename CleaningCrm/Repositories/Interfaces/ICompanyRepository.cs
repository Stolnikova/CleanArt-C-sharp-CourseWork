using CleaningCrm.Entities;

namespace CleaningCrm.Repositories.Interfaces;

public interface ICompanyRepository
{
    Task<IEnumerable<Company>> GetAllAsync();
    Task<IEnumerable<Company>> SearchAsync(string query);
    Task<Company?> GetByIdAsync(int id);
    Task<Company> CreateAsync(Company company);
    Task<Company> UpdateAsync(Company company);
    Task DeleteAsync(int id);
}
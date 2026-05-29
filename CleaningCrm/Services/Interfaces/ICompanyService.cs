using CleaningCrm.Entities;

namespace CleaningCrm.Services.Interfaces;

public interface ICompanyService
{
    Task<IEnumerable<Company>> GetAllAsync();
    Task<IEnumerable<Company>> SearchAsync(string query);
    Task<Company?> GetByIdAsync(int id);
    Task<Company> CreateAsync(Company company);
    Task<Company> UpdateAsync(Company company);
    Task DeleteAsync(int id);
}
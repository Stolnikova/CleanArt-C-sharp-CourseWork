using CleaningCrm.Entities;
using CleaningCrm.Repositories.Interfaces;
using CleaningCrm.Services.Interfaces;

namespace CleaningCrm.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _repository;

    public CompanyService(ICompanyRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Company>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<IEnumerable<Company>> SearchAsync(string query)
    {
        return await _repository.SearchAsync(query);
    }

    public async Task<Company?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Company> CreateAsync(Company company)
    {
        return await _repository.CreateAsync(company);
    }

    public async Task<Company> UpdateAsync(Company company)
    {
        return await _repository.UpdateAsync(company);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
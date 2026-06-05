using CleaningCrm.Entities;
using CleaningCrm.Repositories.Interfaces;
using CleaningCrm.Services.Interfaces;

namespace CleaningCrm.Services;

public class ContactPersonService : IContactPersonService
{
    private readonly IContactPersonRepository _repository;

    public ContactPersonService(IContactPersonRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ContactPerson>> GetByCompanyIdAsync(int companyId)
    {
        return await _repository.GetByCompanyIdAsync(companyId);
    }

    public async Task<ContactPerson?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<ContactPerson> CreateAsync(ContactPerson contactPerson)
    {
        return await _repository.CreateAsync(contactPerson);
    }

    public async Task<ContactPerson> UpdateAsync(ContactPerson contactPerson)
    {
        return await _repository.UpdateAsync(contactPerson);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
    
    public async Task<IEnumerable<ContactPerson>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
    public async Task<IEnumerable<ContactPerson>> SearchAsync(string search)
    {
        return await _repository.SearchAsync(search);
    }
}
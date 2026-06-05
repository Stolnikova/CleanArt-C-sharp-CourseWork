using CleaningCrm.Entities;

namespace CleaningCrm.Repositories.Interfaces;

public interface IContactPersonRepository
{
    Task<IEnumerable<ContactPerson>> GetAllAsync();
    Task<IEnumerable<ContactPerson>> GetByCompanyIdAsync(int companyId);
    Task<ContactPerson?> GetByIdAsync(int id);
    Task<ContactPerson> CreateAsync(ContactPerson contactPerson);
    Task<ContactPerson> UpdateAsync(ContactPerson contactPerson);
    Task DeleteAsync(int id);
    Task<IEnumerable<ContactPerson>> SearchAsync(string search);
}
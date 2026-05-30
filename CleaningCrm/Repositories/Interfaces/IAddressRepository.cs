using CleaningCrm.Entities;

namespace CleaningCrm.Repositories.Interfaces;

public interface IAddressRepository
{
    Task<IEnumerable<Address>> GetByContactPersonIdAsync(int contactPersonId);
    Task<Address?> GetByIdAsync(int id);
    Task<Address> CreateAsync(Address address);
    Task<Address> UpdateAsync(Address address);
    Task DeleteAsync(int id);
}
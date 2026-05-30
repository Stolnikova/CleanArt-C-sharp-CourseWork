using CleaningCrm.Entities;

namespace CleaningCrm.Services.Interfaces;

public interface IAddressService
{
    Task<IEnumerable<Address>> GetByContactPersonIdAsync(int contactPersonId);
    Task<Address?> GetByIdAsync(int id);
    Task<Address> CreateAsync(Address address);
    Task<Address> UpdateAsync(Address address);
    Task DeleteAsync(int id);
}
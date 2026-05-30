using CleaningCrm.Entities;
using CleaningCrm.Repositories.Interfaces;
using CleaningCrm.Services.Interfaces;

namespace CleaningCrm.Services;

public class AddressService : IAddressService
{
    private readonly IAddressRepository _repository;

    public AddressService(IAddressRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Address>> GetByContactPersonIdAsync(int contactPersonId)
    {
        return await _repository.GetByContactPersonIdAsync(contactPersonId);
    }

    public async Task<Address?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<Address> CreateAsync(Address address)
    {
        return await _repository.CreateAsync(address);
    }

    public async Task<Address> UpdateAsync(Address address)
    {
        return await _repository.UpdateAsync(address);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
using CleaningCrm.Entities;
using CleaningCrm.Repositories.Interfaces;
using CleaningCrm.Services.Interfaces;

namespace CleaningCrm.Services;

public class ServiceCatalogService : IServiceCatalogService
{
    private readonly IServiceItemRepository _repository;

    public ServiceCatalogService(IServiceItemRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<IEnumerable<ServiceItem>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<ServiceItem> GetByIdAsync(int id)
    {
        ServiceItem? item = await _repository.GetByIdAsync(id);
        if (item == null)
        {
            throw new KeyNotFoundException($"ServiceItem with id {id} not found.");
        }
        return item;
    }
    
    public async Task<ServiceItem> CreateAsync(ServiceItem serviceItem)
    {
        return await _repository.CreateAsync(serviceItem);
    }

    public async Task<ServiceItem> UpdateAsync(ServiceItem serviceItem)
    {
        return await _repository.UpdateAsync(serviceItem);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
    
}
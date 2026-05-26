using CleaningCrm.Entities;

namespace CleaningCrm.Repositories.Interfaces;

public interface IServiceItemRepository
{
    Task<IEnumerable<ServiceItem>> GetAllAsync();
    Task<ServiceItem?> GetByIdAsync(int id);
    Task<ServiceItem> CreateAsync(ServiceItem serviceItem);
    Task<ServiceItem> UpdateAsync(ServiceItem serviceItem);
    Task DeleteAsync(int id);
}
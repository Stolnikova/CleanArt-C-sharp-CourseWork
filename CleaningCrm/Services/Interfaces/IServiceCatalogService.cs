using CleaningCrm.Entities;

namespace CleaningCrm.Services.Interfaces;

public interface IServiceCatalogService
{
    Task<IEnumerable<ServiceItem>> GetAllAsync();
    Task<ServiceItem> GetByIdAsync(int id);
    Task<ServiceItem> CreateAsync(ServiceItem serviceItem);
    Task<ServiceItem> UpdateAsync(ServiceItem serviceItem);
    Task DeleteAsync(int id);
}
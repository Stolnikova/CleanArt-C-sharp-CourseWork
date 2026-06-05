using CleaningCrm.Entities;

namespace CleaningCrm.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<Order>> GetAllAsync();
    Task<IEnumerable<Order>> GetFilteredAsync(
        DateTime? from,
        DateTime? to,
        string? companyName,
        string? contactName,
        string? address,
        string? serviceName);
    Task<Order?> GetByIdAsync(int id);
    Task<Order> CreateAsync(Order order);
    Task<Order> UpdateAsync(Order order);
    Task DeleteAsync(int id);
}
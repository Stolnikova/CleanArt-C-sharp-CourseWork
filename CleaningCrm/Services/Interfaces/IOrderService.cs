using CleaningCrm.Entities;
using CleaningCrm.Enums;

namespace CleaningCrm.Services.Interfaces;
    
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<IEnumerable<Order>> GetFilteredAsync(
            DateTime? from,
            DateTime? to,
            string? companyName,
            string? contactName,
            string? address,
            string? serviceName);
        Task<Order> GetByIdAsync(int id);
        Task<Order> CreateAsync(Order order);
        Task<Order> UpdateAsync(Order order);
        Task ChangeStatusAsync(int id, OrderStatus status);
        Task DeleteAsync(int id);
    }

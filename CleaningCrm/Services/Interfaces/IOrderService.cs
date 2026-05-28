using CleaningCrm.Entities;
using CleaningCrm.Enums;

namespace CleaningCrm.Services.Interfaces;
    
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllAsync();
        Task<IEnumerable<Order>> GetByDateRangeAsync(DateTime from, DateTime to);
        Task<Order> GetByIdAsync(int id);
        Task<Order> CreateAsync(Order order);
        Task<Order> UpdateAsync(Order order);
        Task ChangeStatusAsync(int id, OrderStatus status);
        Task DeleteAsync(int id);
    }

using CleaningCrm.Entities;
using CleaningCrm.Enums;
using CleaningCrm.Repositories.Interfaces;
using CleaningCrm.Services.Interfaces;

namespace CleaningCrm.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IServiceItemRepository _serviceItemRepository;

    public OrderService(IOrderRepository repository, IServiceItemRepository serviceItemRepository)
    {
        _repository = repository;
        _serviceItemRepository = serviceItemRepository;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    
    public async Task<IEnumerable<Order>> GetFilteredAsync(
        DateTime? from,
        DateTime? to,
        string? companyName,
        string? contactName,
        string? address,
        string? serviceName)
    {
        return await _repository.GetFilteredAsync(from, to, companyName, contactName, address, serviceName);
    }

    public async Task<Order> GetByIdAsync(int id)
    {
        Order? order = await _repository.GetByIdAsync(id);
        if (order == null)
        {
            throw new KeyNotFoundException($"Order with id {id} not found.");
        }
        return order;
    }

    public async Task<Order> CreateAsync(Order order)
    {
        foreach (OrderItem item in order.Items)
        {
            ServiceItem? serviceItem = await _serviceItemRepository.GetByIdAsync(item.ServiceItemId);
            if (serviceItem == null)
            {
                throw new KeyNotFoundException($"ServiceItem with id {item.ServiceItemId} not found.");
            }

            item.ServiceName = serviceItem.Name;
            item.UnitSnapshot = serviceItem.Unit.ToUkrainianString();
            item.PriceSnapshot = serviceItem.Price;
        }

        order.TotalAmount = order.Items.Sum(i => i.PriceSnapshot * i.Quantity);
        return await _repository.CreateAsync(order);
    }

    public async Task<Order> UpdateAsync(Order order)
    {
        foreach (OrderItem item in order.Items)
        {
            ServiceItem? serviceItem = await _serviceItemRepository.GetByIdAsync(item.ServiceItemId);
            if (serviceItem == null)
            {
                throw new KeyNotFoundException($"ServiceItem with id {item.ServiceItemId} not found.");
            }

            item.ServiceName = serviceItem.Name;
            item.UnitSnapshot = serviceItem.Unit.ToUkrainianString();
            item.PriceSnapshot = serviceItem.Price;
        }

        order.TotalAmount = order.Items.Sum(i => i.PriceSnapshot * i.Quantity);
        return await _repository.UpdateAsync(order);
    }

    public async Task ChangeStatusAsync(int id, OrderStatus status)
    {
        Order order = await GetByIdAsync(id);
        if (order.Status == OrderStatus.Completed || order.Status == OrderStatus.Cancelled)
        {
            throw new InvalidOperationException(
                $"Cannot change status of order that is already {order.Status}.");
        }
        order.Status = status;
        await _repository.UpdateAsync(order);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
using CleaningCrm.DTOs.Requests;
using CleaningCrm.DTOs.Responses;
using CleaningCrm.Entities;

namespace CleaningCrm.Mappers;

public class OrderMapper
{
    public OrderResponse ToResponse(Order entity)
    {
        return new OrderResponse
        {
            Id = entity.Id,
            ClientId = entity.ClientId,
            ClientFullName = entity.Client.FullName,
            Address = entity.Address,
            ScheduledDate = entity.ScheduledDate,
            CreatedAt = entity.CreatedAt,
            Status = entity.Status,
            TotalAmount = entity.TotalAmount,
            Items = entity.Items.Select(ToItemResponse).ToList()
        };
    }

    public IEnumerable<OrderResponse> ToResponseList(IEnumerable<Order> entities)
    {
        return entities.Select(ToResponse);
    }

    public OrderItemResponse ToItemResponse(OrderItem entity)
    {
        return new OrderItemResponse
        {
            Id = entity.Id,
            ServiceItemId = entity.ServiceItemId,
            ServiceName = entity.ServiceName,
            UnitSnapshot = entity.UnitSnapshot,
            Quantity = entity.Quantity,
            PriceSnapshot = entity.PriceSnapshot,
            Total = entity.Quantity * entity.PriceSnapshot
        };
    }

    public Order ToEntity(CreateOrderRequest request)
    {
        return new Order
        {
            ClientId = request.ClientId,
            Address = request.Address,
            ScheduledDate = request.ScheduledDate,
            Items = request.Items.Select(ToItemEntity).ToList()
        };
    }

    public Order ToEntity(UpdateOrderRequest request, int id)
    {
        return new Order
        {
            Id = id,
            ClientId = request.ClientId,
            Address = request.Address,
            ScheduledDate = request.ScheduledDate,
            Items = request.Items.Select(ToItemEntity).ToList()
        };
    }

    private OrderItem ToItemEntity(OrderItemRequest request)
    {
        return new OrderItem
        {
            ServiceItemId = request.ServiceItemId,
            Quantity = request.Quantity
        };
    }
}
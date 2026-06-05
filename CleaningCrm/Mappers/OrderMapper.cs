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
            CompanyId = entity.CompanyId,
            CompanyName = entity.Company.Name,
            ContactPersonId = entity.ContactPersonId,
            ContactPersonFullName = entity.ContactPerson.FullName,
            AddressId = entity.AddressId,
            AddressLine = entity.Address != null ? entity.Address.Line : null,
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
            CompanyId = request.CompanyId,
            ContactPersonId = request.ContactPersonId,
            AddressId = request.AddressId,
            ScheduledDate = DateTime.SpecifyKind(request.ScheduledDate, DateTimeKind.Utc),
            Items = request.Items.Select(ToItemEntity).ToList()
        };
    }

    public Order ToEntity(UpdateOrderRequest request, int id)
    {
        return new Order
        {
            Id = id,
            CompanyId = request.CompanyId,
            ContactPersonId = request.ContactPersonId,
            AddressId = request.AddressId,
            ScheduledDate = DateTime.SpecifyKind(request.ScheduledDate, DateTimeKind.Utc),
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
using CleaningCrm.DTOs.Requests;
using CleaningCrm.DTOs.Responses;
using CleaningCrm.Entities;
using CleaningCrm.Enums;

namespace CleaningCrm.Mappers;

public class ServiceItemMapper
{
    public ServiceItemResponse ToResponse(ServiceItem serviceItem)
    {
        return new ServiceItemResponse
        {
            Id = serviceItem.Id,
            Name = serviceItem.Name,
            Unit = serviceItem.Unit.ToUkrainianString(),
            Price = serviceItem.Price,
            
        };
    }
    
    public IEnumerable<ServiceItemResponse> ToResponseList(IEnumerable<ServiceItem> serviceItems)
    {
        return serviceItems.Select(ToResponse);
    }
    
    public ServiceItem ToEntity(CreateServiceItemRequest request)
    {
        return new ServiceItem
        {
            Name = request.Name,
            Unit = request.Unit,
            Price = request.Price
        };
    }

    public ServiceItem ToEntity(UpdateServiceItemRequest request, int id)
    {
        return new ServiceItem
        {
            Id = id,
            Name = request.Name,
            Unit = request.Unit,
            Price = request.Price
        };
    }
    
}
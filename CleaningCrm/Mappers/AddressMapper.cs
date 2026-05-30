using CleaningCrm.DTOs.Requests;
using CleaningCrm.DTOs.Responses;
using CleaningCrm.Entities;

namespace CleaningCrm.Mappers;

public class AddressMapper
{
    public AddressResponse ToResponse(Address entity)
    {
        return new AddressResponse
        {
            Id = entity.Id,
            ContactPersonId = entity.ContactPersonId,
            Line = entity.Line,
            Notes = entity.Notes
        };
    }

    public IEnumerable<AddressResponse> ToResponseList(IEnumerable<Address> entities)
    {
        return entities.Select(ToResponse);
    }

    public Address ToEntity(CreateAddressRequest request, int contactPersonId)
    {
        return new Address
        {
            ContactPersonId = contactPersonId,
            Line = request.Line,
            Notes = request.Notes
        };
    }

    public Address ToEntity(UpdateAddressRequest request, int id, int contactPersonId)
    {
        return new Address
        {
            Id = id,
            ContactPersonId = contactPersonId,
            Line = request.Line,
            Notes = request.Notes
        };
    }
}
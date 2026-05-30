using CleaningCrm.DTOs.Requests;
using CleaningCrm.DTOs.Responses;
using CleaningCrm.Entities;

namespace CleaningCrm.Mappers;

public class ContactPersonMapper
{
    public ContactPersonResponse ToResponse(ContactPerson entity)
    {
        return new ContactPersonResponse
        {
            Id = entity.Id,
            CompanyId = entity.CompanyId,
            FullName = entity.FullName,
            Phone = entity.Phone,
            Email = entity.Email,
            Notes = entity.Notes,
            CreatedAt = entity.CreatedAt,
            Addresses = entity.Addresses
                .Select(a => new AddressSummaryResponse
                {
                    Id = a.Id,
                    Line = a.Line,
                    Notes = a.Notes
                })
                .ToList()
        };
    }

    public IEnumerable<ContactPersonResponse> ToResponseList(IEnumerable<ContactPerson> entities)
    {
        return entities.Select(ToResponse);
    }

    public ContactPerson ToEntity(CreateContactPersonRequest request, int companyId)
    {
        return new ContactPerson
        {
            CompanyId = companyId,
            FullName = request.FullName,
            Phone = request.Phone,
            Email = request.Email,
            Notes = request.Notes
        };
    }

    public ContactPerson ToEntity(UpdateContactPersonRequest request, int id, int companyId)
    {
        return new ContactPerson
        {
            Id = id,
            CompanyId = companyId,
            FullName = request.FullName,
            Phone = request.Phone,
            Email = request.Email,
            Notes = request.Notes
        };
    }

    public ContactPerson ToEntityIndependent(CreateContactPersonRequest request)
    {
        return new ContactPerson
        {
            CompanyId = null,
            FullName = request.FullName,
            Phone = request.Phone,
            Email = request.Email,
            Notes = request.Notes
        };
    }

    public ContactPerson ToEntityIndependent(UpdateContactPersonRequest request, int id)
    {
        return new ContactPerson
        {
            Id = id,
            CompanyId = null,
            FullName = request.FullName,
            Phone = request.Phone,
            Email = request.Email,
            Notes = request.Notes
        };
    }
}
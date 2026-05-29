using CleaningCrm.DTOs.Requests;
using CleaningCrm.DTOs.Responses;
using CleaningCrm.Entities;

namespace CleaningCrm.Mappers;

public class CompanyMapper
{
    public CompanyResponse ToResponse(Company entity)
    {
        return new CompanyResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Notes = entity.Notes,
            CreatedAt = entity.CreatedAt,
            ContactPersons = entity.ContactPersons
                .Select(cp => new ContactPersonSummaryResponse
                {
                    Id = cp.Id,
                    FullName = cp.FullName,
                    Phone = cp.Phone,
                    Email = cp.Email
                })
                .ToList()
        };
    }

    public IEnumerable<CompanyResponse> ToResponseList(IEnumerable<Company> entities)
    {
        return entities.Select(ToResponse);
    }

    public Company ToEntity(CreateCompanyRequest request)
    {
        return new Company
        {
            Name = request.Name,
            Notes = request.Notes
        };
    }

    public Company ToEntity(UpdateCompanyRequest request, int id)
    {
        return new Company
        {
            Id = id,
            Name = request.Name,
            Notes = request.Notes
        };
    }
}
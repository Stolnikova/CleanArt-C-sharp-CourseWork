using CleaningCrm.DTOs.Requests;
using CleaningCrm.DTOs.Responses;
using CleaningCrm.Entities;

namespace CleaningCrm.Mappers;

public class ClientMapper
{
    public ClientResponse ToResponse(Client entity)
    {
        return new ClientResponse
        {
            Id = entity.Id,
            FullName = entity.FullName,
            Phone = entity.Phone,
            Notes = entity.Notes,
            CompanyName = entity.CompanyName,
            CreatedAt = entity.CreatedAt,
        };
    }
    
    public IEnumerable<ClientResponse> ToResponseList(IEnumerable<Client> clients)
    {
        return clients.Select(ToResponse);
    }

    public Client ToEntity(CreateClientRequest request)
    {
        return new Client
        {
            FullName = request.FullName,
            Phone = request.Phone,
            CompanyName = request.CompanyName,
            Notes = request.Notes,
        };
    }
    
    public Client ToEntity(UpdateClientRequest request, int id)
    {
        return new Client
        {
            Id = id,
            FullName = request.FullName,
            Phone = request.Phone,
            CompanyName = request.CompanyName,
            Notes = request.Notes,
        };
    }
}
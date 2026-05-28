using CleaningCrm.Entities;

namespace CleaningCrm.Services.Interfaces;

public interface IClientService
{
    Task<IEnumerable<Client>> GetAllAsync();
    Task<Client> GetByIdAsync(int id);
    Task<IEnumerable<Client>> SearchAsync(string query);
    Task<Client> CreateAsync(Client client);
    Task<Client> UpdateAsync(Client client);
    Task DeleteAsync(int id);
}
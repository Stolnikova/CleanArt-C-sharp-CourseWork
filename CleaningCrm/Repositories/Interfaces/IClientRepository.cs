using CleaningCrm.Entities;

namespace CleaningCrm.Repositories.Interfaces;

public interface IClientRepository
{
    Task<IEnumerable<Client>> GetAllAsync();
    Task<Client?> GetByIdAsync(int id);
    Task<IEnumerable<Client>> SearchAsync(string query);
    Task<Client> CreateAsync(Client client);
    Task<Client> UpdateAsync(Client client);
    Task DeleteAsync(int id);
}
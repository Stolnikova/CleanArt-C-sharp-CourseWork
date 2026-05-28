using CleaningCrm.Entities;
using CleaningCrm.Repositories;
using CleaningCrm.Repositories.Interfaces;
using CleaningCrm.Services.Interfaces;

namespace CleaningCrm.Services;

public class ClientService : IClientService
{
     private readonly IClientRepository _repository;

     public ClientService(IClientRepository repository)
     {
          _repository = repository;
     }
     
     public async Task<IEnumerable<Client>> GetAllAsync()
     {
          return await _repository.GetAllAsync();
     }
     
     public async Task<Client> GetByIdAsync(int id)
     {
          Client? item = await _repository.GetByIdAsync(id);
          if (item == null)
          {
               throw new KeyNotFoundException($"Client with id {id} not found.");
          }
          return item;
     }
    
     public async Task<Client> CreateAsync(Client client)
     {
          return await _repository.CreateAsync(client);
     }

     public async Task<Client> UpdateAsync(Client client)
     {
          return await _repository.UpdateAsync(client);
     }

     public async Task DeleteAsync(int id)
     {
          await _repository.DeleteAsync(id);
     }

     public async Task<IEnumerable<Client>> SearchAsync(string query)
     {
         return await _repository.SearchAsync(query);
     }
}
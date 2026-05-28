using CleaningCrm.Data;
using CleaningCrm.Entities;
using CleaningCrm.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CleaningCrm.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;
    
    public ClientRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        return await _context.Clients.ToListAsync();
    }

    public async Task<Client?> GetByIdAsync(int id)
    {
        return await _context.Clients.FirstOrDefaultAsync(c => c.Id == id );
    }

    public async Task<Client> CreateAsync(Client client)
    {
        _context.Clients.Add(client);
        await _context.SaveChangesAsync();
        return client ;
    }

    public async Task<Client> UpdateAsync(Client client )
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
        return client;
    }
    
    public async Task DeleteAsync(int id)
    {
        Client? client = await _context.Clients.FindAsync(id);
        if (client == null) return;
        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
    }
    
    public async Task<IEnumerable<Client>> SearchAsync(string query)
    {
        return await _context.Clients
            .Where(c => c.FullName.Contains(query) ||
                        c.Phone.Contains(query) ||
                        c.CompanyName.Contains(query))
            .ToListAsync();
    }
}
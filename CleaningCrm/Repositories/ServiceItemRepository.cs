using CleaningCrm.Data;
using CleaningCrm.Entities;
using CleaningCrm.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CleaningCrm.Repositories;

public class ServiceItemRepository : IServiceItemRepository
{
    private readonly AppDbContext _context;
    
    public ServiceItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ServiceItem>> GetAllAsync()
    {
        return await _context.ServiceItems.Where(s => !s.IsDeleted).ToListAsync();
    }

    public async Task<ServiceItem?> GetByIdAsync(int id)
    {
        return await _context.ServiceItems.FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
    }
    
    public async Task<ServiceItem> CreateAsync(ServiceItem serviceItem)
    {
        _context.ServiceItems.Add(serviceItem);
        await _context.SaveChangesAsync();
        return serviceItem;
    }

    public async Task<ServiceItem> UpdateAsync(ServiceItem serviceItem)
    {
        _context.ServiceItems.Update(serviceItem);
        await _context.SaveChangesAsync();
        return serviceItem;
    }
    
    public async Task DeleteAsync(int id)
    {
        ServiceItem? item = await _context.ServiceItems.FindAsync(id);
        if (item != null)
        {
            return;
        }
        item.IsDeleted = true;
        await _context.SaveChangesAsync();
    }
}
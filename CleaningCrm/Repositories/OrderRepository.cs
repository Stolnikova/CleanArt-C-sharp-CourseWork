using CleaningCrm.Data;
using CleaningCrm.Entities;
using CleaningCrm.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CleaningCrm.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Order>> GetAllAsync()
    {
        return await _context.Orders
            .Include(o => o.Company)
            .Include(o => o.ContactPerson)
            .Include(o => o.Address)
            .Include(o => o.Items)
            .ThenInclude(i => i.ServiceItem)
            .ToListAsync();
    }
    

    public async Task<Order?> GetByIdAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.Company)
            .Include(o => o.ContactPerson)
            .Include(o => o.Address)
            .Include(o => o.Items)
            .ThenInclude(i => i.ServiceItem)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Order> CreateAsync(Order order)
    {
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return await _context.Orders
            .Include(o => o.Company)
            .Include(o => o.ContactPerson)
            .Include(o => o.Address)
            .Include(o => o.Items)
            .ThenInclude(i => i.ServiceItem)
            .FirstAsync(o => o.Id == order.Id);
    }

    public async Task<Order> UpdateAsync(Order order)
    {
        _context.Orders.Update(order);
        await _context.SaveChangesAsync();

        return await _context.Orders
            .Include(o => o.Company)
            .Include(o => o.ContactPerson)
            .Include(o => o.Address)
            .Include(o => o.Items)
            .ThenInclude(i => i.ServiceItem)
            .FirstAsync(o => o.Id == order.Id);
    }
    public async Task DeleteAsync(int id)
    {
        Order? order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return;
        }
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }
    
    
    public async Task<IEnumerable<Order>> GetFilteredAsync(
        DateTime? from,
        DateTime? to,
        string? companyName,
        string? contactName,
        string? address,
        string? serviceName)
    {
        IQueryable<Order> query = _context.Orders
            .Include(o => o.Company)
            .Include(o => o.ContactPerson)
            .Include(o => o.Address)
            .Include(o => o.Items)
            .ThenInclude(i => i.ServiceItem);

        if (from.HasValue)
        {
            query = query.Where(o => o.ScheduledDate >= from.Value);
        }
        if (to.HasValue)
        {
            query = query.Where(o => o.ScheduledDate <= to.Value);
        }
        if (!string.IsNullOrWhiteSpace(companyName))
        {
            query = query.Where(o => o.Company.Name.ToLower().Contains(companyName.ToLower()));
        }
        if (!string.IsNullOrWhiteSpace(contactName))
        {
            query = query.Where(o => o.ContactPerson.FullName.ToLower().Contains(contactName.ToLower()));
        }
        if (!string.IsNullOrWhiteSpace(address))
        {
            query = query.Where(o => o.Address != null && o.Address.Line.ToLower().Contains(address.ToLower()));
        }
        if (!string.IsNullOrWhiteSpace(serviceName))
        {
            query = query.Where(o => o.Items.Any(i => i.ServiceName.ToLower().Contains(serviceName.ToLower())));
        }

        return await query.ToListAsync();
    }
}
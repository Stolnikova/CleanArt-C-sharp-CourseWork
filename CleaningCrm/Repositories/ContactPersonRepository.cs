using CleaningCrm.Data;
using CleaningCrm.Entities;
using CleaningCrm.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CleaningCrm.Repositories;

public class ContactPersonRepository : IContactPersonRepository
{
    private readonly AppDbContext _context;

    public ContactPersonRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ContactPerson>> GetByCompanyIdAsync(int companyId)
    {
        return await _context.ContactPersons
            .Include(cp => cp.Addresses)
            .Where(cp => cp.CompanyId == companyId)
            .ToListAsync();
    }

    public async Task<ContactPerson?> GetByIdAsync(int id)
    {
        return await _context.ContactPersons
            .Include(cp => cp.Addresses)
            .FirstOrDefaultAsync(cp => cp.Id == id);
    }

    public async Task<ContactPerson> CreateAsync(ContactPerson contactPerson)
    {
        _context.ContactPersons.Add(contactPerson);
        await _context.SaveChangesAsync();
        return contactPerson;
    }

    public async Task<ContactPerson> UpdateAsync(ContactPerson contactPerson)
    {
        _context.ContactPersons.Update(contactPerson);
        await _context.SaveChangesAsync();
        return contactPerson;
    }

    public async Task DeleteAsync(int id)
    {
        ContactPerson? contactPerson = await _context.ContactPersons.FindAsync(id);
        if (contactPerson == null)
        {
            return;
        }
        _context.ContactPersons.Remove(contactPerson);
        await _context.SaveChangesAsync();
    }
}
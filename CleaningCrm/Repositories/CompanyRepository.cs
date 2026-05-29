using CleaningCrm.Data;
using CleaningCrm.Entities;
using CleaningCrm.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CleaningCrm.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly AppDbContext _context;

    public CompanyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Company>> GetAllAsync()
    {
        return await _context.Companies
            .Include(c => c.ContactPersons)
            .ToListAsync();
    }

    public async Task<IEnumerable<Company>> SearchAsync(string query)
    {
        return await _context.Companies
            .Include(c => c.ContactPersons)
            .Where(c => c.Name.Contains(query))
            .ToListAsync();
    }

    public async Task<Company?> GetByIdAsync(int id)
    {
        return await _context.Companies
            .Include(c => c.ContactPersons)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Company> CreateAsync(Company company)
    {
        _context.Companies.Add(company);
        await _context.SaveChangesAsync();
        return company;
    }

    public async Task<Company> UpdateAsync(Company company)
    {
        _context.Companies.Update(company);
        await _context.SaveChangesAsync();
        return company;
    }

    public async Task DeleteAsync(int id)
    {
        Company? company = await _context.Companies.FindAsync(id);
        if (company == null)
        {
            return;
        }
        _context.Companies.Remove(company);
        await _context.SaveChangesAsync();
    }
}
using CleaningCrm.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleaningCrm.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<ContactPerson> ContactPersons { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<ServiceItem> ServiceItems { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        modelBuilder.Entity<RefreshToken>()
            .HasOne(rt => rt.User)
            .WithMany()
            .HasForeignKey(rt => rt.UserId);

        modelBuilder.Entity<ServiceItem>()
            .Property(s => s.Unit)
            .HasConversion<string>();

        modelBuilder.Entity<Company>()
            .HasMany(c => c.ContactPersons)
            .WithOne(cp => cp.Company)
            .HasForeignKey(cp => cp.CompanyId);

        modelBuilder.Entity<ContactPerson>()
            .HasMany(cp => cp.Addresses)
            .WithOne(a => a.ContactPerson)
            .HasForeignKey(a => a.ContactPersonId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Company)
            .WithMany()
            .HasForeignKey(o => o.CompanyId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.ContactPerson)
            .WithMany()
            .HasForeignKey(o => o.ContactPersonId);

        modelBuilder.Entity<Order>()
            .HasOne(o => o.Address)
            .WithMany()
            .HasForeignKey(o => o.AddressId);

        modelBuilder.Entity<Order>()
            .Property(o => o.Status)
            .HasConversion<string>();

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.Items)
            .HasForeignKey(oi => oi.OrderId);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.ServiceItem)
            .WithMany()
            .HasForeignKey(oi => oi.ServiceItemId);
    }
}
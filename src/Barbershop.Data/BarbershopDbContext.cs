using Barbershop.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Barbershop.Data;

public class BarbershopDbContext(DbContextOptions<BarbershopDbContext> options)
    : DbContext(options)
{
    public DbSet<ServiceEntity> Service { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        => modelBuilder.ApplyConfigurationsFromAssembly(typeof(BarbershopDbContext).Assembly);
}
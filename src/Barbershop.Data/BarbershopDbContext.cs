using Barbershop.Domain.Entity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Barbershop.Data;

public class BarbershopDbContext : IdentityDbContext//<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
{
    public BarbershopDbContext(DbContextOptions<BarbershopDbContext> options)
        : base(options) { }

    public DbSet<ServiceEntity> Service { get; set; } = default!;
    public DbSet<UserEntity> User { get; set; } = default!;
    public DbSet<BarberEntity> Barber { get; set; } = default!;
    public DbSet<BarbershopOperationEntity> BarbershopOperations { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BarbershopDbContext).Assembly);
    }
}
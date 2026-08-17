using Barbershop.Domain.Contract.Repository;
using Barbershop.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Barbershop.Data.Repository;

public class ServiceRepository(BarbershopDbContext context) : IServiceRepository
{
    public void Add(ServiceEntity service)
        => context.Service.Add(service);

    public async Task<ServiceEntity[]> GetAllAsync(CancellationToken cancellationToken)
        => await context.Service
            .AsNoTracking()
            .Where(s => s.IsActive)
            .ToArrayAsync(cancellationToken);

    public async Task<ServiceEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await context.Service
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    public async Task<ServiceEntity?> GetForReadingAsync(Guid id, CancellationToken cancellationToken)
        => await context.Service
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    public void Update(ServiceEntity service)
        => context.Service.Update(service);
}
using Barbershop.Domain.Contract.Repository;
using Barbershop.Domain.Entity;
using Barbershop.Shareable.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Barbershop.Data.Repository;

public class BarbershopRepository(BarbershopDbContext context) : IBarbershopRepository
{
    public void Add(BarbershopOperationEntity barbershop)
        => context.BarbershopOperations.Add(barbershop);
    public void AddRange(IEnumerable<BarbershopOperationEntity> barbershops)
        => context.BarbershopOperations.AddRange(barbershops);

    public async Task<BarbershopOperationEntity[]> GetAllAsync(CancellationToken cancellationToken)
        => await context.BarbershopOperations
            .AsNoTracking()
            .ToArrayAsync(cancellationToken);

    public async Task<BarbershopOperationEntity[]> GetAllToUpdateAsync(CancellationToken cancellationToken)
        => await context.BarbershopOperations
            .ToArrayAsync(cancellationToken);

    public async Task<BarbershopOperationEntity?> GetByIdForUpdateAsync(Guid id, CancellationToken cancellationToken)
        => await context.BarbershopOperations
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

    public async Task<BarbershopOperationEntity?> GetByIdForReadyAsync(Guid id, CancellationToken cancellationToken)
        => await context.BarbershopOperations
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

    public void Update(BarbershopOperationEntity barbershop)
        => context.BarbershopOperations.Update(barbershop);
}
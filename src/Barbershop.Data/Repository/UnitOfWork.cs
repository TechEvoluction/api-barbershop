using Barbershop.Domain.Contract.Repository;

namespace Barbershop.Data.Repository;

public class UnitOfWork(BarbershopDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        => await context.SaveChangesAsync(cancellationToken);

    public void CleanTracker()
        => context.ChangeTracker.Clear();
}
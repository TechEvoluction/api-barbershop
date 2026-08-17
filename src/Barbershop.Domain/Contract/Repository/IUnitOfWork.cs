namespace Barbershop.Domain.Contract.Repository;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    void CleanTracker();
}
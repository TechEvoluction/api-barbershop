using Barbershop.Domain.Entity;

namespace Barbershop.Domain.Contract.Repository;

public interface IBarberRepository
{
    void Add(BarberEntity barber);
    Task<BarberEntity?> GetByIdForUpdateAsync(string id, CancellationToken cancellationToken);
    Task<BarberEntity?> GetByIdForReadyAsync(string id, CancellationToken cancellationToken);
    Task<BarberEntity[]> GetAllAsync(CancellationToken cancellationToken);
    void Update(BarberEntity barber);
}
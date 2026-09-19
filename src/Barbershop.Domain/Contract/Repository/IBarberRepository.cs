using Barbershop.Domain.Entity;

namespace Barbershop.Domain.Contract.Repository;

public interface IBarberRepository
{
    void Add(BarberEntity barber);
    void Add(BarberWorkdayEntity workday);
    Task<BarberEntity?> GetByIdForUpdateAsync(string id, CancellationToken cancellationToken);
    Task<BarberEntity?> GetByIdForReadyAsync(Guid id, CancellationToken cancellationToken);
    Task<BarberEntity[]> GetAllAsync(CancellationToken cancellationToken);
    void Update(BarberEntity barber);
    void Update(BarberWorkdayEntity workday);
}
using Barbershop.Domain.Entity;

namespace Barbershop.Domain.Contract.Repository;

public interface IServiceRepository
{
    void Add(ServiceEntity service);
    Task<ServiceEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ServiceEntity?> GetForReadingAsync(Guid id, CancellationToken cancellationToken);
    Task<ServiceEntity[]> GetAllAsync(CancellationToken cancellationToken);
    void Update(ServiceEntity service);
}
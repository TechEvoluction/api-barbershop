using Barbershop.Domain.Entity;

namespace Barbershop.Domain.Contract.Repository;

public interface IBarbershopRepository
{
    void Add(BarbershopOperationEntity barbershop);
    void AddRange(IEnumerable<BarbershopOperationEntity> barbershops);
    Task<BarbershopOperationEntity?> GetByIdForUpdateAsync(Guid id, CancellationToken cancellationToken);
    Task<BarbershopOperationEntity?> GetByIdForReadyAsync(Guid id, CancellationToken cancellationToken);
    Task<BarbershopOperationEntity[]> GetAllAsync(CancellationToken cancellationToken);
    Task<BarbershopOperationEntity[]> GetAllToUpdateAsync(CancellationToken cancellationToken);
    void Update(BarbershopOperationEntity barbershop);
}
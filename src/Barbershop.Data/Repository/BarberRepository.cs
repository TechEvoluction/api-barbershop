using Barbershop.Domain.Contract.Repository;
using Barbershop.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Barbershop.Data.Repository;

public class BarberRepository(BarbershopDbContext context) : IBarberRepository
{
    public void Add(BarberEntity barber)
        => context.Barber.Add(barber);

    public async Task<BarberEntity[]> GetAllAsync(CancellationToken cancellationToken)
        => await context.Barber
            .AsNoTracking()
            .Include(x => x.User)
            .ToArrayAsync(cancellationToken);

    public async Task<BarberEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => await context.Barber
            .AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

    public void Update(BarberEntity barber)
        => context.Barber.Update(barber);
}
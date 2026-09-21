using Barbershop.Domain.Contract.Repository;
using Barbershop.Domain.Entity;
using Barbershop.Shareable.Extensions;
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

    public async Task<BarberEntity?> GetByIdForUpdateAsync(string id, CancellationToken cancellationToken)
        => await context.Barber
            .Include(x => x.Workdays)
            .Include(x => x.Schedules)
            .Include(x => x.ScheduleBlocks)
            .FirstOrDefaultAsync(b => b.UserId == id, cancellationToken);

    public async Task<BarberEntity?> GetByIdForReadyAsync(string id, CancellationToken cancellationToken)
        => await context.Barber
            .AsNoTracking()
            .Include(x => x.Workdays)
            .Include(x => x.Schedules)
            .Include(x => x.ScheduleBlocks
                .Where(x => x.Date >= DateOnly.FromDateTime(DateTimeExtensions.BrazilDateTime().Date)))
            .FirstOrDefaultAsync(b => b.UserId == id, cancellationToken);

    public void Update(BarberEntity barber)
        => context.Barber.Update(barber);
}
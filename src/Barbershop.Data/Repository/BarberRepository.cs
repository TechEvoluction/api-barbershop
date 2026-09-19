using Barbershop.Domain.Contract.Repository;
using Barbershop.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Barbershop.Data.Repository;

public class BarberRepository(BarbershopDbContext context) : IBarberRepository
{
    public void Add(BarberEntity barber)
        => context.Barber.Add(barber);

    public void Add(BarberWorkdayEntity workday)
        => context.BarberWorkday.Add(workday);

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

    public async Task<BarberEntity?> GetByIdForReadyAsync(Guid id, CancellationToken cancellationToken)
        => await context.Barber
            .AsNoTracking()
            .Include(x => x.User)
            .Include(x => x.Workdays)
            .Include(x => x.Schedules)
            .Include(x => x.ScheduleBlocks)
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

    public void Update(BarberEntity barber)
        => context.Barber.Update(barber);

    public void Update(BarberWorkdayEntity workday)
        => context.BarberWorkday.Update(workday);
}
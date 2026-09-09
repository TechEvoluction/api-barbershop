namespace Barbershop.Domain.Entity;

public class BarberWorkdayEntity : BaseEntity
{
    public Guid BarberId { get; }
    public BarberEntity Barber { get; } = default!;
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public TimeOnly LunchStarts { get; private set; }
    public TimeOnly LunchEnds { get; private set; }
}
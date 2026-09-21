namespace Barbershop.Domain.Entity;

public class BarberWorkdayEntity : BaseEntity
{
    public string BarberId { get; }
    public BarberEntity Barber { get; } = default!;
    public DayOfWeek DayOfWeek { get; private set; }
    public TimeOnly StartTime { get; private set; }
    public TimeOnly EndTime { get; private set; }
    public TimeOnly? LunchStarts { get; private set; }
    public TimeOnly? LunchEnds { get; private set; }

    internal BarberWorkdayEntity(string barberId, DayOfWeek dayOfWeek, TimeOnly startTime, TimeOnly endTime, TimeOnly? lunchStarts, TimeOnly? lunchEnds)
    {
        BarberId = barberId;
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        EndTime = endTime;
        LunchStarts = lunchStarts;
        LunchEnds = lunchEnds;
    }

    public void UpdateWorkload(TimeOnly startTime, TimeOnly endTime, TimeOnly? lunchStarts, TimeOnly? lunchEnds)
    {
        if (lunchStarts is not null && lunchEnds is null
            || lunchStarts is null && lunchEnds is not null)
            throw new ArgumentNullException("Invalid lunch hours");

        StartTime = startTime;
        EndTime = endTime;
        LunchStarts = lunchStarts;
        LunchEnds = lunchEnds;
    }
}
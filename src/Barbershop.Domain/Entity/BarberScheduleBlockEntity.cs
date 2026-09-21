namespace Barbershop.Domain.Entity;

public class BarberScheduleBlockEntity : BaseEntity
{
    public string BarberId { get; }
    public BarberEntity Barber { get; } = default!;
    public DateOnly Date { get; private set; }
    public TimeOnly? StartTime { get; private set; }
    public TimeOnly? EndTime { get; private set; }
    public string Reason { get; init; } = string.Empty;
    public bool AllDay => StartTime is null && EndTime is null;

    internal BarberScheduleBlockEntity(string barberId, DateOnly date, TimeOnly? startTime, TimeOnly? endTime, string reason)
    {
        if (startTime is not null && endTime is null
            || startTime is null && endTime is not null)
            throw new ArgumentNullException("Invalid block schedule hours");

        BarberId = barberId;
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        Reason = reason;
    }
}
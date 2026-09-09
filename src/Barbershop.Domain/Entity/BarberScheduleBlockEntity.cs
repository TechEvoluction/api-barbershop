namespace Barbershop.Domain.Entity;

public class BarberScheduleBlockEntity : BaseEntity
{
    public Guid BarberId { get; }
    public BarberEntity Barber { get; } = default!;
    public DateTime? StartTime { get; private set; }
    public DateTime? EndTime { get; private set; }
    public string Reason { get; init; } = string.Empty;
    public bool AllDay => StartTime is null && EndTime is null;
}
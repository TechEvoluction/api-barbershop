namespace Barbershop.Domain.Entity;

public class BarberEntity : BaseEntity
{
    // especialidades ??
    public string UserId { get; private set; }
    public UserEntity User { get; } = default!;
    public string? Biography { get; private set; } = string.Empty;
    public List<SchedulingEntity> Schedules { get; } = [];
    public List<BarberWorkdayEntity> Workdays { get; } = [];
    public List<BarberScheduleBlockEntity> ScheduleBlocks { get; } = [];
    public bool AvailableToAssist { get; private set; } = false;

    private BarberEntity(string userId, string? biography, bool availableToAssist)
    {
        UserId = userId;
        Biography = biography;
        AvailableToAssist = availableToAssist;
    }

    public static BarberEntity Create(string userId, string? biography = null)
        => new(userId, biography, true);

    // method fechar agenda
}
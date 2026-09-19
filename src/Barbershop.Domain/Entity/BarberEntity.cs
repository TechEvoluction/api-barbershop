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

    public BarberWorkdayEntity AddWorkday(DayOfWeek dayOfWeek, TimeOnly startTime, TimeOnly endTime, TimeOnly? lunchStarts, TimeOnly? lunchEnds)
    {
        var workday = new BarberWorkdayEntity(Id, dayOfWeek, startTime, endTime, lunchStarts, lunchEnds);

        if (Workdays.Any(x => x.DayOfWeek == workday.DayOfWeek))
            throw new ArgumentException("Workday already exists for this day of the week");

        Workdays.Add(workday);

        return workday;
    }

    // method fechar agenda
}
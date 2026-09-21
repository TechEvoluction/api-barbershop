using Barbershop.Shareable.DTO;
using Barbershop.Shareable.Exceptions;

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

    private BarberWorkdayEntity AddWorkday(DayOfWeek dayOfWeek, TimeOnly startTime, TimeOnly endTime, TimeOnly? lunchStarts, TimeOnly? lunchEnds)
    {
        var workday = new BarberWorkdayEntity(UserId, dayOfWeek, startTime, endTime, lunchStarts, lunchEnds);

        if (Workdays.Any(x => x.DayOfWeek == workday.DayOfWeek))
            throw new ArgumentException("Workday already exists for this day of the week");

        Workdays.Add(workday);

        return workday;
    }

    public BarberScheduleBlockEntity BlockSchedule(DateOnly date, TimeOnly? startTime, TimeOnly? endTime, string reason)
    {
        if (ScheduleBlocks.Any(x => x.Date == date))
            throw new AppException("It is not permitted to close the schedule twice on the same day.", "SCHEDULE_BLOCK");

        var blockSchedule = new BarberScheduleBlockEntity(UserId, date, startTime, endTime, reason);

        ScheduleBlocks.Add(blockSchedule);

        return blockSchedule;
    }

    public List<WorkdayStatusDTO> SetWorkdays(List<WorkdayDTO> workdays)
    {
        List<WorkdayStatusDTO> workdaysResponse = [];

        foreach (var workday in workdays)
        {
            var workdayResponse = new WorkdayStatusDTO
            {
                DayOfWeek = workday.DayOfWeek,
                StartTime = workday.StartTime,
                EndTime = workday.EndTime,
                LunchStarts = workday.LunchStarts,
                LunchEnds = workday.LunchEnds
            };

            var workload = (workday.EndTime - workday.StartTime).TotalHours;

            if (workload > 9)
            {
                workdaysResponse.Add(workdayResponse with { MessageStatus = "Workday cannot exceed 8 hours" });
                continue;
            }

            if (workload > 8
                && (workday.LunchStarts is null || workday.LunchEnds is null))
            {
                workdaysResponse.Add(workdayResponse with { MessageStatus = "An 8-hour workday must include a lunch break" });
                continue;
            }

            var workdayRegistered = Workdays
                .FirstOrDefault(x => x.DayOfWeek == workday.DayOfWeek);

            if (workdayRegistered is not null)
            {
                workdayRegistered.UpdateWorkload(
                    workday.StartTime,
                    workday.EndTime,
                    workday.LunchStarts,
                    workday.LunchEnds);

                workdaysResponse.Add(workdayResponse with { MessageStatus = $"Updated {workday.DayOfWeek} workload" });
                continue;
            }

            AddWorkday(workday.DayOfWeek, workday.StartTime, workday.EndTime, workday.LunchStarts, workday.LunchEnds);

            workdaysResponse.Add(workdayResponse with { MessageStatus = "Successfully registered" });
        }

        return workdaysResponse;
    }

    // method fechar agenda
}
namespace Barbershop.Shareable.DTO;

public record WorkdayDTO
{
    public DayOfWeek DayOfWeek { get; set; }
    public TimeOnly StartTime { get; set; }
    public TimeOnly? LunchStarts { get; set; }
    public TimeOnly? LunchEnds { get; set; }
    public TimeOnly EndTime { get; set; }
}
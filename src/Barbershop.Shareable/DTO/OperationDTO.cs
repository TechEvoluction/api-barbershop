namespace Barbershop.Shareable.DTO;

public record OperationDTO
{
    public DateOnly? Date { get; set; }
    public DayOfWeek? DayOfWeek { get; set; }
    public TimeOnly? OpeningHours { get; set; }
    public TimeOnly? ClosingTime { get; set; }
}
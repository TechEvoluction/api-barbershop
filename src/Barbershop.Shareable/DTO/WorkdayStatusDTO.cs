namespace Barbershop.Shareable.DTO;

public record WorkdayStatusDTO : WorkdayDTO
{
    public string MessageStatus { get; set; } = string.Empty;
}
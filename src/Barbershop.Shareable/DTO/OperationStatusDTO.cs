using Barbershop.Shareable.Enum;

namespace Barbershop.Shareable.DTO;

public record OperationStatusDTO : OperationDTO
{
    public string MessageStatus { get; set; } = string.Empty;
    public ProcessingStatus Status { get; init; }
}
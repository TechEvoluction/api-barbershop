using Barbershop.Shareable.DTO;

namespace Barbershop.Shareable.Response;

public record OperationsResponse(List<OperationStatusDTO> Operations);
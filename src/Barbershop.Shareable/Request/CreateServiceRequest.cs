using Barbershop.Shareable.Enum;

namespace Barbershop.Shareable.Request;

public record CreateServiceRequest(
    string Name,
    string Description,
    decimal Price,
    TimeInMinutes Duration
);
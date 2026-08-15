namespace Barbershop.Shareable.Response;

public record ServiceResponse(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    int DurationInMinutes,
    string DurationInMinutesDescription,
    decimal? PromotionalPrice,
    DateTime? PromotionalPriceEndDate,
    bool IsActive);
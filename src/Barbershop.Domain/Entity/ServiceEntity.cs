using Barbershop.Shareable.Enum;
using Barbershop.Shareable.Exceptions;
using Barbershop.Shareable.Request;

namespace Barbershop.Domain.Entity;

public class ServiceEntity : BaseEntity
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public TimeInMinutes Duration { get; private set; }
    public bool IsActive { get; private set; } = false;
    public decimal? PromotionalPrice { get; private set; }
    public DateTime? PromotionalPriceEndDate { get; private set; }
    // Salvar fotos ???

    private ServiceEntity(string name, string description, decimal price, TimeInMinutes duration)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Description cannot be null or empty.");

        if (price <= 0)
            throw new NegativePriceException();

        Name = name;
        Description = description;
        Price = price;
        Duration = duration;
    }

    public static ServiceEntity CreateService(CreateServiceRequest request)
        => new(request.Name, request.Description, request.Price, request.Duration);

    public void PutServiceOnSale(decimal promotionalPrice, DateTime? promotionalPriceEndDate)
    {
        if (promotionalPrice <= 0)
            throw new NegativePriceException();

        if (promotionalPrice >= Price)
            throw new NegativePriceException(message: "Preço promocional deve ser inferior ao preço atual do serviço.");

        // REGRA DE NEGÓCIO: Validar com Jonatas
        promotionalPriceEndDate ??= DateTime.UtcNow.AddDays(15);

        PromotionalPrice = promotionalPrice;
        PromotionalPriceEndDate = promotionalPriceEndDate;
    }

    public void ActivateService() => IsActive = true;
}
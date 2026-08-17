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
    public byte[]? Image { get; private set; }
    public decimal DiscountPercentage => PromotionalPrice.HasValue ? (Price - PromotionalPrice.Value) / Price * 100 : 0m;
    public bool IsPromotional => PromotionalPrice.HasValue && PromotionalPriceEndDate!.Value > DateTime.UtcNow;
    public DateTime PromotionalDeadline { get; } = DateTime.UtcNow.AddHours(-3).AddDays(90);

    private ServiceEntity(string name, string description, decimal price, TimeInMinutes duration, byte[]? image)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be null or empty.");

        if (string.IsNullOrWhiteSpace(description) || description.Length > 500)
            throw new ArgumentException("Description cannot be null or empty.");

        if (price <= 0)
            throw new NegativePriceException();

        Name = name;
        Description = description;
        Price = price;
        Duration = duration;
        Image = image;
    }

    public static ServiceEntity Create(CreateServiceRequest request)
        => new(request.Name, request.Description, request.Price, request.Duration, request.Image);

    public ServiceEntity Update(UpdateServiceRequest request)
    {
        Name = request.Name;
        Description = request.Description;
        Price = request.Price;
        Duration = request.Duration;
        Image = request.Image;
        return this;
    }

    public void PutServiceOnSale(ServiceOnSaleRequest request)
    {
        var promotionalPrice = request.PromotionalPrice;
        var promotionalPriceEndDate = request.PromotionalPriceEndDate;

        if (promotionalPrice <= 0)
            throw new NegativePriceException();

        if (promotionalPrice >= Price)
            throw new NegativePriceException(message: "Preço promocional deve ser inferior ao preço atual do serviço.", httpStatusCode: 422);

        if (promotionalPriceEndDate.HasValue && promotionalPriceEndDate.Value > PromotionalDeadline)
            throw new PromotionalEndDateException(PromotionalDeadline);

        // REGRA DE NEGÓCIO: Validar com Jonatas
        // REGRA DE NEGÓCIO: Limitar uma data máxima para o fim da promoção ?
        promotionalPriceEndDate ??= DateTime.UtcNow.AddDays(15);

        PromotionalPrice = promotionalPrice;
        PromotionalPriceEndDate = promotionalPriceEndDate;
    }

    public void ActivateService() => IsActive = true;

    public void DeactivateService() => IsActive = false;

    public void RemoveServiceFromSale()
        => (PromotionalPrice, PromotionalPriceEndDate) = (null, null);
}
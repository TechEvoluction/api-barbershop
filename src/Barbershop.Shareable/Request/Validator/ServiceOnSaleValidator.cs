using FluentValidation;

namespace Barbershop.Shareable.Request.Validator;

public sealed class ServiceOnSaleValidator : AbstractValidator<ServiceOnSaleRequest>
{
    public ServiceOnSaleValidator()
    {
        RuleFor(x => x.PromotionalPrice)
            .GreaterThan(0)
            .WithMessage("O preço promocional do serviço deve ser maior que zero.");

        RuleFor(x => x.PromotionalPriceEndDate)
            .InclusiveBetween(DateTime.UtcNow.AddHours(-3).AddDays(1), DateTime.UtcNow.AddDays(90).AddHours(-3))
            .When(x => x.PromotionalPriceEndDate.HasValue)
            .WithMessage($"A data de término do preço promocional deve estar entre '{DateTime.UtcNow.AddDays(1).AddHours(-3):dd/MM/yyyy HH:mm:ss}' e '{DateTime.UtcNow.AddDays(90).AddHours(-3):dd/MM/yyyy HH:mm:ss}'.");
    }
}
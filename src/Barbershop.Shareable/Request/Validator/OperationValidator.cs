using Barbershop.Shareable.DTO;
using Barbershop.Shareable.Extensions;
using FluentValidation;

namespace Barbershop.Shareable.Request.Validator;

public sealed class OperationValidator : AbstractValidator<OperationDTO>
{
    public OperationValidator()
    {
        RuleFor(x => x.Date)
            .GreaterThan(DateOnly.FromDateTime(DateTimeExtensions.BrazilDateTime()))
            .When(x => x.Date.HasValue)
            .WithMessage("The date must be later than the current date.");

        RuleFor(x => x.DayOfWeek)
            .IsInEnum()
            .When(x => x.DayOfWeek.HasValue)
            .WithMessage("Invalid day of week.");

        RuleFor(x => x.OpeningHours)
            .NotNull()
            .When(x => x.ClosingTime.HasValue)
            .WithMessage("Opening hours is required when closing time is provided.");

        RuleFor(x => x.ClosingTime)
            .NotNull()
            .When(x => x.OpeningHours.HasValue)
            .WithMessage("Closing time is required when opening hours is provided.");
    }
}
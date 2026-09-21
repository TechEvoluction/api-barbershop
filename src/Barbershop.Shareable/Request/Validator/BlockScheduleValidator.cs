using Barbershop.Shareable.Request.Barber;
using FluentValidation;

namespace Barbershop.Shareable.Request.Validator;

public sealed class BlockScheduleValidator : AbstractValidator<BlockScheduleRequest>
{
    public BlockScheduleValidator()
    {
        RuleFor(x => x.Date)
            .NotEmpty()
            .WithMessage("Date must be provided.");

        RuleFor(x => x.Reason)
            .NotEmpty()
            .WithMessage("Reason not be empty.");

        RuleFor(x => x.StartTime)
            .NotNull()
            .When(x => x.EndTime.HasValue)
            .WithMessage("Start time is required when end time is provided.");

        RuleFor(x => x.EndTime)
            .NotNull()
            .When(x => x.StartTime.HasValue)
            .WithMessage("End time is required when start time is provided.");
    }
}
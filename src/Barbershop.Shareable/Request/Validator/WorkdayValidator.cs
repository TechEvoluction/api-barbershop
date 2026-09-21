using Barbershop.Shareable.DTO;
using FluentValidation;

namespace Barbershop.Shareable.Request.Validator;

public sealed class WorkdayValidator : AbstractValidator<WorkdayDTO>
{
    public WorkdayValidator()
    {
        RuleFor(x => x.DayOfWeek)
            .IsInEnum()
            .WithMessage("Invalid day of week.");

        RuleFor(x => x.StartTime)
            .NotNull()
            .WithMessage("Start time is required.");

        RuleFor(x => x.EndTime)
            .NotNull()
            .WithMessage("End time is required.");

        RuleFor(x => x.LunchEnds)
            .NotNull()
            .When(x => x.LunchStarts.HasValue)
            .WithMessage("Lunch end is required when lunch starts is provided.");

        RuleFor(x => x.LunchStarts)
            .NotNull()
            .When(x => x.LunchEnds.HasValue)
            .WithMessage("Lunch start is required when lunch end is provided.");
    }
}
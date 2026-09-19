using Barbershop.Shareable.Request.Barber;
using FluentValidation;

namespace Barbershop.Shareable.Request.Validator;

public sealed class BarberWorksdayValidator : AbstractValidator<BarberWorksdayRequest>
{
    public BarberWorksdayValidator()
    {
        RuleFor(x => x.Workdays)
            .NotEmpty()
            .WithMessage("At least one workday must be provided.");

        RuleFor(x => x.Workdays)
            .Must(workdays =>
                workdays.Select(x => x.DayOfWeek).Distinct().Count() == workdays.Count)
            .WithMessage("The day of the week cannot be repeated.");

        RuleForEach(x => x.Workdays)
            .SetValidator(new WorkdayValidator());
    }
}
using Barbershop.Shareable.Request.Barber;
using FluentValidation;

namespace Barbershop.Shareable.Request.Validator;

public sealed class OperationsValidator : AbstractValidator<OperationsRequest>
{
    public OperationsValidator()
    {
        RuleFor(x => x.Operations)
            .NotEmpty()
            .WithMessage("At least one operation must be provided.");

        RuleFor(x => x.Operations)
            .Must(workdays =>
                workdays.Select(x => x.DayOfWeek).Distinct().Count() == workdays.Count)
            .WithMessage("The day of the week cannot be repeated.");

        RuleForEach(x => x.Operations)
            .SetValidator(new OperationValidator());
    }
}
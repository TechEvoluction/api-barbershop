using FluentValidation;

namespace Barbershop.Shareable.Request.Validator;

public sealed class CreateServiceValidator : AbstractValidator<CreateServiceRequest>
{
    public CreateServiceValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("O nome do serviço é obrigatório.")
            .MaximumLength(50).WithMessage("O nome do serviço não pode exceder 50 caracteres.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("A descrição do serviço é obrigatória.")
            .MaximumLength(500).WithMessage("A descrição do serviço não pode exceder 500 caracteres.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("O preço do serviço deve ser maior que zero.");

        RuleFor(x => x.Duration)
            .IsInEnum().WithMessage("A duração do serviço é obrigatória e deve ser válida.");
    }
}
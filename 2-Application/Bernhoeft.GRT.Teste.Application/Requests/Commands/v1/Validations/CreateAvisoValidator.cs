using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations;

public class CreateAvisoValidator: AbstractValidator<CreateAvisoRequest>
{
    public CreateAvisoValidator()
    {
        RuleFor(x => x.Titulo)
            .NotEmpty().WithMessage("O título é obrigatório.")
            .NotNull().WithMessage("O título não pode ser nulo.")
            .MinimumLength(3).WithMessage("O título deve ter no mínimo 3 caracteres.")
            .MaximumLength(100).WithMessage("O título deve ter no máximo 100 caracteres.");

        RuleFor(x => x.Mensagem)
            .NotEmpty().WithMessage("A mensagem é obrigatória.")
            .NotNull().WithMessage("A mensagem não pode ser nula.")
            .MinimumLength(10).WithMessage("A mensagem deve ter no mínimo 10 caracteres.")
            .MaximumLength(500).WithMessage("A mensagem deve ter no máximo 500 caracteres.");
    }
}
using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations;

public class DeleteAvisoValidator: AbstractValidator<DeleteAvisoRequest>
{
    public DeleteAvisoValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("O Id deve ser maior que zero.");
    }
}
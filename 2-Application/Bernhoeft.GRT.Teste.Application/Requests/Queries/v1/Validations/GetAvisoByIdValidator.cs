using FluentValidation;

namespace Bernhoeft.GRT.Teste.Application.Requests.Queries.v1.Validations;

public class GetAvisoByIdValidator : AbstractValidator<GetAvisoByIdRequest>
{
    public GetAvisoByIdValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("O Id deve ser maior que zero.");
    }
}
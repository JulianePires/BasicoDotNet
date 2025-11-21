using Bernhoeft.GRT.Core.Enums;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations;
using Bernhoeft.GRT.Teste.Domain.Entities;
using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using FluentValidation;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1;

public class CreateAvisoHandler : IRequestHandler<CreateAvisoRequest, IOperationResult<string>>
{
    private readonly IAvisoRepository _avisoRepository;

    public CreateAvisoHandler(IServiceProvider serviceProvider)
    {
        _avisoRepository = (IAvisoRepository)serviceProvider.GetService(typeof(IAvisoRepository));
    }

    public async Task<IOperationResult<string>> Handle(CreateAvisoRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateAvisoValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return OperationResult<string>.Return(CustomHttpStatusCode.BadRequest, string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));

        var aviso = new AvisoEntity()
        {
            Titulo = request.Titulo,
            Mensagem = request.Mensagem,
            DataAtualizacao = DateTime.UtcNow,
            Ativo = true
        };
        await _avisoRepository.CriarAvisoAsync(aviso, cancellationToken);

        return OperationResult<string>.ReturnCreated();
    }
}
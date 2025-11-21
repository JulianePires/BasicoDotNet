using Bernhoeft.GRT.Core.Enums;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Exceptions;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Commands.v1.Validations;
using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using FluentValidation;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Commands.v1;

public class UpdateAvisoHandler : IRequestHandler<UpdateAvisoRequest, IOperationResult<string>>
{
    private readonly IAvisoRepository _avisoRepository;

    public UpdateAvisoHandler(IServiceProvider serviceProvider)
    {
        _avisoRepository = (IAvisoRepository)serviceProvider.GetService(typeof(IAvisoRepository));
    }

    public async Task<IOperationResult<string>> Handle(UpdateAvisoRequest request, CancellationToken cancellationToken)
    {
        var validator = new UpdateAvisoValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return OperationResult<string>.Return(CustomHttpStatusCode.BadRequest, string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));

        var avisoExistente = await _avisoRepository.ObterAvisoPorIdAsync(request.Id, cancellationToken);
        if (avisoExistente == null) return OperationResult<string>.ReturnNotFound();

        avisoExistente.Ativo = false;
        avisoExistente.DataAtualizacao = DateTime.UtcNow;

        await _avisoRepository.AtualizarAvisoAsync(avisoExistente);
        return OperationResult<string>.Return(CustomHttpStatusCode.Ok, "Aviso atualizado com sucesso.");
    }
}
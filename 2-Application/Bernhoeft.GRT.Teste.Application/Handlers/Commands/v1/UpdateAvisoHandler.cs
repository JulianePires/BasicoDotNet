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

public class UpdateAvisoHandler: IRequestHandler<UpdateAvisoRequest, IOperationResult<string>>
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IAvisoRepository _avisoRepository;
    private readonly AbstractValidator<UpdateAvisoRequest> _validator;

    public UpdateAvisoHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _avisoRepository = (IAvisoRepository)_serviceProvider.GetService(typeof(IAvisoRepository));
        _validator = new UpdateAvisoValidator();
    }

    public async Task<IOperationResult<string>> Handle(UpdateAvisoRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return OperationResult<string>.Return(CustomHttpStatusCode.Ok, validationResult.Errors.ToArray().ToString());

        var avisoExistente = await _avisoRepository.ObterAvisoPorIdAsync(request.Id, cancellationToken);
        if (avisoExistente == null) return OperationResult<string>.ReturnNotFound();

        avisoExistente.DataAtualizacao = new DateTime();

        await _avisoRepository.AtualizarAvisoAsync(avisoExistente);
        return OperationResult<string>.Return(CustomHttpStatusCode.Ok, "Aviso atualizado com sucesso.");
    }
}
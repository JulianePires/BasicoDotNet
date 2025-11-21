using Bernhoeft.GRT.Core.Enums;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1.Validations;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using FluentValidation;
using MediatR;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Queries.v1;

public class GetAvisoByIdHandler : IRequestHandler<GetAvisoByIdRequest, IOperationResult<GetAvisosResponse>>
{
    private readonly IServiceProvider _serviceProvider;
    private IAvisoRepository _avisoRepository;
    private AbstractValidator<GetAvisoByIdRequest> _validator;

    public GetAvisoByIdHandler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _avisoRepository = (IAvisoRepository)_serviceProvider.GetService(typeof(IAvisoRepository));
        _validator = new GetAvisoByIdValidator();
    }

    public async Task<IOperationResult<GetAvisosResponse>> Handle(GetAvisoByIdRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
            return OperationResult<GetAvisosResponse>.ReturnBadRequest();

        var result = await _avisoRepository.ObterAvisoPorIdAsync(request.Id, cancellationToken);
        if (result == null)
            return OperationResult<GetAvisosResponse>.ReturnNoContent();

        var response = (GetAvisosResponse)result;
        return OperationResult<GetAvisosResponse>.ReturnOk(response);
    }
}
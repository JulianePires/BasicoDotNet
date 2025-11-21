using Bernhoeft.GRT.Core.Extensions;
using Bernhoeft.GRT.Core.Interfaces.Results;
using Bernhoeft.GRT.Core.Models;
using Bernhoeft.GRT.Teste.Application.Requests.Queries.v1;
using Bernhoeft.GRT.Teste.Application.Responses.Queries.v1;
using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Bernhoeft.GRT.Teste.Application.Handlers.Queries.v1
{
    public class GetAvisosHandler : IRequestHandler<GetAvisosRequest, IOperationResult<IEnumerable<GetAvisosResponse>>>
    {
        private readonly IServiceProvider _serviceProvider;
        private IAvisoRepository AvisoRepository => _serviceProvider.GetRequiredService<IAvisoRepository>();

        public GetAvisosHandler(IServiceProvider serviceProvider) => _serviceProvider = serviceProvider;

        public async Task<IOperationResult<IEnumerable<GetAvisosResponse>>> Handle(GetAvisosRequest request, CancellationToken cancellationToken)
        {
            var result = await AvisoRepository.ObterTodosAvisosAsync();
            if (!result.HaveAny())
                return OperationResult<IEnumerable<GetAvisosResponse>>.ReturnNoContent();

            return OperationResult<IEnumerable<GetAvisosResponse>>.ReturnOk(result.Select(x => (GetAvisosResponse)x));
        }
    }
}
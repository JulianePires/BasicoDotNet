
using Bernhoeft.GRT.Core.EntityFramework.Domain.Interfaces;
using Bernhoeft.GRT.Teste.Domain.Entities;

namespace Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories
{
    public interface IAvisoRepository : IRepository<AvisoEntity>
    {
        Task CriarAvisoAsync(AvisoEntity aviso, CancellationToken cancellationToken = default);
        Task AtualizarAvisoAsync(AvisoEntity aviso);
        Task DeletarAvisoAsync(int id, CancellationToken cancellationToken);
        Task<AvisoEntity> ObterAvisoPorIdAsync(int id, CancellationToken cancellationToken);
        Task<List<AvisoEntity>> ObterTodosAvisosAsync();
    }
}
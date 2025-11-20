using Bernhoeft.GRT.Core.Attributes;
using Bernhoeft.GRT.Core.EntityFramework.Infra;
using Bernhoeft.GRT.Teste.Domain.Entities;
using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using Bernhoeft.GRT.Teste.Application.Exceptions;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Bernhoeft.GRT.Teste.Infra.Persistence.InMemory.Repositories
{
    [InjectService(Interface: typeof(IAvisoRepository))]
    [UsedImplicitly]
    public class AvisoRepository : Repository<AvisoEntity>, IAvisoRepository
    {
        private readonly DbContext _dbContext;
        private DbSet<AvisoEntity> Avisos => _dbContext.Set<AvisoEntity>();

        public AvisoRepository(DbContext dbContext) : base(dbContext) =>
            _dbContext = dbContext;

        public async Task CriarAvisoAsync(AvisoEntity aviso, CancellationToken cancellationToken = default)
        {
            await Avisos.AddAsync(aviso, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AtualizarAvisoAsync(AvisoEntity aviso)
        {
            var tracked = await Avisos.FindAsync(aviso.Id);
            if (tracked != null)
            {
                _dbContext.Entry(tracked).CurrentValues.SetValues(aviso);
            }
            else
            {
                Avisos.Update(aviso);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletarAvisoAsync(int id, CancellationToken cancellationToken)
        {
            var tracked = await Avisos.FindAsync(id);
            NotFoundException.ThrowIfNull(tracked, "Aviso não encontrado.");
            var aviso = tracked;
            aviso.Ativo = false;
            if (tracked != null)
            {
                _dbContext.Entry(tracked).CurrentValues.SetValues(aviso);
            }
            else
            {
                Avisos.Update(aviso);
            }
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<AvisoEntity> ObterAvisoPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var aviso = await Avisos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Ativo, cancellationToken);
            NotFoundException.ThrowIfNull(aviso, "Aviso não encontrado.");
            return aviso;
        }

        public Task<List<AvisoEntity>> ObterTodosAvisosAsync()
        {
            var avisos = Avisos.AsNoTracking().Where(x => x.Ativo).ToListAsync();
            return avisos;
        }
    }
}
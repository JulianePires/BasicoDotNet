using Bernhoeft.GRT.Core.Attributes;
using Bernhoeft.GRT.Core.EntityFramework.Infra;
using Bernhoeft.GRT.Teste.Domain.Entities;
using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using Bernhoeft.GRT.Teste.Infra.Persistence.InMemory.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace Bernhoeft.GRT.Teste.Infra.Persistence.InMemory.Repositories
{
    [InjectService(Interface: typeof(IAvisoRepository))]
    [UsedImplicitly]
    public class AvisoRepository : Repository<AvisoEntity>, IAvisoRepository
    {
        private readonly AvisoContext _dbContext;
        private DbSet<AvisoEntity> Avisos => _dbContext.Set<AvisoEntity>();

        public AvisoRepository(AvisoContext dbContext) : base(dbContext) =>
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
                aviso.DataAtualizacao = new DateTime();
                _dbContext.Entry(tracked!).CurrentValues.SetValues(aviso);
            }
            else
            {
                Avisos.Update(aviso);
            }
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletarAvisoAsync(int id, CancellationToken cancellationToken)
        {
            var aviso = await Avisos.FirstOrDefaultAsync(x => x.Id == id && x.Ativo, cancellationToken);
            Avisos.Remove(aviso);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task<AvisoEntity> ObterAvisoPorIdAsync(int id, CancellationToken cancellationToken)
        {
            var aviso = await Avisos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id && x.Ativo, cancellationToken);
            return aviso;
        }

        public Task<List<AvisoEntity>> ObterTodosAvisosAsync()
        {
            var avisos = Avisos.AsNoTracking().Where(x => x.Ativo).ToListAsync();
            return avisos;
        }
    }
}
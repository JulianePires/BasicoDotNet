using Microsoft.EntityFrameworkCore;
using Bernhoeft.GRT.Teste.Domain.Entities;

namespace Bernhoeft.GRT.Teste.IntegrationTests.Infra.Repositories.AvisoRepositoryTest;

public class TestDbContext : DbContext
{
    public TestDbContext(DbContextOptions<TestDbContext> options) : base(options) { }
    public DbSet<AvisoEntity> Avisos { get; set; }
}


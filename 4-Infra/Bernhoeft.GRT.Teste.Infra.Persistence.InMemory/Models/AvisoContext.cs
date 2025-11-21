using Bernhoeft.GRT.Teste.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bernhoeft.GRT.Teste.Infra.Persistence.InMemory.Models;


public partial class AvisoContext : DbContext
{

    public AvisoContext(DbContextOptions<AvisoContext> options) : base(options) { }

    public DbSet<AvisoEntity> Avisos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using Bernhoeft.GRT.Teste.Infra.Persistence.InMemory.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Bernhoeft.GRT.Teste.IntegrationTests.Infra.Repositories.AvisoRepositoryTest;

public class AvisoRepositoryTestCollectionFixture : IDisposable
{
    public ServiceProvider ServiceProvider { get; }

    public AvisoRepositoryTestCollectionFixture()
    {
        var services = new ServiceCollection();
        services.AddDbContext<TestDbContext>(options =>
            options.UseInMemoryDatabase("integration-tests-db"));
        services.AddScoped<DbContext>(provider => provider.GetRequiredService<TestDbContext>());
        services.AddScoped<IAvisoRepository, AvisoRepository>();
        ServiceProvider = services.BuildServiceProvider();
    }

    public void Dispose()
    {
        ServiceProvider?.Dispose();
    }
}

[CollectionDefinition("AvisoRepositoryTestCollection")]
public class AvisoRepositoryTestCollection : ICollectionFixture<AvisoRepositoryTestCollectionFixture>
{
}

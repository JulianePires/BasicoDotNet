using Bogus;
using Microsoft.EntityFrameworkCore;

namespace Bernhoeft.GRT.Teste.IntegrationTests.Base;

public class BaseFixture
{
    public BaseFixture()
        => Faker = new Faker("pt_BR");

    protected Faker Faker { get; set; }

    public DbContext CreateDbContext(bool preserveData = false)
    {
        var context = new DbContext(
            new DbContextOptionsBuilder<DbContext>()
                .UseInMemoryDatabase("integration-tests-db")
                .Options
        );
        if (preserveData == false)
            context.Database.EnsureDeleted();
        return context;
    }
}
using Bernhoeft.GRT.Teste.Domain.Entities;
using Bernhoeft.GRT.Teste.IntegrationTests.Base;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Bernhoeft.GRT.Teste.IntegrationTests.Infra.Repositories.AvisoRepositoryTest;

[CollectionDefinition(nameof(AvisoRepositoryTestFixture))]
public class CategoryRepositoryTestFixtureCollection
    : ICollectionFixture<AvisoRepositoryTestFixture>
{}

public class AvisoRepositoryTestFixture : BaseFixture
{
    public string GerarTituloValido()
    {
        var titulo = Faker.Lorem.Sentence(7);
        return titulo;
    }

    public string GerarMensagemValida()
    {
        var mensagem = Faker.Lorem.Paragraph(1);
        return mensagem;
    }

    public AvisoEntity GerarAvisoValido()
    {
        var aviso = new AvisoEntity
        {
            Titulo = GerarTituloValido(),
            Mensagem = GerarMensagemValida(),
        };
        return aviso;
    }
}
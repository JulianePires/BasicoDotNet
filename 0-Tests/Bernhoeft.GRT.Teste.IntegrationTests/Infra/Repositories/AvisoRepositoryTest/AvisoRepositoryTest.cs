using Bernhoeft.GRT.Teste.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Bernhoeft.GRT.Teste.Application.Exceptions;
using Xunit;

namespace Bernhoeft.GRT.Teste.IntegrationTests.Infra.Repositories.AvisoRepositoryTest;

[Collection("AvisoRepositoryTestCollection")]
public class AvisoRepositoryTest : IClassFixture<AvisoRepositoryTestFixture>
{
    private readonly IAvisoRepository _avisoRepository;
    private readonly AvisoRepositoryTestFixture _fixture;
    private readonly AvisoRepositoryTestCollectionFixture _collectionFixture;

    public AvisoRepositoryTest(AvisoRepositoryTestCollectionFixture collectionFixture,
        AvisoRepositoryTestFixture fixture)
    {
        _collectionFixture = collectionFixture;
        _avisoRepository = (IAvisoRepository)collectionFixture.ServiceProvider.GetService(typeof(IAvisoRepository));
        _fixture = fixture;
    }

    [Fact]
    public async Task Teste_AvisoRepository_Deve_ObterTodosAvisosAsync()
    {
        // Arrange
        var aviso = _fixture.GerarAvisoValido();
        await _avisoRepository.CriarAvisoAsync(aviso);

        // Act
        var avisos = await _avisoRepository.ObterTodosAvisosAsync();

        // Assert
        Assert.NotNull(avisos);
        Assert.Contains(avisos, a => a.Id == aviso.Id);
    }

    [Fact]
    public async Task Teste_AvisoRepository_Deve_Obter_Apenas_Avisos_Ativos()
    {
        // Arrange
        var avisoAtivo = _fixture.GerarAvisoValido();
        await _avisoRepository.CriarAvisoAsync(avisoAtivo);

        var avisoInativo = _fixture.GerarAvisoValido();
        avisoInativo.Ativo = false;
        await _avisoRepository.CriarAvisoAsync(avisoInativo);

        // Act
        var avisos = await _avisoRepository.ObterTodosAvisosAsync();

        // Assert
        Assert.NotNull(avisos);
        Assert.Contains(avisos, a => a.Id == avisoAtivo.Id);
        Assert.DoesNotContain(avisos, a => a.Id == avisoInativo.Id);
    }

    [Fact]
    public async Task Teste_AvisoRepository_Deve_ObterAvisoPorIdAsync()
    {
        // Arrange
        var aviso = _fixture.GerarAvisoValido();
        await _avisoRepository.CriarAvisoAsync(aviso);

        // Act
        var avisoObtido = await _avisoRepository.ObterAvisoPorIdAsync(aviso.Id, new CancellationToken());

        // Assert
        Assert.NotNull(avisoObtido);
        Assert.Equal(aviso.Id, avisoObtido.Id);
    }

    [Fact]
    public async Task Teste_AvisoRepository_Deve_LancarExcecaoAoObterAvisoInexistente()
    {
        // Arrange
        var avisoInexistenteId = -1;

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _avisoRepository.ObterAvisoPorIdAsync(avisoInexistenteId, CancellationToken.None);
        });
    }

    [Fact]
    public async Task Teste_AvisoRepository_Deve_AtualizarAvisoAsync()
    {
        // Arrange
        var aviso = _fixture.GerarAvisoValido();
        await _avisoRepository.CriarAvisoAsync(aviso);

        var novoTitulo = _fixture.GerarTituloValido();
        aviso.Titulo = novoTitulo;

        // Act
        await _avisoRepository.AtualizarAvisoAsync(aviso);
        var avisoAtualizado = await _avisoRepository.ObterAvisoPorIdAsync(aviso.Id, new CancellationToken());

        // Assert
        Assert.NotNull(avisoAtualizado);
        Assert.Equal(novoTitulo, avisoAtualizado.Titulo);
    }

    [Fact]
    public async Task Teste_AvisoRepository_Deve_DeletarAvisoAsync()
    {
        // Arrange
        var aviso = _fixture.GerarAvisoValido();
        await _avisoRepository.CriarAvisoAsync(aviso);

        // Act
        await _avisoRepository.DeletarAvisoAsync(aviso.Id, CancellationToken.None);

        // Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _avisoRepository.ObterAvisoPorIdAsync(aviso.Id, CancellationToken.None);
        });
    }

    [Fact]
    public async Task Teste_AvisoRepository_Deve_LancarExcecaoAoDeletarAvisoInexistente()
    {
        // Arrange
        var avisoInexistenteId = -1;

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
        {
            await _avisoRepository.DeletarAvisoAsync(avisoInexistenteId, CancellationToken.None);
        });
    }

    [Fact]
    public async Task Teste_AvisoRepository_Deve_CriarAvisoAsync()
    {
        // Arrange
        var aviso = _fixture.GerarAvisoValido();

        // Act
        await _avisoRepository.CriarAvisoAsync(aviso);
        var avisoCriado = await _avisoRepository.ObterAvisoPorIdAsync(aviso.Id, CancellationToken.None);

        // Assert
        Assert.NotNull(avisoCriado);
        Assert.Equal(aviso.Titulo, avisoCriado.Titulo);
        Assert.Equal(aviso.Mensagem, avisoCriado.Mensagem);
    }
}
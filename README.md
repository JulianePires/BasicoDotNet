# Bernhoeft.GRT.Teste

## Resumo do Projeto

Este projeto é uma API desenvolvida em ASP.NET Core, utilizando o padrão CQRS (Command Query Responsibility Segregation) e MediatR para separação de comandos e consultas. O objetivo é fornecer uma estrutura básica para gerenciamento de avisos, com persistência em banco de dados InMemory (pode ser facilmente adaptado para outros bancos via Entity Framework Core).

### Principais Tecnologias Utilizadas
- ASP.NET Core 9
- Entity Framework Core (InMemory)
- MediatR
- FluentValidation
- Swashbuckle (Swagger)
- API Versioning

## Estrutura do Projeto
- **1-Presentation**: Camada de apresentação (API)
- **2-Application**: Camada de aplicação (Handlers, Requests, Responses)
- **3-Domain**: Camada de domínio (Entidades, Interfaces)
- **4-Infra**: Infraestrutura (Repositórios, Mapeamentos)
- **0-Tests**: Testes de integração

## Endpoints Disponíveis

### Avisos

#### `GET /api/v1/avisos`
Retorna a lista de avisos cadastrados.

#### `GET /api/v1/avisos/{id}`
Retorna um aviso específico pelo ID.

#### `POST /api/v1/avisos`
Cria um novo aviso.
- Body: JSON com os dados do aviso.

#### `PUT /api/v1/avisos/{id}`
Atualiza um aviso existente.
- Body: JSON com os dados atualizados.

#### `DELETE /api/v1/avisos/{id}`
Remove um aviso pelo ID.

## Implementação

- **CQRS**: Handlers separados para comandos (criação, atualização, remoção) e queries (busca).
- **MediatR**: Utilizado para orquestrar a comunicação entre controllers e handlers.
- **FluentValidation**: Validação de requests antes do processamento.
- **Swagger**: Documentação automática dos endpoints, disponível em `/swagger`.
- **Injeção de Dependência**: Configurada via `Program.cs`.
- **Banco de Dados**: Utiliza InMemory para facilitar testes e desenvolvimento.

## Como Executar

1. Restaure os pacotes NuGet:
   ```bash
   dotnet restore
   ```
2. Execute a aplicação:
   ```bash
   dotnet run --project 1-Presentation/Bernhoeft.GRT.Teste.Api/Bernhoeft.GRT.Teste.Api.csproj
   ```
3. Acesse a documentação Swagger em: [http://localhost:5000/swagger](http://localhost:5000/swagger)

## Observações
- O projeto está pronto para ser adaptado para outros bancos de dados, bastando alterar a configuração do `DbContext`.
- Os testes de integração estão localizados na pasta `0-Tests`.

---

> Projeto desenvolvido para fins de estudo e demonstração de arquitetura limpa com CQRS e MediatR.


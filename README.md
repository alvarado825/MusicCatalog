# MusicCatalog API

Uma Web API RESTful para gerenciamento de catálogo musical, desenvolvida como projeto de aprofundamento em ASP.NET Core, arquitetura de software e design de APIs.

---

## Sobre o Projeto

O MusicCatalog permite gerenciar um catálogo de músicas (tracks), com suporte a artistas, álbuns e gêneros. Tracks seguem um ciclo de vida controlado (Draft → Published / Inactive), e o catálogo público expõe apenas faixas publicadas e ativas.

### Contexto e motivação

Tenho experiência como desenvolvedor backend .NET em sistemas de missão crítica — integrações REST/XML, sustentação de sistemas em produção, investigação de incidentes e análise de logs. Esse histórico me deu vivência real com o custo de decisões técnicas ruins em produção.

O que me faltava era exposição a práticas mais amplas de engenharia: arquitetura em camadas, design de domínio e contratos de API bem definidos. O MusicCatalog foi construído para preencher essa lacuna de forma prática e intencional — não como exercício acadêmico, mas como aplicação real dos conceitos que estudo.

O objetivo não foi criar algo complexo. Foi criar algo **pequeno, mas estruturalmente profissional**: consistente, explicável e evoluível.

Para detalhes sobre as escolhas de design, consulte o [ARCHITECTURE.md](./ARCHITECTURE.md).

---

## Stack Tecnológica

| Tecnologia | Uso |
|---|---|
| **.NET 8 / C#** | Framework principal |
| **ASP.NET Core** | Web API |
| **Entity Framework Core** | ORM |
| **MySQL 8.0** | Banco de dados relacional |
| **FluentValidation** | Validação de entrada |
| **Asp.Versioning** | Versionamento de API |
| **Docker / Docker Compose** | Containerização do banco de dados |
| **Swagger / OpenAPI** | Documentação interativa da API |

---

## Arquitetura

O projeto segue os princípios da **Clean Architecture**, separado em 4 camadas:

```
MusicCatalog/
├── MusicCatalog.Domain         # Entidades, Value Objects, Enums, regras de domínio
├── MusicCatalog.Application    # Casos de uso, interfaces, validações, exceções
├── MusicCatalog.Infrastructure # EF Core, Repositórios, UnitOfWork, Migrations
└── MusicCatalog.Api            # Controllers, Middlewares, Filters, configuração HTTP
```

### Fluxo de dependências

```
Api → Application → Domain
Infrastructure → Application
Infrastructure → Domain
```

> O Domínio não depende de nada. A Infraestrutura implementa as interfaces definidas na Application.

---

## Ciclo de Vida de uma Track

```
[Criação] → Draft
              │
              ▼
           Published  ←── requer AlbumId e GenreId preenchidos
              │
              ▼
           Inactive   ←── IsActive = false (reativação prevista no roadmap)
```

- **Draft**: estado inicial. Track não aparece no catálogo público.
- **Published**: visível no catálogo público. Exige `AlbumId` e `GenreId`.
- **Inactive**: desativada via soft delete (`IsActive = false`). Reativação prevista como melhoria futura — a estrutura já suporta.

---

## Endpoints

Base: `/api/v1`

### Tracks

| Método | Rota | Descrição |
|---|---|---|
| `GET` | `/tracks` | Lista o catálogo público (paginado, com filtros) |
| `GET` | `/tracks/{id}` | Busca uma track por ID |
| `POST` | `/tracks` | Cria uma nova track (status: Draft) |
| `PUT` | `/tracks/{id}` | Atualiza dados da track |
| `PATCH` | `/tracks/{id}/publish` | Publica a track |
| `PATCH` | `/tracks/{id}/deactivate` | Desativa a track |

#### Query Parameters — `GET /tracks`

| Parâmetro | Tipo | Obrigatório | Descrição |
|---|---|---|---|
| `page` | int | Sim | Número da página |
| `pageSize` | int | Sim | Itens por página |
| `artistId` | int | Não | Filtra por artista |
| `genreId` | int | Não | Filtra por gênero |

### Albums

| Método | Rota | Descrição |
|---|---|---|
| `POST` | `/albums` | Cria um álbum (com criação de artista opcional) |

---

## Como Executar

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/)

### 1. Clonar o repositório

```bash
git clone https://github.com/alvarado825/MusicCatalog
cd MusicCatalog
```

### 2. Subir o banco de dados

```bash
docker-compose up -d
```

### 3. Aplicar migrations

```bash
dotnet ef database update --project MusicCatalog.Infrastructure --startup-project MusicCatalog.Api
```

### 4. Rodar a API

```bash
cd MusicCatalog.Api
dotnet run
```

### 5. Acessar o Swagger

```
https://localhost:{porta}/swagger
```

> Swagger disponível apenas em ambiente **Development**.

---

## Decisões Importantes

**Clean Architecture** — separação em camadas com fluxo de dependência unidirecional. O domínio não conhece nada além de si mesmo.

**CQRS leve** — Commands trabalham com entidades de domínio para aplicar regras de negócio, retornando o estado atualizado por conveniência do cliente. Queries projetam diretamente no DTO sem instanciar entidades, trazendo apenas os campos necessários do banco.

**Use Cases isolados** — cada funcionalidade tem seu próprio Use Case. O controller apenas recebe a requisição e delega. Nenhuma regra de negócio nos controllers.

**Entidades com comportamento** — sem setters públicos. A entidade controla seu próprio estado via métodos (`Publish()`, `Deactivate()`), impedindo estados inválidos.

**Track como foco** — Artist e Genre existem como referências, simplificados intencionalmente. A complexidade está onde faz sentido: no ciclo de vida da Track.

**Draft → Publish** — criação permissiva. A validação completa acontece no Publish, que exige `AlbumId` e `GenreId`.

**PUT para update** — evita a ambiguidade entre campo ausente e campo nulo intencional, sem adicionar complexidade de implementação.

**Unit of Work** — centraliza o `CommitAsync` e isola os Use Cases do `DbContext` diretamente.

---

## Melhorias Planejadas

- **Reativação de tracks** — método `Reactivate()` na entidade e endpoint `PATCH /tracks/{id}/reactivate`
- **Testes automatizados** — criação de testes unitários para o domínio e use cases; integração para os endpoints
- **Autenticação e autorização** — Implementação da autenticação JWT
- **Containerização da API** — Dockerfile + docker-compose completo
- **CRUD de Artist e Genre** — implementar CRUD para gestão das entidades Artist e Genre
- **Result Pattern** — substituir exceções de fluxo nos Use Cases por resultados tipados, separando erros de negócio de erros inesperados do sistema. Exceções passam a representar apenas falhas reais, reduzindo custo computacional desnecessário em cenários esperados como validações e regras de negócio
- **Encapsulamento de queries nos repositórios** — mover queries complexas dos Use Cases para métodos específicos nos repositórios, centralizando a lógica de consulta e    restringindo o acesso direto ao IQueryable para evitar vazamento de lógica de dados entre camadas.

---

## Autor

Alan Alvarado — [GitHub](https://github.com/alvarado825)

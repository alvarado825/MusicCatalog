# Decisões de Arquitetura — MusicCatalog

Registro das principais decisões que fui tomando durante o projeto, o motivo delas existirem e qual problema cada uma tenta resolver.

---

## Clean Architecture

O projeto foi dividido em `Domain`, `Application`, `Infrastructure` e `Api`, cada camada com uma responsabilidade específica.

A ideia aqui foi evitar misturar regra de negócio com EF Core, HTTP ou detalhes de framework. O domínio fica isolado e não depende de nada externo, enquanto a infraestrutura apenas implementa o que a aplicação precisa.

Isso deixa o código mais previsível, mais fácil de manter e principalmente mais fácil de explicar tecnicamente.

---

## CQRS Leve

Separei leitura e escrita em `Commands` e `Queries`.

Os Commands trabalham com as entidades de domínio e aplicam regra de negócio. Já as Queries fazem projeção direta para DTO usando `Select`, trazendo só os dados necessários do banco.

Decidi não implementar CQRS completo com MediatR, handlers, bus, eventos e separação física de leitura/escrita porque o projeto não tinha complexidade suficiente para justificar isso.

A ideia foi usar apenas a parte do CQRS que realmente traz benefício sem adicionar arquitetura desnecessária.

---

## Commands retornam o estado atualizado

Os endpoints de escrita retornam o recurso atualizado na própria resposta.

A decisão aqui foi puramente pragmática: evitar que o cliente precise fazer uma segunda requisição logo após um create/update só para buscar o estado atualizado do objeto.

---

## Use Cases separados

Cada ação do sistema possui seu próprio Use Case.

O controller apenas recebe a requisição HTTP e delega a execução. Toda regra de negócio fica centralizada na camada de aplicação/domínio.

Também optei por usar classes concretas diretamente ao invés de MediatR porque, no contexto do projeto, adicionar mais indireção não resolveria nenhum problema real.

---

## Domínio Rico

As entidades não são apenas classes com getters e setters públicos.

O estado interno fica protegido e só pode ser alterado através de métodos que representam comportamento de negócio, como `Publish()` ou `Deactivate()`.

A ideia foi garantir consistência da entidade independentemente de quem esteja utilizando ela.

---

## Value Objects

Alguns conceitos do domínio viraram tipos explícitos ao invés de strings soltas, como `TrackName`.

Isso centraliza validação e normalização dentro do próprio tipo e evita espalhar regra de validação pelo sistema inteiro.

Além disso, deixa o domínio mais expressivo e mais próximo da linguagem de negócio.

---

## Track não possui navigation properties

desta forma é evitado lazy loading e referências cíclicas nas queries do catálogo. Album e Artist mantêm navegação entre si por conta da relação hierárquica direta usada na criação.

---

## Ciclo de vida da Track

A Track nasce em estado `Draft`.

Na criação, nem todos os dados são obrigatórios. A validação mais rígida acontece apenas no `Publish`, onde `AlbumId` e `GenreId` passam a ser obrigatórios.

A ideia foi separar claramente o momento de criação do momento de publicação no catálogo.

Também mantive `IsActive` separado de `TrackStatus` para permitir cenários futuros como desativação temporária e reativação sem alterar o status da música.

---

## Tratamento de erros

As exceções são separadas por categoria (`NotFoundException`, `BusinessRuleException`, etc.) e tratadas por um middleware global.

Isso evita try/catch espalhado pelos controllers e garante respostas HTTP padronizadas para toda a API.

Além disso, facilita manutenção e evolução das regras de erro.

---

## Decisões pragmáticas

### PUT ao invés de PATCH

Preferi usar PUT para evitar ambiguidade entre:
- campo não enviado
- campo enviado como null intencionalmente

Isso simplifica bastante a implementação.

---

### Unit of Work

O `UnitOfWork` centraliza o `CommitAsync()` e evita que os Use Cases dependam diretamente do `DbContext`.

A ideia foi manter a aplicação menos acoplada ao EF Core.

---

### Docker apenas no banco

Usei Docker somente para o banco de dados para simplificar o ambiente local durante o desenvolvimento.

Containerizar a API inteira ficou como evolução futura.

---

### Sem microserviços e sem complexidade desnecessária

O projeto foi pensado para ser simples, mas bem estruturado.

Evitei adicionar microserviços, mensageria, CQRS completo ou outras arquiteturas mais pesadas porque o domínio ainda não exigia isso.

A ideia sempre foi adicionar complexidade apenas quando existir um problema real que justifique ela.
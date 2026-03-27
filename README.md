# Futebol API ⚽

API RESTful desenvolvida em **.NET 10** para consulta e gestão de elencos de times de futebol, integrando dados reais de ligas com armazenamento local.

## Funcionalidades
- Consulta de times e elencos em tempo real via **API-Football**.
- Sincronização de jogadores da API externa para o banco de dados local.
- CRUD completo de jogadores salvos no banco.
- Paginação de resultados para otimização de performance.

## Tecnologias
- **Linguagem/Framework:** C# / .NET 10.0 (LTS)
- **ORM:** Entity Framework Core 10.0
- **Banco de Dados:** PostgreSQL (via Docker)
- **Documentação:** Swagger / OpenAPI
- **Integração Externa:** API-Football

## Pré-requisitos
- [.NET 10 SDK](https://dotnet.microsoft.com/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (banco de dados)
- Chave de acesso da [API-Sports](https://api-sports.io/)

## Como rodar o projeto localmente

**1. Clone o repositório:**
```bash
git clone [https://github.com/SEU_USUARIO/SEU_REPOSITORIO.git](https://github.com/SEU_USUARIO/SEU_REPOSITORIO.git)
cd SEU_REPOSITORIO

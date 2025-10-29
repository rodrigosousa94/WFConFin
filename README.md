# 💰 WFConFin — API de Controle Financeiro

API RESTful desenvolvida em **C# com ASP.NET Core**, projetada para gerenciar informações financeiras de forma **segura** e **escalável**.  
O sistema utiliza **JWT Bearer Authentication**, **criptografia de senha** e **Entity Framework Core** para integração com **PostgreSQL**.

---

## 🚀 Tecnologias Utilizadas

| Tecnologia | Descrição |
|-------------|------------|
| **C# / ASP.NET Core** | Framework principal da API |
| **Entity Framework Core (EF)** | ORM para manipulação do banco de dados |
| **PostgreSQL** | Banco de dados relacional |
| **JWT (Bearer Authentication)** | Autenticação e autorização baseada em tokens |
| **Criptografia de senha (Hash + Salt)** | Segurança dos dados de login |
| **Swagger / OpenAPI** | Documentação e testes de endpoints |

---

## 🧩 Estrutura do Projeto

WFConFin/
├── Controllers/
│ ├── PessoaController.cs
│ ├── UsuarioController.cs
│ ├── CidadeController.cs
│ ├── EstadoController.cs
│ └── ContaController.cs
├── Data/
│ └── WFConFinDbContext.cs
├── Models/
│ ├── Usuario.cs
│ ├── Cidade.cs
│ ├── Estado.cs
│ ├── PaginacaoResponse.cs
│ ├── Conta.cs
│ ├── Situacao.cs
│ ├── UsuarioLogin.cs
│ ├── UsuarioResponse.cs
│ └── Pessoa.cs
├── Program.cs
├── appsettings.json
└── WFConFin.csproj

yaml
Copiar código

---

## 🔐 Autenticação

A API utiliza **JWT Bearer Tokens** para autenticação e controle de acesso.

- O endpoint de **login** retorna um token JWT.  
- Cada requisição protegida requer o header:

```http
Authorization: Bearer <seu_token_aqui>
Funções com [Authorize(Roles = "Gerente,Empregado")] limitam o acesso conforme o perfil do usuário.

🧠 Controllers Principais
🏙️ CidadeController
Gerencia o cadastro e a consulta de Cidades.
Todos os endpoints são protegidos por autenticação JWT.

Método	Rota	Descrição	Permissão
GET	/api/cidade	Lista todas as cidades	🔒 Autenticado
GET	/api/cidade/{id}	Busca cidade pelo ID	🔒 Autenticado
GET	/api/cidade/pesquisa?valor=SP	Pesquisa por nome ou estado	🔒 Autenticado
GET	/api/cidade/paginacao?skip=1&take=10	Retorna lista paginada	🔒 Autenticado
POST	/api/cidade	Cria nova cidade	🔒 Gerente / Empregado
PUT	/api/cidade	Atualiza cidade existente	🔒 Gerente / Empregado
DELETE	/api/cidade/{id}	Exclui cidade	🔒 Gerente

Lógica Interna

Utiliza o EF Core para todas as operações CRUD.

Realiza paginação e busca dinâmica por nome ou sigla do estado.

Retorna mensagens claras de sucesso ou erro.

Implementa um objeto de resposta genérica PaginacaoResponse<T>.

🧍 UsuarioController
Gerencia usuários do sistema e autenticação.

Método	Rota	Descrição
POST	/api/usuario/register	Cadastra novo usuário com senha criptografada
POST	/api/usuario/login	Autentica usuário e retorna token JWT
GET	/api/usuario	Lista usuários (restrito a Gerente)

Segurança

Senhas são hashadas e salgadas antes de serem armazenadas.

Login gera token JWT com roles e tempo de expiração.

🌎 EstadoController
Controla as Unidades Federativas (UFs) relacionadas às cidades.

Método	Rota	Descrição
GET	/api/estado	Lista estados
GET	/api/estado/paginacao	Paginação de estados
POST	/api/estado	Cadastra estado
PUT	/api/estado	Atualiza estado
DELETE	/api/estado/{id}	Remove estado

🛠️ Como Executar o Projeto
🔧 Pré-requisitos
.NET 8 SDK

PostgreSQL

Ferramenta de testes de API: Postman, Insomnia ou Swagger

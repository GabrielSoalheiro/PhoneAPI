# 📞 Phone API

API REST para gerenciamento de contatos telefônicos, desenvolvida em **.NET 8** com **Clean Architecture**, **CQRS/MediatR**, **MongoDB Atlas**, **FluentValidation** e **Docker**.

---

## ✨ Funcionalidades

* CRUD completo de contatos (`/api/contacts`).
* Busca por termo (nome, email ou telefone).
* Validações robustas com FluentValidation.
* Separação de camadas (API / Application / Domain / Infrastructure).
* CQRS com Commands, Queries, Handlers via MediatR.
* Logs estruturados com Serilog.
* Swagger/OpenAPI automático.
* Health Check (`/health`).
* Testes unitários com xUnit, Moq e FluentAssertions.

---

## 🗂️ Estrutura de Pastas

```text
PhoneAPI/
│  Phone.sln
│  Dockerfile
│  docker-compose.yml
│
└─ src
   ├─ Phone.API/            # Interface HTTP (Controllers, Program)
   ├─ Phone.Application/    # Use-Cases (Commands, Queries, Handlers, DTOs, Validators)
   ├─ Phone.Domain/         # Entidades e Interfaces de Repositório
   └─ Phone.Infrastructure/ # MongoDB Context & Repositories
└─ tests
   ├─ Phone.Application.Tests/
   └─ Phone.Domain.Tests/
```

---

## 🚀 Como Executar Localmente

### 1. Clonar e restaurar pacotes
```bash
git clone <seu-repo>
cd PhoneDirectoryAPI
dotnet restore
```

### 2. Definir string de conexão Atlas
No `src/PhoneDirectory.API/appsettings.json`:
```json
"MongoDbSettings": {
  "ConnectionString": "mongodb+srv://<user>:<pwd>@cluster.mongodb.net",
  "DatabaseName": "PhoneDirectoryDB"
}
```

### 3. Rodar API
```bash
cd src/Phone.API
dotnet run
```

* Swagger: https://localhost:5001/swagger
* Health:  https://localhost:5001/health

---

## 🐳 Executar com Docker (API apenas)
```bash
# compilar imagem
docker build -t phone-directory-api .

# executar
Docker run -p 8080:80 \
  -e MongoDbSettings__ConnectionString="<ATLAS_URI>" \
  -e MongoDbSettings__DatabaseName="PhoneDirectoryDB" \
  phone-directory-api
```
Acesse http://localhost:8080/swagger

---

## 📦 Endpoints Principais
| Método | Rota | Descrição |
|--------|------|-----------|
| GET    | /api/contacts | Lista todos os contatos |
| GET    | /api/contacts/{id} | Recupera contato por ID |
| GET    | /api/contacts/search?term=foo | Busca por termo |
| POST   | /api/contacts | Cria novo contato |
| PUT    | /api/contacts/{id} | Atualiza contato |
| DELETE | /api/contacts/{id} | Remove contato |

### Esquema JSON de Criação
```json
{
  "nome": "João Silva",
  "telefone": "(11) 99999-9999",
  "email": "joao@email.com",
  "dataNascimento": "1990-01-01",
  "enderecos": [
    {
      "logradouro": "Rua A, 100",
      "numero": "10",
      "bairro": "Centro",
      "cidade": "São Paulo",
      "estado": "SP",
      "cep": "01000-000"
    }
  ]
}
```

---

## 🧪 Testes

```bash
# executar todos
dotnet test tests
```

Estrutura de testes cobre Handlers, Validators e Entidades.

---

## 🛠️ Tecnologias
* .NET 8 / ASP.NET Core
* MongoDB Driver 2.24
* MediatR 12
* FluentValidation 11
* Serilog
* Docker
* xUnit • Moq • FluentAssertions

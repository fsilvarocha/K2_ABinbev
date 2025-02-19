# AB inbev Developer Evaluation | K2

## 📌 Visão Geral
Este projeto é uma API desenvolvida para avaliação técnica da K2 Consultoria para AB inbev, utilizando **.NET 8**, **PostgreSQL**, **MongoDB** e **Redis**.

## 🛠️ Tecnologias Utilizadas
- **.NET 8**
- **Entity Framework Core**
- **PostgreSQL**
- **MongoDB**
- **Redis**
- **Docker e Docker Compose**
- **Swagger**

## 🚀 Configuração do Ambiente
### 1️⃣ **Pré-requisitos**
Antes de começar, certifique-se de ter instalado:
- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)
- [.NET SDK 8.0+](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)

### 2️⃣ **Clonar o Repositório**
```sh
git clone https://github.com/seu-usuario/seu-repositorio.git
cd seu-repositorio
```

### 3️⃣ **Configurar o Banco de Dados**
O banco de dados roda em um container Docker. Para subir os serviços necessários:
```sh
docker-compose up -d
```
Isso iniciará **PostgreSQL, MongoDB e Redis**.

### 4️⃣ **Aplicar Migrations no PostgreSQL**
```sh
cd template/backend/src/Ambev.DeveloperEvaluation.WebApi

dotnet ef database update
```
Caso o comando `dotnet ef` não funcione, instale a ferramenta:
```sh
dotnet tool install --global dotnet-ef
```

### 5️⃣ **Rodar a Aplicação**
Agora, podemos iniciar o servidor:
```sh
dotnet run
```
Se tudo estiver correto, a API estará rodando em:
🔗 **http://localhost:5119/swagger**

---

## 📢 Testando os Endpoints
A API conta com uma interface **Swagger** disponível em:
🔗 **[http://localhost:5119/swagger](http://localhost:5119/swagger)**

### 🏷️ Criar um Usuário
```
POST /api/Users
```
```json
{
  "name": "Fabricio Silva",
  "email": "fabricio.silva@gmail.com",
  "password": "senha123"
}
```

### 🔑 Autenticação
```
POST /api/Auth
```
```json
{
  "email": "fabricio.silva@gmail.com",
  "password": "senha123"
}
```
O retorno será um `token JWT`.

### 🛒 Criar uma Venda
```
POST /api/Sales
```
```json
{
  "saleNumber": "123456",
  "saleDate": "2025-02-13T14:30:00",
  "customer": "Fabricio Silva",
  "branch": "Minas Gerais",
  "items": [
    {
      "productName": "Cerveja Malte",
      "quantity": 4,
      "unitPrice": 6.30
    }
  ]
}
```

### 📊 Listar Vendas
```
GET /api/Sales
```

### 🔎 Buscar Venda Específica
```
GET /api/Sales/{id}
```

### ❌ Excluir Venda
```
DELETE /api/Sales/{id}
```

---

## 📌 **Parando e Removendo os Containers**
Caso precise parar os serviços:
```sh
docker-compose down
```
Para limpar os volumes do banco de dados:
```sh
docker-compose down -v
```

---

## 🎯 Conclusão
Se todos os endpoints responderem corretamente, significa que a API está funcional! Caso tenha dúvidas ou precise de ajustes, me avise. 🚀🔥

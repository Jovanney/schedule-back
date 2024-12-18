# **ScheduleApp API**

Bem-vindo ao **ScheduleApp API**, um projeto para gerenciar contatos, permitindo a criação, consulta, atualização e exclusão de contatos. Este documento serve como um guia para configurar, executar e utilizar os recursos da aplicação, incluindo a documentação automática gerada pelo Swagger e Scalar.

---

## **Pré-requisitos**

Antes de iniciar, você precisará das seguintes ferramentas instaladas:

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/sql-server)
- [Docker (opcional)](https://www.docker.com)
- Um editor de código como o [Visual Studio Code](https://code.visualstudio.com) ou [Visual Studio](https://visualstudio.microsoft.com)

---

## **Configuração**

### 1. **Clonar o Repositório**

```bash
git clone https://github.com/seu-usuario/schedule-app.git
cd schedule-app
```

### 2. **Configurar o Banco de Dados**

O projeto utiliza um banco de dados SQL Server. Certifique-se de ter o SQL Server rodando localmente ou configure um contêiner Docker.

#### Configuração no arquivo `.env`:

Crie um arquivo `.env` na raiz do projeto e adicione suas configurações do banco:

```
DB_SERVER=localhost
DB_NAME=ScheduleAppDb
DB_USER=sa
DB_PASSWORD=SuaSenhaSegura
```

### 3. **Executar a Aplicação**

Rode os seguintes comandos para restaurar as dependências e iniciar a aplicação:

```bash
dotnet restore
dotnet run --project ScheduleApp.API
```

A API estará disponível em:  
**`http://localhost:5032`**

---

## **Usando o Swagger e Scalar**

A documentação da API é gerada automaticamente e pode ser acessada nos seguintes endpoints:

### 1. **Swagger (OpenAPI)**

Acesse a documentação Swagger para explorar os endpoints da API de forma interativa.

URL:  
**`http://localhost:5032/openapi`**

#### Recursos no Swagger:

- Lista de todos os endpoints disponíveis.
- Detalhes de parâmetros e respostas para cada endpoint.
- Funcionalidade para testar requisições diretamente no navegador.

### 2. **Scalar API Reference**

Um visualizador simplificado para os endpoints da API, organizado para facilitar a navegação.

URL:  
**`http://localhost:5032/scalar/v1`**

#### Recursos no Scalar:

- Interface minimalista para visualizar a estrutura da API.
- Visualização rápida de rotas e métodos HTTP.

---

## **Endpoints Principais**

### **ContactsController**

Rota base: `/api/contacts`

| Método | Rota    | Descrição                                       |
| ------ | ------- | ----------------------------------------------- |
| GET    | `/`     | Retorna todos os contatos (com opção de busca). |
| GET    | `/{id}` | Retorna um contato específico pelo ID.          |
| POST   | `/`     | Cria um novo contato.                           |
| PUT    | `/{id}` | Atualiza um contato existente.                  |
| DELETE | `/{id}` | Remove um contato existente.                    |

---

## **Testando a API**

### **Usando o Swagger**

1. Acesse **`http://localhost:5032/openapi`** no navegador.
2. Escolha um endpoint da lista.
3. Clique em **"Try it out"** para enviar requisições diretamente da interface do Swagger.

### **Usando o Postman ou Insomnia**

1. Crie uma nova requisição no Postman.
2. Configure a URL da requisição com base nos endpoints fornecidos.
3. Adicione os parâmetros necessários (corpo, query ou cabeçalhos) conforme descrito no Swagger.

### **Exemplo de Requisição**

**Endpoint:**  
`POST /api/contacts`

**Corpo (JSON):**

```json
{
  "name": "John Doe",
  "email": "johndoe@example.com",
  "phoneNumber": "123456789"
}
```

**Resposta Esperada:**

```json
{
  "id": "f4a1e8d5-d923-4c6a-bf2a-9e30a1bc728f",
  "name": "John Doe",
  "email": "johndoe@example.com",
  "phoneNumber": "123456789"
}
```

---

## **Contribuindo**

Se quiser contribuir com o projeto:

1. Faça um fork do repositório.
2. Crie uma branch para sua feature ou correção:  
   `git checkout -b minha-feature`
3. Envie um pull request quando estiver pronto.

---

## **Licença**

Este projeto é licenciado sob a [MIT License](LICENSE).

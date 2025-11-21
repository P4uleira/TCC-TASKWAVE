# 🌀 **TaskWave – Sistema de Gestão de Tarefas e Ambientes**

## 📌 **Descrição Geral**

**TaskWave** é um sistema desenvolvido para o Trabalho de Conclusão de Curso, com o objetivo de criar uma plataforma simples e eficiente para **gestão de tarefas**, **organização de ambientes** e **controle de acessos**, utilizando tecnologias modernas do ecossistema .NET.

O projeto foi construído seguindo boas práticas de arquitetura, autenticação robusta via **JWT** e um modelo claro de camadas para garantir organização, escalabilidade e fácil manutenção.

## 📌 Documentação do projeto: 

[TGII-TASKWAVE .pdf](https://github.com/user-attachments/files/23676798/TGII-TASKWAVE.pdf)

---

## 🎯 **Objetivo do Sistema**

O TaskWave permite que usuários gerenciem:

* **Ambientes**
* **Tarefas**
* **Usuários com níveis de acesso**
* **Autenticação e autorização via JWT**

O foco é oferecer uma API enxuta, organizada e pronta para ser integrada com aplicações front-end ou mobile.

---

## 🏛️ **Arquitetura da Aplicação**

A solução segue uma divisão em camadas seguindo princípios de **DDD simplificado**:

```
/API        → Contém as APIs, Requests, Responses e configuração de Swagger.
/Domain     → Contém entidades, interfaces, serviços e regras de negócio.
/Infra      → Contém o DbContext, mapeamentos (TypeConfiguration), repositórios e migrations.
```

Além disso, o projeto utiliza:

* **APIs Rest no .NET 8**
* **Repository Pattern**
* **DTOs (Requests e Responses)**
* **Service Layer**
* **EF Core 8 com Fluent API**

---

## 🔐 **Autenticação e Autorização (JWT)**

O sistema implementa autenticação baseada em JWT contendo:

* ID do usuário
* Acessos vinculados
* Permissões do usuário
* Data de expiração do token

A estrutura utiliza as entidades:

* **Usuário**
* **Acesso**
* **UsuarioAcesso** (tabela relacional)

Esses acessos são convertidos em **Claims** durante a geração do token.

---

## 📦 **Funcionalidades da Plataforma**

### 👤 **Usuários**

* Cadastro de usuário
* Edição de perfil
* Vinculação de acessos
* Login e obtenção de JWT
* Consulta por ID

---

### 🧩 **Ambientes**

O sistema permite:

* Criar ambiente
* Editar ambiente
* Listar ambientes
* Consultar ambiente por ID
* Excluir ambiente

Com regras aplicadas via `AmbienteService`.

---

### 📋 **Tarefas**

Cada ambiente pode conter tarefas, com:

* Criação
* Edição
* Exclusão
* Visualização
* Status (pendente/concluída)

---

### 🧪 **Swagger**

O projeto possui documentação completa no **Swagger**, permitindo testar os endpoints diretamente na interface gráfica.

Endereços usuais:

```
https://localhost:<porta>/swagger
```

---

## 🧰 **Tecnologias Utilizadas**

| Tecnologia                         | Uso                                |
| ---------------------------------- | ---------------------------------- |
| **.NET 8**                         | Base da aplicação                  |
| **C#**                             | Linguagem principal                |
| **APIs**                           | Estruturação dos endpoints         |
| **Entity Framework Core 8**        | Acesso ao banco e migrations       |
| **SQL Server**                     | Banco de dados                     |
| **JWT (JSON Web Token)**           | Autenticação                       |
| **Swagger / Swashbuckle**          | Documentação interativa            |
| **AutoMapper**                     | Conversão entre Request e Response |
| **TypeConfiguration**              | Mapeamento de entidades            |

---

## 🗂️ **Estrutura do Banco de Dados**

Principais entidades:

* **Usuario**
* **Acesso**
* **UsuarioAcesso**
* **Ambiente**
* **Tarefa**

Com relações:

* 1 Usuário → N Acessos
* 1 Ambiente → N Tarefas

---

## ▶️ **Como Executar o Projeto**

### 1️⃣ Clone o repositório

```bash
git clone https://github.com/P4uleira/TCC-TASKWAVE
```

### 2️⃣ Configure o `appsettings.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=...;Database=TaskWave;Trusted_Connection=True;"
}
```

### 3️⃣ Execute as migrations

```bash
dotnet ef database update
```

### 4️⃣ Execute a aplicação

```bash
dotnet run
```

### 5️⃣ Acesse o Swagger

```
https://localhost:<porta>/swagger
```

---

## 📄 **Exemplo de Endpoints**

* **POST /auth/login**
* **GET /ambientes**
* **POST /ambientes**
* **PUT /ambientes/{id}**
* **DELETE /ambientes/{id}**
* **GET /tarefas/ambiente/{id}**

Todos usam Responses estruturados e Requests tipados.

---

## 🧹 **Padrões e Boas Práticas Adotadas**

* Clean Code
* Padrão Repository
* Domain-Driven Design (simplificado)
* Separação clara entre camadas
* DTOs para comunicação entre API e Domain
* Autenticação padronizada
* Uso correto de Status Codes HTTP
* Swagger documentado

---

## 📌 **Status do Projeto**

🟢 **Projeto concluído para TCC**
🔧 Possíveis melhorias futuras:

* Dashboard com estatísticas
* Logs via Serilog
* Controle refinado de permissões
* Deploy em nuvem (Azure / AWS)

---

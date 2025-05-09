# 📋 API de Gerenciamento de Tarefas

Esta API simula o gerenciamento de tarefas em um sistema de organização pessoal ou em equipe. Ela permite **criar, consultar, atualizar e excluir tarefas**, seguindo os princípios REST. É útil, por exemplo, como base para uma aplicação do tipo Trello ou Todoist.

---

## 👥 Integrantes da Dupla

- Giovanny dos Santos - [Giovanny1603](https://github.com/Giovanny1603)

---

## 🛠️ Tecnologias Utilizadas

- **Linguagem:** C# (.NET 8)
- **Framework:** ASP.NET Core
- **ORM:** Entity Framework Core
- **Banco de Dados:** MySQL
- **Front-end:** JavaScript (caso aplicável)
- **Versionamento:** Git + GitHub

---

🚀 Como Executar o Projeto
✅ Pré-requisitos

- .NET SDK 8.0+
- MySQL
- Git

---

⚙️ Passos
Copiar código
# 1. Clone o repositório
git clone https://github.com/Giovanny1603/TarefasAPI

# 2. Acesse a pasta do projeto
cd. C:\Users\Giovanny\ProjetoTarefas> 

# 3. Restaure os pacotes
dotnet restore

# 4. Execute as migrações (opcional)
dotnet ef database update

# 5. Execute a aplicação
dotnet run

---

🌐 Acessando o Swagger
Após rodar o projeto, acesse:

http://localhost:5039/swagger

Aqui você poderá testar todos os endpoints da API.

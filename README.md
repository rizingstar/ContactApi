# ContactApi

A lightweight, production-ready **serverless REST API** built with **.NET 8 Isolated Azure Functions** and **Azure SQL Database** for persistent contact management.  
Includes modern **OpenAPI/Swagger UI** for instant testing and exploration.

---

## 🚀 Live Demo

- **API Endpoint:** [https://contactapi-nk.azurewebsites.net/api/contacts](https://contactapi-nk.azurewebsites.net/api/contacts)
- **Swagger Docs:** [https://contactapi-nk.azurewebsites.net/api/swagger/index.html](https://contactapi-nk.azurewebsites.net/api/swagger/index.html)

---

## 🧑‍💻 Features

- **.NET 8 Azure Functions (Isolated Process):** Modern DI, scalable, and serverless
- **Azure SQL Database integration:** Persistent, reliable storage
- **REST API endpoints:** GET, POST (full CRUD support coming soon)
- **OpenAPI/Swagger UI:** For easy testing and documentation
- **Azure-ready:** One-click deployment with GitHub Actions CI/CD
- **Clean codebase:** Easy to extend for any business need

---

## 🔌 API Endpoints

| Method | Route           | Description         |
|--------|-----------------|---------------------|
| GET    | `/api/contacts` | List all contacts   |
| POST   | `/api/contacts` | Add new contact     |

### Example: POST `/api/contacts` Request Body

```json
{
  "name": "Ada Lovelace",
  "email": "ada@example.com"
}

## 🏃‍♂️ Run Locally

### **Prerequisites**
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Azure Functions Core Tools](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=windows%2Ccsharp%2Cbash)
- SQL Server ([Local SQL Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) or Azure SQL Database)
- Git

---

### 1. Clone the Repo

```sh
git clone https://github.com/rizingstar/ContactApi.git
cd ContactApi

### 2. Create Local Settings

In the project root, create a file named `local.settings.json`:

```json
{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "SQL_CONNECTION_STRING": "Server=localhost;Database=ContactDb;User Id=sa;Password=yourStrong(!)Password;",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet"
  }
}

### 3. Set Up the Database

Create the `Contacts` table in your SQL database:

```sql
CREATE TABLE Contacts (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL
);

### 4. Build & Run the API

```sh
dotnet build
func start

### 5. Open Swagger UI

Open your browser and go to:  
[http://localhost:7071/api/swagger/index.html](http://localhost:7071/api/swagger/index.html)

You can now interact with your local Contact API!

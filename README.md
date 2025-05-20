# ContactApi

A lightweight, production-ready serverless REST API built with **.NET 8 Isolated Azure Functions**, fully integrated with **Azure SQL Database** for persistent contact management.  
Includes modern OpenAPI/Swagger UI for instant testing and exploration.

---

## 🚀 Live Demo

- **API Endpoint:** [https://contactapi-nk.azurewebsites.net/api/contacts](#)  
- **Swagger Docs:** [https://contactapi-nk.azurewebsites.net/api/swagger/index.html](#)  
*(Replace with your actual Azure links after deployment!)*

---

## 🧑‍💻 Features

- .NET 8 Azure Functions (Isolated Process: modern DI, scalable, serverless)
- Azure SQL Database integration (persistent, reliable storage)
- REST API endpoints: GET, POST (full CRUD support coming soon)
- OpenAPI/Swagger UI out-of-the-box for easy testing and documentation
- Ready for one-click deployment to Azure with GitHub Actions CI/CD
- Clean code structure, easy to extend for any business need

---

## 🔌 API Endpoints

| Method | Route             | Description         |
|--------|-------------------|---------------------|
| GET    | `/api/contacts`   | List all contacts   |
| POST   | `/api/contacts`   | Add new contact     |

### **POST /api/contacts** – Example Body

```json
{
  "name": "Ada Lovelace",
  "email": "ada@example.com"
}

## 🏃‍♂️ Run Locally

**Prerequisites:**
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [Azure Functions Core Tools](https://learn.microsoft.com/en-us/azure/azure-functions/functions-run-local?tabs=windows%2Ccsharp%2Cbash)
- SQL Server (can be [local SQL Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads), or Azure SQL Database)
- Git

**1. Clone the repo:**
```sh
git clone https://github.com/rizingstar/ContactApi.git
cd ContactApi


2. Create local settings file:

In the project root, create a file named local.settings.json with the following content (replace the SQL connection string as needed):

{
  "IsEncrypted": false,
  "Values": {
    "AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "SQL_CONNECTION_STRING": "Server=localhost;Database=ContactDb;User Id=sa;Password=yourStrong(!)Password;",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet"
  }
}

3. Set up the database:

Create the Contacts table in your SQL database (run this script in SQL Server Management Studio or Azure Data Studio):

CREATE TABLE Contacts (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(255) NOT NULL
);

4. Build and run the API:

dotnet build
func start
5. Open the Swagger UI:

In your browser, go to http://localhost:7071/api/swagger/index.html

You can now interact with your local Contact API!


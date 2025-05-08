# ContactApi

A lightweight, serverless REST API built with **.NET 8 isolated Azure Functions**, integrated with **Azure SQL Database** for persistent contact storage.  
Includes OpenAPI/Swagger UI for easy testing.

---

## 🚀 Features

- ✅ **.NET 8 isolated worker model** (future-proof, clean DI)
- ✅ **Azure SQL Database** integration via `Microsoft.Data.SqlClient`
- ✅ **HTTP-triggered endpoints** (`GET`, `POST`)
- ✅ **OpenAPI / Swagger UI** for testing
- ✅ Ready for **Azure deployment** and GitHub Actions CI/CD

---

## 🧪 Endpoints

| Method | Route            | Description              |
|--------|------------------|--------------------------|
| GET    | `/api/contacts`  | List all contacts        |
| POST   | `/api/contacts`  | Add a new contact (JSON) |

### POST Body Example

```json
{
  "name": "Ada Lovelace",
  "email": "ada@example.com"
}

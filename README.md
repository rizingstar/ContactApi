# ContactApi

A lightweight, production-ready serverless REST API built with **.NET 8 Isolated Azure Functions**, fully integrated with **Azure SQL Database** for persistent contact management.  
Includes modern OpenAPI/Swagger UI for instant testing and exploration.

---

## 🚀 Live Demo

- **API Endpoint:** [https://YOUR-APP.azurewebsites.net/api/contacts](#)  
- **Swagger Docs:** [https://YOUR-APP.azurewebsites.net/api/swagger/index.html](#)  
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

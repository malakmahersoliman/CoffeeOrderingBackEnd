# Coffee Ordering System API

A RESTful backend API designed for managing customers, coffee menu items, order items, and coffee orders. Built with ASP.NET Core Web API, Entity Framework Core, and SQL Server following clean architecture, separation of concerns, and best practices.

---

## Technical Stack
* **Framework**: ASP.NET Core 10 (Web API)
* **Database Access**: Entity Framework Core 10 (Code-First)
* **Database**: Microsoft SQL Server
* **Documentation & Testing**: Swagger UI (Swashbuckle)
* **Design Patterns**: Service-Repository pattern (Controller → Service → DbContext), DTO projection, Dependency Injection, Fluent Data Configurations.

---

## Key Features
- **Customers Management**: Full CRUD operations for customer records, including validations.
- **Menu Items Management**: Complete CRUD operations for coffee/beverage menu items, category classification, and availability state toggles.
- **Coffee Orders Processing**: Handles ordering workflows. Calculates unit prices at the time of order placing, validates client & stock/menu availability, and persists ordered items.
- **Relational Integrity**: Uses EF Core configurations to establish strong 1-to-Many and Many-to-1 relationships between entities.
- **Validation Rules**: Standard DTO validations (via Data Annotations) combined with manual business logic validations in the Service layer to ensure data sanitization.

---

## Architecture

This project implements a clean separation of concerns:
```
Controller (HTTP Request Handlers)
      ↓
Services (Business Logic Layer)
      ↓
DbContext (Data Access Layer via EF Core)
      ↓
SQL Server Database
```

* **Domain (Entities)**: Focuses entirely on the model definition.
* **Data (DbContext & Configurations)**: Decoupled Entity Configurations (`IEntityTypeConfiguration`) using EF Fluent API for clean and maintainable mappings.
* **Services**: Encapsulates all query projections, business checks, calculations, and exception handling.
* **DTOs**: Data Transfer Objects ensure that domain models are never directly exposed to the API consumer.

---

## Entity Relationships

```
Customer (1) <───────> (M) CoffeeOrder
                             │
                             └── (1) <───────> (M) OrderItem (M) ───────> (1) MenuItem
```

---

## API Endpoints

### Customers (`/api/Customers`)
* `GET /api/Customers` - Retrieves all customers.
* `GET /api/Customers/{id}` - Retrieves a specific customer by ID.
* `POST /api/Customers` - Creates a new customer.
* `PUT /api/Customers/{id}` - Updates customer details.
* `DELETE /api/Customers/{id}` - Deletes a customer.

### Menu Items (`/api/MenuItems`)
* `GET /api/MenuItems` - Retrieves all menu items.
* `GET /api/MenuItems/{id}` - Retrieves a specific menu item by ID.
* `POST /api/MenuItems` - Creates a new menu item.
* `PUT /api/MenuItems/{id}` - Updates a menu item.
* `DELETE /api/MenuItems/{id}` - Deletes a menu item.

### Coffee Orders (`/api/CoffeeOrders`)
* `GET /api/CoffeeOrders` - Retrieves all coffee orders, including customer details and ordered items with totals.
* `GET /api/CoffeeOrders/{id}` - Retrieves a specific coffee order by ID.
* `POST /api/CoffeeOrders` - Places a new coffee order (automates price calculations based on current menu item prices and validates item availability).
* `DELETE /api/CoffeeOrders/{id}` - Cancels/deletes a coffee order.

---

## How to Run Locally

### 1. Prerequisites
* [.NET 10 SDK](https://dotnet.microsoft.com/download)
* [SQL Server (LocalDB or Express)](https://www.microsoft.com/sql-server/)
* IDE like Visual Studio 2022 / JetBrains Rider or VS Code

### 2. Configure connection string
Open `appsettings.json` and adjust the `DefaultConnection` string if needed to match your SQL Server instance:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=CoffeeOrderingDb;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

### 3. Run migrations and database update
Apply the database migrations to create tables and relationships:
```bash
dotnet ef database update
```

### 4. Launch the API
Build and run the project:
```bash
dotnet run
```
Once started, the Swagger UI will be available at:
`https://localhost:7198/swagger/index.html` (or matching port printed in your console).

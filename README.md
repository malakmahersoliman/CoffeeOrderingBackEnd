# Coffee Ordering System API (CQRS & MediatR)

A RESTful backend API designed for managing customers, coffee menu items, order items, and coffee orders. Built with ASP.NET Core Web API, Entity Framework Core, SQL Server, and fully refactored to implement the CQRS (Command Query Responsibility Segregation) pattern with MediatR for clean separation of concerns and maintainability.

---

## Technical Stack
* **Framework**: ASP.NET Core 10 (Web API)
* **MediatR**: MediatR 14 for decoupled in-process messaging
* **Database Access**: Entity Framework Core 10 (Code-First)
* **Database**: Microsoft SQL Server
* **Documentation & Testing**: Swagger UI (Swashbuckle)
* **Design Patterns**: CQRS Pattern (Command Query Responsibility Segregation), Mediator Pattern (Controller → Mediator → Handler → DbContext), DTO Projection, Fluent Data Configurations.

---

## Key Features
- **Customers Management (CQRS)**: Dedicated commands (Create, Update, Delete) and queries (Get All, Get by ID) for customer records, including validation.
- **Menu Items Management (CQRS)**: Dedicated commands and queries for coffee/beverage menu items, category classification, and availability state toggles.
- **Coffee Orders Processing (CQRS)**: Handles ordering workflows under CQRS handlers. Validates customer existence and item availability, automates unit price calculations at the time of order placement, and persists order details.
- **Relational Integrity**: Uses EF Core configurations to establish strong 1-to-Many and Many-to-1 relationships between entities.
- **Validation Rules**: Standard DTO validations (via Data Annotations) combined with domain business validations inside the command handlers.

---

## Architecture

This project implements a clean separation of concerns via CQRS and MediatR:
```
           Controller (HTTP Request Handlers)
                         ↓
           IMediator (In-process Dispatcher)
                         ↓
  ┌──────────────────────┴──────────────────────┐
  ▼                                             ▼
Queries (Read)                               Commands (Write)
  └──────────────┬──────────────────────────────┘
                 ▼
          MediatR Handlers
                 ↓
      DbContext (EF Core Access)
                 ↓
        SQL Server Database
```

* **Domain (Entities)**: Focuses entirely on the model definition.
* **Data (DbContext & Configurations)**: Decoupled Entity Configurations (`IEntityTypeConfiguration`) using EF Fluent API for clean and maintainable mappings.
* **Features (CQRS Command/Queries & Handlers)**: Organized under the `Features/` directory by domain area (`Customers`, `MenuItems`, `CoffeeOrders`), containing isolated commands, queries, and their handlers.
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
* `PUT /api/CoffeeOrders/{id}` - Updates order status and take-away flag.
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
dotnet run --project CoffeeOrderingApi
```
Once started, the Swagger UI will be available at:
`https://localhost:7245/swagger/index.html` or `http://localhost:5074/swagger/index.html` (see the URL printed in your console).

### 5. Run tests
```bash
dotnet test
```


# E-commerce Management System

A modern, scalable e-commerce management system built with ASP.NET Core following Clean Architecture principles and Domain-Driven Design (DDD) patterns.

## 🏗️ Architecture Overview

This project implements Clean Architecture with the following layers:

### Core Layer
- **Domain Layer**: Contains business entities, value objects, and domain rules
- **Service Abstraction**: Defines service contracts and interfaces
- **Service Implementation**: Contains business logic implementation

### Infrastructure Layer
- **Presentation Layer**: API controllers and presentation logic
- **Persistence Layer**: Data access implementation with Entity Framework Core

### Presentation Layer
- **E-Commerce.Web**: Main Web API application

### Shared Layer
- **Shared**: Common DTOs, error models, and shared utilities

## 🚀 Features

- **Product Management**: CRUD operations for products with advanced filtering and sorting
- **User Authentication**: Secure authentication and authorization system
- **Shopping Basket**: Complete shopping cart functionality
- **Order Management**: Order processing and tracking
- **Payment Processing**: Integrated payment system
- **Caching**: Redis caching for improved performance
- **Exception Handling**: Global exception handling middleware
- **API Response Factory**: Standardized API responses
- **Pagination**: Efficient data pagination
- **Specifications Pattern**: Flexible query building

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core
- **Database**: Entity Framework Core
- **Authentication**: ASP.NET Core Identity
- **Caching**: Redis
- **Architecture**: Clean Architecture
- **Patterns**: Repository Pattern, Specification Pattern, CQRS
- **API Documentation**: Swagger/OpenAPI

## 📁 Project Structure

```
├── Core/
│   ├── DomainLayer/           # Domain entities and business rules
│   ├── Service-Abstraction/   # Service interfaces
│   └── ServiceImplementation/ # Business logic implementation
├── Infrastructure/
│   ├── Presentation-Layer/    # API controllers
│   └── Presistence-Layer/     # Data access layer
├── E-Commerce.Web/            # Main Web API project
└── Shared/                    # Shared models and utilities
```

## 🚦 Getting Started

### Prerequisites

- .NET 8.0 SDK or later
- SQL Server (LocalDB or full instance)
- Redis (for caching)
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/MohamedSaber2004/E-commerce-Management-System.git
   cd E-commerce-Management-System
   ```

2. **Restore dependencies**
   ```bash
   dotnet restore
   ```

3. **Update connection strings**
   - Navigate to `E-Commerce.Web/appsettings.json`
   - Update the database connection string
   - Configure Redis connection if using external Redis instance

4. **Run database migrations**
   ```bash
   dotnet ef database update --project Infrastructure/Presistence-Layer --startup-project E-Commerce.Web
   ```

5. **Run the application**
   ```bash
   dotnet run --project E-Commerce.Web
   ```

The API will be available at `https://localhost:7xxx` and `http://localhost:5xxx`

## 📖 API Documentation

Once the application is running, you can access the Swagger documentation at:
- `https://localhost:7xxx/swagger`

## 🏛️ Design Patterns

- **Repository Pattern**: Abstraction for data access
- **Specification Pattern**: Flexible query composition
- **Factory Pattern**: API response creation
- **Dependency Injection**: Loose coupling between components
- **SOLID Principles**: Clean, maintainable code structure

## 🔧 Configuration

Key configuration sections in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ECommerceDB;Trusted_Connection=true"
  },
  "Redis": {
    "ConnectionString": "localhost:6379"
  }
}
```

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add some amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 👨‍💻 Author

**Mohamed Saber**
- GitHub: [@MohamedSaber2004](https://github.com/MohamedSaber2004)

## 🙏 Acknowledgments

- Clean Architecture by Robert C. Martin
- ASP.NET Core team for the excellent framework
- Entity Framework Core team
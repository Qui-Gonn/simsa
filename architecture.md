# Tkd.Simsa - Codebase Architecture Documentation

## Overview

Tkd.Simsa is a (ITF) Taekwon-Do management system built using .NET 9 with a clean architecture approach. The system
manages persons and events within a martial arts context, featuring a modern Blazor-based web application with both
server-side and WebAssembly components.

## File & Folder Structure

```
Tkd.Simsa/
├── LICENSE
├── README.md
├── Tkd.Simsa.db                           # SQLite database file
├── src/                                   # Main source code
│   ├── Directory.Build.props              # Global MSBuild properties
│   ├── stylecop.ruleset                   # Code style rules
│   ├── Tkd.Simsa.sln                      # Visual Studio Solution
│   │
│   ├── Tkd.Simsa.Domain/                  # 🏗️ Domain Layer
│   │   ├── Common/                        # Shared domain concepts
│   │   ├── PersonManagement/              # Person domain models
│   │   │   ├── Person.cs                  # Main Person aggregate
│   │   │   ├── PersonInfo.cs              # Person information value object
│   │   │   ├── PersonName.cs              # Name value object
│   │   │   ├── BirthDate.cs               # Birth date value object
│   │   │   └── Gender.cs                  # Gender enumeration
│   │   └── EventManagement/               # Event domain models
│   │       ├── Event.cs                   # Main Event aggregate
│   │       ├── Participant.cs             # Event participation
│   │       └── ParticipationData.cs       # Participation details
│   │
│   ├── Tkd.Simsa.Application/             # 🎯 Application Layer
│   │   ├── Common/                        # Shared application contracts
│   │   │   ├── IGenericRepository.cs      # Generic repository interface
│   │   │   ├── IGenericItemService.cs     # Generic service interface
│   │   │   ├── Requests.cs                # Command/Query definitions
│   │   │   ├── TransactionMode.cs         # Transaction management
│   │   │   └── Filtering/                 # Query parameter handling
│   │   ├── PersonManagement/
│   │   │   └── IPersonRepository.cs       # Person-specific repository
│   │   ├── EventManagement/
│   │   │   └── IEventRepository.cs        # Event-specific repository
│   │   └── Extensions/
│   │       └── ExpressionExtensions.cs    # LINQ expression utilities
│   │
│   ├── Tkd.Simsa.Persistence/             # 🗃️ Infrastructure/Data Layer
│   │   ├── SimsaDbContext.cs              # Entity Framework DbContext
│   │   ├── DbContextInitializer.cs        # Database initialization
│   │   ├── Entities/                      # Database entities
│   │   │   ├── PersonEntity.cs            # Person database model
│   │   │   └── EventEntity.cs             # Event database model
│   │   ├── Configurations/                # EF Core configurations
│   │   │   ├── PersonConfiguration.cs     # Person entity config
│   │   │   ├── EventConfiguration.cs      # Event entity config
│   │   │   └── ConfigurationConstants.cs  # Shared constants
│   │   ├── Repositories/                  # Repository implementations
│   │   │   ├── GenericRepository.cs       # Base generic repository
│   │   │   ├── PersonRepository.cs        # Person repository
│   │   │   └── EventRepository.cs         # Event repository
│   │   ├── Mapper/                        # Entity-Domain mapping
│   │   ├── Extensions/                    # Query extensions
│   │   └── Filtering/                     # Database filtering logic
│   │
│   ├── Tkd.Simsa.Blazor.WebApp/           # 🌐 Server-Side Web Application
│   │   ├── Program.cs                     # Application entry point
│   │   ├── Components/                    # Blazor components
│   │   ├── Features/                      # Feature-organized code
│   │   │   ├── EventManagement/           # Event-related features
│   │   │   └── RequestHandler/            # MediatR request handlers
│   │   │       ├── AddItemHandler.cs      # Add item command handler
│   │   │       ├── GetItemsHandler.cs     # Get items query handler
│   │   │       ├── GetItemByIdHandler.cs  # Get by ID query handler
│   │   │       ├── UpdateItemHandler.cs   # Update command handler
│   │   │       └── DeleteItemHandler.cs   # Delete command handler
│   │   ├── Endpoints/                     # API endpoints
│   │   └── Extensions/                    # Service registration
│   │
│   ├── Tkd.Simsa.Blazor.WebApp.Client/    # 📱 WebAssembly Client
│   │   ├── Program.cs                     # Client app entry point
│   │   ├── Features/                      # Client-side features
│   │   │   └── Common/                    # Shared client functionality
│   │   │       ├── GenericItemService.cs  # HTTP API client service
│   │   │       └── RequestHandler/        # Client-side handlers
│   │   └── Extensions/                    # Client service registration
│   │
│   ├── Tkd.Simsa.Blazor.Ui/               # 🎨 Shared UI Components
│   │   ├── Features/                      # Feature-organized UI
│   │   │   ├── Common/                    # Shared UI components
│   │   │   ├── PersonManagement/          # Person management UI
│   │   │   ├── EventManagement/           # Event management UI
│   │   │   └── Exam/                      # Examination UI
│   │   └── Extensions/                    # UI service extensions
│   │
│   └── Tkd.Simsa.DataGenerator/           # 🔧 Data Generation Utility
│       ├── DataGenerator.cs               # Main data generation logic
│       └── FakerCollection.cs             # Bogus fake data generators
│
└── tests/                                 # 🧪 Test Projects
    ├── Tkd.Simsa.Application.Test/        # Application layer tests
    │   ├── QueryParametersTest.cs         # Query parameter tests
    │   └── Tkd.Simsa.Application.Test.csproj
    └── Tkd.Simsa.Persistence.Test/        # Persistence layer tests
        ├── GenericRepositoryTest.cs       # Repository tests
        ├── QueryableExtensionsPagingTest.cs # Paging tests
        ├── QueryableExtensionsSortingTest.cs # Sorting tests
        └── Helper/                        # Test utilities
```

## Architecture Layers

### 🏗️ Domain Layer (`Tkd.Simsa.Domain`)

**Purpose**: Contains the core business logic and domain models without external dependencies.

**Key Components**:

- **Aggregates**: `Person`, `Event` - Main business entities
- **Value Objects**: `PersonName`, `BirthDate`, `ParticipationData` - Immutable domain concepts
- **Enumerations**: `Gender` - Domain-specific enums
- **Interfaces**: `IModelWithId<Guid>`, `IHasId<Guid>` - Common abstractions

**Responsibilities**:

- Business rules and invariants
- Domain model definitions
- No external dependencies (pure domain logic)

### 🎯 Application Layer (`Tkd.Simsa.Application`)

**Purpose**: Orchestrates business workflows and defines application contracts.

**Key Components**:

- **Repository Interfaces**: `IPersonRepository`, `IEventRepository`, `IGenericRepository<T>`
- **Service Interfaces**: `IGenericItemService<T>`
- **Request/Response Models**: Commands and Queries for CQRS pattern
- **Filtering**: `QueryParameters<T>` for advanced querying

**Responsibilities**:

- Application service contracts
- Repository abstractions
- Transaction management
- Query parameter handling

### 🗃️ Persistence Layer (`Tkd.Simsa.Persistence`)

**Purpose**: Handles data access and implements repository patterns using Entity Framework Core.

**Key Components**:

- **DbContext**: `SimsaDbContext` - EF Core database context
- **Entities**: `PersonEntity`, `EventEntity` - Database representations
- **Configurations**: Entity Framework configurations for each entity
- **Repositories**: Concrete implementations of repository interfaces
- **Mappers**: Entity-to-Domain model mapping

**Database Model Relations**:

```csharp
// SimsaDbContext relationships:
// - Person: Independent entity with personal information
// - Event: Independent entity with event details and participation data
// - No direct foreign key relationships currently implemented
// - Participation data stored as JSON/serialized within Event entity
```

### 🌐 Presentation Layer

#### Server-Side (`Tkd.Simsa.Blazor.WebApp`)

**Purpose**: Blazor Server application with API endpoints.

**Key Components**:

- **MediatR Handlers**: `AddItemHandler<T>`, `GetItemsHandler<T>`, etc.
- **API Endpoints**: RESTful endpoints for CRUD operations
- **Dependency Injection**: Service registration and configuration

#### Client-Side (`Tkd.Simsa.Blazor.WebApp.Client`)

**Purpose**: Blazor WebAssembly client application.

**Key Components**:

- **HTTP Services**: `GenericItemService<T>` - API client implementation
- **Client Handlers**: MediatR handlers for client-side operations
- **Configuration**: Endpoint configuration for API communication

#### Shared UI (`Tkd.Simsa.Blazor.Ui`)

**Purpose**: Reusable Blazor components shared between server and client.

**Key Components**:

- **Feature Components**: Person management, Event management UI
- **Common Components**: Shared UI abstractions and interfaces
- **Edit Models**: `IEditItem<T>` for form handling

## UI Framework: MudBlazor

Tkd.Simsa uses [MudBlazor](https://mudblazor.com/) as its primary UI component library for all Blazor-based user
interfaces. MudBlazor provides a modern, Material Design-inspired component set for Blazor, enabling:

- Consistent, responsive, and accessible UI across server and WebAssembly
- Rich set of ready-to-use components (tables, dialogs, forms, navigation, etc.)
- Theming and customization for martial arts branding
- Rapid development of feature-rich, interactive screens (e.g., examination documentation, participant management)

MudBlazor is used in:

- `Tkd.Simsa.Blazor.WebApp` (server-side UI)
- `Tkd.Simsa.Blazor.WebApp.Client` (WebAssembly client UI)
- `Tkd.Simsa.Blazor.Ui` (shared UI components)

All new UI components and screens should leverage MudBlazor for layout, input, and display, unless a custom solution is
required for martial arts-specific needs.

## Service Layers & Dependencies

### Generic Service Pattern

The application uses a sophisticated generic service pattern:

```
IGenericRepository<T> (Application)
    ↓ implements
GenericRepository<TEntity, TModel> (Persistence)
    ↓ uses
Entity Framework Core
    ↓ maps to
Database Tables

IGenericItemService<T> (Application)
    ↓ implements (Server-side)
Direct Repository Access via MediatR
    ↓ implements (Client-side)
GenericItemService<T> via HTTP API
```

### MediatR Pattern

Both server and client use MediatR for request/response handling:

**Server-Side Handlers**: Directly use repositories
**Client-Side Handlers**: Use HTTP services to call server APIs

### Dependency Injection Structure

```
WebApp (Server):
- Repository implementations
- MediatR handlers (direct repository access)
- Database context and EF Core services

WebApp.Client:
- HTTP client services
- MediatR handlers (HTTP-based)
- API endpoint configurations

Shared UI:
- UI component services
- Form handling abstractions
```

## Testing Framework

### Test Structure

- **Framework**: xUnit with FluentAssertions
- **Mocking**: NSubstitute for dependency mocking
- **Data Generation**: AutoBogus for test data creation
- **Coverage**: Coverlet for code coverage analysis

### Test Projects

1. **`Tkd.Simsa.Application.Test`**:
    - Query parameter validation
    - Application service contract testing

2. **`Tkd.Simsa.Persistence.Test`**:
    - Repository implementation testing
    - Database query extension testing
    - Paging and sorting functionality

### Test Categories

- **Unit Tests**: Individual component testing
- **Integration Tests**: Repository and database testing
- **Query Tests**: LINQ expression and filtering validation

## State Management

### Server-Side State

- **Database**: SQLite database (`Tkd.Simsa.db`)
- **EF Core Context**: Scoped per request
- **Transaction Management**: Configurable (Auto/Manual modes)

### Client-Side State

- **Component State**: Blazor component lifecycle
- **HTTP Client**: Transient API calls
- **No Persistent Client State**: Stateless client architecture

### Service Connections

```
Blazor Components
    ↓ (MediatR)
Request Handlers
    ↓ (Server: Direct | Client: HTTP)
Repositories/Services
    ↓ (Entity Framework)
Database

Data Flow:
UI → MediatR → Repository → EF Core → Database
```

## Key Architectural Patterns

1. **Clean Architecture**: Clear separation of concerns across layers
2. **Generic Repository Pattern**: Type-safe, reusable data access
3. **CQRS (Command Query Responsibility Segregation)**: Separate command and query operations
4. **MediatR**: Decoupled request/response handling
5. **Value Objects**: Immutable domain concepts (PersonName, BirthDate)
6. **Entity-Domain Mapping**: Separate database and domain representations
7. **Feature Folders**: Organization by business capability
8. **Blazor Hybrid**: Both server-side and WebAssembly deployment options

## Technology Stack

- **.NET 9**: Latest framework version
- **Blazor Server + WebAssembly**: Hybrid web application
- **Entity Framework Core**: ORM for data access
- **SQLite**: Lightweight database
- **MediatR**: Request/response pattern implementation
- **xUnit**: Testing framework
- **Bogus**: Test data generation
- **StyleCop**: Code style enforcement
- **DotSettings**: Rider/Resharper Settings for code style etc.

This architecture provides a scalable, maintainable, and testable foundation for the Taekwon-Do management system with
clear separation of concerns and modern .NET development practices.

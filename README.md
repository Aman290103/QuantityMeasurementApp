# Quantity Measurement Application - Refactored

This is a refactored version of the Quantity Measurement Application following a clean layered architecture pattern.

## Project Structure

```
QuantityMeasurementApp/
├── QuantityMeasurementApp.Entity/          # Domain entities, models, and DTOs
│   ├── Models/                              # Core domain models
│   │   ├── QuantityModel.cs                 # Generic quantity model
│   │   ├── LengthUnit.cs                    # Length measurement units
│   │   ├── WeightUnit.cs                    # Weight measurement units
│   │   ├── VolumeUnit.cs                    # Volume measurement units
│   │   ├── Temperature.cs                   # Temperature measurement units
│   │   ├── QuantityMeasurementEntity.cs     # Database entity model
│   │   └── UnitMapper.cs                    # Helper to map string to units
│   ├── Interface/
│   │   └── IMeasurable.cs                   # Interface for measurable units
│   └── DTO/
│       └── QuantityDTO.cs                   # Data transfer object
│
├── QuantityMeasurementApp.Service/          # Business logic layer
│   ├── QuantityMeasurementService.cs        # Service implementation
│   ├── Interface/
│   │   └── IQuantityMeasurementService.cs   # Service contract
│   └── Exceptions/
│       └── QuantityMeasurementException.cs  # Service-level exception
│
├── QuantityMeasurementApp.Repository/       # Data access layer
│   ├── QuantityMeasurementDatabaseRepository.cs  # SQL Server repository
│   ├── QuantityMeasurementCacheRepository.cs     # In-memory cache repository
│   ├── Interface/
│   │   └── IQuantityMeasurementRepository.cs     # Repository contract
│   ├── Database/
│   │   ├── SqlStatements.cs                      # SQL query constants
│   │   └── QuantityMeasurementRowMapper.cs       # Maps SQL rows to entities
│   ├── Sync/
│   │   ├── PendingSyncStore.cs                   # Offline sync queue
│   │   └── QuantityMeasurementSyncRepository.cs  # Sync repository wrapper
│   └── Exceptions/
│       └── DatabaseException.cs                  # Data access exception
│
├── QuantityMeasurementApp.Controller/       # Presentation layer controller
│   └── QuantityMeasurementController.cs     # Handles user interactions
│
├── QuantityMeasurementApp.App/              # Application entry point
│   ├── Program.cs                            # Main entry point
│   ├── Menu.cs                               # Console menu implementation
│   ├── Interface/
│   │   └── IMenu.cs                          # Menu contract
│   └── Helpers/
│       └── MenuHelpers.cs                    # Menu utility functions
│
└── QuantityMeasurementApp.Tests/            # Unit tests
    ├── QuantityTests.cs                      # Core quantity model tests
    └── QuantityMeasurementDatabaseRepositoryTest.cs  # Repository tests
```

## Architecture Layers

### 1. Entity Layer
- Contains domain models, entities, and DTOs
- No dependencies on other layers
- Pure business objects

### 2. Repository Layer
- Data access and persistence logic
- Depends on: Entity
- Supports multiple storage backends (SQL Server, Cache, Sync)

### 3. Service Layer
- Business logic and orchestration
- Depends on: Entity, Repository
- Contains validation and business rules

### 4. Controller Layer
- Handles user interactions
- Depends on: Entity, Service
- Coordinates between UI and Service

### 5. App Layer
- Application entry point and UI
- Depends on: All layers
- Console-based menu system

## Key Features

- **Generic Quantity System**: Type-safe measurements with compile-time checking
- **Multiple Unit Types**: Length, Weight, Volume, Temperature
- **Arithmetic Operations**: Add, Subtract, Divide quantities
- **Unit Conversion**: Convert between compatible units
- **Persistence**: SQL Server database integration
- **Offline Support**: Cache-based repository with sync capabilities
- **Comprehensive Tests**: NUnit test coverage

## Building and Running

```bash
# Build the solution
dotnet build QuantityMeasurementApp.slnx

# Run the application
dotnet run --project QuantityMeasurementApp.App

# Run tests
dotnet test QuantityMeasurementApp.Tests
```

## Configuration

The application supports configuration through `appsettings.json`:

```json
{
  "UseDatabase": "false",
  "ConnectionStrings": {
    "SqlServer": "Server=.\\SQLEXPRESS;Database=QuantityMeasurementDB;..."
  }
}
```

## Refactoring Summary

This project was refactored from the original structure to follow clean architecture principles:

- ✅ Separated concerns into distinct layers
- ✅ Proper folder organization (Models/, Interface/, DTO/, Database/, Sync/, Helpers/, Exceptions/)
- ✅ Consistent naming conventions (Entity instead of Core/Models mix)
- ✅ Improved testability through interfaces
- ✅ Better dependency management
- ✅ Enhanced maintainability and scalability

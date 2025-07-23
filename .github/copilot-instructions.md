# Tkd.Simsa - Repository Custom Instructions

## Project Overview

Tkd.Simsa is a Taekwon-Do management system built with .NET 9 using Clean Architecture principles. The system manages
persons and events in a martial arts context, featuring a modern Blazor hybrid application with both server-side and
WebAssembly components.

## Folder Structure

- `/src/Tkd.Simsa.Domain/`: Pure business logic, domain models, value objects
- `/src/Tkd.Simsa.Application/`: Application contracts, repository interfaces, MediatR requests
- `/src/Tkd.Simsa.Persistence/`: Entity Framework Core, repositories, database entities
- `/src/Tkd.Simsa.Blazor.WebApp/`: Server-side Blazor application with API endpoints
- `/src/Tkd.Simsa.Blazor.WebApp.Client/`: WebAssembly client application
- `/src/Tkd.Simsa.Blazor.Ui/`: Shared Blazor UI components
- `/tests/`: Unit and integration tests mirroring source structure

## Libraries and Frameworks

- .NET 9 with C# 13
- Blazor Server and WebAssembly for hybrid UI
- Entity Framework Core with SQLite
- MediatR for CQRS pattern implementation
- xUnit, FluentAssertions, NSubstitute for testing
- AutoBogus for test data generation

## Architecture Patterns

- Clean Architecture with strict layer separation
- Generic Repository Pattern using `IGenericRepository<T>`
- CQRS with MediatR requests: `GetItemsQuery<T>`, `AddItemCommand<T>`, etc.
- Value Objects for domain concepts (PersonName, BirthDate)
- Feature-based organization over technical layers
- Entity-Domain mapping with separate database and domain models

## Coding Standards

- Namespaces follow `Tkd.Simsa.{Layer}.{Feature}` pattern
- Interfaces prefixed with `I` (IPersonRepository)
- Database entities suffixed with `Entity` (PersonEntity)
- Domain models have no suffix (Person, Event)
- Use feature folders to organize related functionality
- Place shared components in `Common/` directories
- Test files mirror source structure with `.Test` suffix
- Use async/await throughout for I/O operations
- Implement immutable value objects where appropriate

## Coding Instructions

- Write the absolute minimum code required
- No sweeping changes
- No unrelated edits - focus on just the task you're on
- Make code precise, modular, testable
- Don’t break existing functionality
- If I need to do anything (e.g. Azure/AWS config), tell me clearly

## Domain Context

This system manages Taekwon-Do practitioners and martial arts events:

- Person Management: Practitioners with personal information, belt rankings, contact details
- Event Management: Competitions, examinations, training sessions with participation tracking
- Domain terminology: Use martial arts and Taekwon-Do-specific terms when appropriate

# Event Booking System

A comprehensive Event Booking System built with .NET 9.0 following Clean Architecture principles.

## Architecture

This project follows Clean Architecture with the following layers:

- **Domain Layer** (`EventBooking.Domain`): Contains core business entities (Event, User, Booking)
- **Application Layer** (`EventBooking.Application`): Contains business logic, DTOs, interfaces, and services
- **Infrastructure Layer** (`EventBooking.Infrastructure`): Contains data access implementation with Entity Framework Core
- **API Layer** (`EventBooking.Api`): Contains RESTful API controllers and configuration

## Features

- Event Management (Create, Read, Update, Delete)
- User Management (Create, Read, Update, Delete)
- Booking Management (Create bookings, Cancel bookings)
- Automatic seat availability tracking
- Duplicate booking prevention
- Pagination support for upcoming events

## Technology Stack

- .NET 9.0
- ASP.NET Core Web API
- Entity Framework Core 9.0
- SQL Server LocalDB
- Swagger/Swashbuckle

## Prerequisites

- .NET 9.0 SDK
- SQL Server LocalDB or SQL Server

## Getting Started

### 1. Clone the repository

```bash
git clone <repository-url>
cd EventBooking
```

### 2. Update the database connection string

Edit `src/EventBooking.Api/appsettings.json` if you need a different connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=EventBookingDb;Trusted_Connection=true;TrustServerCertificate=true;"
}
```

### 3. Apply database migrations

```bash
dotnet ef database update --project EventBooking.Infrastructure --startup-project src/EventBooking.Api
```

### 4. Build the solution

```bash
dotnet build
```

### 5. Run the application

```bash
cd src/EventBooking.Api
dotnet run
```

The API will be available at:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`
- Swagger UI: `https://localhost:5001/swagger` (in Development mode)

## API Endpoints

### Events

- `GET /api/events` - Get all events
- `GET /api/events/upcoming?page=1&pageSize=10` - Get upcoming events with pagination
- `GET /api/events/{id}` - Get event by ID
- `POST /api/events` - Create new event
- `PUT /api/events/{id}` - Update event
- `DELETE /api/events/{id}` - Delete event

### Users

- `GET /api/users` - Get all users
- `GET /api/users/{id}` - Get user by ID
- `GET /api/users/email/{email}` - Get user by email
- `POST /api/users` - Create new user
- `PUT /api/users/{id}` - Update user
- `DELETE /api/users/{id}` - Delete user

### Bookings

- `GET /api/bookings/{id}` - Get booking by ID
- `GET /api/bookings/user/{userId}` - Get all bookings for a user
- `GET /api/bookings/event/{eventId}` - Get all bookings for an event
- `POST /api/bookings` - Create new booking
- `DELETE /api/bookings/{id}` - Cancel booking

## Example Requests

### Create Event

```json
POST /api/events
{
  "title": "Tech Conference 2026",
  "description": "Annual technology conference",
  "date": "2026-06-15T09:00:00",
  "location": "Convention Center",
  "capacity": 500
}
```

### Create User

```json
POST /api/users
{
  "email": "user@example.com",
  "name": "John Doe"
}
```

### Create Booking

```json
POST /api/bookings
{
  "eventId": 1,
  "userId": 1
}
```

## Project Structure

```
EventBooking/
├── EventBooking.Domain/
│   └── Entities/
│       ├── Event.cs
│       ├── User.cs
│       └── Booking.cs
├── EventBooking.Application/
│   ├── DTOs/
│   ├── Interfaces/
│   ├── Services/
│   └── DependencyInjection.cs
├── EventBooking.Infrastructure/
│   ├── Persistance/
│   │   ├── AppDbContext.cs
│   │   ├── Repositories/
│   │   └── Migrations/
│   └── DependencyInjection.cs
└── src/
    └── EventBooking.Api/
        ├── Controllers/
        ├── Program.cs
        └── appsettings.json
```

## Database Schema

### Events Table
- Id (PK)
- Title
- Description
- Date
- Location
- Capacity
- AvailableSeats
- CreatedAt
- UpdatedAt

### Users Table
- Id (PK)
- Email (Unique)
- Name
- CreatedAt

### Bookings Table
- Id (PK)
- EventId (FK)
- UserId (FK)
- CreatedAt
- Status
- Unique constraint on (EventId, UserId)

## Business Rules

1. Users cannot book the same event twice
2. Events cannot be overbooked (available seats must be > 0)
3. When a booking is created, available seats decrease by 1
4. When a booking is cancelled, available seats increase by 1
5. Email addresses must be unique for users

## Development

### Adding a new migration

```bash
dotnet ef migrations add <MigrationName> --project EventBooking.Infrastructure --startup-project src/EventBooking.Api
```

### Removing the last migration

```bash
dotnet ef migrations remove --project EventBooking.Infrastructure --startup-project src/EventBooking.Api
```

### Updating the database

```bash
dotnet ef database update --project EventBooking.Infrastructure --startup-project src/EventBooking.Api
```

## License

This project is licensed under the MIT License.
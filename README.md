# Car Listing Service

This service manages car listings for sale.

## Tech Stack
- .NET 8 Web API
- MongoDB or PostgreSQL
- RabbitMQ (for event communication)

## Features
- Create, update, delete car listings
- Manage car details (brand, model, year, mileage, condition, etc.)
- Emits events: `car-listed`, `car-sold`
- Communicates with order-service and notification-service

## Getting Started
1. Install .NET 8 SDK
2. Install and run MongoDB locally (default connection: `mongodb://localhost:27017`)
3. Configure your database (MongoDB or PostgreSQL) in `appsettings.json`
4. Configure RabbitMQ connection in `appsettings.json`
5. Run the service:
   ```bash
   dotnet run
   ```

## API Endpoints
- `GET /Car` - Get all car listings
- `GET /Car/{id}` - Get car by id
- `POST /Car` - Create a new car listing
- `PUT /Car/{id}` - Update a car listing
- `DELETE /Car/{id}` - Delete a car listing

## API Documentation
Swagger UI available at `/swagger` when running locally. 
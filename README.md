# Northcoders Record Shop API

A backend Web API for a record shop's inventory, built with ASP.NET Core and .NET 8.
It stores albums and lets you list, search, add, update and delete them.

This is a mini project from the Northcoders C# Full-Stack bootcamp.

## Running the project

```
cd "NC-Record Shop API Mini Project"
dotnet run
```

The API runs on http://localhost:5113. In development, Swagger is at `/swagger`.

In development it uses an Entity Framework in-memory database that is seeded with a
few albums on startup. In production it connects to SQL Server (see `appsettings.Production.json`).

## Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/albums` | List all albums |
| GET | `/api/albums?artist=&genre=&releaseYear=&name=` | Filter albums |
| GET | `/api/albums/{id}` | Get one album by id |
| POST | `/api/albums` | Add a new album |
| PUT | `/api/albums/{id}` | Update an album |
| DELETE | `/api/albums/{id}` | Delete an album |
| GET | `/health` | Check the API and database are healthy |

The filter parameters are optional and can be combined. Artist and name match part of
the text; genre and release year match exactly.

## Project structure

The app is split into layers:

- **Controllers** - handle HTTP requests and responses
- **Services** - business logic
- **Repositories** - data access with Entity Framework
- **Models** - the Album model
- **Data** - the AppDbContext

## Tests

Unit tests are in the test project, with a folder per layer. Run them with:

```
dotnet test
```

## Assumptions

- An album has a name, artist, genre, release year, stock and price.
- Album names are not unique.

## Things I'd add next

- Pagination for large numbers of albums
- User ratings
- Authentication for the add, update and delete endpoints

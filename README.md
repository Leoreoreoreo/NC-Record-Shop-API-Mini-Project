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
| POST | `/api/albums/{id}/ratings` | Add a rating (1-5 stars) to an album |
| GET | `/api/albums/{id}/ratings` | Get an album's average rating and count |
| GET | `/health` | Check the API and database are healthy |

The filter parameters are optional and can be combined. Artist and name match part of
the text; genre and release year match exactly.

### Pagination

Add `pageSize` (and optionally `page`, default 1) to page through the results, for example
`/api/albums?page=2&pageSize=10`. Pagination can be combined with the filters above. When
`pageSize` is given the response is an object with the albums plus paging info:

```json
{
  "albums": [ ... ],
  "page": 2,
  "pageSize": 10,
  "totalCount": 53,
  "totalPages": 6
}
```

Without `pageSize` the endpoint returns the full list as before.

### Authentication

The write endpoints (POST, PUT and DELETE on `/api/albums`) require an API key. Send it in an
`X-Api-Key` header; requests without a valid key get a 401. The key comes from the `ApiKey`
configuration value (a dev value is set in `appsettings.json`; in production it should come from
an environment variable or secret). Reads and the ratings endpoints are open.

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

- Sorting results by price, release year or rating
- Replacing the single API key with proper user accounts

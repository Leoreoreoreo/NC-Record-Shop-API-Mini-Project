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
| POST | `/api/auth/register` | Register a new user |
| POST | `/api/auth/login` | Log in and receive a JWT |

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

Authentication uses ASP.NET Core Identity with JWT bearer tokens. Register with
`POST /api/auth/register` and log in with `POST /api/auth/login` (both take an email and a
password); login returns a token to send as an `Authorization: Bearer <token>` header.

The write endpoints (POST, PUT and DELETE on `/api/albums`) require a token from a user in the
`Admin` role — other signed-in users get a 403 and anonymous requests get a 401. Reads and the
ratings endpoints are open. A development admin account is seeded on startup (see the `AdminUser`
config in `appsettings.json`); the JWT signing key and issuer/audience live under `Jwt`.

### Sorting

Add `sortBy` to order the results: `name`, `artist`, `releaseYear`, `price` or `rating`
(`rating` sorts by the album's average rating; unrated albums count as 0). Add `order=desc`
for descending (the default is ascending), for example `/api/albums?sortBy=price&order=desc`.
Sorting works on its own and combines with the filters and with pagination.

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

- Refresh tokens so logins last longer
- A simple orders / checkout flow

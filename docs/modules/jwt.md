# JWT

`Griffin.Jwt` configures JWT bearer authentication, claim mapping, and default authorization policies.

## Installation

```bash
dotnet add package Griffin.Jwt
```

## Register Authentication

```csharp
using Griffin.Jwt;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddJwt();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.Run();
```

## Configuration

`AddJwt` reads the `Jwt` section:

```json
{
  "Jwt": {
    "Authority": "https://identity.example.com",
    "Audience": "orders-api"
  }
}
```

Token validation enables issuer, audience, lifetime, and signing-key checks, with a two-second clock skew. The `name` and `role` claims map to `User.Identity.Name` and `User.IsInRole`, and inbound claims are preserved.

`RequireHttpsMetadata` is `false` in this registration. In production ensure token endpoints are reachable only over HTTPS, or override this setting when the identity provider requires it.

## Default Policies

Registration adds three policies:

- `ApiScope`: authenticated user with a `scope` claim matching the audience.
- `Admin`: authenticated user in the `Admin` role.
- `User`: authenticated user in the `User` role.

Protect endpoints with `RequireAuthorization` or `[Authorize]`; registration only configures the schemes and policies.

## Related Documentation

- [JWT source](https://github.com/meysamhadeli/Griffin/tree/main/src/Jwt)
- [Web correlation and versioning](/modules/web)

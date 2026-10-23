# Mapster

`Griffin.Mapster` registers Mapster mapping configuration and a singleton mapper.

## Installation

```bash
dotnet add package Griffin.Mapster
```

## Register Mappings

```csharp
using Griffin.Mapster;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCustomMapster(typeof(Program).Assembly);
```

`AddCustomMapster` scans the supplied assemblies into the global `TypeAdapterConfig` and registers Mapster's `IMapper` as a singleton. Inject `IMapper` from `MapsterMapper` into application services.

Define mappings with `IRegister` implementations in scanned assemblies, or use Mapster's conventions. Pass all assemblies containing mapping registrations; unscanned assemblies are not configured.

## Related Documentation

- [Mapster source](https://github.com/meysamhadeli/Griffin/tree/main/src/Mapster)
- [Mapster](https://github.com/MapsterMapper/Mapster)

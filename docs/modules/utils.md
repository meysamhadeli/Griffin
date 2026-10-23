# Utils

`Griffin.Utils` contains general-purpose assembly scanning, service location, and synchronization-context helpers.

## Installation

```bash
dotnet add package Griffin.Utils
```

## Type Discovery

`TypeProvider` resolves types across the application domain and walks assembly references:

- `GetTypeFromAnyReferencingAssembly(name)` finds a type by full or simple name in referenced assemblies.
- `GetFirstMatchingTypeFromCurrentDomainAssembly(name)` finds the first match in loaded assemblies.
- `GetReferencedAssemblies(root)` performs a breadth-first walk of assembly references.
- `GetApplicationPartAssemblies(root)` returns application-part assemblies whose names share the root namespace.

`TypeProvider` is the mechanism several Griffin modules use for endpoint and handler discovery.

## Service Locator

`ServiceLocator` exposes static `Current`, a `SetLocatorProvider` initializer, and `GetInstance<T>` helpers. Prefer constructor injection; the service locator is intended only where constructor injection is impractical.

## Synchronization Context

`NoSynchronizationContextScope.Enter()` removes the current synchronization context and restores it on disposal. Use it in test or background code where captured contexts cause deadlocks.

## Related Documentation

- [Utils source](https://github.com/meysamhadeli/Griffin/tree/main/src/Utils)

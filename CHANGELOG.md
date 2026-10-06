# Changelog

All notable changes to this project will be documented here.

The format follows [Keep a Changelog](https://keepachangelog.com/en/1.1.0/).

## [1.0.0] - 2026-10-06

### Added

- First Griffin release: all class libraries published at version 1.0.0.

### Changed

- Renamed the library family from Woo to Griffin across packages, namespaces, assembly names, project files, documentation, and assets.
- `woo.slnx` renamed to `griffin.slnx`; each `Woo.*.csproj` renamed to `Griffin.*.csproj`.
- NuGet package IDs changed to `Griffin.*` with `Griffin.*` namespaces and assembly names.

## [Unreleased]

### Added

- Automatically publish every Griffin class library to NuGet.org with the shared version when a GitHub release is published.

### Changed

- Renamed the `Griffin.Logging` package, namespace, and assembly to `Griffin.Log` (`src/Logging` moved to `src/Log`); the `Griffin.Logging` ID was already owned by another publisher on NuGet.org.
- Each Griffin class library owns its direct NuGet dependencies; the former shared dependency project was removed.
- Added repository guidance, CI, Copilot setup, issue templates, and architecture documentation.

# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project overview

ApiDecimatio (namespace/assembly: `Decimatio.WebApi`) is a .NET Web API for generating and managing tickets for events/concerts ("Decimatio" / "ResonancePass"). It issues QR-coded ticket vouchers, handles payments (MercadoPago, PayPal), sends emails, and stores files in Azure Blob Storage. See `README.md` and `arquitectura.png` for the original stack summary.

## Commands

```bash
# Restore, build, run (from repo root)
dotnet restore
dotnet build
dotnet run --project ApiDecimatio/Decimatio.WebApi.csproj

# Swagger UI is enabled unconditionally (not gated to Development) at the app root when running
```

There is no test project in this solution — do not assume `dotnet test` has coverage; if asked to add tests, a new test project must be created first.

Solution file `ApiDecimatio.sln` groups projects into folders numbered `01. Api` … `05. Common` mirroring the layer order described below.

## Architecture

Layered/Clean-Architecture-style solution with five projects, dependencies flowing inward:

```
Decimatio.WebApi (ApiDecimatio/)   -- ASP.NET Core controllers, host, DI wiring, auth
        v
Decimatio.Application               -- services (business logic), DTOs, FluentValidation validators, AutoMapper profile
        v
Decimatio.Infraestructure            -- Dapper repositories (raw SQL via SqlConnection), DB config
        v
Decimatio.Domain                     -- entities, DTOs-adjacent value objects, exceptions, custom entities (paging, ApiResponse)
        ^
Decimatio.Common                     -- cross-cutting services: email, PDF generation, QR generation, Azure Blob storage
```

- Each project has an `_Imports.cs` with `global using` directives — check it before adding `using` statements; most common namespaces are already global within that project.
- Each layer exposes its own `DependencyConfiguration`/`DependencyContainer` static class (`AddApplicationDependencies`, `AddRepositories`, `AddCommonDependencies`) registered from `ApiDecimatio/Configuration/DependencyInjectorConfiguration.cs`, which is the single place wiring the whole app together (auth scheme, pagination options, FluentValidation, AutoMapper, per-feature options binding).
- Controllers are thin: call one `I*Service`, wrap the result in `Decimatio.Domain.CustomEntities.ApiResponse<T>`, return `Ok(response)`. Errors are not caught per-controller — they flow to `GlobalExceptionHandlerMiddleware` (`ApiDecimatio/Middleware`), which maps `NotFoundException` / `BadRequestException` / `ValidationResultException` / `NoContentException` (all in `Decimatio.Domain.Exceptions`) to the corresponding HTTP status and a JSON `ErrorResponse`; anything else becomes a 500.
- Services (`Decimatio.Application/Services`) contain the business logic: run FluentValidation validators explicitly (`validator.Validate(dto)`, not the pipeline filter), throw domain exceptions on failure, map between entities and DTOs with AutoMapper, and call one or more repositories/common services. Services are registered as `internal sealed class` implementing a public interface from `Decimatio.Application/Interfaces/Services`.
- Repositories (`Decimatio.Infraestructure/Repositories`) use **Dapper** directly against `SqlConnection` (SQL Server) — no EF Core, no `DbContext`. Each method opens its own `using var conn = new SqlConnection(...)`. SQL text lives in the RESX resource `Querys.resx` (accessed as `Querys.QUERY_NAME` via the generated `Querys.Designer.cs`), not inline strings — add new queries there rather than embedding SQL in C#.
- Pagination: query filters (`Decimatio.Domain/QueryFilters`) carry `PageNumber`/`PageSize`; services normalize defaults from `PaginationOptions` (bound from config section `Pagination`), repositories return the full filtered set, and `PagedList<T>.CreatePaginationFromDb` + `PagedListExtensions.ToMetaData` build the paged result and `MetaData` returned as both response `Meta` and an `X-Pagination` header.
- Auth: custom Basic Authentication scheme (`ApiDecimatio/Authentication/BasicAuthenticationHandler.cs`), credentials bound from config section `BasicAuthCredentials`. Controllers are `[Authorize]` by default — there is no JWT/OAuth here.
- Validation: FluentValidation validators live in `ApiDecimatio/Validations` (one per Create/Update DTO) and are registered from the assembly via `AddFluentValidation` in `DependencyInjectorConfiguration`, but are invoked manually inside services rather than as an automatic MVC filter.
- Mapping: only `Decimatio.Application/Mappings/AutoMapperProfile.cs` is registered/active. `Decimatio.Application/Helpers/AutoMapperProfile.cs` is a stale/unused duplicate (mostly commented out) — don't edit it expecting it to take effect; consolidate into `Mappings` if touching mapping code.
- Payments: `MercadoPagoController`/`PayPalController` plus `PayPalService` under Application handle preference/order creation; options bound from `MercadoPagoOptions`/`PayPalOptions` config sections.
- File/QR/PDF generation and email are cross-cutting `Decimatio.Common` services (`IBlobFilesService`, `IQRGeneratorService`, `IPDFGeneratorService`, `IEmailSenderService`) using QRCoder, QuestPDF/PDFSharp, and Azure.Storage.Blobs; ticket flyers/templates are read from `ApiDecimatio/Template`.

## Configuration notes

- Config sections are bound with `IOptions<T>` from `Decimatio.Domain/ValueObjects` (e.g. `MercadoPagoOptions`, `PasswordOptions`, `EncryptedTicketConfig`, `BlobContainerConfig`, `EmailSenderOptions`) or plain singletons (e.g. `DataBaseConfig`) registered directly in the container.
- `appsettings.json` in this repo currently contains real-looking connection strings, storage account keys, and API secrets in plaintext (DB, Azure Blob, MercadoPago, PayPal, email sender). Treat this file as sensitive — do not print its contents back to chat/logs beyond what's needed, and avoid committing further plaintext secrets; prefer environment-specific overrides or user-secrets for any new configuration.
- `ApiDecimatio/Dockerfile` targets the `mcr.microsoft.com/dotnet/sdk:6.0` image while the actual project TargetFrameworks are `net8.0` (all projects except `Decimatio.Domain`, which is still `net6.0`) — this mismatch means the Dockerfile is likely stale; verify/update the SDK version before relying on it for a container build.

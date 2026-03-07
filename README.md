# Playwright Portfolio Automation (C#)

## Prerequisites

- .NET SDK 10.0 (or SDK 8.0+ if you change target framework)
- PowerShell 7+ (or Windows PowerShell 5.1)

## Setup

Run from repo root:

```powershell
dotnet restore --configfile NuGet.Config
dotnet build
pwsh .\PlaywrightTests\bin\Debug\net10.0\playwright.ps1 install
```

If `dotnet` is not on your `PATH`, run with full path:

```powershell
& "C:\Program Files\dotnet\dotnet.exe" restore --configfile NuGet.Config
& "C:\Program Files\dotnet\dotnet.exe" build
powershell -ExecutionPolicy Bypass -File .\PlaywrightTests\bin\Debug\net10.0\playwright.ps1 install
```

## Run tests

```powershell
dotnet test
```

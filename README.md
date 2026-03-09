# Playwright Portfolio Automation (C#)

A comprehensive UI test automation suite for the portfolio website at [carlosng07.vercel.app](https://carlosng07.vercel.app) using Playwright and C#.

## Overview

This project provides automated end-to-end (E2E) tests for a portfolio website. Tests are organized by page/section and cover functionality across:

- **Home Page** - Navigation, hero section, featured projects, skills, certifications, publications
- **About** - About page content verification
- **Education** - Educational background validation
- **Experience** - Work experience section testing
- **Projects** - Project listings and details
- **Resume** - Resume download and content verification
- **Certifications** - Certification details and validations
- **Contact** - Contact form and information testing

## Tech Stack

- **Framework**: [Playwright](https://playwright.dev) (v1.56.0)
- **Language**: C# with .NET 10.0
- **Test Runner**: NUnit 4.3.2
- **IDE**: Visual Studio, VS Code, or Rider

## Prerequisites

- **.NET SDK**: Version 10.0 (minimum .NET 8.0 if you modify target framework)
- **PowerShell**: 7+ (or Windows PowerShell 5.1)
- **Git**: For cloning the repository

## Installation

1. **Clone the repository**:
   ```powershell
   git clone <repository-url>
   cd playwright_portfolio_automation_csharp
   ```

2. **Restore dependencies**:
   ```powershell
   dotnet restore --configfile NuGet.Config
   ```

3. **Build the project**:
   ```powershell
   dotnet build
   ```

4. **Install Playwright browsers**:
   ```powershell
   pwsh .\PlaywrightTests\bin\Debug\net10.0\playwright.ps1 install
   ```

### If `dotnet` is not in PATH:

Use the full path to dotnet:

```powershell
& "C:\Program Files\dotnet\dotnet.exe" restore --configfile NuGet.Config
& "C:\Program Files\dotnet\dotnet.exe" build
powershell -ExecutionPolicy Bypass -File .\PlaywrightTests\bin\Debug\net10.0\playwright.ps1 install
```

## Running Tests

### Run all tests:
```powershell
dotnet test
```

### Run specific test file:
```powershell
dotnet test --filter "PlaywrightTests.Tests.HomeTests"
```

### Run test with verbose output:
```powershell
dotnet test --verbosity normal
```

### Run with filtered test names:
```powershell
dotnet test --filter "Name~VerifyHomePage"
```

## Project Structure

```
playwright_portfolio_automation_csharp/
├── PlaywrightTests/
│   ├── Framework/
│   │   └── PortfolioTestBase.cs       # Base class with Playwright setup/teardown
│   ├── Tests/
│   │   ├── HomeTests.cs               # Home page tests
│   │   ├── AboutTests.cs              # About page tests
│   │   ├── EducationTests.cs          # Education section tests
│   │   ├── ExperienceTests.cs         # Experience section tests
│   │   ├── ProjectsTests.cs           # Projects section tests
│   │   ├── ResumeTests.cs             # Resume page tests
│   │   ├── CertificationsTests.cs     # Certifications section tests
│   │   └── ContactTests.cs            # Contact page tests
│   └── PlaywrightTests.csproj         # Project file with dependencies
├── README.md                           # This file
├── LICENSE                             # License information
└── NuGet.Config                        # NuGet configuration
```

## Key Features

- **Page Object Model Pattern**: Tests inherit from `PortfolioTestBase` which manages Playwright initialization
- **Async/Await Pattern**: All tests use modern async patterns for better performance
- **Cross-browser Testing Ready**: Configured for Chromium (easily extensible to Firefox/WebKit)
- **CI/CD Ready**: Detects CI environments and runs in headless mode automatically
- **Accessibility Testing**: Includes verification of ARIA roles and semantic elements
- **API Testing Support**: Includes APIRequestContext for API validation when needed

## Dependencies

| Package | Version | Purpose |
|---------|---------|---------|
| Microsoft.Playwright | 1.56.0 | Browser automation framework |
| NUnit | 4.3.2 | Unit testing framework |
| Microsoft.NET.Test.Sdk | 17.14.0 | Test SDK for running tests |
| NUnit3TestAdapter | 5.0.0 | Test adapter for Visual Studio/VS Code |
| coverlet.collector | 6.0.4 | Code coverage collection |
| NUnit.Analyzers | 4.7.0 | Code analysis for NUnit tests |

## Configuration

### Base URL
The tests are configured to run against: `https://carlosng07.vercel.app`

To change the target URL, modify the `BaseUrl` constant in [PlaywrightTests/Framework/PortfolioTestBase.cs](PlaywrightTests/Framework/PortfolioTestBase.cs):

```csharp
protected const string BaseUrl = "https://carlosng07.vercel.app";
```

### Headless Mode
By default, tests run in headless mode in CI environments. To run with UI:

1. Set environment variable: `CI=false`
2. Or modify `IsCiEnvironment()` logic in the base class

## Debugging Tests

### Run tests with browser visible:
```powershell
# Set CI to false to disable headless mode
$env:CI = "false"
dotnet test
```

### Debug in VS Code:
1. Install "Playwright Test for VSCode" extension
2. Set breakpoints in test files
3. Use the Test Explorer in the sidebar

### Debug in Visual Studio:
1. Open the Test Explorer (Test > Test Explorer)
2. Right-click on a test and select "Debug Selected Tests"

## Common Issues

### Issue: Playwright browsers not found
**Solution**: Run the playwright install script:
```powershell
pwsh .\PlaywrightTests\bin\Debug\net10.0\playwright.ps1 install
```

### Issue: PowerShell execution policy error
**Solution**: Run with execution policy bypass:
```powershell
powershell -ExecutionPolicy Bypass -File .\PlaywrightTests\bin\Debug\net10.0\playwright.ps1 install
```

### Issue: Tests timeout
**Solution**: Increase timeout in test methods or configuration:
```csharp
[SetUp]
public void SetTimeout()
{
    Page.SetDefaultTimeout(30000); // 30 seconds
}
```

## CI/CD Integration

This project is ready for CI/CD pipelines. Tests automatically detect CI environments and run in headless mode. Example GitHub Actions workflow:

```yaml
- name: Run Playwright Tests
  run: dotnet test
```

## License

See the [LICENSE](LICENSE) file for licensing information.

## Contributing

Contributions are welcome! Please ensure:
1. All tests pass before submitting
2. New tests follow the existing naming conventions
3. Code follows C# naming guidelines

## Support

For issues or questions:
1. Check existing test files for examples
2. Review Playwright documentation: https://playwright.dev/dotnet
3. Check NUnit documentation: https://docs.nunit.org

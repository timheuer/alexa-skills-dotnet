# AI Agent Guide for Alexa Skills SDK .NET

This file provides guidance for AI coding agents working with the Alexa Skills SDK for .NET repository.

## Project Overview

Alexa.NET is a helper library for working with Amazon Alexa skill requests/responses in C#. The library provides a strongly-typed object model for the Alexa API, supporting both AWS Lambda and self-hosted service scenarios.

- **Language**: C# / .NET
- **Target Framework**: .NET Standard 2.0
- **Primary Library**: Alexa.NET
- **Test Framework**: xUnit (used in Alexa.NET.Tests)
- **Key Dependencies**: Newtonsoft.Json (12.0.2)

## Repository Structure

```
/Alexa.NET/              # Main library source code
/Alexa.NET.Tests/        # Unit tests
/docs/                   # Documentation files
/.github/workflows/      # CI/CD GitHub Actions workflows
```

## Setup and Prerequisites

### Prerequisites
- .NET SDK 6.0.x or later (recommended for development; the library targets .NET Standard 2.0)
- Visual Studio 2019+ or Visual Studio Code (optional)

### Initial Setup
```bash
# Restore dependencies
dotnet restore

# Build the project
dotnet build --configuration Release
```

## Build Commands

```bash
# Restore NuGet packages
dotnet restore

# Build in Debug mode
dotnet build --configuration Debug

# Build in Release mode
dotnet build --configuration Release --no-restore

# Create NuGet package
dotnet pack --configuration Release -o finalpackage --no-build
```

## Testing

```bash
# Run all tests
dotnet test

# Run tests for specific project
dotnet test Alexa.NET.Tests/Alexa.NET.Tests.csproj

# Run tests with detailed output
dotnet test --verbosity normal
```

## Coding Conventions

### General Guidelines
- **Target Framework**: All code must target .NET Standard 2.0 for maximum compatibility
- **C# Version**: Language version 8.0 is configured in the project
- **JSON Serialization**: Use Newtonsoft.Json for all JSON operations
- **Naming**: Follow standard C# naming conventions (PascalCase for classes, methods, properties; camelCase for private fields)

### Code Style
- Use meaningful names for classes, methods, and variables
- Keep methods focused and single-purpose
- Add XML documentation comments for public APIs
- Follow existing patterns in the codebase for consistency

### Request/Response Handling
- All Alexa requests inherit from `Request` base class
- All responses use `SkillResponse` object model
- Use `ResponseBuilder` helper class for creating responses (e.g., `ResponseBuilder.Tell()`, `ResponseBuilder.Ask()`)
- Support for SSML via `SsmlOutputSpeech` class

### Testing Patterns
- Place tests in `Alexa.NET.Tests` project
- Follow existing test naming patterns (e.g., `*Tests.cs`)
- Use xUnit test framework
- Include tests for new features and bug fixes

## Project Files

- **Alexa.NET.csproj**: Main library project file - contains version, package metadata, and dependencies
- **Alexa.NET.sln**: Solution file containing both library and test projects
- **README.md**: User-facing documentation (keep distinct from AGENTS.md)

## CI/CD Workflows

The repository uses GitHub Actions for continuous integration:

- **PR Build** (`prbuild.yaml`): Runs on pull requests - builds and tests the code
- **Build and Deploy** (`build.yml`): Runs on master branch - builds, tests, packs, signs, and publishes to NuGet

### Workflow Commands
The CI/CD pipeline executes:
1. `dotnet restore`
2. `dotnet build --configuration Release --no-restore`
3. `dotnet test` (via custom action)
4. `dotnet pack --configuration Release -o finalpackage --no-build`

## Boundaries and Restrictions

### Do Not Modify
- `.github/workflows/` directory - CI/CD workflow files (*.yml and *.yaml) unless specifically tasked
- `alexa-skills-dotnet.snk` - Strong name key file
- `nuget-icon.png` - Package icon
- Version numbers in `Alexa.NET.csproj` without explicit instruction

### Dependency Management
- Do not add or update dependencies without justification
- Maintain compatibility with .NET Standard 2.0
- Consider impact on existing consumers when updating Newtonsoft.Json version

### Breaking Changes
- Avoid breaking changes to public APIs
- Maintain backward compatibility where possible
- Document any necessary breaking changes clearly

## Security Considerations

- Never commit secrets, API keys, or certificates to the repository
- The `.pfx` certificate file is already in `.gitignore`
- Review authentication and authorization code carefully
- Validate all input from Alexa requests
- Follow secure coding practices for JSON deserialization

## Common Tasks

### Adding a New Request Type
1. Create a new class in `/Alexa.NET/Request/` that inherits from `Request`
2. Implement required properties and JSON serialization attributes
3. Add corresponding tests in `/Alexa.NET.Tests/`
4. Update documentation if it's a user-facing feature

### Adding a New Response Type
1. Create appropriate classes in `/Alexa.NET/Response/`
2. Add helper methods to `ResponseBuilder` if applicable
3. Include tests for serialization and builder methods
4. Document usage patterns in code comments

### Fixing Bugs
1. Write a failing test that reproduces the bug
2. Fix the bug with minimal code changes
3. Ensure the test passes
4. Run full test suite to check for regressions
5. Update documentation if behavior changes

## Package Publishing

- Package is published to NuGet.org automatically on merge to master
- Package is also published to GitHub Packages
- Signing is handled automatically in the workflow
- Version is read from `VersionPrefix` in `Alexa.NET.csproj`
- Release notes are read from `PackageReleaseNotes` in project file

## Additional Notes

- This is a community-driven project following the [.NET Foundation Code of Conduct](https://dotnetfoundation.org/code-of-conduct)
- The project uses the `all-contributors` specification
- Spanish documentation is available in `/docs/README_es.md`
- The library serves as a foundation for various Alexa skill extensions by other contributors

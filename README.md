
# Install/setup solution

```bash
# Create new solution and projects
$dotnet new sln -o WebPortal
$cd .\WebPortal\
$dotnet new webapi -o WebPortal.Api
$dotnet new classlib -o WebPortal.Contracts
$dotnet new classlib -o WebPortal.Infrastructure
$dotnet new classlib -o WebPortal.Application
$dotnet new classlib -o WebPortal.Domain

# Add all projects to the solution
$dotnet sln add (ls -r **\*.csproj)

# Check that it builds
$dotnet build

# Connect the projects
$dotnet add .\WebPortal.Api\ reference .\WebPortal.Contracts\ .\WebPortal.Application\
$dotnet add .\WebPortal.Infrastructure\ reference .\WebPortal.Application\
$dotnet add .\WebPortal.Application\ reference .\WebPortal.Domain\
$dotnet add .\WebPortal.Api\ reference .\WebPortal.Infrastructure\

# Check that it builds
$dotnet build

# Add gitignore
$dotnet new gitignore

```

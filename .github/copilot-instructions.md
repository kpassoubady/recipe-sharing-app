<!-- Use this file to provide workspace-specific custom instructions to Copilot. -->

## Recipe Sharing App Project

### Project Overview
- **Backend**: .NET 10 ASP.NET Core Web API
- **Frontend**: Blazor WebAssembly
- **Database**: SQLite with Entity Framework Core
- **Development**: Mac, Visual Studio Code
- **Timeline**: 1-day MVP with seed data

### Architecture
- RecipeApp.Api - ASP.NET Core Web API backend
- RecipeApp.Client - Blazor WebAssembly frontend
- RecipeApp.Shared - Shared DTOs and models

### Features
- Recipe CRUD operations
- Ingredient management
- Recipe categories and tags
- Rating and review system
- Nutritional information
- Difficulty levels (Easy, Medium, Hard)
- Prep/cook time tracking
- Search by ingredients
- Favorites functionality

### Development Guidelines
- Use repository pattern for data access
- DTOs for API responses
- RESTful API design
- Swagger/OpenAPI documentation
- CORS enabled for local development
- Local file system for recipe images
- Comprehensive seed data included

### Running the Application

#### Start the API (Terminal 1)
```bash
cd RecipeApp.Api
dotnet run
```
API will be available at: https://localhost:7068
Swagger UI: https://localhost:7068/swagger

#### Start the Client (Terminal 2)
```bash
cd RecipeApp.Client
dotnet run
```
Client will be available at: https://localhost:7164

- [x] Verify copilot-instructions.md file created
- [x] Get project setup information
- [x] Scaffold the project
- [x] Customize the project
- [x] Install required extensions
- [x] Compile the project
- [x] Create and run task
- [x] Ensure documentation is complete

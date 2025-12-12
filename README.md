# Recipe Sharing App

A full-stack web application built with .NET 10 for discovering, sharing, and managing recipes. Features a RESTful API backend with ASP.NET Core and a modern Blazor WebAssembly frontend.

## 🚀 Features

- **Recipe Management**: Complete CRUD operations for recipes
- **Ingredient Search**: Find recipes by available ingredients
- **Rating & Reviews**: User ratings and detailed reviews for each recipe
- **Nutritional Information**: Track calories, protein, carbs, fat, and fiber
- **Difficulty Levels**: Easy, Medium, and Hard classifications
- **Categories & Tags**: Organize recipes by cuisine and dietary preferences
- **Time Tracking**: Prep time and cook time for each recipe
- **Rich Seed Data**: 10 pre-loaded recipes with complete information

## 🏗️ Architecture

### Technology Stack

- **Backend**: .NET 10, ASP.NET Core Web API
- **Frontend**: Blazor WebAssembly
- **Database**: SQLite with Entity Framework Core
- **API Documentation**: Swagger/OpenAPI
- **Development**: macOS, Visual Studio Code

### Project Structure

```
RecipeApp/
├── RecipeApp.Api/              # ASP.NET Core Web API
│   ├── Controllers/            # API endpoints
│   ├── Data/                   # Database context and seed data
│   ├── Repositories/           # Data access layer
│   └── wwwroot/               # Static files (recipe images)
├── RecipeApp.Client/          # Blazor WebAssembly
│   ├── Pages/                 # Razor components
│   ├── Services/              # API client services
│   └── Layout/                # App layout components
└── RecipeApp.Shared/          # Shared library
    ├── Models/                # Entity models
    └── DTOs/                  # Data Transfer Objects
```

## 📋 Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- Visual Studio Code (recommended) or Visual Studio 2025+
- SQLite (included with .NET)

## 🛠️ Getting Started

### 1. Clone or Open the Project

```bash
cd /Users/kangs/Copilot-Exercises/recipe-sharing-app
```

### 2. Restore Dependencies

```bash
dotnet restore
```

### 3. Build the Solution

```bash
dotnet build
```

### 4. Run the API (Terminal 1)

```bash
cd RecipeApp.Api
dotnet run
```

The API will start at:
- HTTPS: `https://localhost:7068`
- HTTP: `http://localhost:5068`
- Swagger UI: `https://localhost:7068/swagger`

### 5. Run the Blazor Client (Terminal 2)

```bash
cd RecipeApp.Client
dotnet run
```

The client will start at:
- HTTPS: `https://localhost:7164`
- HTTP: `http://localhost:5164`

### 6. Access the Application

Open your browser and navigate to: `https://localhost:7164`

## 🗄️ Database

The application uses SQLite with automatic database creation and seeding on first run. The database file `recipes.db` will be created in the `RecipeApp.Api` directory.

### Seed Data Includes:

- **10 Recipes**: From various cuisines (Italian, Asian, Mediterranean, Thai, Mexican, etc.)
- **50 Ingredients**: Comprehensive ingredient lists
- **10 Nutrition Profiles**: Complete nutritional information
- **13 User Ratings**: Sample reviews and ratings
- **10 Tags**: Vegetarian, Vegan, Gluten-Free, Quick & Easy, etc.

## 🔌 API Endpoints

### Recipes

- `GET /api/recipes` - Get all recipes
- `GET /api/recipes/{id}` - Get recipe by ID
- `GET /api/recipes/search?ingredients={list}` - Search recipes by ingredients
- `POST /api/recipes` - Create new recipe
- `PUT /api/recipes/{id}` - Update recipe
- `DELETE /api/recipes/{id}` - Delete recipe

### Ratings

- `POST /api/recipes/{id}/ratings` - Add rating to recipe

### Tags

- `GET /api/recipes/tags` - Get all available tags

## 📚 API Documentation

Interactive API documentation is available via Swagger UI when running in development mode:

**URL**: `https://localhost:7068/swagger`

Use this interface to:
- Explore all available endpoints
- Test API calls directly
- View request/response schemas
- Understand data models

## 🎨 Frontend Features

### Pages

1. **Home** (`/`)
   - Welcome page with feature highlights
   - Quick navigation to recipes

2. **Recipes** (`/recipes`)
   - Recipe card grid view
   - Search by ingredients
   - Filter and sort options
   - Detailed recipe modal view

### Recipe Card Information

- Recipe name and description
- Category badge
- Difficulty level
- Prep and cook times
- Average rating and review count
- Tags (dietary preferences)

### Recipe Details Modal

- Full ingredient list with quantities
- Step-by-step instructions
- Nutritional information
- User reviews and ratings
- Cooking time breakdown

## 🔧 Configuration

### API Configuration (appsettings.json)

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=recipes.db"
  }
}
```

### CORS Configuration

The API is configured to allow requests from:
- `https://localhost:5001`
- `http://localhost:5000`

Update the CORS policy in `Program.cs` if using different ports.

### Blazor Client Configuration

The HttpClient is configured to point to the API at `https://localhost:7068`. Update `Program.cs` if your API runs on a different port.

## 🧪 Development

### Adding New Recipes

Use the Swagger UI or create a POST request to `/api/recipes`:

```json
{
  "name": "Recipe Name",
  "description": "Description",
  "instructions": "Step by step instructions",
  "prepTimeMinutes": 15,
  "cookTimeMinutes": 30,
  "servings": 4,
  "difficultyLevel": 1,
  "category": "Category Name",
  "ingredients": [
    {
      "name": "Ingredient",
      "quantity": 1,
      "unit": "cup"
    }
  ],
  "nutritionInfo": {
    "calories": 350,
    "protein": 25,
    "carbohydrates": 40,
    "fat": 10,
    "fiber": 5
  },
  "tags": ["Tag1", "Tag2"]
}
```

### Database Reset

To reset the database and reload seed data:

1. Stop the API
2. Delete `recipes.db` file
3. Restart the API - database will be recreated

## 📦 NuGet Packages

### RecipeApp.Api

- Microsoft.EntityFrameworkCore.Sqlite (10.0.1)
- Microsoft.EntityFrameworkCore.Design (10.0.1)
- Swashbuckle.AspNetCore (10.0.1)

### RecipeApp.Client

- Microsoft.AspNetCore.Components.WebAssembly (10.0.0)

## 🎯 Future Enhancements

Potential features for future development:

- [ ] User authentication and authorization
- [ ] Image upload functionality
- [ ] Recipe favorites/bookmarks
- [ ] Shopping list generator
- [ ] Recipe categories filtering
- [ ] Advanced search with multiple criteria
- [ ] Recipe sharing via social media
- [ ] Print-friendly recipe view
- [ ] Meal planning calendar
- [ ] User recipe submissions

## 🤝 Contributing

This is a demo/learning project. Feel free to:

1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

## 📝 License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

## 🔒 Security

For information about security policies and reporting vulnerabilities, please see our [SECURITY.md](SECURITY.md) file.

## 👥 Author

Created as a student demo project showcasing full-stack .NET development.

## 🆘 Troubleshooting

### API Not Connecting

- Verify the API is running on `https://localhost:7068`
- Check CORS configuration in `Program.cs`
- Ensure no other process is using the ports

### Database Issues

- Delete `recipes.db` and restart the API
- Check file permissions in the API directory

### Build Errors

- Run `dotnet restore` to ensure all packages are installed
- Verify .NET 10 SDK is installed: `dotnet --version`
- Clean and rebuild: `dotnet clean && dotnet build`

### Blazor Client Issues

- Verify the HttpClient BaseAddress points to the correct API URL
- Check browser console for errors
- Clear browser cache and reload

## 📞 Support

For issues or questions:
1. Check the troubleshooting section
2. Review API documentation via Swagger
3. Check application logs in the terminal

---

**Happy Cooking! 🍳👨‍🍳**

using Microsoft.EntityFrameworkCore;
using RecipeApp.Api.Data;
using RecipeApp.Api.Repositories;
using RecipeApp.Shared.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure SQLite Database
builder.Services.AddDbContext<RecipeDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? "Data Source=recipes.db"));

// Register repositories
builder.Services.AddScoped<IRecipeRepository, RecipeRepository>();

// Configure CORS for Blazor WebAssembly
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorWasm", policy =>
    {
        policy.SetIsOriginAllowed(origin =>
              {
                  if (string.IsNullOrWhiteSpace(origin))
                      return false;

                  if (!Uri.TryCreate(origin, UriKind.Absolute, out var uri))
                      return false;

                  return uri.Host is "localhost" or "127.0.0.1";
              })
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Ensure database is created and seeded
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<RecipeDbContext>();
    dbContext.Database.EnsureCreated();

    if (!dbContext.Recipes.Any())
    {
        var tagQuickEasy = new Tag { Name = "Quick & Easy" };
        var tagHealthy = new Tag { Name = "Healthy" };
        var tagVegetarian = new Tag { Name = "Vegetarian" };

        dbContext.Tags.AddRange(tagQuickEasy, tagHealthy, tagVegetarian);
        dbContext.SaveChanges();

        var recipe1 = new Recipe
        {
            Name = "Avocado Toast",
            Description = "A simple breakfast with creamy avocado on toasted bread.",
            Instructions = "1. Toast bread\n2. Mash avocado with salt and lemon\n3. Spread on toast\n4. Top with pepper and optional chili flakes",
            PrepTimeMinutes = 5,
            CookTimeMinutes = 2,
            Servings = 1,
            DifficultyLevel = DifficultyLevel.Easy,
            Category = "Breakfast",
            CreatedDate = DateTime.UtcNow,
            Ingredients = new List<Ingredient>
            {
                new Ingredient { Name = "Bread", Quantity = 2, Unit = "slices" },
                new Ingredient { Name = "Avocado", Quantity = 1, Unit = "whole" },
                new Ingredient { Name = "Lemon juice", Quantity = 1, Unit = "tsp" },
                new Ingredient { Name = "Salt", Quantity = 0.25m, Unit = "tsp" }
            },
            RecipeTags = new List<RecipeTag>
            {
                new RecipeTag { TagId = tagQuickEasy.Id },
                new RecipeTag { TagId = tagVegetarian.Id }
            }
        };

        var recipe2 = new Recipe
        {
            Name = "Chicken & Veggie Rice Bowl",
            Description = "A quick weeknight bowl with chicken, vegetables, and rice.",
            Instructions = "1. Cook rice\n2. Sauté chicken until cooked\n3. Add vegetables and cook until tender\n4. Season and serve over rice",
            PrepTimeMinutes = 10,
            CookTimeMinutes = 15,
            Servings = 2,
            DifficultyLevel = DifficultyLevel.Easy,
            Category = "Main Course",
            CreatedDate = DateTime.UtcNow,
            Ingredients = new List<Ingredient>
            {
                new Ingredient { Name = "Chicken breast", Quantity = 300, Unit = "g" },
                new Ingredient { Name = "Rice", Quantity = 1, Unit = "cup" },
                new Ingredient { Name = "Broccoli", Quantity = 150, Unit = "g" },
                new Ingredient { Name = "Soy sauce", Quantity = 2, Unit = "tbsp" }
            },
            RecipeTags = new List<RecipeTag>
            {
                new RecipeTag { TagId = tagQuickEasy.Id },
                new RecipeTag { TagId = tagHealthy.Id }
            }
        };

        dbContext.Recipes.AddRange(recipe1, recipe2);
        dbContext.SaveChanges();
    }
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // For serving recipe images
app.UseCors("AllowBlazorWasm");
app.UseAuthorization();
app.MapControllers();

app.MapGet("/", (IHostEnvironment env) =>
{
    if (env.IsDevelopment())
    {
        return Results.Redirect("/swagger");
    }

    return Results.Text("RecipeApp API is running.");
});

app.Run();

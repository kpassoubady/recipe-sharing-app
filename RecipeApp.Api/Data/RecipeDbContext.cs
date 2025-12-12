using Microsoft.EntityFrameworkCore;
using RecipeApp.Shared.Models;

namespace RecipeApp.Api.Data;

public class RecipeDbContext : DbContext
{
    public RecipeDbContext(DbContextOptions<RecipeDbContext> options) : base(options)
    {
    }

    public DbSet<Recipe> Recipes => Set<Recipe>();
    public DbSet<Ingredient> Ingredients => Set<Ingredient>();
    public DbSet<NutritionInfo> NutritionInfos => Set<NutritionInfo>();
    public DbSet<Rating> Ratings => Set<Rating>();
    public DbSet<Tag> Tags => Set<Tag>();
    public DbSet<RecipeTag> RecipeTags => Set<RecipeTag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure RecipeTag many-to-many relationship
        modelBuilder.Entity<RecipeTag>()
            .HasKey(rt => new { rt.RecipeId, rt.TagId });

        modelBuilder.Entity<RecipeTag>()
            .HasOne(rt => rt.Recipe)
            .WithMany(r => r.RecipeTags)
            .HasForeignKey(rt => rt.RecipeId);

        modelBuilder.Entity<RecipeTag>()
            .HasOne(rt => rt.Tag)
            .WithMany(t => t.RecipeTags)
            .HasForeignKey(rt => rt.TagId);

        // Configure one-to-one relationship for NutritionInfo
        modelBuilder.Entity<Recipe>()
            .HasOne(r => r.NutritionInfo)
            .WithOne(n => n.Recipe)
            .HasForeignKey<NutritionInfo>(n => n.RecipeId);

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // Seed Tags
        var tags = new[]
        {
            new Tag { Id = 1, Name = "Vegetarian" },
            new Tag { Id = 2, Name = "Vegan" },
            new Tag { Id = 3, Name = "Gluten-Free" },
            new Tag { Id = 4, Name = "Dairy-Free" },
            new Tag { Id = 5, Name = "Quick & Easy" },
            new Tag { Id = 6, Name = "Comfort Food" },
            new Tag { Id = 7, Name = "Healthy" },
            new Tag { Id = 8, Name = "Dessert" },
            new Tag { Id = 9, Name = "Breakfast" },
            new Tag { Id = 10, Name = "Main Course" }
        };
        modelBuilder.Entity<Tag>().HasData(tags);

        // Seed Recipes
        var recipes = new[]
        {
            new Recipe
            {
                Id = 1,
                Name = "Classic Spaghetti Carbonara",
                Description = "A traditional Italian pasta dish with eggs, cheese, and bacon",
                Instructions = "1. Boil pasta according to package directions\n2. Fry bacon until crispy\n3. Mix eggs and cheese\n4. Combine hot pasta with bacon\n5. Add egg mixture and toss quickly\n6. Serve immediately with extra cheese",
                PrepTimeMinutes = 10,
                CookTimeMinutes = 20,
                Servings = 4,
                DifficultyLevel = DifficultyLevel.Easy,
                Category = "Italian",
                ImagePath = "/images/carbonara.jpg",
                CreatedDate = DateTime.Now.AddDays(-30)
            },
            new Recipe
            {
                Id = 2,
                Name = "Chicken Stir Fry",
                Description = "Quick and healthy Asian-inspired stir fry with vegetables",
                Instructions = "1. Cut chicken into bite-sized pieces\n2. Marinate chicken in soy sauce\n3. Heat oil in wok\n4. Stir fry chicken until cooked\n5. Add vegetables and cook until tender-crisp\n6. Add sauce and serve over rice",
                PrepTimeMinutes = 15,
                CookTimeMinutes = 15,
                Servings = 4,
                DifficultyLevel = DifficultyLevel.Easy,
                Category = "Asian",
                ImagePath = "/images/stirfry.jpg",
                CreatedDate = DateTime.Now.AddDays(-25)
            },
            new Recipe
            {
                Id = 3,
                Name = "Vegetarian Buddha Bowl",
                Description = "Nutritious bowl packed with quinoa, roasted vegetables, and tahini dressing",
                Instructions = "1. Cook quinoa according to package\n2. Roast chickpeas and vegetables at 400°F for 25 mins\n3. Prepare tahini dressing\n4. Assemble bowl with quinoa base\n5. Top with roasted veggies and chickpeas\n6. Drizzle with dressing",
                PrepTimeMinutes = 20,
                CookTimeMinutes = 30,
                Servings = 2,
                DifficultyLevel = DifficultyLevel.Medium,
                Category = "Healthy",
                ImagePath = "/images/buddha-bowl.jpg",
                CreatedDate = DateTime.Now.AddDays(-20)
            },
            new Recipe
            {
                Id = 4,
                Name = "Chocolate Chip Cookies",
                Description = "Classic homemade cookies that are crispy on the outside and chewy inside",
                Instructions = "1. Cream butter and sugars\n2. Add eggs and vanilla\n3. Mix in flour, baking soda, and salt\n4. Fold in chocolate chips\n5. Drop spoonfuls onto baking sheet\n6. Bake at 375°F for 10-12 minutes",
                PrepTimeMinutes = 15,
                CookTimeMinutes = 12,
                Servings = 24,
                DifficultyLevel = DifficultyLevel.Easy,
                Category = "Dessert",
                ImagePath = "/images/cookies.jpg",
                CreatedDate = DateTime.Now.AddDays(-15)
            },
            new Recipe
            {
                Id = 5,
                Name = "Beef Tacos",
                Description = "Flavorful Mexican-style tacos with seasoned ground beef",
                Instructions = "1. Brown ground beef in skillet\n2. Add taco seasoning and water\n3. Simmer until thickened\n4. Warm taco shells\n5. Fill shells with beef\n6. Top with lettuce, cheese, tomatoes, and sour cream",
                PrepTimeMinutes = 10,
                CookTimeMinutes = 15,
                Servings = 6,
                DifficultyLevel = DifficultyLevel.Easy,
                Category = "Mexican",
                ImagePath = "/images/tacos.jpg",
                CreatedDate = DateTime.Now.AddDays(-10)
            },
            new Recipe
            {
                Id = 6,
                Name = "Greek Salad",
                Description = "Fresh Mediterranean salad with feta cheese and olives",
                Instructions = "1. Chop cucumbers, tomatoes, and bell peppers\n2. Slice red onion thinly\n3. Combine vegetables in large bowl\n4. Add olives and feta cheese\n5. Drizzle with olive oil and lemon juice\n6. Season with oregano, salt, and pepper",
                PrepTimeMinutes = 15,
                CookTimeMinutes = 0,
                Servings = 4,
                DifficultyLevel = DifficultyLevel.Easy,
                Category = "Mediterranean",
                ImagePath = "/images/greek-salad.jpg",
                CreatedDate = DateTime.Now.AddDays(-8)
            },
            new Recipe
            {
                Id = 7,
                Name = "Pancakes",
                Description = "Fluffy breakfast pancakes perfect for weekend mornings",
                Instructions = "1. Mix dry ingredients in bowl\n2. Whisk wet ingredients separately\n3. Combine wet and dry ingredients\n4. Heat griddle to medium\n5. Pour 1/4 cup batter per pancake\n6. Flip when bubbles form, cook until golden",
                PrepTimeMinutes = 10,
                CookTimeMinutes = 15,
                Servings = 4,
                DifficultyLevel = DifficultyLevel.Easy,
                Category = "Breakfast",
                ImagePath = "/images/pancakes.jpg",
                CreatedDate = DateTime.Now.AddDays(-5)
            },
            new Recipe
            {
                Id = 8,
                Name = "Beef Wellington",
                Description = "Elegant beef tenderloin wrapped in puff pastry",
                Instructions = "1. Sear beef tenderloin on all sides\n2. Brush with mustard\n3. Wrap in prosciutto and mushroom duxelles\n4. Encase in puff pastry\n5. Brush with egg wash\n6. Bake at 425°F for 25-30 minutes",
                PrepTimeMinutes = 45,
                CookTimeMinutes = 30,
                Servings = 6,
                DifficultyLevel = DifficultyLevel.Hard,
                Category = "British",
                ImagePath = "/images/wellington.jpg",
                CreatedDate = DateTime.Now.AddDays(-3)
            },
            new Recipe
            {
                Id = 9,
                Name = "Thai Green Curry",
                Description = "Aromatic and spicy Thai curry with coconut milk",
                Instructions = "1. Heat oil and fry curry paste\n2. Add coconut milk and bring to simmer\n3. Add chicken and vegetables\n4. Simmer until chicken is cooked\n5. Add fish sauce and sugar\n6. Garnish with basil and serve with rice",
                PrepTimeMinutes = 15,
                CookTimeMinutes = 25,
                Servings = 4,
                DifficultyLevel = DifficultyLevel.Medium,
                Category = "Thai",
                ImagePath = "/images/green-curry.jpg",
                CreatedDate = DateTime.Now.AddDays(-2)
            },
            new Recipe
            {
                Id = 10,
                Name = "Caprese Salad",
                Description = "Simple Italian salad with fresh mozzarella, tomatoes, and basil",
                Instructions = "1. Slice tomatoes and mozzarella\n2. Arrange alternating slices on plate\n3. Tuck basil leaves between slices\n4. Drizzle with olive oil\n5. Season with salt and pepper\n6. Add balsamic glaze if desired",
                PrepTimeMinutes = 10,
                CookTimeMinutes = 0,
                Servings = 4,
                DifficultyLevel = DifficultyLevel.Easy,
                Category = "Italian",
                ImagePath = "/images/caprese.jpg",
                CreatedDate = DateTime.Now.AddDays(-1)
            }
        };
        modelBuilder.Entity<Recipe>().HasData(recipes);

        // Seed Ingredients
        var ingredients = new List<Ingredient>
        {
            // Carbonara (1)
            new Ingredient { Id = 1, RecipeId = 1, Name = "Spaghetti", Quantity = 400, Unit = "g" },
            new Ingredient { Id = 2, RecipeId = 1, Name = "Bacon", Quantity = 200, Unit = "g" },
            new Ingredient { Id = 3, RecipeId = 1, Name = "Eggs", Quantity = 4, Unit = "whole" },
            new Ingredient { Id = 4, RecipeId = 1, Name = "Parmesan Cheese", Quantity = 100, Unit = "g" },
            new Ingredient { Id = 5, RecipeId = 1, Name = "Black Pepper", Quantity = 1, Unit = "tsp" },
            
            // Chicken Stir Fry (2)
            new Ingredient { Id = 6, RecipeId = 2, Name = "Chicken Breast", Quantity = 500, Unit = "g" },
            new Ingredient { Id = 7, RecipeId = 2, Name = "Bell Peppers", Quantity = 2, Unit = "whole" },
            new Ingredient { Id = 8, RecipeId = 2, Name = "Broccoli", Quantity = 200, Unit = "g" },
            new Ingredient { Id = 9, RecipeId = 2, Name = "Soy Sauce", Quantity = 3, Unit = "tbsp" },
            new Ingredient { Id = 10, RecipeId = 2, Name = "Garlic", Quantity = 3, Unit = "cloves" },
            
            // Buddha Bowl (3)
            new Ingredient { Id = 11, RecipeId = 3, Name = "Quinoa", Quantity = 200, Unit = "g" },
            new Ingredient { Id = 12, RecipeId = 3, Name = "Chickpeas", Quantity = 400, Unit = "g" },
            new Ingredient { Id = 13, RecipeId = 3, Name = "Sweet Potato", Quantity = 2, Unit = "medium" },
            new Ingredient { Id = 14, RecipeId = 3, Name = "Kale", Quantity = 100, Unit = "g" },
            new Ingredient { Id = 15, RecipeId = 3, Name = "Tahini", Quantity = 3, Unit = "tbsp" },
            
            // Chocolate Chip Cookies (4)
            new Ingredient { Id = 16, RecipeId = 4, Name = "Butter", Quantity = 225, Unit = "g" },
            new Ingredient { Id = 17, RecipeId = 4, Name = "Brown Sugar", Quantity = 200, Unit = "g" },
            new Ingredient { Id = 18, RecipeId = 4, Name = "All-Purpose Flour", Quantity = 280, Unit = "g" },
            new Ingredient { Id = 19, RecipeId = 4, Name = "Chocolate Chips", Quantity = 300, Unit = "g" },
            new Ingredient { Id = 20, RecipeId = 4, Name = "Eggs", Quantity = 2, Unit = "whole" },
            
            // Beef Tacos (5)
            new Ingredient { Id = 21, RecipeId = 5, Name = "Ground Beef", Quantity = 500, Unit = "g" },
            new Ingredient { Id = 22, RecipeId = 5, Name = "Taco Shells", Quantity = 12, Unit = "shells" },
            new Ingredient { Id = 23, RecipeId = 5, Name = "Lettuce", Quantity = 1, Unit = "head" },
            new Ingredient { Id = 24, RecipeId = 5, Name = "Cheddar Cheese", Quantity = 200, Unit = "g" },
            new Ingredient { Id = 25, RecipeId = 5, Name = "Tomatoes", Quantity = 2, Unit = "whole" },
            
            // Greek Salad (6)
            new Ingredient { Id = 26, RecipeId = 6, Name = "Cucumber", Quantity = 2, Unit = "whole" },
            new Ingredient { Id = 27, RecipeId = 6, Name = "Tomatoes", Quantity = 4, Unit = "whole" },
            new Ingredient { Id = 28, RecipeId = 6, Name = "Feta Cheese", Quantity = 200, Unit = "g" },
            new Ingredient { Id = 29, RecipeId = 6, Name = "Kalamata Olives", Quantity = 100, Unit = "g" },
            new Ingredient { Id = 30, RecipeId = 6, Name = "Red Onion", Quantity = 1, Unit = "whole" },
            
            // Pancakes (7)
            new Ingredient { Id = 31, RecipeId = 7, Name = "All-Purpose Flour", Quantity = 250, Unit = "g" },
            new Ingredient { Id = 32, RecipeId = 7, Name = "Milk", Quantity = 350, Unit = "ml" },
            new Ingredient { Id = 33, RecipeId = 7, Name = "Eggs", Quantity = 2, Unit = "whole" },
            new Ingredient { Id = 34, RecipeId = 7, Name = "Baking Powder", Quantity = 2, Unit = "tsp" },
            new Ingredient { Id = 35, RecipeId = 7, Name = "Sugar", Quantity = 2, Unit = "tbsp" },
            
            // Beef Wellington (8)
            new Ingredient { Id = 36, RecipeId = 8, Name = "Beef Tenderloin", Quantity = 800, Unit = "g" },
            new Ingredient { Id = 37, RecipeId = 8, Name = "Puff Pastry", Quantity = 500, Unit = "g" },
            new Ingredient { Id = 38, RecipeId = 8, Name = "Mushrooms", Quantity = 400, Unit = "g" },
            new Ingredient { Id = 39, RecipeId = 8, Name = "Prosciutto", Quantity = 100, Unit = "g" },
            new Ingredient { Id = 40, RecipeId = 8, Name = "Dijon Mustard", Quantity = 2, Unit = "tbsp" },
            
            // Thai Green Curry (9)
            new Ingredient { Id = 41, RecipeId = 9, Name = "Chicken Thighs", Quantity = 500, Unit = "g" },
            new Ingredient { Id = 42, RecipeId = 9, Name = "Coconut Milk", Quantity = 400, Unit = "ml" },
            new Ingredient { Id = 43, RecipeId = 9, Name = "Green Curry Paste", Quantity = 3, Unit = "tbsp" },
            new Ingredient { Id = 44, RecipeId = 9, Name = "Thai Basil", Quantity = 1, Unit = "bunch" },
            new Ingredient { Id = 45, RecipeId = 9, Name = "Fish Sauce", Quantity = 2, Unit = "tbsp" },
            
            // Caprese Salad (10)
            new Ingredient { Id = 46, RecipeId = 10, Name = "Fresh Mozzarella", Quantity = 250, Unit = "g" },
            new Ingredient { Id = 47, RecipeId = 10, Name = "Tomatoes", Quantity = 4, Unit = "large" },
            new Ingredient { Id = 48, RecipeId = 10, Name = "Fresh Basil", Quantity = 1, Unit = "bunch" },
            new Ingredient { Id = 49, RecipeId = 10, Name = "Olive Oil", Quantity = 3, Unit = "tbsp" },
            new Ingredient { Id = 50, RecipeId = 10, Name = "Balsamic Vinegar", Quantity = 2, Unit = "tbsp" }
        };
        modelBuilder.Entity<Ingredient>().HasData(ingredients);

        // Seed Nutrition Info
        var nutritionInfos = new[]
        {
            new NutritionInfo { Id = 1, RecipeId = 1, Calories = 520, Protein = 22m, Carbohydrates = 65m, Fat = 18m, Fiber = 3m },
            new NutritionInfo { Id = 2, RecipeId = 2, Calories = 350, Protein = 35m, Carbohydrates = 25m, Fat = 12m, Fiber = 5m },
            new NutritionInfo { Id = 3, RecipeId = 3, Calories = 450, Protein = 18m, Carbohydrates = 55m, Fat = 16m, Fiber = 12m },
            new NutritionInfo { Id = 4, RecipeId = 4, Calories = 150, Protein = 2m, Carbohydrates = 20m, Fat = 8m, Fiber = 1m },
            new NutritionInfo { Id = 5, RecipeId = 5, Calories = 380, Protein = 25m, Carbohydrates = 30m, Fat = 18m, Fiber = 4m },
            new NutritionInfo { Id = 6, RecipeId = 6, Calories = 220, Protein = 10m, Carbohydrates = 12m, Fat = 15m, Fiber = 3m },
            new NutritionInfo { Id = 7, RecipeId = 7, Calories = 280, Protein = 8m, Carbohydrates = 45m, Fat = 7m, Fiber = 2m },
            new NutritionInfo { Id = 8, RecipeId = 8, Calories = 620, Protein = 42m, Carbohydrates = 35m, Fat = 32m, Fiber = 2m },
            new NutritionInfo { Id = 9, RecipeId = 9, Calories = 420, Protein = 28m, Carbohydrates = 18m, Fat = 26m, Fiber = 4m },
            new NutritionInfo { Id = 10, RecipeId = 10, Calories = 280, Protein = 16m, Carbohydrates = 8m, Fat = 20m, Fiber = 2m }
        };
        modelBuilder.Entity<NutritionInfo>().HasData(nutritionInfos);

        // Seed Ratings
        var ratings = new List<Rating>
        {
            new Rating { Id = 1, RecipeId = 1, Score = 5, Review = "Amazing! Just like in Italy!", ReviewerName = "John Doe", ReviewDate = DateTime.Now.AddDays(-5) },
            new Rating { Id = 2, RecipeId = 1, Score = 4, Review = "Very good, but I added more bacon", ReviewerName = "Jane Smith", ReviewDate = DateTime.Now.AddDays(-3) },
            new Rating { Id = 3, RecipeId = 2, Score = 5, Review = "Quick and healthy weeknight meal", ReviewerName = "Bob Wilson", ReviewDate = DateTime.Now.AddDays(-4) },
            new Rating { Id = 4, RecipeId = 2, Score = 4, Review = "Tasty! Used tofu instead of chicken", ReviewerName = "Alice Brown", ReviewDate = DateTime.Now.AddDays(-2) },
            new Rating { Id = 5, RecipeId = 3, Score = 5, Review = "Love this bowl! So nutritious", ReviewerName = "Sarah Lee", ReviewDate = DateTime.Now.AddDays(-6) },
            new Rating { Id = 6, RecipeId = 4, Score = 5, Review = "Best cookies ever!", ReviewerName = "Mike Johnson", ReviewDate = DateTime.Now.AddDays(-7) },
            new Rating { Id = 7, RecipeId = 4, Score = 5, Review = "My kids love these", ReviewerName = "Emily Davis", ReviewDate = DateTime.Now.AddDays(-1) },
            new Rating { Id = 8, RecipeId = 5, Score = 4, Review = "Great taco night recipe", ReviewerName = "Chris Martinez", ReviewDate = DateTime.Now.AddDays(-3) },
            new Rating { Id = 9, RecipeId = 6, Score = 5, Review = "Fresh and delicious", ReviewerName = "Anna White", ReviewDate = DateTime.Now.AddDays(-2) },
            new Rating { Id = 10, RecipeId = 7, Score = 5, Review = "Fluffy and perfect!", ReviewerName = "David Clark", ReviewDate = DateTime.Now.AddDays(-1) },
            new Rating { Id = 11, RecipeId = 8, Score = 4, Review = "Impressive but challenging", ReviewerName = "Lisa Taylor", ReviewDate = DateTime.Now.AddHours(-12) },
            new Rating { Id = 12, RecipeId = 9, Score = 5, Review = "Authentic Thai flavor!", ReviewerName = "Tom Anderson", ReviewDate = DateTime.Now.AddHours(-6) },
            new Rating { Id = 13, RecipeId = 10, Score = 5, Review = "Simple and elegant", ReviewerName = "Maria Garcia", ReviewDate = DateTime.Now.AddHours(-3) }
        };
        modelBuilder.Entity<Rating>().HasData(ratings);

        // Seed RecipeTags
        var recipeTags = new[]
        {
            new RecipeTag { RecipeId = 1, TagId = 6 }, // Carbonara - Comfort Food
            new RecipeTag { RecipeId = 1, TagId = 10 }, // Carbonara - Main Course
            new RecipeTag { RecipeId = 2, TagId = 5 }, // Stir Fry - Quick & Easy
            new RecipeTag { RecipeId = 2, TagId = 7 }, // Stir Fry - Healthy
            new RecipeTag { RecipeId = 2, TagId = 10 }, // Stir Fry - Main Course
            new RecipeTag { RecipeId = 3, TagId = 1 }, // Buddha Bowl - Vegetarian
            new RecipeTag { RecipeId = 3, TagId = 2 }, // Buddha Bowl - Vegan
            new RecipeTag { RecipeId = 3, TagId = 7 }, // Buddha Bowl - Healthy
            new RecipeTag { RecipeId = 4, TagId = 8 }, // Cookies - Dessert
            new RecipeTag { RecipeId = 5, TagId = 5 }, // Tacos - Quick & Easy
            new RecipeTag { RecipeId = 5, TagId = 10 }, // Tacos - Main Course
            new RecipeTag { RecipeId = 6, TagId = 1 }, // Greek Salad - Vegetarian
            new RecipeTag { RecipeId = 6, TagId = 3 }, // Greek Salad - Gluten-Free
            new RecipeTag { RecipeId = 6, TagId = 7 }, // Greek Salad - Healthy
            new RecipeTag { RecipeId = 7, TagId = 9 }, // Pancakes - Breakfast
            new RecipeTag { RecipeId = 7, TagId = 1 }, // Pancakes - Vegetarian
            new RecipeTag { RecipeId = 8, TagId = 10 }, // Wellington - Main Course
            new RecipeTag { RecipeId = 9, TagId = 10 }, // Thai Curry - Main Course
            new RecipeTag { RecipeId = 9, TagId = 4 }, // Thai Curry - Dairy-Free
            new RecipeTag { RecipeId = 10, TagId = 1 }, // Caprese - Vegetarian
            new RecipeTag { RecipeId = 10, TagId = 3 }, // Caprese - Gluten-Free
            new RecipeTag { RecipeId = 10, TagId = 5 } // Caprese - Quick & Easy
        };
        modelBuilder.Entity<RecipeTag>().HasData(recipeTags);
    }
}

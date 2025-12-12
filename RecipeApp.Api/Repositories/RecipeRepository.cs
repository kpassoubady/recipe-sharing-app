using Microsoft.EntityFrameworkCore;
using RecipeApp.Api.Data;
using RecipeApp.Shared.Models;

namespace RecipeApp.Api.Repositories;

public interface IRecipeRepository
{
    Task<List<Recipe>> GetAllRecipesAsync();
    Task<Recipe?> GetRecipeByIdAsync(int id);
    Task<List<Recipe>> SearchRecipesByIngredientsAsync(string ingredients);
    Task<Recipe> CreateRecipeAsync(Recipe recipe);
    Task<Recipe?> UpdateRecipeAsync(Recipe recipe);
    Task<bool> DeleteRecipeAsync(int id);
    Task<Rating> AddRatingAsync(Rating rating);
    Task<List<Tag>> GetAllTagsAsync();
}

public class RecipeRepository : IRecipeRepository
{
    private readonly RecipeDbContext _context;

    public RecipeRepository(RecipeDbContext context)
    {
        _context = context;
    }

    public async Task<List<Recipe>> GetAllRecipesAsync()
    {
        return await _context.Recipes
            .Include(r => r.Ingredients)
            .Include(r => r.NutritionInfo)
            .Include(r => r.Ratings)
            .Include(r => r.RecipeTags)
                .ThenInclude(rt => rt.Tag)
            .ToListAsync();
    }

    public async Task<Recipe?> GetRecipeByIdAsync(int id)
    {
        return await _context.Recipes
            .Include(r => r.Ingredients)
            .Include(r => r.NutritionInfo)
            .Include(r => r.Ratings)
            .Include(r => r.RecipeTags)
                .ThenInclude(rt => rt.Tag)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<List<Recipe>> SearchRecipesByIngredientsAsync(string searchTerms)
    {
        var terms = searchTerms.Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(t => t.Trim().ToLower())
            .ToList();

        var recipes = await _context.Recipes
            .Include(r => r.Ingredients)
            .Include(r => r.NutritionInfo)
            .Include(r => r.Ratings)
            .Include(r => r.RecipeTags)
                .ThenInclude(rt => rt.Tag)
            .ToListAsync();

        return recipes.Where(r => 
            terms.Any(term => 
                r.Name.ToLower().Contains(term) ||
                r.Description.ToLower().Contains(term) ||
                r.Ingredients.Any(i => i.Name.ToLower().Contains(term))))
            .ToList();
    }

    public async Task<Recipe> CreateRecipeAsync(Recipe recipe)
    {
        recipe.CreatedDate = DateTime.Now;
        _context.Recipes.Add(recipe);
        await _context.SaveChangesAsync();
        return recipe;
    }

    public async Task<Recipe?> UpdateRecipeAsync(Recipe recipe)
    {
        var existing = await _context.Recipes
            .Include(r => r.Ingredients)
            .Include(r => r.NutritionInfo)
            .Include(r => r.RecipeTags)
            .FirstOrDefaultAsync(r => r.Id == recipe.Id);

        if (existing == null)
            return null;

        existing.Name = recipe.Name;
        existing.Description = recipe.Description;
        existing.Instructions = recipe.Instructions;
        existing.PrepTimeMinutes = recipe.PrepTimeMinutes;
        existing.CookTimeMinutes = recipe.CookTimeMinutes;
        existing.Servings = recipe.Servings;
        existing.DifficultyLevel = recipe.DifficultyLevel;
        existing.Category = recipe.Category;

        // Update ingredients
        _context.Ingredients.RemoveRange(existing.Ingredients);
        existing.Ingredients = recipe.Ingredients;

        // Update nutrition info
        if (existing.NutritionInfo != null && recipe.NutritionInfo != null)
        {
            existing.NutritionInfo.Calories = recipe.NutritionInfo.Calories;
            existing.NutritionInfo.Protein = recipe.NutritionInfo.Protein;
            existing.NutritionInfo.Carbohydrates = recipe.NutritionInfo.Carbohydrates;
            existing.NutritionInfo.Fat = recipe.NutritionInfo.Fat;
            existing.NutritionInfo.Fiber = recipe.NutritionInfo.Fiber;
        }

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeleteRecipeAsync(int id)
    {
        var recipe = await _context.Recipes.FindAsync(id);
        if (recipe == null)
            return false;

        _context.Recipes.Remove(recipe);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<Rating> AddRatingAsync(Rating rating)
    {
        rating.ReviewDate = DateTime.Now;
        _context.Ratings.Add(rating);
        await _context.SaveChangesAsync();
        return rating;
    }

    public async Task<List<Tag>> GetAllTagsAsync()
    {
        return await _context.Tags.ToListAsync();
    }
}

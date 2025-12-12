using Microsoft.AspNetCore.Mvc;
using RecipeApp.Api.Repositories;
using RecipeApp.Shared.DTOs;
using RecipeApp.Shared.Models;

namespace RecipeApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecipesController : ControllerBase
{
    private readonly IRecipeRepository _repository;

    public RecipesController(IRecipeRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<ActionResult<List<RecipeDto>>> GetAllRecipes()
    {
        var recipes = await _repository.GetAllRecipesAsync();
        var recipeDtos = recipes.Select(MapToDto).ToList();
        return Ok(recipeDtos);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<RecipeDto>> GetRecipe(int id)
    {
        var recipe = await _repository.GetRecipeByIdAsync(id);
        if (recipe == null)
            return NotFound();

        return Ok(MapToDto(recipe));
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<RecipeDto>>> SearchRecipes([FromQuery] string ingredients)
    {
        if (string.IsNullOrWhiteSpace(ingredients))
            return BadRequest("Ingredients parameter is required");

        var recipes = await _repository.SearchRecipesByIngredientsAsync(ingredients);
        var recipeDtos = recipes.Select(MapToDto).ToList();
        return Ok(recipeDtos);
    }

    [HttpPost]
    public async Task<ActionResult<RecipeDto>> CreateRecipe([FromBody] CreateRecipeDto createDto)
    {
        var recipe = new Recipe
        {
            Name = createDto.Name,
            Description = createDto.Description,
            Instructions = createDto.Instructions,
            PrepTimeMinutes = createDto.PrepTimeMinutes,
            CookTimeMinutes = createDto.CookTimeMinutes,
            Servings = createDto.Servings,
            DifficultyLevel = createDto.DifficultyLevel,
            Category = createDto.Category,
            Ingredients = createDto.Ingredients.Select(i => new Ingredient
            {
                Name = i.Name,
                Quantity = i.Quantity,
                Unit = i.Unit
            }).ToList()
        };

        if (createDto.NutritionInfo != null)
        {
            recipe.NutritionInfo = new NutritionInfo
            {
                Calories = createDto.NutritionInfo.Calories,
                Protein = createDto.NutritionInfo.Protein,
                Carbohydrates = createDto.NutritionInfo.Carbohydrates,
                Fat = createDto.NutritionInfo.Fat,
                Fiber = createDto.NutritionInfo.Fiber
            };
        }

        var created = await _repository.CreateRecipeAsync(recipe);
        var recipeDto = MapToDto(created);
        return CreatedAtAction(nameof(GetRecipe), new { id = created.Id }, recipeDto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<RecipeDto>> UpdateRecipe(int id, [FromBody] CreateRecipeDto updateDto)
    {
        var recipe = new Recipe
        {
            Id = id,
            Name = updateDto.Name,
            Description = updateDto.Description,
            Instructions = updateDto.Instructions,
            PrepTimeMinutes = updateDto.PrepTimeMinutes,
            CookTimeMinutes = updateDto.CookTimeMinutes,
            Servings = updateDto.Servings,
            DifficultyLevel = updateDto.DifficultyLevel,
            Category = updateDto.Category,
            Ingredients = updateDto.Ingredients.Select(i => new Ingredient
            {
                Name = i.Name,
                Quantity = i.Quantity,
                Unit = i.Unit,
                RecipeId = id
            }).ToList()
        };

        if (updateDto.NutritionInfo != null)
        {
            recipe.NutritionInfo = new NutritionInfo
            {
                RecipeId = id,
                Calories = updateDto.NutritionInfo.Calories,
                Protein = updateDto.NutritionInfo.Protein,
                Carbohydrates = updateDto.NutritionInfo.Carbohydrates,
                Fat = updateDto.NutritionInfo.Fat,
                Fiber = updateDto.NutritionInfo.Fiber
            };
        }

        var updated = await _repository.UpdateRecipeAsync(recipe);
        if (updated == null)
            return NotFound();

        return Ok(MapToDto(updated));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecipe(int id)
    {
        var success = await _repository.DeleteRecipeAsync(id);
        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpPost("{id}/ratings")]
    public async Task<ActionResult<RatingDto>> AddRating(int id, [FromBody] CreateRatingDto ratingDto)
    {
        var rating = new Rating
        {
            RecipeId = id,
            Score = ratingDto.Score,
            Review = ratingDto.Review,
            ReviewerName = ratingDto.ReviewerName
        };

        var created = await _repository.AddRatingAsync(rating);
        var dto = new RatingDto
        {
            Id = created.Id,
            Score = created.Score,
            Review = created.Review,
            ReviewerName = created.ReviewerName,
            ReviewDate = created.ReviewDate
        };

        return CreatedAtAction(nameof(GetRecipe), new { id = created.RecipeId }, dto);
    }

    [HttpGet("tags")]
    public async Task<ActionResult<List<string>>> GetAllTags()
    {
        var tags = await _repository.GetAllTagsAsync();
        return Ok(tags.Select(t => t.Name).ToList());
    }

    private static RecipeDto MapToDto(Recipe recipe)
    {
        return new RecipeDto
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Description = recipe.Description,
            Instructions = recipe.Instructions,
            PrepTimeMinutes = recipe.PrepTimeMinutes,
            CookTimeMinutes = recipe.CookTimeMinutes,
            Servings = recipe.Servings,
            DifficultyLevel = recipe.DifficultyLevel,
            Category = recipe.Category,
            ImagePath = recipe.ImagePath,
            CreatedDate = recipe.CreatedDate,
            AverageRating = recipe.Ratings.Any() ? recipe.Ratings.Average(r => r.Score) : 0,
            RatingCount = recipe.Ratings.Count,
            Ingredients = recipe.Ingredients.Select(i => new IngredientDto
            {
                Id = i.Id,
                Name = i.Name,
                Quantity = i.Quantity,
                Unit = i.Unit
            }).ToList(),
            NutritionInfo = recipe.NutritionInfo != null ? new NutritionInfoDto
            {
                Calories = recipe.NutritionInfo.Calories,
                Protein = recipe.NutritionInfo.Protein,
                Carbohydrates = recipe.NutritionInfo.Carbohydrates,
                Fat = recipe.NutritionInfo.Fat,
                Fiber = recipe.NutritionInfo.Fiber
            } : null,
            Ratings = recipe.Ratings.Select(r => new RatingDto
            {
                Id = r.Id,
                Score = r.Score,
                Review = r.Review,
                ReviewerName = r.ReviewerName,
                ReviewDate = r.ReviewDate
            }).ToList(),
            Tags = recipe.RecipeTags.Select(rt => rt.Tag!.Name).ToList()
        };
    }
}

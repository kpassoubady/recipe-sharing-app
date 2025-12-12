using System.Net.Http.Json;
using RecipeApp.Shared.DTOs;

namespace RecipeApp.Client.Services;

public class RecipeService
{
    private readonly HttpClient _httpClient;

    public RecipeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<RecipeDto>> GetAllRecipesAsync(CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<List<RecipeDto>>("api/recipes", cancellationToken) ?? new List<RecipeDto>();
    }

    public async Task<RecipeDto?> GetRecipeByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<RecipeDto>($"api/recipes/{id}", cancellationToken);
    }

    public async Task<List<RecipeDto>> SearchRecipesByIngredientsAsync(string ingredients, CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<List<RecipeDto>>($"api/recipes/search?ingredients={ingredients}", cancellationToken) ?? new List<RecipeDto>();
    }

    public async Task<RecipeDto?> CreateRecipeAsync(CreateRecipeDto recipe)
    {
        var response = await _httpClient.PostAsJsonAsync("api/recipes", recipe);
        return await response.Content.ReadFromJsonAsync<RecipeDto>();
    }

    public async Task<RecipeDto?> UpdateRecipeAsync(int id, CreateRecipeDto recipe)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/recipes/{id}", recipe);
        return await response.Content.ReadFromJsonAsync<RecipeDto>();
    }

    public async Task<bool> DeleteRecipeAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"api/recipes/{id}");
        return response.IsSuccessStatusCode;
    }

    public async Task<RatingDto?> AddRatingAsync(int recipeId, CreateRatingDto rating)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/recipes/{recipeId}/ratings", rating);
        return await response.Content.ReadFromJsonAsync<RatingDto>();
    }

    public async Task<List<string>> GetAllTagsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<string>>("api/recipes/tags") ?? new List<string>();
    }
}

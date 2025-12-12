using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RecipeApp.Client;
using RecipeApp.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Configure HttpClient to point to the API
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5111") });

// Register RecipeService
builder.Services.AddScoped<RecipeService>();

await builder.Build().RunAsync();

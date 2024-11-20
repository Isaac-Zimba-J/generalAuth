using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Core;
using MudBlazor.Services;
using Shared.Services;
using Shared.Services.Contracts;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddMudServices();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7004") });

await builder.Build().RunAsync();
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ServerOne.Data;
using Shared;
using Shared.Services;
using UserService = ServerOne.Services.UserService;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddHttpClient();
builder.Services.AddAuthorizationBuilder();

builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    }));

// all services 
builder.Services.AddScoped<IUserService, UserService>();

// connection strting 
var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
if (connectionString == null)
    throw new Exception("Invalid connections string");
builder.Services.AddDbContext<BaseDbContext>(
    options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));


// boilerplate identity stuff 

builder.Services.AddAuthorizationBuilder();
// builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//     .AddRoleManager<RoleManager<IdentityRole>>()
//     .AddEntityFrameworkStores<AcademicProjectDbContext>()
//     .AddDefaultTokenProviders()
//     .AddApiEndpoints();

builder.Services
    .AddIdentityApiEndpoints<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddEntityFrameworkStores<BaseDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders()
    .AddApiEndpoints();

// add CORS policy for Wasm client
builder.Services.AddCors(
    options => options.AddPolicy(
        "wasm",
        policy => policy.WithOrigins(
                "https://localhost:7032"
            )
            .AllowAnyMethod()
            .SetIsOriginAllowed(pol => true)
            .AllowAnyHeader()
            .AllowCredentials()));
builder.Services.AddAuthorization();



var app = builder.Build();

// seed roles 
// Apply migrations at startup
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<BaseDbContext>();
    dbContext.Database.Migrate();

    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await RoleSeeder.SeedRolesAsync(roleManager);
}


app.MapIdentityApi<ApplicationUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("wasm");

app.UseHttpsRedirection();
app.MapControllers();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();


//use authorization
app.UseAuthorization();

app.Run();

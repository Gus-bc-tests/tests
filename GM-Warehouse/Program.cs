using System.Reflection;
using System.Text;
using GM_Warehouse.Components;
using GM_Warehouse.Components.Mappings;
using GM_Warehouse.Components.Services;
using GM_Warehouse.Data;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using MudBlazor.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using GM_Warehouse.Middleware;


var builder = WebApplication.CreateBuilder(args);

// Get the connection string from the configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddServerSideBlazor();

// Register DbContext for dependency injection
builder.Services.AddDbContext<DataBaseContext>(options =>
    options.UseSqlite(connectionString)); // Use the connection string from the config

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddProfile<CustomerMappingProfile>();
    cfg.AddProfile<OrderItemMappingProfile>();
    cfg.AddProfile<OrderMappingProfile>();
    cfg.AddProfile<ProductMappingProfile>();
    cfg.AddProfile<SupplierMappingProfile>();
    cfg.AddProfile<SupplierOrdersMappingProfile>();
    cfg.AddProfile<SupplierOrderItemMappingProfile>();
    cfg.AddProfile<UserMappingProfile>();
});

// Add any other required services (e.g., authentication, logging, etc.)
builder.Services.AddScoped<CustomerService>();          // Register CustomerService
builder.Services.AddScoped<UserService>();              // Register UserService
builder.Services.AddScoped<SupplierService>();          // Register SupplierService
builder.Services.AddScoped<ProductService>();           // Register ProductService
builder.Services.AddScoped<OrderService>();             // Register OrderService

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "YourIssuer",
            ValidAudience = "YourAudience",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSecretKey"))
        };
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("SalesmanOnly", policy => policy.RequireClaim("Role", "Salesman"));
});


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "GM-Warehouse API",
        Description = "An ASP.NET Core Web API for managing the warehouse",
        TermsOfService = new Uri("https://example.com/terms"),
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://github.com/me-shaon/GLWTPL/blob/master/LICENSE")
        }
    });

    // Define API Key scheme
    options.AddSecurityDefinition("ApiKey", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "X-API-KEY",
        Type = SecuritySchemeType.ApiKey,
        Description = "API Key needed to access the endpoints. Example: X-API-KEY: TestKey"
    });

    // Add security requirement for API Key
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "ApiKey"
                }
            },
            new string[] {}
        }
    });
});


builder.Services
    .AddMudServices()
    .AddMudBlazorKeyInterceptor();

StaticWebAssetsLoader.UseStaticWebAssets(builder.Environment, builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.Use(async (context, next) =>
{
    // Enforce API Key for Swagger endpoints
    if (context.Request.Path.StartsWithSegments("/api"))
    {
        if (!context.Request.Headers.TryGetValue("X-API-KEY", out var extractedApiKey) || extractedApiKey != "TestKey")
        {
            context.Response.StatusCode = 401; // Unauthorized
            await context.Response.WriteAsync("Unauthorized - Missing or invalid API Key for Swagger.");
            return;
        }
    }

    await next();
});

app.UseAuthentication();
app.UseAuthorization();

app.UseStaticFiles();

app.UseRouting();


// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios.
    app.UseHsts();
}
else
{
    app.UseExceptionHandler("/Error");
    app.Use(async (context, next) =>
    {
        if (context.Request.Path.StartsWithSegments("/api"))
        {
            if (!context.Request.Headers.TryGetValue("X-API-KEY", out var extractedApiKey) || extractedApiKey != "TestKey")
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized - Missing or invalid API Key for Swagger.");
                return;
            }
        }

        await next();
    });

    app.UseSwagger();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/swagger/v1/swagger.json", "GM-Warehouse API"));
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

// Map Razor Components and set up interactive server render mode
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.MapControllers();
app.MapRazorPages();

// Run the app
app.Run();
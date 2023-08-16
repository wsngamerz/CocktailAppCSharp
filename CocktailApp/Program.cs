using System.Reflection;
using CocktailApp.Auth;
using CocktailApp.Data;
using CocktailApp.Models;
using CocktailApp.Repositories;
using CocktailApp.Repositories.Abstractions;
using CocktailApp.Services;
using CocktailApp.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();

    builder.Services.AddDbContext<CocktailAppContext>(options =>
    {
        options.UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection")
        );
    });

    builder.Services.AddSingleton<ISupabaseService>(new SupabaseService(
        builder.Configuration["Supabase:Url"] ?? string.Empty,
        builder.Configuration["Supabase:Key"] ?? string.Empty
    ));

    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearerConfiguration(builder.Configuration["Supabase:JwtSecret"] ?? string.Empty);

    builder.Services.AddScoped<IBarItemRepository, BarItemRepository>();
    builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
    builder.Services.AddScoped<ICocktailRepository, CocktailRepository>();
    builder.Services.AddScoped<IIngredientRepository, IngredientRepository>();
    builder.Services.AddScoped<IUserRepository, UserRepository>();

    builder.Services.AddScoped<IFavouriteRepository<CocktailFavourite>, CocktailFavouriteRepository>();
    builder.Services.AddScoped<IFavouriteRepository<CocktailListFavourite>, CocktailListFavouriteRepository>();

    builder.Services.AddScoped<ICategoryService, CategoryService>();
    builder.Services.AddScoped<ICocktailService, CocktailService>();
    builder.Services.AddScoped<IIngredientService, IngredientService>();
    builder.Services.AddScoped<IUserService, UserService>();

    builder.Services.AddScoped<IFavouriteService<CocktailFavourite>, FavouriteService<CocktailFavourite>>();
    builder.Services.AddScoped<IFavouriteService<CocktailListFavourite>, FavouriteService<CocktailListFavourite>>();

    builder.Services.AddRouting(options => options.LowercaseUrls = true);
    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "CocktailApp API",
            Description = "The cocktail app API"
        });

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseExceptionHandler("/error");
    app.UseHttpsRedirection();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}
using CocktailApp.Extensions;
using CocktailApp.Services.Cocktails;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Logging.ClearProviders();
    builder.Logging.AddConsole();
    
    builder.Services.AddControllers();
    builder.Services.AddSingleton<ICocktailService, CocktailService>();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
}

var app = builder.Build();
{
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.ConfigureExceptionHandler();
    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    app.Run();
}

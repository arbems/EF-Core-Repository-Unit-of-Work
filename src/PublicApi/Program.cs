using Application;
using Infrastructure;
using Infrastructure.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers()
    .AddFluentValidation(x => x.AutomaticValidationEnabled = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "EF Core - Generic Repository",
        Description = "Generic Repository in Entity Framework Core",
    });
});

var app = builder.Build();

// Seed Data
if (app.Configuration.GetValue<bool>("UseInMemoryDatabase"))
{
    using (var scope = app.Services.CreateScope())
    {
        var scopeProvider = scope.ServiceProvider;
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

        await ApplicationContextSeed.SeedSampleDataAsync(dbContext);
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.InjectStylesheet("/swagger-ui/custom.css");
    });
}

app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

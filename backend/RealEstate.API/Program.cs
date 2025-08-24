using RealEstate.Domain.Repositories;
using RealEstate.Infrastructure.Persistence;
using RealEstate.Infrastructure.Repositories;
using RealEstate.Application.UseCases;
using RealEstate.API.Commands;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Configuración de MongoDB
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings"));

// Registrar MongoDbContext
builder.Services.AddSingleton<MongoDbContext>(sp =>
{
    var settings = builder.Configuration
        .GetSection("DatabaseSettings")
        .Get<DatabaseSettings>() ?? throw new InvalidOperationException("DatabaseSettings not found");

    return new MongoDbContext(settings.ConnectionString, settings.DatabaseName);
});

// Registrar IMongoDatabase
builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var mongoDbContext = sp.GetRequiredService<MongoDbContext>();
    return mongoDbContext.Database ?? throw new InvalidOperationException("Database is null");
});

// Registrar DataSeeder
builder.Services.AddSingleton<DataSeeder>(sp =>
{
    var database = sp.GetRequiredService<IMongoDatabase>();
    return new DataSeeder(database);
});

// Registrar comando de migración
builder.Services.AddHostedService<MigrateCommand>();

// Repositorios
builder.Services.AddScoped<IPropertyRepository, MongoPropertyRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();

// Use Cases (solo consultas)
builder.Services.AddScoped<GetPropertiesUseCase>();
builder.Services.AddScoped<GetPropertyByIdUseCase>();
builder.Services.AddScoped<GetAllOwnersUseCase>();

// Servicios ASP.NET Core
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapControllers();
app.Run();
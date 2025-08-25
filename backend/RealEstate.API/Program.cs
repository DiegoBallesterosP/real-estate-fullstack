using RealEstate.Domain.Repositories;
using RealEstate.Infrastructure.Persistence;
using RealEstate.Infrastructure.Repositories;
using RealEstate.Application.UseCases;
using RealEstate.API.Commands;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddSingleton<MongoDbContext>(sp =>
{
    var settings = builder.Configuration
        .GetSection("DatabaseSettings")
        .Get<DatabaseSettings>() ?? throw new InvalidOperationException("DatabaseSettings not found");

    return new MongoDbContext(settings.ConnectionString, settings.DatabaseName);
});

builder.Services.AddSingleton<IMongoDatabase>(sp =>
{
    var mongoDbContext = sp.GetRequiredService<MongoDbContext>();
    return mongoDbContext.Database ?? throw new InvalidOperationException("Database is null");
});

builder.Services.AddSingleton<DataSeeder>(sp =>
{
    var database = sp.GetRequiredService<IMongoDatabase>();
    return new DataSeeder(database);
});

builder.Services.AddHostedService<MigrateCommand>();

builder.Services.AddScoped<IPropertyRepository, MongoPropertyRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();

builder.Services.AddScoped<GetPropertiesUseCase>();
builder.Services.AddScoped<GetPropertyByIdUseCase>();
builder.Services.AddScoped<GetAllOwnersUseCase>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.MapControllers();
app.Run();
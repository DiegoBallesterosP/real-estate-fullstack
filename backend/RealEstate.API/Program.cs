using RealEstate.Domain.Repositories;
using RealEstate.Infrastructure.Persistence;
using RealEstate.Infrastructure.Repositories;
using RealEstate.Application.UseCases;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings"));

builder.Services.AddSingleton<MongoDbContext>(sp =>
{
    var settings = builder.Configuration
        .GetSection("DatabaseSettings")
        .Get<DatabaseSettings>();

    return new MongoDbContext(settings.ConnectionString, settings.DatabaseName);
});

builder.Services.AddScoped<IPropertyRepository, MongoPropertyRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();

builder.Services.AddScoped<GetPropertiesUseCase>();
builder.Services.AddScoped<GetPropertyByIdUseCase>();
builder.Services.AddScoped<CreatePropertyUseCase>();
builder.Services.AddScoped<UpdatePropertyUseCase>();
builder.Services.AddScoped<GetAllOwnersUseCase>();
builder.Services.AddScoped<DeletePropertyUseCase>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
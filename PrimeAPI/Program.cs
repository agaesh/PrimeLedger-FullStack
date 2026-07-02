using Microsoft.EntityFrameworkCore;
using PrimeAPI.Application.Interface;
using PrimeAPI.Application.Service;
using PrimeAPI.Application.Services;
using PrimeAPI.Infrasfructure;
using PrimeAPI.Infrastructure.Repositories;
using PrimeAPI.Infrastructure.Services;
using PrimeAPI.Repositories;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Register AppDbContext. 
// Only register if not in Integration Testing mode - the test factory will override this.
// Check both environment and configuration to allow test overrides.
var isIntegrationTest = builder.Environment.IsEnvironment("IntegrationTesting") || 
                        builder.Configuration.GetValue<bool>("IntegrationTesting", false);

if (!isIntegrationTest)
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection")
        ));
}

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new System.Text.Json.Serialization.JsonStringEnumConverter()
        );
    });

builder.Services.AddScoped<IProductMetadataService,ProductMetadataService>();
builder.Services.AddScoped<IProductMetadataRepository, ProductMetadataRepository>();

builder.Services.AddScoped<ITaxTreatmentService, TaxTreatmentService>();
builder.Services.AddScoped<ITaxTreatmentRepository, TaxTreatmentRepository>();

builder.Services.AddScoped<IGLAccountService, GLAccountService>();
builder.Services.AddScoped<IGLAccountRepository, GLAccountRepository>();

builder.Services.AddScoped<ITaxRegimeRepository, TaxRegimeRepository>();
builder.Services.AddScoped<ITaxRegimeService, TaxRegimeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

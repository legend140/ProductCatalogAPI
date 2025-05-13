using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Application.Interfaces;
using ProductCatalogAPI.Application.Services;
using ProductCatalogAPI.Core.Interfaces;
using ProductCatalogAPI.Infrastructure.Data;
using ProductCatalogAPI.Infrastructure.Repositories;
using System;

var builder = WebApplication.CreateBuilder(args);

// Enable switching between PostgreSQL and SQL Server
var usePostgres = false;

// Read the appropriate connection string from user secrets
var configuration = builder.Configuration;
var connectionString = usePostgres
    ? configuration.GetConnectionString("PostgreSqlConnection")
    : configuration.GetConnectionString("SqlServerConnection");

// Register the DbContext with the chosen provider
if (usePostgres)
{
    builder.Services.AddDbContext<ProductDbContext>(options =>
        options.UseNpgsql(connectionString));
}
else
{
    builder.Services.AddDbContext<ProductDbContext>(options =>
        options.UseSqlServer(connectionString));
}

// Add services to the container.
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

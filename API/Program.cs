using Aplication.DTO;
using Aplication.Factories;
using Aplication.Helpers;
using Aplication.Services;
using Aplication.Services.Options;
using Aplication.Validators;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using Infrastructure.Data;
using Infrastructure.Middleware;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<Context>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Configure<StorageOptions>(builder.Configuration.GetSection("StorageOptions"));

builder.Services.AddScoped<IRepository<FilterResult>, FilterResultRepository>();
builder.Services.AddScoped<IRepository<District>, DistrictRepository>();
builder.Services.AddScoped<IRepository<Order>, OrderRepository>();

builder.Services.AddScoped<IDataSaver,DbDataSaver>();
builder.Services.AddScoped<IDataSaver, FileDataSaver>();
builder.Services.AddScoped<IDataSaverFactory, DataSaverFactory>();
builder.Services.AddScoped<OrderFiltrator>();
builder.Services.AddScoped<IValidator<OrderFilterRequest>, OrderFilterRequestValidator>();

builder.Host.UseSerilog((context, services, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();
using (var scope = app.Services.CreateScope())
    DataSeeder.Seed(scope.ServiceProvider.GetService<Context>());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExeptionHandler>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();

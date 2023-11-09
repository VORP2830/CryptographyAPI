using AutoMapper;
using Cryptography.API.Context;
using Cryptography.API.DTOs.Mapping;
using Cryptography.API.Repositories;
using Cryptography.API.Repositories.Interfaces;
using Cryptography.API.Services;
using Cryptography.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(
    context => context.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<ISensitiveDataRepository, SensitiveDataRepository>();
builder.Services.AddScoped<ISensitiveDataService, SensitiveDataService>();



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

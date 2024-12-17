
using DotNetEnv; 
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using ScheduleApp.Application.Interfaces;
using ScheduleApp.Application.Services;
using ScheduleApp.Application.Validation;
using ScheduleApp.Infrastructure.Data;
using ScheduleApp.Infrastructure.Profiles;
using ScheduleApp.Infrastructure.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyHeader()                   
              .AllowAnyMethod()                    
              .AllowCredentials();                 
    });
});

Env.Load();

var dbServer = Environment.GetEnvironmentVariable("DB_SERVER");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbUser = Environment.GetEnvironmentVariable("DB_USER");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

var connectionString = $"Server={dbServer};Database={dbName};User Id={dbUser};Password={dbPassword};TrustServerCertificate=True;";

var useInMemoryDb = Environment.GetEnvironmentVariable("USE_INMEMORY_DB");

builder.Services.AddDbContext<ScheduleAppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(typeof(ContactProfile));

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<ContactValidator>();

builder.Services.AddScoped<IContactRepository, ContactRepository>(); 
builder.Services.AddScoped<IContactService, ContactService>();       
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseCors("AllowFrontend");

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseRouting();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();


public partial class Program { }
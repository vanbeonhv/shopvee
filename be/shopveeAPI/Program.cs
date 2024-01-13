using DotNetEnv;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using shopveeAPI.DbContext;
using shopveeAPI.Services.User;
using shopveeAPI.Services.User.Dto.Request;
using shopveeAPI.Services.User.Validator;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
Env.Load();

// Add services to the container
builder.Services.AddDbContext<ShopveeDbContext>(opts =>
    opts.UseSqlServer(configuration.GetConnectionString("CONNECTION_STRING").Replace("${DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD"))));

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserServices, UserServices>();
builder.Services.AddScoped<IValidator<UserRequest>, UserRequestValidator>();


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
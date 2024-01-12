using Microsoft.EntityFrameworkCore;
using shopveeAPI.DbContext;
using shopveeAPI.Services.User;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
DotNetEnv.Env.Load();

// Add services to the container.
builder.Services.AddDbContext<ShopveeDbContext>(opts =>
    opts.UseSqlServer(configuration.GetConnectionString("CONNECTION_STRING").Replace("${DB_PASSWORD}", System.Environment.GetEnvironmentVariable("DB_PASSWORD"))));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IUserServices, UserServices>();

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
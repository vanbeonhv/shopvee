using System.Text;
using DotNetEnv;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using shopveeAPI.AutoMapper;
using shopveeAPI.Dapper;
using shopveeAPI.DbContext;
using shopveeAPI.Middleware;
using shopveeAPI.Services.Auth;
using shopveeAPI.Services.Product;
using shopveeAPI.Services.User;
using shopveeAPI.Services.User.Dto.Request;
using shopveeAPI.Services.User.Validator;
using shopveeAPI.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
Env.Load();

// Add services to the container
builder.Services.AddDbContext<ShopveeDbContext>(opts =>
        opts.UseNpgsql(configuration.GetConnectionString("CONNECTION_STRING")
            .Replace("${DB_PASSWORD}", Environment.GetEnvironmentVariable("DB_PASSWORD"))))
    ;
builder.Services.AddTransient<IApplicationDbConnection, ApplicationDbConnection>();

//Add service JwtBea
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:ValidIssuer"],
        ValidAudience = builder.Configuration["Jwt:ValidAudience"],
        IssuerSigningKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET") ?? String.Empty))
    };
});


builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserGenericService, UserGenericServices>();
builder.Services.AddScoped<IUserServiceDapper, UserServiceDapper>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IProductService,ProductService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddScoped<IValidator<UserRequest>, UserRequestValidator>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseHttpsRedirection();
app.UseMiddleware<AccessTokenMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
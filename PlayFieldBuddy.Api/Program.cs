
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using PlayFieldBuddy.Api.Services;
using PlayFieldBuddy.Repositories;
using PlayFieldBuddy.Repositories.Interfaces;
using Serilog;
using AutoMapper;
using FluentValidation;
using PlayFieldBuddy.Domain.Models;

using FluentValidation.AspNetCore;
using PlayFieldBuddy.Api.Validation;
using PlayFieldBuddy.Api;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var authenticationSettings = new AuthenticationSettings();


builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);


builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
})
.AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),

    };
});
builder.Services.AddHttpClient();
builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddControllers().AddJsonOptions(options => 
{ 
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services
    .AddScoped<IUserRepository, UserRepository>()
    .AddScoped<IUserService, UserService>()
    .AddScoped<IPitchRepository, PitchRepository>()
    .AddScoped<IPitchService, PitchService>()
    .AddScoped<IGameService, GameService>()
    .AddScoped<IValidator<UserCreateRequest>, UserCreateRequestValidator>()
    .AddScoped<IGameRepository, GameRepository>();
   

    

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetSection("Settings:ConnectionString").Value;
builder.Services.AddDbContext<PlayFieldBuddyDbContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();



app.Run();

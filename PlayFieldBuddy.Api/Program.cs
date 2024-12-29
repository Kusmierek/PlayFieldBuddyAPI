
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using PlayFieldBuddy.Api.Services;
using PlayFieldBuddy.Repositories;
using PlayFieldBuddy.Repositories.Interfaces;
using Serilog;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.AddHttpClient();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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

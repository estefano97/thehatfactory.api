using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using thehatfactory.api.Models;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = new ConfigurationBuilder()
                         .AddJsonFile("appsettings.json")
                         .Build();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<the_hat_factoryContext>(options =>
       options.UseSqlServer(builder.Configuration.GetConnectionString("cn")));

builder.Services.AddCors(opciones =>
{
    opciones.AddDefaultPolicy(builder =>
    {
        //builder.WithOrigins("http://*", "https://*").AllowAnyMethod().AllowAnyHeader();
        //builder.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
        builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        //.AllowCredentials();
    });
});

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddDbContext<the_hat_factoryContext>(option => option.UseSqlServer(configuration["ConnectionStrings:cn"]));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();

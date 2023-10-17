using LABO_DAL.Repositories;

using Microsoft.Extensions.Configuration;

using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Lire la chaîne de connexion depuis appsettings.json
string connectionString = builder.Configuration.GetConnectionString("Dev");

// Ajouter UserRepo comme service et injecter la connexion
builder.Services.AddScoped<UserRepo>(provider => new UserRepo(new SqlConnection(connectionString)));





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

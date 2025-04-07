using InventoryManagementBackend.Data;
using InventoryManagementBackend.API;
using InventoryManagementBackend.Interfaces;
var builder = WebApplication.CreateBuilder(args);


// Register controllers
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDatabaseConnector, DatabaseConnector>();

var app = builder.Build();


// Enable controllers to handle requests
app.UseRouting();
app.UseAuthorization();
app.MapControllers();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.Run();

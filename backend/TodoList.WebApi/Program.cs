using System.Reflection;
using TodoList.Application.Extensions;
using TodoList.Infrastructure.Extensions;
using TodoList.Persistence.Extensions;
using TodoList.Persistence.MigrationTools;
using TodoList.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer();
builder.Services.AddPersistenceLayer(builder.Configuration);

builder.Services.AddSwaggerGen();

builder.Services.AddLogging();

var app = builder.Build();

// Миграции при запуске
using var scoped = app.Services.CreateScope();
var migrator = scoped.ServiceProvider.GetRequiredService<Migrator>();
await migrator.MigrateAsync();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(b => b
    .WithOrigins("http://localhost:5173")
    .AllowAnyMethod()                     
    .AllowAnyHeader()                      
    .AllowCredentials()); 

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<RequestValidationMiddleware>();

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
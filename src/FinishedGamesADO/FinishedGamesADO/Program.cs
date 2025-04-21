using FinishedGamesADO.Models;
using FinishedGamesADO.Repository;
using FinishedGamesADO.Repository.Abstracitions;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<SqlConnection>(provider =>
{
    var configuration = provider.GetRequiredService<IConfiguration>();
    return new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IGameRepository, GameRepository>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("v1/Games", async (CancellationToken cancellationToken, IGameRepository repository)
    => Results.Ok(await repository.GetAllAsync()));

app.MapGet("v1/Games/{id}", async (CancellationToken cancellationToken, IGameRepository repository, Guid id)
    => Results.Ok(await repository.GetByIdAsync(id)));

app.MapPost("v1/Games", async (CancellationToken cancellationToken, IGameRepository repository, Game game)
    => Results.Ok(await repository.CreateAsync(game)));

app.MapPut("v1/Games/{id}", async (CancellationToken cancellationToken, IGameRepository repository, Guid id, Game game)
    => Results.Ok(await repository.UpdateAsync(id, game)));

app.MapDelete("v1/Games/{id}", async (CancellationToken cancellationToken, IGameRepository repository, Guid id)
    => Results.Ok(await repository.DeleteAsync(id)));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
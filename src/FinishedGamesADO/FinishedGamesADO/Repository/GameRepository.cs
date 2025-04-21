using FinishedGamesADO.Models;
using FinishedGamesADO.Repository.Abstracitions;
using Microsoft.Data.SqlClient;

namespace FinishedGamesADO.Repository;

public class GameRepository : IGameRepository
{
    private readonly SqlConnection _connection;

    public GameRepository(SqlConnection connection)
    {
        _connection = connection;
    }

    public async Task<Game> CreateAsync(Game game)
    {
        const string queryInsertGame = "INSERT INTO GAMES (ID, TITLE, DESCRIPTION, " +
            "PLATFORM, GENRE, GRADE ) VALUES (" +
            "@ID, @TITLE, @DESCRIPTION, " +
            "@PLATFORM, @GENRE, @GRADE)";

        var createdGame = new Game();

        await _connection.OpenAsync();

        using var command = new SqlCommand(queryInsertGame, _connection);

        try
        {
            command.Parameters.AddWithValue("@ID", Guid.NewGuid());
            command.Parameters.AddWithValue("@TITLE", game.Title);
            command.Parameters.AddWithValue("@DESCRIPTION", game.Description);
            command.Parameters.AddWithValue("@PLATFORM", game.Platform);
            command.Parameters.AddWithValue("@GENRE", game.Genre);
            command.Parameters.AddWithValue("@GRADE", game.Grade);
            command.ExecuteNonQuery();

            createdGame = new Game
            {
                Id = Guid.NewGuid(),
                Title = game.Title,
                Description = game.Description,
                Platform = game.Platform,
                Genre = game.Genre,
                Grade = game.Grade
            };
            return await Task.FromResult(createdGame);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<Game> DeleteAsync(Guid id)
    {
        const string queryDeleteGame = "DELETE FROM GAMES " +
            "WHERE ID = @ID";

        var game = new Game();

        await _connection.OpenAsync();

        using var command = new SqlCommand(queryDeleteGame, _connection);

        try
        {
            command.Parameters.AddWithValue("@ID", id);
            command.ExecuteNonQuery();

            return game;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }

    public async Task<List<Game>> GetAllAsync()
    {
        const string queryAllGames = "SELECT ID, TITLE, DESCRIPTION, " +
            "PLATFORM, GENRE, GRADE " +
            "FROM GAMES";

        var games = new List<Game>();

        await _connection.OpenAsync();

        using var command = new SqlCommand(queryAllGames, _connection);

        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            var game = new Game
            {
                Id = Guid.Parse(reader["Id"].ToString()!),
                Title = reader["Title"].ToString()!,
                Description = reader["Description"].ToString()!,
                Platform = reader["Platform"].ToString()!,
                Genre = reader["Genre"].ToString()!,
                Grade = reader["Grade"].ToString()!
            };

            games.Add(game);
        }
        await _connection.CloseAsync();

        return games;
    }

    public async Task<Game> GetByIdAsync(Guid id)
    {
        const string queryById = "SELECT ID, TITLE, DESCRIPTION, " +
            "PLATFORM, GENRE, GRADE " +
            "FROM GAMES " +
            "WHERE ID = @ID";

        await _connection.OpenAsync();

        var game = new Game();

        using var command = new SqlCommand(queryById, _connection);
        try
        {
            command.Parameters.AddWithValue("@ID", id);

            SqlDataReader reader = command.ExecuteReader();

            while (await reader.ReadAsync())
            {
                game = new Game
                {
                    Id = Guid.Parse(reader["Id"].ToString()!),
                    Title = reader["Title"].ToString()!,
                    Description = reader["Description"].ToString()!,
                    Platform = reader["Platform"].ToString()!,
                    Genre = reader["Genre"].ToString()!,
                    Grade = reader["Grade"].ToString()!
                };
                return await Task.FromResult(game);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
        finally
        {
            await _connection.CloseAsync();
        }
        return game;
    }

    public async Task<Game> UpdateAsync(Guid id, Game game)
    {
        const string queryUpdateGame = "UPDATE GAMES SET " +
            "TITLE = @TITLE, " +
            "DESCRIPTION = @DESCRIPTION, " +
            "PLATFORM = @PLATFORM, " +
            "GENRE = @GENRE, " +
            "GRADE = @GRADE " +
            "WHERE ID = @ID";

        var updatedGame = new Game();

        await _connection.OpenAsync();

        using var command = new SqlCommand(queryUpdateGame, _connection);
        try
        {
            command.Parameters.AddWithValue("@ID", id);
            command.Parameters.AddWithValue("@TITLE", game.Title);
            command.Parameters.AddWithValue("@DESCRIPTION", game.Description);
            command.Parameters.AddWithValue("@PLATFORM", game.Platform);
            command.Parameters.AddWithValue("@GENRE", game.Genre);
            command.Parameters.AddWithValue("@GRADE", game.Grade);

            SqlDataReader reader = command.ExecuteReader();

            if (await reader.ReadAsync())
            {
                updatedGame = new Game
                {
                    Id = Guid.Parse(reader["Id"].ToString()!),
                    Title = reader["Title"].ToString()!,
                    Description = reader["Description"].ToString()!,
                    Platform = reader["Platform"].ToString()!,
                    Genre = reader["Genre"].ToString()!,
                    Grade = reader["Grade"].ToString()!
                };
            }

            return await Task.FromResult(updatedGame);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
        finally
        {
            await _connection.CloseAsync();
        }
    }
}
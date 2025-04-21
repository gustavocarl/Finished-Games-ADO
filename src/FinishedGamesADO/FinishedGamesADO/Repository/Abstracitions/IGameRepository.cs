using FinishedGamesADO.Models;

namespace FinishedGamesADO.Repository.Abstracitions;

public interface IGameRepository
{
    Task<List<Game>> GetAllAsync();

    Task<Game> GetByIdAsync(Guid id);

    Task<Game> CreateAsync(Game game);

    Task<Game> UpdateAsync(Guid id, Game game);

    Task<Game> DeleteAsync(Guid id);
}
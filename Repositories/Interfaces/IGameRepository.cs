using PappuPictureChart.API.Models;

namespace PappuPictureChart.API.Repositories.Interfaces;

public interface IGameRepository
{
    Task<List<Game>> GetAllAsync();
    Task<Game?> GetByIdAsync(int id);
    Task<Game> CreateAsync(Game game);
    Task<Game> UpdateAsync(Game game);
    Task<bool> DeleteAsync(int id);
}
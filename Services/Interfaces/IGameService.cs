using PappuPictureChart.API.Models;

namespace PappuPictureChart.API.Services.Interfaces;

public interface IGameService
{
    Task<List<Game>> GetAllAsync();
    Task<Game?> GetByIdAsync(int id);
    Task<Game> CreateAsync(Game game);
    Task<Game> UpdateAsync(int id, Game game);
    Task<bool> DeleteAsync(int id);
}
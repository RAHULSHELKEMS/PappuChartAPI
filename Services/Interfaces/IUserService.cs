using PappuPictureChart.API.Models;

namespace PappuPictureChart.API.Services.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
}
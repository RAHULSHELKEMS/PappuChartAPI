using PappuPictureChart.API.Models;

namespace PappuPictureChart.API.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByMobileAsync(string mobile);

    Task<User?> GetByIdAsync(int id);

    Task<List<User>> GetAllAsync();

    Task<User> CreateAsync(User user);

    Task<User> UpdateAsync(User user);

    Task<bool> DeleteAsync(int id);
}
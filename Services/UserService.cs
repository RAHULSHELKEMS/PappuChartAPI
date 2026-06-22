using Microsoft.EntityFrameworkCore;
using PappuPictureChart.API.Data;
using PappuPictureChart.API.Models;
using PappuPictureChart.API.Services.Interfaces;

namespace PappuPictureChart.API.Services;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
using Microsoft.EntityFrameworkCore;
using PappuPictureChart.API.Data;
using PappuPictureChart.API.DTOs;
using PappuPictureChart.API.Helpers;
using PappuPictureChart.API.Models;
using PappuPictureChart.API.Repositories.Interfaces;
using PappuPictureChart.API.Services.Interfaces;
using System;

namespace PappuPictureChart.API.Services;


 public class AuthService
{
    private readonly ApplicationDbContext _db;

    public AuthService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<User?> Register( string name,string mobile,string password)
    {
        if (await _db.Users.AnyAsync(x => x.Mobile == mobile))
            return null;

        var user = new User
        {
            Name = name,
            Mobile = mobile,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(password),
            Coins = 50
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return user;
    }

    public async Task<User?> Login(
        string mobile,
        string password)
    {
        var user = await _db.Users
            .FirstOrDefaultAsync(x => x.Mobile == mobile);

        if (user == null)
            return null;

        if (!BCrypt.Net.BCrypt.Verify(
            password,
            user.PasswordHash))
            return null;

        return user;
    }
}

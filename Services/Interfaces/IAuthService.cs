using PappuPictureChart.API.DTOs;

namespace PappuPictureChart.API.Services.Interfaces;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterRequest request);
    Task<string> LoginAsync(LoginRequest request);
}
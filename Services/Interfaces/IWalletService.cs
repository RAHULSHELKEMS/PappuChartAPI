using PappuPictureChart.API.Models;

namespace PappuPictureChart.API.Services.Interfaces;

public interface IWalletService
{
    Task<decimal> GetBalanceAsync(int userId);
    Task<List<WalletTransaction>> GetHistoryAsync(int userId);
}
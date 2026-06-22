using PappuPictureChart.API.Models;

namespace PappuPictureChart.API.Repositories.Interfaces;

public interface IWalletRepository
{
    Task<decimal> GetBalanceAsync(int userId);
    Task<List<WalletTransaction>> GetHistoryAsync(int userId);
}
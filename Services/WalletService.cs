using PappuPictureChart.API.Models;
using PappuPictureChart.API.Repositories.Interfaces;
using PappuPictureChart.API.Services.Interfaces;

namespace PappuPictureChart.API.Services;

public class WalletService : IWalletService
{
    private readonly IWalletRepository _repository;

    public WalletService(IWalletRepository repository)
    {
        _repository = repository;
    }

    public async Task<decimal> GetBalanceAsync(int userId)
        => await _repository.GetBalanceAsync(userId);

    public async Task<List<WalletTransaction>> GetHistoryAsync(int userId)
        => await _repository.GetHistoryAsync(userId);
}
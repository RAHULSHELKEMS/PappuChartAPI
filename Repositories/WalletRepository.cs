using Microsoft.EntityFrameworkCore;
using PappuPictureChart.API.Data;
using PappuPictureChart.API.Models;
using PappuPictureChart.API.Repositories.Interfaces;

namespace PappuPictureChart.API.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly ApplicationDbContext _context;

    public WalletRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<decimal> GetBalanceAsync(int userId)
    {
        return await _context.WalletTransactions
            .Where(x => x.UserId == userId)
            .SumAsync(x => x.Amount);
    }

    public async Task<List<WalletTransaction>> GetHistoryAsync(int userId)
    {
        return await _context.WalletTransactions
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedDate)
            .ToListAsync();
    }

    public async Task<WalletTransaction> CreateAsync(WalletTransaction transaction)
    {
        _context.WalletTransactions.Add(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }
}
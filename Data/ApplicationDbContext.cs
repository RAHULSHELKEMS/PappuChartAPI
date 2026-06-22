using Microsoft.EntityFrameworkCore;
using PappuPictureChart.API.Models;

namespace PappuPictureChart.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Game> Games { get; set; }

    public DbSet<Chart> Charts { get; set; }

    public DbSet<Result> Results { get; set; }

    public DbSet<WalletTransaction> WalletTransactions { get; set; }
    public DbSet<Picture> Pictures => Set<Picture>();

    public DbSet<GameRound> GameRounds => Set<GameRound>();

    public DbSet<Bet> Bets => Set<Bet>();

    public DbSet<Transaction> Transactions => Set<Transaction>();

    public DbSet<Deposit> Deposits => Set<Deposit>();

    public DbSet<Withdrawal> Withdrawals => Set<Withdrawal>();
}
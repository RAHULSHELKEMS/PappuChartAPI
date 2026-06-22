using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PappuPictureChart.API.Data;
using PappuPictureChart.API.Models;
using PappuPictureChart.API.Repositories.Interfaces;
using PappuPictureChart.API.Services.Interfaces;
using System;

namespace PappuPictureChart.API.Services;

public class GameService
{
    private readonly ApplicationDbContext _db;

    public GameService(ApplicationDbContext db)
    {
        _db = db;
    }


    public async Task PlaceMultiBet(
    int userId,
    BetRequest request)
    {
        var user = await _db.Users.FindAsync(userId);

        if (user == null)
            throw new Exception("User Not Found");

        decimal totalCoins = request.Bets.Sum(x => x.Coins);

        if (user.Coins < totalCoins)
            throw new Exception("Insufficient Coins");

        foreach (var bet in request.Bets)
        {
            _db.Bets.Add(new Bet
            {
                UserId = userId,
                RoundId = request.RoundId,
                PictureId = bet.PictureId,
                Coins = bet.Coins
            });
        }

        user.Coins -= totalCoins;

        await _db.SaveChangesAsync();
    }

    //public async Task PlaceMultiBet(int userId, BetRequest request)
    //{
    //    var round = await _db.GameRounds
    //        .FirstOrDefaultAsync(x =>
    //            x.Id == request.RoundId &&
    //            !x.IsCompleted);

    //    if (round == null)
    //        throw new Exception("Round Closed");

    //    var user = await _db.Users.FindAsync(userId);

    //    if (user == null)
    //        throw new Exception("User Not Found");

    //    decimal totalCoins =
    //        request.Bets.Sum(x => x.Coins);

    //    if (user.Coins < totalCoins)
    //        throw new Exception("Insufficient Coins");

    //    foreach (var bet in request.Bets)
    //    {
    //        if (bet.Coins < 1 || bet.Coins > 100)
    //            throw new Exception(
    //                $"Picture {bet.PictureId} coin must be between 1 and 100");

    //        _db.Bets.Add(new Bet
    //        {
    //            UserId = userId,
    //            RoundId = request.RoundId,
    //            PictureId = bet.PictureId,
    //            Coins = bet.Coins
    //        });
    //    }

    //    user.Coins -= totalCoins;

    //    await _db.SaveChangesAsync();
    //}


    public async Task<int> CalculateResult(int roundId)
    {
        var bets = await _db.Bets
            .Where(x => x.RoundId == roundId)
            .ToListAsync();

        if (!bets.Any())
            return Random.Shared.Next(1, 13);

        var result = bets
            .GroupBy(x => x.PictureId)
            .Select(x => new
            {
                PictureId = x.Key,
                TotalCoins = x.Sum(y => y.Coins)
            })
            .OrderBy(x => x.TotalCoins)
            .First();

        return result.PictureId;
    }
    public async Task DistributeWinning(
        int roundId,
        int resultPictureId)
    {
        var winners = await _db.Bets
            .Where(x =>
                x.RoundId == roundId &&
                x.PictureId == resultPictureId)
            .ToListAsync();

        foreach (var winner in winners)
        {
            var user =
                await _db.Users.FindAsync(winner.UserId);

            if (user == null)
                continue;

            var winningCoins = winner.Coins * 10;

            user.Coins += winningCoins;

            _db.Transactions.Add(new Transaction
            {
                UserId = user.Id,
                Coins = winningCoins,
                Type = "Win",
                Description =
                    $"Won Round {roundId}"
            });
        }

        await _db.SaveChangesAsync();
    }

}
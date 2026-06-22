using Microsoft.EntityFrameworkCore;
using PappuPictureChart.API.Data;
using PappuPictureChart.API.Models;

namespace PappuPictureChart.API.Services;

public class RoundBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public RoundBackgroundService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope =
                _scopeFactory.CreateScope();

            var db =
                scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();

            var game =
                scope.ServiceProvider
                .GetRequiredService<GameService>();

            var activeRound =
                await db.GameRounds
                .FirstOrDefaultAsync(x => !x.IsCompleted);

            if (activeRound == null)
            {
                var round = new GameRound
                {
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow.AddMinutes(3),
                    IsCompleted = false
                };

                db.GameRounds.Add(round);
                await db.SaveChangesAsync();
            }
            else
            {
                if (DateTime.UtcNow >= activeRound.EndTime)
                {
                    int result =
                        await game.CalculateResult(activeRound.Id);

                    activeRound.ResultPictureId = result;
                    activeRound.IsCompleted = true;

                    await game.DistributeWinning(
                        activeRound.Id,
                        result);

                    await db.SaveChangesAsync();

                    var newRound = new GameRound
                    {
                        StartTime = DateTime.UtcNow,
                        EndTime = DateTime.UtcNow.AddMinutes(3),
                        IsCompleted = false
                    };

                    db.GameRounds.Add(newRound);
                    await db.SaveChangesAsync();
                }
            }

            await Task.Delay(
                TimeSpan.FromSeconds(5),
                stoppingToken);
        }
    }
}

//using Microsoft.EntityFrameworkCore;
//using PappuPictureChart.API.Data;
//using PappuPictureChart.API.Models;

//namespace PappuPictureChart.API.Services
//{
//    public class RoundBackgroundService : BackgroundService
//    {
//        private readonly IServiceScopeFactory _scopeFactory;

//        public RoundBackgroundService(IServiceScopeFactory scopeFactory)
//        {
//            _scopeFactory = scopeFactory;
//        }

//        protected override async Task ExecuteAsync(
//            CancellationToken stoppingToken)
//        {
//            while (!stoppingToken.IsCancellationRequested)
//            {
//                using var scope =
//                    _scopeFactory.CreateScope();

//                var db = scope.ServiceProvider
//                    .GetRequiredService<ApplicationDbContext>();

//                var gameService = scope.ServiceProvider
//                    .GetRequiredService<GameService>();

//                var round = await db.GameRounds
//                    .Where(x => !x.IsCompleted)
//                    .FirstOrDefaultAsync();

//                if (round == null)
//                {
//                    var roundNumber =
//                        await db.GameRounds.CountAsync() + 1;

//                    db.GameRounds.Add(new GameRound
//                    {
//                        RoundNumber = roundNumber,
//                        StartTime = DateTime.UtcNow,
//                        EndTime = DateTime.UtcNow.AddSeconds(60),
//                        IsCompleted = false
//                    });

//                    await db.SaveChangesAsync();
//                }
//                else if (DateTime.UtcNow >= round.EndTime)
//                {
//                    var result =
//                        await gameService.CalculateResult(round.Id);

//                    round.ResultPictureId = result;
//                    round.IsCompleted = true;

//                    await gameService
//                        .DistributeWinning(round.Id, result);

//                    await db.SaveChangesAsync();

//                    db.GameRounds.Add(new GameRound
//                    {
//                        RoundNumber = round.RoundNumber + 1,
//                        StartTime = DateTime.UtcNow,
//                        EndTime = DateTime.UtcNow.AddSeconds(60),
//                        IsCompleted = false
//                    });

//                    await db.SaveChangesAsync();
//                }

//                await Task.Delay(5000, stoppingToken);
//            }
//        }
//    }
//}
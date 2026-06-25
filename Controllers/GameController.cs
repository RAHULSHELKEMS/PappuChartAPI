using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PappuPictureChart.API.Data;
using PappuPictureChart.API.Models;
using PappuPictureChart.API.Services;
using PappuPictureChart.API.Services.Interfaces;
using System;

namespace PappuPictureChart.API.Controllers;


    [Authorize]
[ApiController]
[Route("api/[controller]")]
public class GameController : ControllerBase
{
    private readonly GameService _game;
    private readonly ApplicationDbContext _db;

    public GameController(
        GameService game,
        ApplicationDbContext db)
    {
        _game = game;
        _db = db;
    }

    [HttpGet("pictures")]
    [AllowAnonymous]
    public IActionResult Pictures()
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";

        var pictures = _db.Pictures
            .Select(x => new
            {
                x.Id,
                x.Name,
                x.Rate,
                ImageUrl = baseUrl + x.ImageUrl
            })
            .ToList();

        return Ok(pictures);
    }


    [Authorize]
    [HttpPost("bet")]
    public async Task<IActionResult> PlaceBet(BetRequest request)
    {
        var userId = int.Parse(
            User.Claims.First(x =>
            x.Type.Contains("nameidentifier")).Value);

        await _game.PlaceMultiBet(userId, request);

        return Ok("Bet Placed");
    }



    //[Authorize]
    //[HttpGet("balance")]
    //public IActionResult Balance()
    //{
    //    var userId = int.Parse(
    //        User.Claims.First(x =>
    //            x.Type.Contains("nameidentifier"))
    //            .Value);

    //    var user = _db.Users.Find(userId);

    //    return Ok(new
    //    {
    //        user.Id,
    //        user.Name,
    //        user.Coins
    //    });
    //}

    [Authorize]
    [HttpGet("balance")]
    public IActionResult Balance()
    {
        var userId = int.Parse(
            User.Claims.First(x =>
            x.Type.Contains("nameidentifier")).Value);

        var user = _db.Users.Find(userId);

        return Ok(new
        {
            user.Id,
            user.Name,
            user.Coins
        });
    }


    [HttpGet("current-round")]
    public async Task<IActionResult> GetCurrentRound()
    {
        var now = DateTime.UtcNow;

        var round = await _db.GameRounds
            .Where(x => !x.IsCompleted)
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync();

        if (round == null)
        {
            round = new GameRound
            {
                RoundNumber = await _db.GameRounds.CountAsync() + 1,
                StartTime = now,
                EndTime = now.AddMinutes(3),
                IsCompleted = false
            };

            _db.GameRounds.Add(round);
            await _db.SaveChangesAsync();
        }

        if (round.EndTime <= now)
        {
            int result = await _game.CalculateResult(round.Id);

            round.ResultPictureId = result;
            round.IsCompleted = true;

            await _game.DistributeWinning(round.Id, result);

            await _db.SaveChangesAsync();

            var newRound = new GameRound
            {
                RoundNumber = round.RoundNumber + 1,
                StartTime = now,
                EndTime = now.AddMinutes(3),
                IsCompleted = false
            };

            _db.GameRounds.Add(newRound);
            await _db.SaveChangesAsync();

            return Ok(new
            {
                RoundId = newRound.Id,
                RoundNumber = newRound.RoundNumber,
                RemainingSeconds = 180,
                LastResult = result
            });
        }

        var remainingSeconds =
            Math.Max(0, (int)(round.EndTime - now).TotalSeconds);

        var lastResult = await _db.GameRounds
            .Where(x => x.IsCompleted && x.ResultPictureId != null)
            .OrderByDescending(x => x.Id)
            .Select(x => x.ResultPictureId)
            .FirstOrDefaultAsync();

        return Ok(new
        {
            RoundId = round.Id,
            RoundNumber = round.RoundNumber,
            RemainingSeconds = remainingSeconds,
            LastResult = lastResult
        });
    }


    //[HttpGet("current-round")]
    //public async Task<IActionResult> GetCurrentRound()
    //{
    //    var round = await _db.GameRounds
    //        .Where(x => !x.IsCompleted)
    //        .OrderByDescending(x => x.Id)
    //        .FirstOrDefaultAsync();

    //    // If no active round, create one automatically
    //    if (round == null)
    //    {
    //        int nextRoundNumber =
    //            await _db.GameRounds.CountAsync() + 1;

    //        round = new GameRound
    //        {
    //            RoundNumber = nextRoundNumber,
    //            StartTime = DateTime.Now,
    //            EndTime = DateTime.Now.AddMinutes(3),
    //            IsCompleted = false
    //        };

    //        _db.GameRounds.Add(round);
    //        await _db.SaveChangesAsync();
    //    }

    //    var remaining =
    //        (int)(round.EndTime - DateTime.Now).TotalSeconds;

    //    // Auto close and start next round
    //    if (remaining <= 0)
    //    {
    //        int result =
    //            await _game.CalculateResult(round.Id);

    //        round.ResultPictureId = result;
    //        round.IsCompleted = true;

    //        await _game.DistributeWinning(
    //            round.Id,
    //            result);

    //        await _db.SaveChangesAsync();

    //        var newRound = new GameRound
    //        {
    //            RoundNumber = round.RoundNumber + 1,
    //            StartTime = DateTime.Now,
    //            EndTime = DateTime.Now.AddMinutes(3),
    //            IsCompleted = false
    //        };

    //        _db.GameRounds.Add(newRound);
    //        await _db.SaveChangesAsync();

    //        return Ok(new
    //        {
    //            RoundId = newRound.Id,
    //            RoundNumber = newRound.RoundNumber,
    //            RemainingSeconds = 180,
    //            LastResult = result
    //        });
    //    }

    //    var lastResult = await _db.GameRounds
    //        .Where(x => x.IsCompleted &&
    //                    x.ResultPictureId != null)
    //        .OrderByDescending(x => x.Id)
    //        .Select(x => x.ResultPictureId)
    //        .FirstOrDefaultAsync();

    //    return Ok(new
    //    {
    //        RoundId = round.Id,
    //        RoundNumber = round.RoundNumber,
    //        RemainingSeconds = remaining,
    //        LastResult = lastResult
    //    });
    //}



    [AllowAnonymous]
    [HttpGet("result")]
    public async Task<IActionResult> Result()
    {
        var round = await _db.GameRounds
            .Where(x => x.IsCompleted)
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync();

        if (round == null)
        {
            return Ok(new
            {
                RoundNumber = 0,
                ResultPictureId = 0
            });
        }

        return Ok(new
        {
            RoundNumber = round.RoundNumber,
            ResultPictureId = round.ResultPictureId
        });
    }



    //[AllowAnonymous]
    //[HttpGet("result")]
    //public IActionResult Result()
    //{
    //    var round = _db.GameRounds
    //        .Where(x => x.IsCompleted)
    //        .OrderByDescending(x => x.Id)
    //        .FirstOrDefault();

    //    if (round == null)
    //        return NotFound();

    //    return Ok(new
    //    {
    //        round.RoundNumber,
    //        round.ResultPictureId
    //    });
    //}

    [HttpGet("last-result")]
    public async Task<IActionResult> LastResult()
    {
        var round = await _db.GameRounds
            .Where(x => x.IsCompleted)
            .OrderByDescending(x => x.Id)
            .FirstOrDefaultAsync();

        if (round == null)
            return Ok();

        return Ok(new
        {
            PictureId = round.ResultPictureId
        });
    }


    [HttpGet("last-results")]
    public IActionResult LastResults()
    {
        var results = _db.GameRounds
            .Where(x => x.IsCompleted)
            .OrderByDescending(x => x.Id)
            .Take(5)
            .Select(x => new
            {
                x.RoundNumber,
                x.ResultPictureId
            })
            .ToList();

        return Ok(results);
    }

    [HttpGet("pay")]
    public IActionResult Pay(decimal amount)
    {
        string razorpayLink =
            $"https://rzp.io/l/YOUR_PAYMENT_LINK?amount={amount}";

        return Redirect(razorpayLink);
    }


    [HttpPost("verify-payment")]
    public IActionResult VerifyPayment(
        [FromBody] VerifyPaymentRequest request)
    {
        var user =
            _db.Users.Find(request.UserId);

        if (user == null)
            return BadRequest("User Not Found");

        user.Coins += request.Amount;

        _db.SaveChanges();

        return Ok(new
        {
            Message = "Coins Added Successfully"
        });
    }







    [HttpGet("checkout")]
    public IActionResult Checkout(
    string orderId,
    decimal amount)
    {
        string html = $@"
<html>
<head>
<script src='https://checkout.razorpay.com/v1/checkout.js'></script>
</head>
<body>

<script>

var options = {{
    key: 'rzp_test_xxxxx',
    amount: '{amount * 100}',
    currency: 'INR',
    order_id: '{orderId}',

    handler: function(response){{
        window.location.href =
        '/success?paymentId=' +
        response.razorpay_payment_id;
    }}
}};

var rzp = new Razorpay(options);
rzp.open();

</script>

</body>
</html>";

        return Content(html, "text/html");
    }
}



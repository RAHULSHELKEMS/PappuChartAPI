using Microsoft.AspNetCore.Mvc;
using PappuPictureChart.API.Data;
using PappuPictureChart.API.Models;
using PappuPictureChart.API.Services.Interfaces;
using System;

namespace PappuPictureChart.API.Controllers;

    [ApiController]
[Route("api/[controller]")]
public class WalletController : ControllerBase
{
    private readonly ApplicationDbContext _db;

    public WalletController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit(
        DepositRequest request)
    {
        var deposit = new Deposit
        {
            UserId = request.UserId,
            Amount = request.Amount,
            PaymentId = request.PaymentId
        };

        _db.Deposits.Add(deposit);
        await _db.SaveChangesAsync();

        return Ok();
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw(
        WithdrawRequest request)
    {
        var withdraw = new Withdrawal
        {
            UserId = request.UserId,
            Amount = request.Amount,
            UpiId = request.UpiId
        };

        _db.Withdrawals.Add(withdraw);
        await _db.SaveChangesAsync();

        return Ok();
    }
}

public class DepositRequest
{
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public string PaymentId { get; set; } = "";
}

public class WithdrawRequest
{
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public string UpiId { get; set; } = "";
}

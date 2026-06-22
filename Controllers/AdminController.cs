using Microsoft.AspNetCore.Mvc;
using PappuPictureChart.API.Data;
using System;

namespace PappuPictureChart.API.Controllers
{
    [ApiController]
    [Route("api/admin")]
    public class AdminController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public AdminController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet("users")]
        public IActionResult Users()
        {
            return Ok(_db.Users.ToList());
        }

        [HttpGet("bets")]
        public IActionResult Bets()
        {
            return Ok(_db.Bets.ToList());
        }

        [HttpGet("withdraws")]
        public IActionResult Withdraws()
        {
            return Ok(_db.Withdrawals.ToList());
        }

        [HttpPost("approve-withdraw/{id}")]
        public async Task<IActionResult> Approve(int id)
        {
            var item = await _db.Withdrawals.FindAsync(id);

            if (item == null)
                return NotFound();

            item.Status = "Approved";

            await _db.SaveChangesAsync();

            return Ok();
        }
    }
}

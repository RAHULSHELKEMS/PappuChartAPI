using Microsoft.AspNetCore.Mvc;
using PappuPictureChart.API.Models;
using PappuPictureChart.API.Services.Interfaces;

namespace PappuPictureChart.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResultController : ControllerBase
{
    private readonly IResultService _resultService;

    public ResultController(IResultService resultService)
    {
        _resultService = resultService;
    }

    [HttpGet("today")]
    public async Task<IActionResult> Today()
    {
        return Ok(await _resultService.GetTodayAsync());
    }

    [HttpGet("history")]
    public async Task<IActionResult> History()
    {
        return Ok(await _resultService.GetHistoryAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Add(Result result)
    {
        return Ok(await _resultService.CreateAsync(result));
    }
}
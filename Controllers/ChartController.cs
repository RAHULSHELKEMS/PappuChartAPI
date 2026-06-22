using Microsoft.AspNetCore.Mvc;
using PappuPictureChart.API.Models;
using PappuPictureChart.API.Services.Interfaces;

namespace PappuPictureChart.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ChartController : ControllerBase
{
    private readonly IChartService _chartService;

    public ChartController(IChartService chartService)
    {
        _chartService = chartService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _chartService.GetAllAsync());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Chart chart)
    {
        return Ok(await _chartService.CreateAsync(chart));
    }
}
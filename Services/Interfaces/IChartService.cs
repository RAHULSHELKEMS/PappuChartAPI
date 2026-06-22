using PappuPictureChart.API.Models;

namespace PappuPictureChart.API.Services.Interfaces;

public interface IChartService
{
    Task<List<Chart>> GetAllAsync();
    Task<Chart> CreateAsync(Chart chart);
}
using PappuPictureChart.API.Models;

namespace PappuPictureChart.API.Repositories.Interfaces;

public interface IChartRepository
{
    Task<List<Chart>> GetAllAsync();
    Task<Chart> CreateAsync(Chart chart);
}
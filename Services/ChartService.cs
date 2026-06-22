using PappuPictureChart.API.Models;
using PappuPictureChart.API.Repositories.Interfaces;
using PappuPictureChart.API.Services.Interfaces;

namespace PappuPictureChart.API.Services;

public class ChartService : IChartService
{
    private readonly IChartRepository _repository;

    public ChartService(IChartRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Chart>> GetAllAsync()
        => await _repository.GetAllAsync();

    public async Task<Chart> CreateAsync(Chart chart)
        => await _repository.CreateAsync(chart);
}
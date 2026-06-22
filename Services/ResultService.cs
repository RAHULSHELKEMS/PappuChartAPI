using PappuPictureChart.API.Models;
using PappuPictureChart.API.Repositories.Interfaces;
using PappuPictureChart.API.Services.Interfaces;

namespace PappuPictureChart.API.Services;

public class ResultService : IResultService
{
    private readonly IResultRepository _repository;

    public ResultService(IResultRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<Result>> GetTodayAsync()
        => await _repository.GetTodayAsync();

    public async Task<List<Result>> GetHistoryAsync()
        => await _repository.GetHistoryAsync();

    public async Task<Result> CreateAsync(Result result)
        => await _repository.CreateAsync(result);
}
using PappuPictureChart.API.Models;

namespace PappuPictureChart.API.Services.Interfaces;

public interface IResultService
{
    Task<List<Result>> GetTodayAsync();
    Task<List<Result>> GetHistoryAsync();
    Task<Result> CreateAsync(Result result);
}
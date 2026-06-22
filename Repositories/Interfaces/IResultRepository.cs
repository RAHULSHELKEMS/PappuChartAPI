using PappuPictureChart.API.Models;

namespace PappuPictureChart.API.Repositories.Interfaces;

public interface IResultRepository
{
    Task<List<Result>> GetTodayAsync();
    Task<List<Result>> GetHistoryAsync();
    Task<Result> CreateAsync(Result result);
}
using Microsoft.EntityFrameworkCore;
using PappuPictureChart.API.Data;
using PappuPictureChart.API.Models;
using PappuPictureChart.API.Repositories.Interfaces;

namespace PappuPictureChart.API.Repositories;

public class ResultRepository : IResultRepository
{
    private readonly ApplicationDbContext _context;

    public ResultRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Result>> GetTodayAsync()
    {
        return await _context.Results
            .Where(x => x.ResultDate.Date == DateTime.Today)
            .ToListAsync();
    }

    public async Task<List<Result>> GetHistoryAsync()
    {
        return await _context.Results
            .OrderByDescending(x => x.ResultDate)
            .ToListAsync();
    }

    public async Task<Result?> GetByIdAsync(int id)
    {
        return await _context.Results.FindAsync(id);
    }

    public async Task<Result> CreateAsync(Result result)
    {
        _context.Results.Add(result);
        await _context.SaveChangesAsync();
        return result;
    }

    public async Task<Result> UpdateAsync(Result result)
    {
        _context.Results.Update(result);
        await _context.SaveChangesAsync();
        return result;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var result = await _context.Results.FindAsync(id);

        if (result == null)
            return false;

        _context.Results.Remove(result);
        await _context.SaveChangesAsync();

        return true;
    }
}
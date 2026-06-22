using Microsoft.EntityFrameworkCore;
using PappuPictureChart.API.Data;
using PappuPictureChart.API.Models;
using PappuPictureChart.API.Repositories.Interfaces;

namespace PappuPictureChart.API.Repositories;

public class ChartRepository : IChartRepository
{
    private readonly ApplicationDbContext _context;

    public ChartRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Chart>> GetAllAsync()
    {
        return await _context.Charts.ToListAsync();
    }

    public async Task<Chart?> GetByIdAsync(int id)
    {
        return await _context.Charts.FindAsync(id);
    }

    public async Task<Chart> CreateAsync(Chart chart)
    {
        _context.Charts.Add(chart);
        await _context.SaveChangesAsync();
        return chart;
    }

    public async Task<Chart> UpdateAsync(Chart chart)
    {
        _context.Charts.Update(chart);
        await _context.SaveChangesAsync();
        return chart;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var chart = await _context.Charts.FindAsync(id);

        if (chart == null)
            return false;

        _context.Charts.Remove(chart);
        await _context.SaveChangesAsync();

        return true;
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAggregator.Database;
using DataAggregator.ViewModels;

namespace DataAggregator.Controllers;
public class StreamFiltersController : Controller
{
    private readonly AppDbContext _context;

    public StreamFiltersController(AppDbContext context)
    {
        _context = context;
    }

    [Route("streams/{streamId:int}/filters/index", Name = "StreamFiltersIndex")]
    public async Task<IActionResult> Index(int streamId)
    {
        var stream = await _context.Streams
            .Include(stream => stream.Filters)
            .FirstOrDefaultAsync(m => m.Id == streamId);

        var streamFilters = stream.Filters.ToList();

        var filters = await _context.Filters.ToListAsync();

        var steamFiltersViewModel = new StreamFiltersViewModel(stream.Name, streamFilters, filters);

        return View(steamFiltersViewModel);
    }

    [Route("streams/{streamId:int}/filters/create")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int streamId, int filterId)
    {
        var stream = await _context.Streams
           .Include(stream => stream.Filters)
           .FirstOrDefaultAsync(m => m.Id == streamId);

        var filter = await _context.Filters.FirstOrDefaultAsync(m => m.Id == filterId);

        stream.Filters.Add(filter);

        _context.SaveChanges();

        return RedirectToRoute("StreamFiltersIndex", new { streamId = streamId });
    }

    [Route("streams/{streamId:int}/filters/delete")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int streamId, int filterId)
    {
        var stream = await _context.Streams
          .Include(stream => stream.Filters)
          .FirstOrDefaultAsync(m => m.Id == streamId);

        var filter = await _context.Filters.FirstOrDefaultAsync(m => m.Id == filterId);

        stream.Filters.Remove(filter);

        _context.SaveChanges();

        return RedirectToRoute("StreamFiltersIndex", new { streamId = streamId });
    }
}

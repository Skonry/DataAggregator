using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DataAggregator.Database;
using DataAggregator.ViewModels;

namespace DataAggregator.Controllers;
public class StreamSourcesController : Controller
{
    private readonly AppDbContext _context;

    public StreamSourcesController(AppDbContext context)
    {
        _context = context;
    }

    [Route("streams/{streamId:int}/sources/index", Name = "StreamSourcesIndex")]
    public async Task<IActionResult> Index(int streamId)
    {
        var streamEntity = await _context.Streams
            .Include(stream => stream.Sources)
            .FirstOrDefaultAsync(m => m.Id == streamId);

        var streamSources = streamEntity.Sources.ToList();

        var sources = await _context.Sources.ToListAsync();

        var steamSourcesViewModel = new StreamSourcesViewModel(streamEntity.Name, streamSources, sources);

        return View(steamSourcesViewModel);
    }

    [Route("streams/{streamId:int}/sources/create")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int streamId, int sourceId)
    {
        var streamEntity = await _context.Streams
           .Include(stream => stream.Sources)
           .FirstOrDefaultAsync(m => m.Id == streamId);

        var sourceEntity = await _context.Sources.FirstOrDefaultAsync(m => m.Id == sourceId);

        streamEntity.Sources.Add(sourceEntity);

        _context.SaveChanges();

        return RedirectToRoute("StreamSourcesIndex", new { streamId = streamId });
    }

    [Route("streams/{streamId:int}/sources/delete")]
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int streamId, int sourceId)
    {
        var streamEntity = await _context.Streams
          .Include(stream => stream.Sources)
          .FirstOrDefaultAsync(m => m.Id == streamId);

        var sourceEntity = await _context.Sources.FirstOrDefaultAsync(m => m.Id == sourceId);

        streamEntity.Sources.Remove(sourceEntity);

        _context.SaveChanges();

        return RedirectToRoute("StreamSourcesIndex", new { streamId = streamId });
    }
}

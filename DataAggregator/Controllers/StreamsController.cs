using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using DataAggregator.Database;
using DataAggregator.Entities;
using DataAggregator.ViewModels;
using DataAggregator.Services;

namespace DataAggregator.Controllers
{
    public class StreamsController : Controller
    {
        private readonly AppDbContext _context;

        public StreamsController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Streams.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id, [FromServices] IDisplayStreamService displayStreamService)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stream = await _context.Streams
                .Include(stream => stream.Sources)
                .Include(stream => stream.Filters)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (stream == null)
            {
                return NotFound();
            }

            DisplayStreamData displayStreamData = displayStreamService.GenerateData(stream);

            var streamViewModel = new StreamViewModel()
            {
                StreamId = stream.Id,
                StreamName = displayStreamData.StreamName,
                Sources = displayStreamData.Sources
            };

            return View(streamViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] StreamEntity stream)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stream);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(stream);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stream = await _context.Streams
                .Include(stream => stream.Sources)
                .Include(stream => stream.Filters)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (stream == null)
            {
                return NotFound();
            }

            return View(stream);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] StreamEntity stream)
        {
            if (id != stream.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stream);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StreamEntityExists(stream.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            return View(stream);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stream = await _context.Streams.FirstOrDefaultAsync(m => m.Id == id);

            if (stream == null)
            {
                return NotFound();
            }

            return View(stream);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stream = await _context.Streams
                .Include(stream => stream.Sources)
                .Include(stream => stream.Filters)
                .FirstOrDefaultAsync(m => m.Id == id);

            foreach (var source in stream.Sources)
            {
                stream.Sources.Remove(source);
            }

            foreach (var filter in stream.Filters)
            {
                stream.Filters.Remove(filter);
            }

            _context.Streams.Remove(stream);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool StreamEntityExists(int id)
        {
            return _context.Streams.Any(e => e.Id == id);
        }
    }
}
